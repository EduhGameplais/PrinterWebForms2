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

    public partial class ItemInput : Form
    {
        public List<BoxSize> BoxSizes = new List<BoxSize>();
        public ItemInput()//Adicionar neste formulário a opção de conseguir adicionar o tamanho da caixa.
        {
            InitializeComponent();
            try
            {
                if (listView1.Focused)
                {
                    materialButton4.Enabled = true;
                }
                else
                {
                    materialButton4.Enabled = false;
                }
            }
            catch
            {
                materialButton4.Enabled = false;
            }
        }

        string Target;

        public void SetTargetAndLoadDefaults(string Target, List<BoxSize> Default)
        {
            BoxSizes = Default;
            this.Target = Target;
            if (Default != null)
                foreach (BoxSize boxSize in Default)
                {
                    if (boxSize.Name != "" && boxSize.Name != null)
                        listView1.Items.Add(boxSize.Name);
                }

            if (Target.Equals("BoxSize"))
            {
                this.Text = "Mudar tamanho chapas";
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (Target.Equals("BoxSize"))
                PrintSystem.SetSizes(BoxSizes);

            this.Dispose();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Focused)
                {
                    comprimentoTextBox.Enabled = true;
                    Larguratextbox.Enabled = true;
                    NameTextBox.Enabled = true;
                    materialButton4.Enabled = true;
                }
                else
                {
                    comprimentoTextBox.Enabled = false;
                    Larguratextbox.Enabled = false;
                    NameTextBox.Enabled = false;
                    materialButton4.Enabled = false;
                }
            }
            catch
            {
                comprimentoTextBox.Enabled = false;
                Larguratextbox.Enabled = false;
                NameTextBox.Enabled = false;
                materialButton4.Enabled = false;
            }
            foreach (var BoxSize in BoxSizes)
            {
                try
                {
                    if (listView1.SelectedItems[0] != null)
                        if (BoxSize.Name == listView1.SelectedItems[0].Text)
                        {
                            Larguratextbox.Text = BoxSize.Largura.ToString();
                            comprimentoTextBox.Text = BoxSize.Comprimento.ToString();
                            NameTextBox.Text = BoxSize.Name;
                        }
                }
                catch
                {

                }
            }
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int RandomNumber = random.Next(0, 100000);
            BoxSizes.Add(new BoxSize() { Name = "Novo Tamanho" + RandomNumber });
            listView1.Items.Add("Novo Tamanho" + RandomNumber);
            listView1.Select();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems[0].Text != null)
                    for (int i = 0; i < BoxSizes.Count; i++)
                    {
                        try
                        {
                            if (listView1.SelectedItems[0].Text != null)
                                if (BoxSizes[i].Name == listView1.SelectedItems[0].Text)
                                {
                                    BoxSizes.Remove(BoxSizes[i]);
                                    listView1.SelectedItems[0].Remove();
                                }
                        }
                        catch
                        {

                        }
                    }
            }
            catch { }
        }

        private void Larguratextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < BoxSizes.Count; i++)
                {
                    if (listView1.SelectedItems[0] != null)
                        if (BoxSizes[i].Name == listView1.SelectedItems[0].Text)
                        {
                            BoxSizes[i].Largura = float.Parse(Larguratextbox.Text);
                        }
                }
            }
            catch
            {

            }
        }

        private void comprimentoTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < BoxSizes.Count; i++)
                {
                    if (listView1.SelectedItems[0] != null)
                        if (BoxSizes[i].Name == listView1.SelectedItems[0].Text)
                        {
                            BoxSizes[i].Comprimento = float.Parse(comprimentoTextBox.Text);
                        }
                }
            }
            catch
            {

            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < BoxSizes.Count; i++)
                {
                    if (listView1.SelectedItems[0] != null)
                        if (BoxSizes[i].Name == listView1.SelectedItems[0].Text)
                        {
                            if (NameTextBox.Text != "")
                            {

                                BoxSizes[i].Name = NameTextBox.Text;
                                listView1.SelectedItems[0].Text = NameTextBox.Text;
                            }
                            else
                            {
                                Random random = new Random();
                                int RandomNumber = random.Next(0, 100000);
                                NameTextBox.Text = "Sem Nome" + RandomNumber;
                                BoxSizes[i].Name = "Sem Nome" + RandomNumber;
                                listView1.SelectedItems[0].Text = "Sem Nome" + RandomNumber;
                            }

                        }
                }
            }
            catch
            {

            }
        }

        private void NameTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (listView1.SelectedItems[0].Text != null)
                        for (int i = 0; i < BoxSizes.Count; i++)
                        {
                            try
                            {
                                if (listView1.SelectedItems[0].Text != null)
                                    if (BoxSizes[i].Name == listView1.SelectedItems[0].Text)
                                    {
                                        BoxSizes.Remove(BoxSizes[i]);
                                        listView1.SelectedItems[0].Remove();
                                    }
                            }
                            catch
                            {

                            }
                        }
                }
                catch { }
            }
        }

        private void ItemInput_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Focused)
                {
                    comprimentoTextBox.Enabled = true;
                    Larguratextbox.Enabled = true;
                    NameTextBox.Enabled = true;
                    materialButton4.Enabled = true;
                }
                else
                {
                    comprimentoTextBox.Enabled = false;
                    Larguratextbox.Enabled = false;
                    NameTextBox.Enabled = false;
                    materialButton4.Enabled = false;
                }
            }
            catch
            {
                comprimentoTextBox.Enabled = false;
                Larguratextbox.Enabled = false;
                NameTextBox.Enabled = false;
                materialButton4.Enabled = false;
            }
        }
    }

}
