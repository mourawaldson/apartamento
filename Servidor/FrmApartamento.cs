using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Xml;

namespace Servidor
{

    enum Acao
    {
        Abrir,
        Fechar,
        Travar,
        Desconectar,
        Null
    }

    public partial class FrmApartamento : Form
    {

        struct EstadoElementos
        {
            public string eeJanela1;
            public string vJanela1;
            public string eeJanela2;
            public string vJanela2;
            public string eeJanela3;
            public string vJanela3;
            public string eeJanela4;
            public string vJanela4;
            public string eeJanela5;
            public string vJanela5;
            public string eeJanela6;
            public string vJanela6;
            public string eeJanela7;
            public string vJanela7;
            public string eeJanela8;
            public string vJanela8;
            public string eePorta1;
            public string vPorta1;
            public string eePorta2;
            public string vPorta2;
        }

        private ArrayList clientList;

        private Socket serverSocket;

        private byte[] byteData = new byte[1024];

        public FrmApartamento()
        {
            clientList = new ArrayList();
            InitializeComponent();
        }
        
        private string elemento;

        private EstadoElementos estadoElementos = new EstadoElementos();

        private void LerXML()
        {
            XmlTextReader reader = new XmlTextReader("xml/EstatoDosElementos.xml");

            /*O método Read continua se movendo pelo arquivo XML seqüencialmente até atingir o final do arquivo,
              onde o método Read retornará um valor "False."*/
            while (reader.Read())
            {
                //De acordo com o TIPO do NÓ atual...
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        while (reader.MoveToNextAttribute())
                        {
                            //atriubui o nome do elemento atual para a variavel elemento!
                            this.elemento = reader.Value;
                        }
                        break;
                    case XmlNodeType.Text:
                        if (this.elemento == "janela1")
                        {
                            this.estadoElementos.eeJanela1 = reader.Value;
                            this.estadoElementos.vJanela1 = "Janela 1";
                        }
                        if (this.elemento == "janela2")
                        {
                            this.estadoElementos.eeJanela2 = reader.Value;
                            this.estadoElementos.vJanela2 = "Janela 2";
                        }
                        if (this.elemento == "janela3")
                        {
                            this.estadoElementos.eeJanela3 = reader.Value;
                            this.estadoElementos.vJanela3 = "Janela 3";
                        }
                        if (this.elemento == "janela4")
                        {
                            this.estadoElementos.eeJanela4 = reader.Value;
                            this.estadoElementos.vJanela4 = "Janela 4";
                        }
                        if (this.elemento == "janela5")
                        {
                            this.estadoElementos.eeJanela5 = reader.Value;
                            this.estadoElementos.vJanela5 = "Janela 5";
                        }
                        if (this.elemento == "janela6")
                        {
                            this.estadoElementos.eeJanela6 = reader.Value;
                            this.estadoElementos.vJanela6 = "Janela 6";
                        }
                        if (this.elemento == "janela7")
                        {
                            this.estadoElementos.eeJanela7 = reader.Value;
                            this.estadoElementos.vJanela7 = "Janela 7";
                        }
                        if (this.elemento == "janela8")
                        {
                            this.estadoElementos.eeJanela8 = reader.Value;
                            this.estadoElementos.vJanela8 = "Janela 8";
                        }
                        if (this.elemento == "porta1")
                        {
                            this.estadoElementos.eePorta1 = reader.Value;
                            this.estadoElementos.vPorta1 = "Porta 1";
                        }
                        if (this.elemento == "porta2")
                        {
                            this.estadoElementos.eePorta2 = reader.Value;
                            this.estadoElementos.vPorta2 = "Porta 2";
                        }
                        break;
                }
            }

        }

        private void FrmApartamento_Load(object sender, EventArgs e)
        {
            try
            {
                this.LerXML();
                this.LoadEstados();
                this.serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 2523);
                this.serverSocket.Bind(ipEndPoint);
                this.serverSocket.Listen(8);
                this.serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Servidor Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void LoadEstados()
        {
            if (this.estadoElementos.vJanela1 == "Janela 1")
            {
                if (this.estadoElementos.eeJanela1 == "Aberta")
                {
                    try
                    {
                        this.pnJanela1.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 1 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela1 == "Fechada")
                {
                    try
                    {
                        this.pnJanela1.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 1 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela1 == "Travada")
                {
                    try
                    {
                        this.pnJanela1.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 1 Travada não encontrada!", "Servidor");
                    }
                }
            }
            if (this.estadoElementos.vJanela2 == "Janela 2")
            {
                if (this.estadoElementos.eeJanela2 == "Aberta")
                {
                    try
                    {
                        this.pnJanela2.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 2 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela2 == "Fechada")
                {
                    try
                    {
                        this.pnJanela2.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 2 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela2 == "Travada")
                {
                    try
                    {
                        this.pnJanela2.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 2 Travada não encontrada!", "Servidor");
                    }
                }
            }

            if (this.estadoElementos.vJanela3 == "Janela 3")
            {
                if (this.estadoElementos.eeJanela3 == "Aberta")
                {
                    try
                    {
                        this.pnJanela3.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 3 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela3 == "Fechada")
                {
                    try
                    {
                        this.pnJanela3.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 3 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela3 == "Travada")
                {
                    try
                    {
                        this.pnJanela3.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 3 Travada não encontrada!", "Servidor");
                    }
                }
            }
            if (this.estadoElementos.vJanela4 == "Janela 4")
            {
                if (this.estadoElementos.eeJanela4 == "Aberta")
                {
                    try
                    {
                        this.pnJanela4.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 4 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela4 == "Fechada")
                {
                    try
                    {
                        this.pnJanela4.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 4 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela4 == "Travada")
                {
                    try
                    {
                        this.pnJanela4.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 4 Travada não encontrada!", "Servidor");
                    }
                }
            }
            if (this.estadoElementos.vJanela5 == "Janela 5")
            {
                if (this.estadoElementos.eeJanela5 == "Aberta")
                {
                    try
                    {
                        this.pnJanela5.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 5 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela5 == "Fechada")
                {
                    try
                    {
                        this.pnJanela5.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 5 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela5 == "Travada")
                {
                    try
                    {
                        this.pnJanela5.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 5 Travada não encontrada!", "Servidor");
                    }
                }
            }
            if (this.estadoElementos.vJanela6 == "Janela 6")
            {
                if (this.estadoElementos.eeJanela6 == "Aberta")
                {
                    try
                    {
                        this.pnJanela6.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 6 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela6 == "Fechada")
                {
                    try
                    {
                        this.pnJanela6.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 6 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela6 == "Travada")
                {
                    try
                    {
                        this.pnJanela6.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 6 Travada não encontrada!", "Servidor");
                    }
                }
            }
            if (this.estadoElementos.vJanela7 == "Janela 7")
            {
                if (this.estadoElementos.eeJanela7 == "Aberta")
                {
                    try
                    {
                        this.pnJanela7.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 7 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela7 == "Fechada")
                {
                    try
                    {
                        this.pnJanela7.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 7 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela7 == "Travada")
                {
                    try
                    {
                        this.pnJanela7.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 7 Travada não encontrada!", "Servidor");
                    }
                }
            }
            if (this.estadoElementos.vJanela8 == "Janela 8")
            {
                if (this.estadoElementos.eeJanela8 == "Aberta")
                {
                    try
                    {
                        this.pnJanela8.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 8 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela8 == "Fechada")
                {
                    try
                    {
                        this.pnJanela8.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 8 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eeJanela8 == "Travada")
                {
                    try
                    {
                        this.pnJanela8.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Janela 8 Travada não encontrada!", "Servidor");
                    }
                }
            }

            if (this.estadoElementos.vPorta1 == "Porta 1")
            {
                if (this.estadoElementos.eePorta1 == "Aberta")
                {
                    try
                    {
                        this.pnPorta1.BackgroundImage = Image.FromFile(@"imagens/portaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Porta 1 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eePorta1 == "Fechada")
                {
                    try
                    {
                        this.pnPorta1.BackgroundImage = Image.FromFile(@"imagens/portaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Porta 1 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eePorta1 == "Travada")
                {
                    try
                    {
                        this.pnPorta1.BackgroundImage = Image.FromFile(@"imagens/portaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Porta 1 Travada não encontrada!", "Servidor");
                    }
                }
            }

            if (this.estadoElementos.vPorta2 == "Porta 2")
            {
                if (this.estadoElementos.eePorta2 == "Aberta")
                {
                    try
                    {
                        this.pnPorta2.BackgroundImage = Image.FromFile(@"imagens/portaAberta.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Porta 2 Aberta não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eePorta2 == "Fechada")
                {
                    try
                    {
                        this.pnPorta2.BackgroundImage = Image.FromFile(@"imagens/portaFechada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Porta 2 Fechada não encontrada!", "Servidor");
                    }
                }
                if (this.estadoElementos.eePorta2 == "Travada")
                {
                    try
                    {
                        this.pnPorta2.BackgroundImage = Image.FromFile(@"imagens/portaTravada.gif");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        MessageBox.Show("Imagem da Porta 2 Travada não encontrada!", "Servidor");
                    }
                }
            }
            
        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = this.serverSocket.EndAccept(ar);
                this.clientList.Add(clientSocket);
                MessageBox.Show("Um Cliente acaba de se conectar..", "Servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,new AsyncCallback(OnReceive), clientSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Servidor OnAccept", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                Protocolo msgReceived = new Protocolo(byteData);
                Protocolo msgToSend = new Protocolo();

                byte[] msg = new byte[1024];

                string elemento = msgReceived.strElemento;
                string acao = msgReceived.cmdAcao.ToString();
                
                msgToSend.strElemento = elemento;
                msgToSend.cmdAcao = msgReceived.cmdAcao;
                msgToSend.strMensagem = null;

                if (acao == "Abrir")
                {

                    switch (elemento)
                    {

                        case "Janela 1":
                            if (this.estadoElementos.eeJanela1 == "Aberta")
                            {
                                msgToSend.strMensagem = "A Janela 1 já está aberta!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela1.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                    this.estadoElementos.eeJanela1 = "Aberta";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 2":
                            if (this.estadoElementos.eeJanela2 == "Aberta")
                            {
                                msgToSend.strMensagem = "A " + elemento + " já está aberta!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela2.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                    this.estadoElementos.eeJanela2 = "Aberta";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 3":
                            try
                            {
                                this.pnJanela3.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                this.estadoElementos.eeJanela3 = "Aberta";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 4":
                            try
                            {
                                this.pnJanela4.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                this.estadoElementos.eeJanela4 = "Aberta";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 5":
                            try
                            {
                                this.pnJanela5.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                this.estadoElementos.eeJanela5 = "Aberta";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 6":
                            try
                            {
                                this.pnJanela6.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                this.estadoElementos.eeJanela6 = "Aberta";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 7":
                            try
                            {
                                this.pnJanela7.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                this.estadoElementos.eeJanela7 = "Aberta";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 8":
                            try
                            {
                                this.pnJanela8.BackgroundImage = Image.FromFile(@"imagens/janelaAberta.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                this.estadoElementos.eeJanela8 = "Aberta";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Porta 1":
                            try
                            {
                                this.pnPorta1.BackgroundImage = Image.FromFile(@"imagens/portaAberta.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                this.estadoElementos.eePorta1 = "Aberta";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!", "Servidor");
                            }
                            break;

                        case "Porta 2":
                            try
                            {
                                this.pnPorta2.BackgroundImage = Image.FromFile(@"imagens/portaAberta.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi aberta!";
                                this.estadoElementos.eePorta2 = "Aberta";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!", "Servidor");
                            }
                            break;
                    }
                }
                if (acao == "Fechar")
                {
                    switch (elemento)
                    {
                        case "Janela 1":
                            try
                            {
                                this.pnJanela1.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eeJanela1 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 2":
                            try
                            {
                                this.pnJanela2.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eeJanela2 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 3":
                            try
                            {
                                this.pnJanela3.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eeJanela3 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 4":
                            try
                            {
                                this.pnJanela4.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eeJanela4 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 5":
                            try
                            {
                                this.pnJanela5.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eeJanela5 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 6":
                            try
                            {
                                this.pnJanela6.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eeJanela6 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 7":
                            try
                            {
                                this.pnJanela7.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eeJanela7 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Janela 8":
                            try
                            {
                                this.pnJanela8.BackgroundImage = Image.FromFile(@"imagens/janelaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eeJanela8 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!","Servidor");
                            }
                            break;

                        case "Porta 1":
                            try
                            {
                                this.pnPorta1.BackgroundImage = Image.FromFile(@"imagens/portaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eePorta1 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!", "Servidor");
                            }
                            break;

                        case "Porta 2":
                            try
                            {
                                this.pnPorta2.BackgroundImage = Image.FromFile(@"imagens/portaFechada.gif");
                                msgToSend.strMensagem = "A " + elemento + " foi fechada!";
                                this.estadoElementos.eePorta2 = "Fechada";
                            }
                            catch (System.IO.FileNotFoundException)
                            {
                                MessageBox.Show("Imagem não encontrada!", "Servidor");
                            }
                            break;
                    }
                }
                if(acao == "Travar")
                {
                    switch (elemento)
                    {
                        case "Janela 1":
                            if (this.estadoElementos.eeJanela1 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a janela, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela1.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela1 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 2":
                            if (this.estadoElementos.eeJanela2 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a janela, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela2.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela2 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 3":
                            if (this.estadoElementos.eeJanela3 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a janela, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela3.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela3 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 4":
                            if (this.estadoElementos.eeJanela4 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a janela, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela4.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela4 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 5":
                            if (this.estadoElementos.eeJanela5 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a janela, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela5.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela5 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 6":
                            if (this.estadoElementos.eeJanela6 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a janela, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela6.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela6 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 7":
                            if (this.estadoElementos.eeJanela7 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a janela, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela7.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela7 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Janela 8":
                            if (this.estadoElementos.eeJanela8 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a janela, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnJanela8.BackgroundImage = Image.FromFile(@"imagens/janelaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela8 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Porta 1":
                            if (this.estadoElementos.eePorta1 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a porta, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnPorta1.BackgroundImage = Image.FromFile(@"imagens/portaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela8 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;

                        case "Porta 2":
                            if (this.estadoElementos.eePorta2 == "Aberta")
                            {
                                msgToSend.strMensagem = "Para travar a porta, ela precisa estar fechada!";
                            }
                            else
                            {
                                try
                                {
                                    this.pnPorta2.BackgroundImage = Image.FromFile(@"imagens/portaTravada.gif");
                                    msgToSend.strMensagem = "A " + elemento + " foi travada!";
                                    this.estadoElementos.eeJanela8 = "Travada";
                                }
                                catch (System.IO.FileNotFoundException)
                                {
                                    MessageBox.Show("Imagem não encontrada!", "Servidor");
                                }
                            }
                            break;
                    }

                }

                if (acao == "Desconectar")
                {
                    int indice = 0;
                    foreach(Socket socket in clientList)
                    {
                        if(socket == clientSocket)
                        {
                            this.clientList.RemoveAt(indice);
                            break;
                        }
                        ++indice;
                    }
                    clientSocket.Close();
                    MessageBox.Show("Um cliente desconectou...", "Servidor");
                }
                else
                {
                    msg = msgToSend.ToByte();
                    foreach (Socket socket in clientList)
                    {
                        socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(OnSend), socket);
                    }
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Servidor OnReceive", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Servidor OnSend", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Disconnect()
        {
            try
            {
                Protocolo msgToSend = new Protocolo();
                msgToSend.strMensagem = null;
                msgToSend.strElemento = null;
                msgToSend.cmdAcao = Acao.Desconectar;
                byte[] b = msgToSend.ToByte();
                foreach (Socket socket in clientList)
                {
                    socket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), socket);
                }
                this.serverSocket.Disconnect(false);
                this.serverSocket.Close();
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Servidor FormClosing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmApartamento_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair do programa?", "Servidor",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            this.Disconnect();
        }

    }
}