namespace XmlConfiugrationFileGenerator
{
    partial class Form1
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
            this.button_Add = new System.Windows.Forms.Button();
            this.label_Cloud_IP = new System.Windows.Forms.Label();
            this.textBox_Cloud_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Cloud_Port = new System.Windows.Forms.TextBox();
            this.textBox_ForwTabl_IP_Src = new System.Windows.Forms.TextBox();
            this.label_ForwTab_IP_Scr = new System.Windows.Forms.Label();
            this.buttonMake = new System.Windows.Forms.Button();
            this.textBox_ForwTabl_Int_Src = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ForwTabl_IP_Dst = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ForwTabl_Int_Dst = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_tab2_Port = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Tab2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_tab2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(16, 254);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(355, 23);
            this.button_Add.TabIndex = 0;
            this.button_Add.Text = "Dodaj nowy wpis do tabeli";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // label_Cloud_IP
            // 
            this.label_Cloud_IP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_Cloud_IP.AutoSize = true;
            this.label_Cloud_IP.Location = new System.Drawing.Point(20, 54);
            this.label_Cloud_IP.Name = "label_Cloud_IP";
            this.label_Cloud_IP.Size = new System.Drawing.Size(132, 13);
            this.label_Cloud_IP.TabIndex = 1;
            this.label_Cloud_IP.Text = "Adres IP chmury kablowej:";
            // 
            // textBox_Cloud_IP
            // 
            this.textBox_Cloud_IP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_Cloud_IP.Location = new System.Drawing.Point(209, 51);
            this.textBox_Cloud_IP.Name = "textBox_Cloud_IP";
            this.textBox_Cloud_IP.Size = new System.Drawing.Size(162, 20);
            this.textBox_Cloud_IP.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Numer portu chmury kablowej:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "Generator plików konfiguracyjncyh";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_Cloud_Port
            // 
            this.textBox_Cloud_Port.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_Cloud_Port.Location = new System.Drawing.Point(210, 77);
            this.textBox_Cloud_Port.Name = "textBox_Cloud_Port";
            this.textBox_Cloud_Port.Size = new System.Drawing.Size(162, 20);
            this.textBox_Cloud_Port.TabIndex = 5;
            // 
            // textBox_ForwTabl_IP_Src
            // 
            this.textBox_ForwTabl_IP_Src.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_ForwTabl_IP_Src.Location = new System.Drawing.Point(189, 150);
            this.textBox_ForwTabl_IP_Src.Name = "textBox_ForwTabl_IP_Src";
            this.textBox_ForwTabl_IP_Src.Size = new System.Drawing.Size(184, 20);
            this.textBox_ForwTabl_IP_Src.TabIndex = 7;
            // 
            // label_ForwTab_IP_Scr
            // 
            this.label_ForwTab_IP_Scr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_ForwTab_IP_Scr.AutoSize = true;
            this.label_ForwTab_IP_Scr.Location = new System.Drawing.Point(21, 157);
            this.label_ForwTab_IP_Scr.Name = "label_ForwTab_IP_Scr";
            this.label_ForwTab_IP_Scr.Size = new System.Drawing.Size(83, 13);
            this.label_ForwTab_IP_Scr.TabIndex = 6;
            this.label_ForwTab_IP_Scr.Text = "Adres IP źródła:";
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(11, 461);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(360, 38);
            this.buttonMake.TabIndex = 8;
            this.buttonMake.Text = "Stwórz plik XML";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.buttonMake_Click);
            // 
            // textBox_ForwTabl_Int_Src
            // 
            this.textBox_ForwTabl_Int_Src.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_ForwTabl_Int_Src.Location = new System.Drawing.Point(190, 176);
            this.textBox_ForwTabl_Int_Src.Name = "textBox_ForwTabl_Int_Src";
            this.textBox_ForwTabl_Int_Src.Size = new System.Drawing.Size(183, 20);
            this.textBox_ForwTabl_Int_Src.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Interfejs żródła:";
            // 
            // textBox_ForwTabl_IP_Dst
            // 
            this.textBox_ForwTabl_IP_Dst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_ForwTabl_IP_Dst.Location = new System.Drawing.Point(190, 202);
            this.textBox_ForwTabl_IP_Dst.Name = "textBox_ForwTabl_IP_Dst";
            this.textBox_ForwTabl_IP_Dst.Size = new System.Drawing.Size(183, 20);
            this.textBox_ForwTabl_IP_Dst.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Adres IP docelowy:";
            // 
            // textBox_ForwTabl_Int_Dst
            // 
            this.textBox_ForwTabl_Int_Dst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_ForwTabl_Int_Dst.Location = new System.Drawing.Point(190, 228);
            this.textBox_ForwTabl_Int_Dst.Name = "textBox_ForwTabl_Int_Dst";
            this.textBox_ForwTabl_Int_Dst.Size = new System.Drawing.Size(183, 20);
            this.textBox_ForwTabl_Int_Dst.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Interfejs docelowy:";
            // 
            // textBox_tab2_Port
            // 
            this.textBox_tab2_Port.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_tab2_Port.Location = new System.Drawing.Point(188, 360);
            this.textBox_tab2_Port.Name = "textBox_tab2_Port";
            this.textBox_tab2_Port.Size = new System.Drawing.Size(183, 20);
            this.textBox_tab2_Port.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 363);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Port docelowy:";
            // 
            // textBox_Tab2
            // 
            this.textBox_Tab2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_Tab2.Location = new System.Drawing.Point(188, 334);
            this.textBox_Tab2.Name = "textBox_Tab2";
            this.textBox_Tab2.Size = new System.Drawing.Size(183, 20);
            this.textBox_Tab2.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Adres IP docelowy:";
            // 
            // button_tab2
            // 
            this.button_tab2.Location = new System.Drawing.Point(11, 386);
            this.button_tab2.Name = "button_tab2";
            this.button_tab2.Size = new System.Drawing.Size(360, 23);
            this.button_tab2.TabIndex = 15;
            this.button_tab2.Text = "Dodaj nowy wpis do tabeli";
            this.button_tab2.UseVisualStyleBackColor = true;
            this.button_tab2.Click += new System.EventHandler(this.button_tab2_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(11, 294);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(360, 37);
            this.label8.TabIndex = 20;
            this.label8.Text = "Tablica IpToPortTable";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(13, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(360, 37);
            this.label9.TabIndex = 21;
            this.label9.Text = "Tablica ForwardingTable";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_path
            // 
            this.textBox_path.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_path.Location = new System.Drawing.Point(188, 435);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(183, 20);
            this.textBox_path.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 438);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Scieżka do pliku:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 511);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_tab2_Port);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_Tab2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_tab2);
            this.Controls.Add(this.textBox_ForwTabl_Int_Dst);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_ForwTabl_IP_Dst);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_ForwTabl_Int_Src);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonMake);
            this.Controls.Add(this.textBox_ForwTabl_IP_Src);
            this.Controls.Add(this.label_ForwTab_IP_Scr);
            this.Controls.Add(this.textBox_Cloud_Port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Cloud_IP);
            this.Controls.Add(this.label_Cloud_IP);
            this.Controls.Add(this.button_Add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Label label_Cloud_IP;
        private System.Windows.Forms.TextBox textBox_Cloud_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Cloud_Port;
        private System.Windows.Forms.TextBox textBox_ForwTabl_IP_Src;
        private System.Windows.Forms.Label label_ForwTab_IP_Scr;
        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.TextBox textBox_ForwTabl_Int_Src;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_ForwTabl_IP_Dst;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ForwTabl_Int_Dst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_tab2_Port;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Tab2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_tab2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Label label10;
    }
}

