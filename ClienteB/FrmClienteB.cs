using System;
using System.IO;
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

    enum Acao
    {
        Abrir,
        Fechar,
        Travar,
        Desconectar,
        Null
    }

    public partial class FrmClienteB : Form
    {

        public Socket clientSocket;

        private bool conectado = false;

        private byte[] byteData = new byte[1024];

        public FrmClienteB()
        {
            InitializeComponent();
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
                MessageBox.Show(ex.Message, "ClienteB OnSend", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("O servidor desconectou...", "ClienteB");
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
                    DateTime agora = DateTime.Now;
                    this.txtLog.Text += agora.ToString("dd/MM/yyyy HH:mm:ss") + " - " + msgReceived.strMensagem + "\r\n";
                    this.txtLog.SelectionStart = this.txtLog.Text.Length;
                    this.txtLog.ScrollToCaret();
                    this.clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
                }

            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClienteB OnReceive", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                this.clientSocket.EndConnect(ar);
                this.conectado = true;
                MessageBox.Show("Você está conectado no servidor!", "ClienteB OnConnect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao se Conectar, servidor não encontrado!", "ClienteB OnConnect", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show(ex.Message, "ClienteB FormClosing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClienteB Disconnect", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception)
            {
                return false;
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            this.Disconnect();
            if (this.conectado == false)
            {
                this.btnDesconectar.Visible = false;
                this.btnConectar.Visible = true;
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
                }
            }
        }

        private void FrmClienteB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair do programa?", "ClienteB",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            string diretorio = "logs/" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            this.GerarLog(diretorio);
            this.Disconnect();
        }

        private void GerarLog(string pdiretorio)
        {
            try
            {
                FileInfo aFile = new FileInfo(pdiretorio);
                string texto = this.txtLog.Text;
                if (!aFile.Exists || texto != "")
                {
                    using (StreamWriter sw = aFile.CreateText())
                    {
                        sw.WriteLine(texto);
                        sw.WriteLine("**** Este log foi gerado às " + DateTime.Now.ToString("HH:mm:ss ***\r\n"));
                        sw.Flush();
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamReader sr = aFile.OpenText())
                    {
                        string txt = sr.ReadToEnd();
                        sr.Close();
                        using (StreamWriter sw = aFile.CreateText())
                        {
                            sw.WriteLine(txt+texto);
                            sw.WriteLine("**** Este log foi gerado às " + DateTime.Now.ToString("HH:mm:ss ***\r\n"));
                            sw.Flush();
                            sw.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possível criar o log!"+e);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}