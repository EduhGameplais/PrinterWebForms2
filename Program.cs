using Printer_Web;
using WebHost;
using Mighty;
using Printer_Web_Forms;

Log.LogFormat = Log.ServerLogFormat;

Console.WriteLine("Iniciando Printer Web Server...");

PrintSystem.CreatePrinters(3,3);
PrintSystem.ScanForClientes();

SaveSystem.LoadSavedFiles("Saved\\sizes.sdat", "Saved\\types.sdat", "Saved\\offsets.sdat");

PrintSystem.LoadTamanhos();

PrintSystem.LoadOffsets();

PrintSystem.LoadTipos();

void Start()
{
    try
    {
        new Task(WebService.Start).Start();
    }
    catch(Exception ex) 
    { 
        Log.PrintLn($"Instancia do servidor web crashou({ex.Message}). Reiniciando..."); 
        Start(); 
    }
}

Start();

ApplicationConfiguration.Initialize();
Painel mainform = new Painel();
Application.Run(mainform);

/*PrintSystem.AddTamanho(new BoxSize("Grande", 20, 10));
PrintSystem.AddTamanho(new BoxSize("Pequeno", 56, 10));
PrintSystem.AddTamanho(new BoxSize("médio", 20, 21));
PrintSystem.AddTamanho(new BoxSize("Grande", 20, 12));*/
