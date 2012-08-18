using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ClienteA
{

    enum Acao
    {
        Abrir,
        Fechar,
        Travar,
        Desconectar,
        Null
    }

    public partial class FrmClienteA : Form
    {

        public Socket clientSocket;

        private bool conectado = false;

        private byte[] byteData = new byte[1024];

        public FrmClienteA()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK1_Click(object sender, EventArgs e)
        {
            if (this.conectado == true)
            {
                try
                {
                    Protocolo msgToSend = new Protocolo();
                    msgToSend.strElemento = this.cbJanelas.Text;
                    string acao1 = this.cbAcao1.Text;
                    if (acao1 == "Abrir")
                        msgToSend.cmdAcao = Acao.Abrir;

                    if (acao1 == "Fechar")
                        msgToSend.cmdAcao = Acao.Fechar;

                    if (acao1 == "Travar")
                        msgToSend.cmdAcao = Acao.Travar;

                    byte[] byteData = msgToSend.ToByte();

                    this.clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível efetuar o comando!", "ClienteA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FrmClienteA_Load(object sender, EventArgs e)
        {
            this.cbJanelas.SelectedIndex = 0;
            this.cbAcao1.SelectedIndex = 0;
            this.cbPortas.SelectedIndex = 0;
            this.cbAcao2.SelectedIndex = 0;
        }

        private void FrmClienteA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair do programa?", "ClienteA",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            this.Disconnect();
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                this.clientSocket.EndSend(ar);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClienteA OnSend", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                this.clientSocket.EndReceive(ar);

                Protocolo msgReceived = new Protocolo(byteData);
                 
                string acao = msgReceived.cmdAcao.ToString();

                if (acao == "Desconectar")
                {
                    MessageBox.Show("O servidor desconectou...", "ClienteA");
                    this.Disconnect();
                    if (this.conectado == false)
                    {
                        this.btnDesconectar.Visible = false;
                        this.btnConectar.Visible = true;
                        this.btnOK1.Enabled = false;
                        this.btnOK2.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show(msgReceived.strMensagem, "ClienteA OnReceive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
                }

            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClienteA OnReceive", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                this.clientSocket.EndConnect(ar);
                this.conectado = true;
                MessageBox.Show("Você está conectado no servidor!", "ClienteA OnConnect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao se Conectar, servidor não encontrado!", "ClienteA OnConnect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Disconnect()
        {
            try
            {
                if (this.conectado == true)
                {
                    try
                    {
                        Protocolo msgToSend = new Protocolo();
                        msgToSend.strMensagem = null;
                        msgToSend.strElemento = null;
                        msgToSend.cmdAcao = Acao.Desconectar;
                        byte[] b = msgToSend.ToByte();
                        this.clientSocket.Send(b, 0, b.Length, SocketFlags.None);
                        this.clientSocket.Disconnect(false);
                        this.clientSocket.Close();
                    }
                    catch (ObjectDisposedException)
                    { }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ClienteA FormClosing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClienteA Disconnect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.conectado = false;
            }
        }

        private bool Connect(IPAddress ip)
        {
            try
            {
                this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAddress = ip;
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 2523);
                this.clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
                return true;
            }
            catch(Exception)
            {
                //MessageBox.Show("Erro ao se conectar..", "Cliente A", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
            if (frmLogin.DialogResult == DialogResult.OK)
            {
                if (this.Connect(frmLogin.ipAddress))
                {
                    this.btnConectar.Visible = false;
                    this.btnDesconectar.Visible = true;
                    this.btnOK1.Enabled = true;
                    this.btnOK2.Enabled = true;
                }
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            this.Disconnect();
            if(this.conectado == false)
            {
                this.btnDesconectar.Visible = false;
                this.btnConectar.Visible = true;
                this.btnOK1.Enabled = false;
                this.btnOK2.Enabled = false;
            }
        }

        private void btnOK2_Click(object sender, EventArgs e)
        {
            if (this.conectado == true)
            {
                try
                {
                    Protocolo msgToSend = new Protocolo();
                    msgToSend.strElemento = this.cbPortas.Text;
                    string acao2 = this.cbAcao2.Text;
                    if (acao2 == "Abrir")
                        msgToSend.cmdAcao = Acao.Abrir;

                    if (acao2 == "Fechar")
                        msgToSend.cmdAcao = Acao.Fechar;

                    if (acao2 == "Travar")
                        msgToSend.cmdAcao = Acao.Travar;

                    byte[] byteData = msgToSend.ToByte();

                    this.clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                }
                catch (Exception)
                {
                    MessageBox.Show("Não foi possível efetuar o comando!", "ClienteA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}