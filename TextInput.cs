using MaterialSkin.Controls;
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
using static System.Windows.Forms.ComboBox;

namespace Printer_Web_Forms
{
    public partial class TextInput : Form
    {
        public TextInput()//Adicionar neste formulário a opção de conseguir adicionar o tamanho da caixa.
        {
            InitializeComponent();
        }

        string Target;

        public void SetTargetAndLoadDefaults(string Target, string Default)
        {
            this.Target = Target;
            textBox1.Text = Default;

            if (Target.Equals("BoxSize"))
            {
                this.Text = "Mudar tamanho chapas";
            }
            if (Target.Equals("BoxType"))
            {
                this.Text = "Mudar tipo chapas";
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (Target.Equals("BoxType"))
                PrintSystem.SetTipos(textBox1.Text.Trim().Split('\n').ToList());
            this.Dispose();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
    }
}
