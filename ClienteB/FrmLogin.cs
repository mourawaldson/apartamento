using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ClienteB
{
    public partial class FrmLogin : Form
    {

        public IPAddress ipAddress;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.txtServerIP.Text = "127.0.0.1";
        }

        private void txtServerIP_TextChanged(object sender, EventArgs e)
        {
            if (txtServerIP.Text.Length > 0)
                this.btnOK.Enabled = true;
            else
                this.btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.ipAddress = IPAddress.Parse(txtServerIP.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Conect ip: "+this.ipAddress, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}