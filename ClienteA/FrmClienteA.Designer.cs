namespace ClienteA
{
    partial class FrmClienteA
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
            this.cbJanelas = new System.Windows.Forms.ComboBox();
            this.cbAcao1 = new System.Windows.Forms.ComboBox();
            this.lbJanela = new System.Windows.Forms.Label();
            this.lbAcao1 = new System.Windows.Forms.Label();
            this.lbPorta = new System.Windows.Forms.Label();
            this.lbAcao2 = new System.Windows.Forms.Label();
            this.cbPortas = new System.Windows.Forms.ComboBox();
            this.cbAcao2 = new System.Windows.Forms.ComboBox();
            this.btnOK1 = new System.Windows.Forms.Button();
            this.btnOK2 = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnDesconectar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbJanelas
            // 
            this.cbJanelas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJanelas.FormattingEnabled = true;
            this.cbJanelas.Items.AddRange(new object[] {
            "Janela 1",
            "Janela 2",
            "Janela 3",
            "Janela 4",
            "Janela 5",
            "Janela 6",
            "Janela 7",
            "Janela 8"});
            this.cbJanelas.Location = new System.Drawing.Point(12, 25);
            this.cbJanelas.Name = "cbJanelas";
            this.cbJanelas.Size = new System.Drawing.Size(121, 21);
            this.cbJanelas.TabIndex = 0;
            // 
            // cbAcao1
            // 
            this.cbAcao1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAcao1.FormattingEnabled = true;
            this.cbAcao1.Items.AddRange(new object[] {
            "Abrir",
            "Fechar",
            "Travar"});
            this.cbAcao1.Location = new System.Drawing.Point(159, 25);
            this.cbAcao1.Name = "cbAcao1";
            this.cbAcao1.Size = new System.Drawing.Size(121, 21);
            this.cbAcao1.TabIndex = 1;
            // 
            // lbJanela
            // 
            this.lbJanela.AutoSize = true;
            this.lbJanela.Location = new System.Drawing.Point(12, 9);
            this.lbJanela.Name = "lbJanela";
            this.lbJanela.Size = new System.Drawing.Size(38, 13);
            this.lbJanela.TabIndex = 2;
            this.lbJanela.Text = "Janela";
            // 
            // lbAcao1
            // 
            this.lbAcao1.AutoSize = true;
            this.lbAcao1.Location = new System.Drawing.Point(156, 9);
            this.lbAcao1.Name = "lbAcao1";
            this.lbAcao1.Size = new System.Drawing.Size(32, 13);
            this.lbAcao1.TabIndex = 3;
            this.lbAcao1.Text = "Ação";
            // 
            // lbPorta
            // 
            this.lbPorta.AutoSize = true;
            this.lbPorta.Location = new System.Drawing.Point(12, 65);
            this.lbPorta.Name = "lbPorta";
            this.lbPorta.Size = new System.Drawing.Size(32, 13);
            this.lbPorta.TabIndex = 4;
            this.lbPorta.Text = "Porta";
            // 
            // lbAcao2
            // 
            this.lbAcao2.AutoSize = true;
            this.lbAcao2.Location = new System.Drawing.Point(156, 65);
            this.lbAcao2.Name = "lbAcao2";
            this.lbAcao2.Size = new System.Drawing.Size(32, 13);
            this.lbAcao2.TabIndex = 5;
            this.lbAcao2.Text = "Ação";
            // 
            // cbPortas
            // 
            this.cbPortas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortas.FormattingEnabled = true;
            this.cbPortas.Items.AddRange(new object[] {
            "Porta 1",
            "Porta 2"});
            this.cbPortas.Location = new System.Drawing.Point(12, 83);
            this.cbPortas.Name = "cbPortas";
            this.cbPortas.Size = new System.Drawing.Size(121, 21);
            this.cbPortas.TabIndex = 6;
            // 
            // cbAcao2
            // 
            this.cbAcao2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAcao2.FormattingEnabled = true;
            this.cbAcao2.Items.AddRange(new object[] {
            "Abrir",
            "Fechar",
            "Travar"});
            this.cbAcao2.Location = new System.Drawing.Point(159, 83);
            this.cbAcao2.Name = "cbAcao2";
            this.cbAcao2.Size = new System.Drawing.Size(121, 21);
            this.cbAcao2.TabIndex = 7;
            // 
            // btnOK1
            // 
            this.btnOK1.Enabled = false;
            this.btnOK1.Location = new System.Drawing.Point(300, 23);
            this.btnOK1.Name = "btnOK1";
            this.btnOK1.Size = new System.Drawing.Size(38, 23);
            this.btnOK1.TabIndex = 8;
            this.btnOK1.Text = "OK";
            this.btnOK1.UseVisualStyleBackColor = true;
            this.btnOK1.Click += new System.EventHandler(this.btnOK1_Click);
            // 
            // btnOK2
            // 
            this.btnOK2.Enabled = false;
            this.btnOK2.Location = new System.Drawing.Point(300, 83);
            this.btnOK2.Name = "btnOK2";
            this.btnOK2.Size = new System.Drawing.Size(38, 23);
            this.btnOK2.TabIndex = 9;
            this.btnOK2.Text = "OK";
            this.btnOK2.UseVisualStyleBackColor = true;
            this.btnOK2.Click += new System.EventHandler(this.btnOK2_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(205, 136);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 10;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(48, 136);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(85, 23);
            this.btnConectar.TabIndex = 11;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnDesconectar
            // 
            this.btnDesconectar.Location = new System.Drawing.Point(48, 136);
            this.btnDesconectar.Name = "btnDesconectar";
            this.btnDesconectar.Size = new System.Drawing.Size(85, 23);
            this.btnDesconectar.TabIndex = 12;
            this.btnDesconectar.Text = "Desconectar";
            this.btnDesconectar.UseVisualStyleBackColor = true;
            this.btnDesconectar.Visible = false;
            this.btnDesconectar.Click += new System.EventHandler(this.btnDesconectar_Click);
            // 
            // FrmClienteA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 176);
            this.Controls.Add(this.btnDesconectar);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnOK2);
            this.Controls.Add(this.btnOK1);
            this.Controls.Add(this.cbAcao2);
            this.Controls.Add(this.cbPortas);
            this.Controls.Add(this.lbAcao2);
            this.Controls.Add(this.lbPorta);
            this.Controls.Add(this.lbAcao1);
            this.Controls.Add(this.lbJanela);
            this.Controls.Add(this.cbAcao1);
            this.Controls.Add(this.cbJanelas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmClienteA";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cliente A";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmClienteA_FormClosing);
            this.Load += new System.EventHandler(this.FrmClienteA_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbJanelas;
        private System.Windows.Forms.ComboBox cbAcao1;
        private System.Windows.Forms.Label lbJanela;
        private System.Windows.Forms.Label lbAcao1;
        private System.Windows.Forms.Label lbPorta;
        private System.Windows.Forms.Label lbAcao2;
        private System.Windows.Forms.ComboBox cbPortas;
        private System.Windows.Forms.ComboBox cbAcao2;
        private System.Windows.Forms.Button btnOK1;
        private System.Windows.Forms.Button btnOK2;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnDesconectar;
    }
}

