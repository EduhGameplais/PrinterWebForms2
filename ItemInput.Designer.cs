namespace Printer_Web_Forms
{
    partial class ItemInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            materialButton1 = new MaterialSkin.Controls.MaterialButton();
            materialButton2 = new MaterialSkin.Controls.MaterialButton();
            materialButton3 = new MaterialSkin.Controls.MaterialButton();
            materialButton4 = new MaterialSkin.Controls.MaterialButton();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            Larguratextbox = new MaterialSkin.Controls.MaterialTextBox();
            comprimentoTextBox = new MaterialSkin.Controls.MaterialTextBox();
            listView1 = new ListView();
            NameTextBox = new MaterialSkin.Controls.MaterialTextBox();
            SuspendLayout();
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.Location = new Point(280, 270);
            materialButton1.Margin = new Padding(4, 6, 4, 6);
            materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(76, 36);
            materialButton1.TabIndex = 1;
            materialButton1.Text = "Salvar";
            materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            materialButton1.Click += materialButton1_Click;
            // 
            // materialButton2
            // 
            materialButton2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton2.Depth = 0;
            materialButton2.HighEmphasis = true;
            materialButton2.Icon = null;
            materialButton2.Location = new Point(470, 270);
            materialButton2.Margin = new Padding(4, 6, 4, 6);
            materialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton2.Name = "materialButton2";
            materialButton2.NoAccentTextColor = Color.Empty;
            materialButton2.Size = new Size(96, 36);
            materialButton2.TabIndex = 2;
            materialButton2.Text = "Cancelar";
            materialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton2.UseAccentColor = false;
            materialButton2.UseVisualStyleBackColor = true;
            materialButton2.Click += materialButton2_Click;
            // 
            // materialButton3
            // 
            materialButton3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton3.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton3.Depth = 0;
            materialButton3.HighEmphasis = true;
            materialButton3.Icon = null;
            materialButton3.Location = new Point(280, 12);
            materialButton3.Margin = new Padding(4, 6, 4, 6);
            materialButton3.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton3.Name = "materialButton3";
            materialButton3.NoAccentTextColor = Color.Empty;
            materialButton3.Size = new Size(98, 36);
            materialButton3.TabIndex = 5;
            materialButton3.Text = "Adicionar";
            materialButton3.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton3.UseAccentColor = false;
            materialButton3.UseVisualStyleBackColor = true;
            materialButton3.Click += materialButton3_Click;
            // 
            // materialButton4
            // 
            materialButton4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton4.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton4.Depth = 0;
            materialButton4.HighEmphasis = true;
            materialButton4.Icon = null;
            materialButton4.Location = new Point(289, 53);
            materialButton4.Margin = new Padding(4, 6, 4, 6);
            materialButton4.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton4.Name = "materialButton4";
            materialButton4.NoAccentTextColor = Color.Empty;
            materialButton4.Size = new Size(89, 36);
            materialButton4.TabIndex = 6;
            materialButton4.Text = "Remover";
            materialButton4.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton4.UseAccentColor = false;
            materialButton4.UseVisualStyleBackColor = true;
            materialButton4.Click += materialButton4_Click;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.Location = new Point(394, 86);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(66, 19);
            materialLabel1.TabIndex = 7;
            materialLabel1.Text = "Medidas:";
            // 
            // Larguratextbox
            // 
            Larguratextbox.AnimateReadOnly = false;
            Larguratextbox.BorderStyle = BorderStyle.None;
            Larguratextbox.Depth = 0;
            Larguratextbox.Enabled = false;
            Larguratextbox.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            Larguratextbox.Hint = "Largura(CM)";
            Larguratextbox.LeadingIcon = null;
            Larguratextbox.Location = new Point(394, 108);
            Larguratextbox.MaxLength = 50;
            Larguratextbox.MouseState = MaterialSkin.MouseState.OUT;
            Larguratextbox.Multiline = false;
            Larguratextbox.Name = "Larguratextbox";
            Larguratextbox.Size = new Size(172, 50);
            Larguratextbox.TabIndex = 8;
            Larguratextbox.Text = "";
            Larguratextbox.TrailingIcon = null;
            Larguratextbox.TextChanged += Larguratextbox_TextChanged;
            // 
            // comprimentoTextBox
            // 
            comprimentoTextBox.AnimateReadOnly = false;
            comprimentoTextBox.BorderStyle = BorderStyle.None;
            comprimentoTextBox.Depth = 0;
            comprimentoTextBox.Enabled = false;
            comprimentoTextBox.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            comprimentoTextBox.Hint = "Comprimento(CM)";
            comprimentoTextBox.LeadingIcon = null;
            comprimentoTextBox.Location = new Point(394, 164);
            comprimentoTextBox.MaxLength = 50;
            comprimentoTextBox.MouseState = MaterialSkin.MouseState.OUT;
            comprimentoTextBox.Multiline = false;
            comprimentoTextBox.Name = "comprimentoTextBox";
            comprimentoTextBox.Size = new Size(172, 50);
            comprimentoTextBox.TabIndex = 9;
            comprimentoTextBox.Text = "";
            comprimentoTextBox.TrailingIcon = null;
            comprimentoTextBox.TextChanged += comprimentoTextBox_TextChanged;
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 9);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(261, 297);
            listView1.TabIndex = 10;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Tile;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            listView1.KeyPress += listView1_KeyPress;
            listView1.KeyUp += listView1_KeyUp;
            // 
            // NameTextBox
            // 
            NameTextBox.AnimateReadOnly = false;
            NameTextBox.BorderStyle = BorderStyle.None;
            NameTextBox.Depth = 0;
            NameTextBox.Enabled = false;
            NameTextBox.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            NameTextBox.Hint = "Nome";
            NameTextBox.LeadingIcon = null;
            NameTextBox.Location = new Point(394, 12);
            NameTextBox.MaxLength = 50;
            NameTextBox.MouseState = MaterialSkin.MouseState.OUT;
            NameTextBox.Multiline = false;
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(172, 50);
            NameTextBox.TabIndex = 11;
            NameTextBox.Text = "";
            NameTextBox.TrailingIcon = null;
            NameTextBox.TextChanged += NameTextBox_TextChanged_1;
            NameTextBox.Enter += NameTextBox_TextChanged;
            NameTextBox.Leave += NameTextBox_TextChanged;
            // 
            // ItemInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(579, 321);
            Controls.Add(NameTextBox);
            Controls.Add(listView1);
            Controls.Add(comprimentoTextBox);
            Controls.Add(Larguratextbox);
            Controls.Add(materialLabel1);
            Controls.Add(materialButton4);
            Controls.Add(materialButton3);
            Controls.Add(materialButton2);
            Controls.Add(materialButton1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ItemInput";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TextInput";
            Click += ItemInput_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private MaterialSkin.Controls.MaterialButton materialButton2;
        private MaterialSkin.Controls.MaterialButton materialButton3;
        private MaterialSkin.Controls.MaterialButton materialButton4;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox Larguratextbox;
        private MaterialSkin.Controls.MaterialTextBox comprimentoTextBox;
        private ListView listView1;
        private MaterialSkin.Controls.MaterialTextBox NameTextBox;
    }
}