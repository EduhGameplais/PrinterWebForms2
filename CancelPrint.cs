using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net;
using WebHost;
using Printer_Web.WebHoster;
using Printer_Web;
using Mighty;

namespace Printer_Web_Forms
{
    internal class CancelPrint
    {
        [Endpoint("/print/cancel")]
        public byte[] CancelPrinting(HttpListenerRequest request)
        {
            ParameterParser parser = new ParameterParser(request);

            byte tower = byte.Parse(parser.GetParameterValue("tower"));
            byte printer = byte.Parse(parser.GetParameterValue("printer"));

            int printer_Id = ((tower - 1) * PrintSystem.PrinterCount) + printer;
            try
            {
                List<PrintJobInfo> jobs = GetPrintJobs($"IMP{printer_Id}");

                if (jobs.Count == 0)
                {
                    return Encoding.UTF8.GetBytes($"not_jobs_avaliable{printer_Id}");
                }

                CancelPrintJob(jobs[0].JobId);

                /*foreach (PrintJobInfo job in jobs)
                {
                    try
                    {
                        CancelPrintJob(job.JobId);
                    }
                    catch (Exception ex)
                    {
                        Log.PrintError(ex);
                    }
                    //PrintSystem.Print(tower, printer);
                }*/
                return Encoding.UTF8.GetBytes("successcancel");
            }
            catch (Exception ex)
            {
                Log.PrintError(ex, $"Ocorreu um erro ao cancelar a impressão da impressora: (IMP{printer_Id})");
                Log.PrintLn($"Erro ao cancelar impressão:$BackRed${ex.Message}");
                return Encoding.UTF8.GetBytes("error");
            }

        }

        [Endpoint("/print/restart")]
        public byte[] RestartPrinting(HttpListenerRequest request)
        {
            ParameterParser parser = new ParameterParser(request);

            byte tower = byte.Parse(parser.GetParameterValue("tower"));
            byte printer = byte.Parse(parser.GetParameterValue("printer"));

            int printer_Id = ((tower - 1) * PrintSystem.PrinterCount) + printer;
            try
            {
                List<PrintJobInfo> jobs = GetPrintJobs($"IMP{printer_Id}");

                if(jobs.Count == 0)
                {
                    return Encoding.UTF8.GetBytes($"not_jobs_avaliable{printer_Id}");
                }

                foreach (PrintJobInfo job in jobs)
                {
                    try
                    {
                        //CancelPrintJob(job.JobId);
                    }
                    catch(Exception ex)
                    {
                        Log.PrintError(ex);
                    }
                    PrintSystem.Print(tower, printer);
                }
                return Encoding.UTF8.GetBytes("success");
            }
            catch(Exception ex)
            {
                Log.PrintError(ex, $"Ocorreu um erro ao reiniciar a impressão da impressora: (IMP{printer_Id})");
                Log.PrintLn($"Erro ao reiniciar impressão:$BackRed${ex.Message}");
                return Encoding.UTF8.GetBytes("error");
            }

        }

        #region GetPrintJobs
        // Estrutura para armazenar informações sobre trabalhos de impressão
        public struct PrintJobInfo
        {
            public int JobId;
            public string Document;
            public string Status;
        }

        // Função para obter todos os trabalhos de impressão na fila da impressora
        public static List<PrintJobInfo> GetPrintJobs(string printerName)
        {
            IntPtr printerHandle = IntPtr.Zero;
            List<PrintJobInfo> printJobs = new List<PrintJobInfo>();

            try
            {
                if (!OpenPrinter(printerName, out printerHandle, IntPtr.Zero))
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }

                // Tamanho de buffer inicial
                int bufferSize = 0;
                // Consulta de informações de trabalho de impressão
                EnumJobs(printerHandle, 0, int.MaxValue, 2, IntPtr.Zero, 0, out int bytesNeeded, out int jobsReturned);

                if (bytesNeeded == 0)
                {
                    return printJobs; // Retorna lista vazia se não há trabalhos
                }

                // Aumenta o tamanho do buffer conforme necessário e tenta novamente
                bufferSize = bytesNeeded;
                IntPtr jobInfoPtr = Marshal.AllocHGlobal(bufferSize);

                try
                {
                    if (EnumJobs(printerHandle, 0, int.MaxValue, 2, jobInfoPtr, bufferSize, out bytesNeeded, out jobsReturned))
                    {
                        int jobInfoSize = Marshal.SizeOf(typeof(JOB_INFO_2));

                        long currentJobInfoPtr = jobInfoPtr.ToInt64() + jobInfoSize;

                        JOB_INFO_2 jobInfo = Marshal.PtrToStructure<JOB_INFO_2>(new IntPtr(currentJobInfoPtr));

                        printJobs.Add(new PrintJobInfo
                        {
                            JobId = jobInfo.JobId,
                            Document = jobInfo.pDocument,
                            Status = jobInfo.pStatus
                        });
                    }
                    else
                    {
                        //throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(jobInfoPtr);
                }
            }
            catch (Exception ex)
            {
                Log.PrintError(ex);
            }
            finally
            {
                if (printerHandle != IntPtr.Zero)
                {
                    ClosePrinter(printerHandle);
                }
            }

            return printJobs;
        }


        // Declaração de funções Win32 para manipulação de impressoras e trabalhos de impressão
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumJobs(IntPtr hPrinter, int FirstJob, int NoJobs, int Level, IntPtr pJob, int cbBuf, out int pcbNeeded, out int pcReturned);

        // Estrutura para armazenar informações sobre trabalhos de impressão
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct JOB_INFO_2
        {
            public int JobId;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPrinterName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pMachineName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pUserName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDocument;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDatatype;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pStatus;
            public int Status;
            public int Priority;
            public int Position;
            public int TotalPages;
            public int PagesPrinted;
            public SYSTEMTIME Submitted;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Milliseconds;
        }

        // Declaração da função Win32 para cancelar um trabalho de impressão
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DeletePrintJob(IntPtr hPrinter, int jobId);
        #endregion
        #region CancelPrintJob

        /// <summary>
        /// Cancel the print job. This functions accepts the job number.
        /// An exception will be thrown if access denied.
        /// </summary>
        /// <param name="printJobID">int: Job number to cancel printing for.</param>
        /// <returns>bool: true if cancel successfull, else false.</returns>
        public bool CancelPrintJob(int printJobID)
        {
            // Variable declarations.
            bool isActionPerformed = false;
            string searchQuery;
            String jobName;
            char[] splitArr;
            int prntJobID;
            ManagementObjectSearcher searchPrintJobs;
            ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.
                searchQuery = "SELECT * FROM Win32_PrintJob";
                // Create an object using the above query.
                searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                // Fire the query to get the collection of the printer jobs.
                prntJobCollection = searchPrintJobs.Get();

                // Look for the job you want to delete/cancel.
                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    // Job name would be of the format [Printer name], [Job ID]
                    splitArr = new char[1];
                    splitArr[0] = Convert.ToChar(",");
                    // Get the job ID.
                    prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                    // If the Job Id equals the input job Id, then cancel the job.
                    if (prntJobID == printJobID)
                    {
                        // Performs a action similar to the cancel
                        // operation of windows print console
                        prntJob.Delete();
                        isActionPerformed = true;
                        break;
                    }
                }
                return isActionPerformed;
            }
            catch (Exception sysException)
            {
                // Log the exception.
                Log.PrintError(sysException);
                return false;
            }
        }

        #endregion CancelPrintJob
    }
}
