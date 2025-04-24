using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mighty;

namespace Printer_Web_Forms
{
    public static class SaveSystem
    {
        static bool ReadedSavedFiles = false;
        
        static List<Data> datas = new List<Data>();
        
        public static void LoadSavedFiles(params string[] WaitedFindFiles)
        {
            Log.PrintLn("Carregando configurações salvas...");

            List<string> WWaitedFindFiles = WaitedFindFiles.ToList();

            try
            {
                Log.PrintLn("Arquivos de configurações salvas encontrados: ");
                foreach (var file in Directory.GetFiles("Saved"))
                {
                    var content = File.ReadAllText(file);

                    if (WaitedFindFiles.Contains(file))
                    {
                        WWaitedFindFiles.Remove(file);
                        Log.PrintLn($"    $Green$\"{file}\"");
                    }
                    else
                    {
                        Log.PrintLn($"    $Yellow$\"{file}\"");
                    }

                    var content_splited = content.Split('○');

                    string DataID = content_splited[0];

                    var dataType = content_splited[1];

                    object dataValue = null;
                    if (dataType == "Double")
                        dataValue = double.Parse(content_splited[2]);
                    if (dataType == "String")
                        dataValue = content_splited[2];

                    datas.Add(new Data(DataID, dataType, dataValue));
                }
            } catch (Exception ex) { Log.PrintLn($"$DarkRed$ERRO: $Red$Ocorreu um erro ao tentar procurar arquivos no diretório \"Saved\\\".({ex.Message})"); }
            
            if(WWaitedFindFiles.Count != 0)
                Log.PrintLn("Arquivos não encontrados: ");

            foreach (var waitedFile in WWaitedFindFiles)
            {
                Log.PrintLn($"    $Red$\"{waitedFile}\"");
            }
        }

        public static string LoadString(string ID)
        {
            Data loaded = Data.GetDataById(datas, ID);
            if (loaded == null)
                return "";

            return (string)loaded.Value;
        }

        public static double LoadDouble(string ID)
        {
            Data loaded = Data.GetDataById(datas, ID);
            if(loaded == null)
                return 0.0;

            return (double)loaded.Value;
        }

        public static void Save(string ID, string Value)
        {
            datas.Add(new Data(ID, "String", Value));
            SaveInToFiles();
        }
        public static void Save(string ID, double Value)
        {
            datas.Add(new Data(ID, "Double", Value));
            SaveInToFiles();
        }
        private static void SaveInToFiles()
        {
            foreach (Data data in datas)
            {
                Directory.CreateDirectory("Saved");
                File.WriteAllText($"Saved\\{data.ID}.sdat",
                    $"{data.ID}" + 
                    $"○{data.Type}"+
                    $"○{data.Value}"
                    ) ;
            }
        }
        
        class Data
        {
            public string ID;
            public string Type;
            public object Value;

            public Data() { }

            public Data(string iD, string type, object value)
            {
                ID = iD;
                Type = type;
                Value = value;
            }

            public static Data GetDataById(List<Data> dataList, string ID)
            {
                foreach(var data in dataList)
                {
                    if(data.ID == ID)
                    {
                        return data;
                    }
                }
                return null;
            } 
        }
    }
}
