using Mighty;

using Printer_Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Printer_Web_Forms
{
    public partial class Painel : Form
    {
        public Painel()
        {
            InitializeComponent();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            ItemInput itemInput = new ItemInput();

            itemInput.SetTargetAndLoadDefaults("BoxSize", PrintSystem.Tamanhos);

            itemInput.Show();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            TextInput textInput = new TextInput();

            string tiposString = string.Empty;

            try
            {
                foreach (var tipo in PrintSystem.Tipos)
                {
                    tiposString += tipo + "\n";
                }
            }
            catch (Exception ex) { Log.PrintError(ex); }

            textInput.SetTargetAndLoadDefaults("BoxType", tiposString.Trim());

            textInput.Show();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Printer.CancelPrint();
        }
    }
}
