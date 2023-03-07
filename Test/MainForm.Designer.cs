namespace Test
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBoxMascote = new PictureBox();
            panelMenu = new Panel();
            label1 = new Label();
            button2 = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMascote).BeginInit();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBoxMascote
            // 
            pictureBoxMascote.BackColor = Color.Transparent;
            pictureBoxMascote.Location = new Point(157, 40);
            pictureBoxMascote.Name = "pictureBoxMascote";
            pictureBoxMascote.Size = new Size(250, 250);
            pictureBoxMascote.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxMascote.TabIndex = 0;
            pictureBoxMascote.TabStop = false;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.MediumSlateBlue;
            panelMenu.Controls.Add(label1);
            panelMenu.Controls.Add(button2);
            panelMenu.Controls.Add(button1);
            panelMenu.Location = new Point(212, 74);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(132, 171);
            panelMenu.TabIndex = 1;
            panelMenu.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(17, 13);
            label1.Name = "label1";
            label1.Size = new Size(94, 21);
            label1.TabIndex = 2;
            label1.Text = "Start Menu";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Control;
            button2.Cursor = Cursors.Hand;
            button2.Enabled = false;
            button2.Location = new Point(27, 111);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Load";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Control;
            button1.Cursor = Cursors.Hand;
            button1.Location = new Point(27, 58);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "New Game";
            button1.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(585, 350);
            Controls.Add(panelMenu);
            Controls.Add(pictureBoxMascote);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MaximumSize = new Size(605, 393);
            MinimizeBox = false;
            MinimumSize = new Size(605, 393);
            Name = "MainForm";
            Text = "Tamagoshi TESTS";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxMascote).EndInit();
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxMascote;
        private Panel panelMenu;
        private Label label1;
        private Button button2;
        private Button button1;
    }
}