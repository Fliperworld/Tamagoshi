namespace Test
{
    partial class TamagoshiSelectorForm
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
            buttonCancel = new Button();
            label1 = new Label();
            pictureBoxIcon = new PictureBox();
            panel1 = new Panel();
            panelWarning = new Panel();
            labelStarterSpecie = new Label();
            label2 = new Label();
            textBoxInfos = new TextBox();
            buttonOk = new Button();
            comboBoxPokemonName = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            panel1.SuspendLayout();
            panelWarning.SuspendLayout();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.Cursor = Cursors.Hand;
            buttonCancel.Location = new Point(33, 414);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 0;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(101, 15);
            label1.TabIndex = 1;
            label1.Text = "Select a Pokemon";
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.BackColor = Color.Transparent;
            pictureBoxIcon.Location = new Point(12, 66);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(96, 96);
            pictureBoxIcon.TabIndex = 2;
            pictureBoxIcon.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(panelWarning);
            panel1.Controls.Add(textBoxInfos);
            panel1.Controls.Add(buttonOk);
            panel1.Location = new Point(119, 55);
            panel1.Name = "panel1";
            panel1.Size = new Size(244, 401);
            panel1.TabIndex = 3;
            // 
            // panelWarning
            // 
            panelWarning.Controls.Add(labelStarterSpecie);
            panelWarning.Controls.Add(label2);
            panelWarning.Location = new Point(6, 271);
            panelWarning.Name = "panelWarning";
            panelWarning.Size = new Size(235, 82);
            panelWarning.TabIndex = 7;
            panelWarning.Visible = false;
            // 
            // labelStarterSpecie
            // 
            labelStarterSpecie.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            labelStarterSpecie.ForeColor = Color.Blue;
            labelStarterSpecie.Location = new Point(10, 37);
            labelStarterSpecie.Name = "labelStarterSpecie";
            labelStarterSpecie.Size = new Size(214, 23);
            labelStarterSpecie.TabIndex = 1;
            labelStarterSpecie.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.OrangeRed;
            label2.Location = new Point(10, 11);
            label2.Name = "label2";
            label2.Size = new Size(214, 15);
            label2.TabIndex = 0;
            label2.Text = "Warning : This Tamagoshi will start as:";
            // 
            // textBoxInfos
            // 
            textBoxInfos.BackColor = Color.Black;
            textBoxInfos.ForeColor = Color.White;
            textBoxInfos.Location = new Point(6, 7);
            textBoxInfos.Multiline = true;
            textBoxInfos.Name = "textBoxInfos";
            textBoxInfos.ReadOnly = true;
            textBoxInfos.Size = new Size(235, 243);
            textBoxInfos.TabIndex = 6;
            // 
            // buttonOk
            // 
            buttonOk.Cursor = Cursors.Hand;
            buttonOk.Location = new Point(16, 359);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(214, 23);
            buttonOk.TabIndex = 5;
            buttonOk.Text = "Use this Pokemon as Tamagoshi";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // comboBoxPokemonName
            // 
            comboBoxPokemonName.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPokemonName.FormattingEnabled = true;
            comboBoxPokemonName.Location = new Point(121, 20);
            comboBoxPokemonName.Name = "comboBoxPokemonName";
            comboBoxPokemonName.Size = new Size(242, 23);
            comboBoxPokemonName.TabIndex = 4;
            // 
            // TamagoshiSelectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(375, 469);
            ControlBox = false;
            Controls.Add(comboBoxPokemonName);
            Controls.Add(panel1);
            Controls.Add(pictureBoxIcon);
            Controls.Add(label1);
            Controls.Add(buttonCancel);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "TamagoshiSelectorForm";
            Text = "Tamagoshi Selector";
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelWarning.ResumeLayout(false);
            panelWarning.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonCancel;
        private Label label1;
        private PictureBox pictureBoxIcon;
        private Panel panel1;
        private ComboBox comboBoxPokemonName;
        private Button buttonOk;
        private Panel panelWarning;
        private Label labelStarterSpecie;
        private Label label2;
        private TextBox textBoxInfos;
    }
}