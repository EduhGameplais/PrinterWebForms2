using Mighty;
using Printer_Web_Forms;

namespace Printer_Web
{
    public static class PrintSystem
    {
        public static List<Printer>[] printers;
        public static List<PaperSettings>[] paperSettings;

        public static List<BoxSize> Tamanhos = new List<BoxSize>();

        public static byte TowerCount { private set; get; } = 0;
        public static byte PrinterCount { private set; get; } = 0;

        public static List<string> Clientes = new List<string>();

        public static string PATH_TO_CLIENTES_FILES = "G:\\.shortcut-targets-by-id\\1-13asQb2_KJ0idxK6utplTHfSexVLJzH\\Artes PNG";

        public static List<string> Tipos = new List<string>();

        public static void LoadTipos()
        {
            string SavedTipos = SaveSystem.LoadString("types");

            string[] TiposSplited = SavedTipos.Split("↓");

            try
            {
                Tipos.AddRange(TiposSplited);
            } catch (Exception ex){
                if(ex.Message == "Object reference not set to an instance of an object.")
                {
                    Log.PrintLn($"$Red$Erro ao tentar carregar os tipos salvos. (Arquivo inexistente ou vázio.)");
                }
                else
                {
                    Log.PrintLn($"$Red$Erro ao tentar carregar os tipos salvos. ({ex.Message})");
                }
            }
        }

        public static void SetSizes(List<BoxSize> boxSizes)
        {
            
            Tamanhos = boxSizes;

            string TamanhosString = String.Empty;

            foreach (var Tamanho in Tamanhos)
            {
                TamanhosString += $"\n{Tamanho.Name}";
                TamanhosString += $"\n{Tamanho.Largura}";
                TamanhosString += $"\n{Tamanho.Comprimento}";
            }

            SaveSystem.Save("sizes", TamanhosString.Trim());

            Log.PrintLn("Os tamanhos foram editados.");
        }

        public static void SetTipos(List<string> newTipos)
        {
            Tipos = newTipos;
            SaveTipos();
        }

        public static void SaveTipos()
        {
            string ToSaveTipos = String.Empty;

            foreach (var tipo in Tipos)
            {
                ToSaveTipos += tipo + "↓";
            }
            ToSaveTipos = ToSaveTipos.Remove(ToSaveTipos.Length - 1, 1);

            SaveSystem.Save("types", ToSaveTipos);

            Log.PrintLn("Os tipos foram editados.");
        }

        public static void LoadOffsets()
        {
            string SavedOffsets = SaveSystem.LoadString("offsets");

            string[] OffsetsSplited = SavedOffsets.Split("°");

            double[] Offsets = new double[OffsetsSplited.Length];

            for (int i = 0; i < OffsetsSplited.Length; i++)
            {
                try
                {
                    var offset = OffsetsSplited[i];

                    Offsets[i] = double.Parse(offset);
                }
                catch { break; }
            }

            int OffsetIndex = 0;

            for (int i = 0; i < paperSettings.Length; i++)
            {
                for(int j = 0; j < paperSettings[i].Count; j++)
                {
                    try
                    {
                        paperSettings[i][j].Offset = Offsets[OffsetIndex];
                        OffsetIndex++;
                    }
                    catch { break; }
                }
            }
        }

        public static void SaveOffsets()
        {
            List<double> Offsets = new List<double>();

            for (int i = 0; i < paperSettings.Length; i++)
            {
                for (int j = 0; j < paperSettings[i].Count; j++)
                {
                    Offsets.Add(paperSettings[i][j].Offset);
                }
            }

            string SaveOffsetsString = String.Empty;

            foreach (var offset in Offsets)
            {
                SaveOffsetsString += offset + "°";
            }

            SaveOffsetsString = SaveOffsetsString.Remove(SaveOffsetsString.Length - 1, 1);

            SaveSystem.Save("offsets", SaveOffsetsString);
        }

        public static void LoadTamanhos()
        {
            string SavedTamanhos = SaveSystem.LoadString("sizes");

            string[] SavedTamanhosSplited = SavedTamanhos.Split("\n");

            //Log.PrintLn(SavedTamanhos);

            Tamanhos = new List<BoxSize>();

            for (int i = 0; i < SavedTamanhosSplited.Length; i = i + 3)
            {
                //Nome\nLargura\nComprimento
                if (SavedTamanhosSplited.Length < 3) break;

                string nome = SavedTamanhosSplited[i];
                double Largura = double.Parse(SavedTamanhosSplited[i+1]);
                double Comprimento = double.Parse(SavedTamanhosSplited[i + 2]);

                Tamanhos.Add(new BoxSize(nome, Largura, Comprimento));

            }
        }

        public static void AddTamanho(BoxSize tamanho)
        {
            Tamanhos.Add(tamanho);

            string TamanhosString = String.Empty;

            foreach (var Tamanho in Tamanhos)
            {
                TamanhosString += $"\n{Tamanho.Name}";
                TamanhosString += $"\n{Tamanho.Largura}";
                TamanhosString += $"\n{Tamanho.Comprimento}";
            }

            SaveSystem.Save("sizes",TamanhosString.Trim());
        }

        public static void ScanForClientes()
        {
            Log.PrintLn($"$Yellow$Procurando por clientes...({PATH_TO_CLIENTES_FILES})");
            
            Clientes.Clear();
            //MessageBox.Show(PathToImages.Text);
            if (!Directory.Exists(PATH_TO_CLIENTES_FILES))
            {
                MessageBox.Show($"Não foi possível encontrar a pasta \"{PATH_TO_CLIENTES_FILES}\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Log.PrintLn("Encontrado:");
            foreach (string file in Directory.GetFiles(PATH_TO_CLIENTES_FILES))
            {
                try
                {
                    string[] fileInfo = file.Split('-');


                    if (fileInfo.Length > 2)
                    {

                        //"Rei da Pizza-grande-oitavada";
                        string path = fileInfo[0];
                        string name = path.Split('\\').Last();

                        if (!Clientes.Contains(name))
                        {
                            Clientes.Add(name);
                            Log.PrintLn($"    \"{name}\"");
                        }
                    }
                }
                catch (Exception ex) 
                {
                    Log.PrintError(ex);
                }
            }
            if(Clientes.Count == 0)
            {
                Log.PrintLn("    $DarkRed$Não foi encontrado nenhum cliente.");
            }
        }

        public static void CreatePrinters(byte towerCount, byte printerCount)
        {
            TowerCount = towerCount;
            PrinterCount = printerCount;

            printers = new List<Printer>[towerCount];
            paperSettings = new List<PaperSettings>[towerCount];

            short printerIndex = 1;
            Log.PrintLn($"Criando Torres... ({towerCount})");
            Log.PrintLn($"Criando Impressoras... ({printerCount})");
            for (int i = 0; i < towerCount; i++)
            {
                printers[i] = new List<Printer>();
                paperSettings[i] = new List<PaperSettings>(); // Aqui você está inicializando a lista de PaperSettings

                Log.PrintLn($"$Green$    Torre{i + 1}: ");
                for (int x = 0; x < printerCount; x++)
                {

                    printers[i].Add(new Printer($"IMP{printerIndex}"));
                    paperSettings[i].Add(PaperSettings.Empty());
                    Log.PrintLn($"        Impressora{x + 1}: $Yellow$IMP{printerIndex}");
                    printerIndex++;
                }
            }
        }
        
        public static string Print(byte tower, byte printer)
        {
            if (tower > TowerCount)
                return "tower parameter is higher than count of towers.";
            else if (printer > PrinterCount)
                return "printer parameter is higher than count of printers per tower.";

            try
            {
                Log.PrintLn($"Tentando imprimir na impressora {printer} da torre {tower}");
                if(File.Exists(paperSettings[tower - 1][printer - 1].ImagePath))
                {
                    printers[tower - 1][printer - 1].Print(paperSettings[tower - 1][printer - 1]);
                    return "success";
                }
                else
                {
                    return "png-not-exists";
                }
            }
            catch(Exception ex)
            {
                Log.PrintError(ex);
                return $"{ex.Message}";
            }
        }

        public static double GetOffset(byte tower, byte printer)
        {
            return paperSettings[tower - 1][printer - 1].Offset;
        }

        public static void EditPaperSettings(byte tower, byte printer, string imagePath="", string boxSizeName="", double offset=0, short imagesPerPrint=0, short copies=0)
        {

            if (imagePath != "")
                paperSettings[tower - 1][printer - 1].ImagePath = imagePath;
            if (boxSizeName != "")
                paperSettings[tower - 1][printer - 1].Tamanho = BoxSize.FindBoxSize(Tamanhos.ToArray(), boxSizeName);
            if (offset != 0)
                paperSettings[tower - 1][printer - 1].Offset = offset;
            if (imagesPerPrint != 0)
                paperSettings[tower - 1][printer - 1].ImagesPerPrint = imagesPerPrint;
            if (copies != 0)
                paperSettings[tower - 1][printer - 1].Copies = copies;
            SaveOffsets();
        }
        public static void EditPaperSettings(byte tower, byte printer, PaperSettings newPaperSettings)
        {
            paperSettings[tower - 1][printer - 1] = newPaperSettings;
            SaveOffsets();
        }
    }
}
