using System;
using System.Collections.Generic;
using System.Text;

namespace ClienteA
{
    class Protocolo
    {
        //Elemento(janela,porta,etc)
        public string strElemento;

        //Tipo do a��o (abrir,fechar,travar, etc)
        public Acao cmdAcao;

        //Troca de mensagens cliente - servidor
        public string strMensagem;

        //Construtor default
        public Protocolo()
        {
            this.cmdAcao = Acao.Null;
            this.strElemento = null;
            this.strMensagem = null;

        }

        //Converte a mensagem de sequ�ncia de bytes para um comando v�lido
        public Protocolo(byte[] data)
        {
            //Os primeiros 4 bytes s�o a a��o
            this.cmdAcao = (Acao)BitConverter.ToInt32(data, 0);

            //Os 4 seguintes armazenam o compimento do elemento
            int elementoLen = BitConverter.ToInt32(data, 4);

            //Os pr�ximos 4 armazenam o comprimento da mensagem
            int msgLen = BitConverter.ToInt32(data, 8);

            //Verifica se realmente foi passado um elemento... E se foi, captura-o.
            if (elementoLen > 0)
                this.strElemento = Encoding.UTF8.GetString(data, 12, elementoLen);
            else
                this.strElemento = null;

            //Checa se a mensagem � nula.
            if (msgLen > 0)
                this.strMensagem = Encoding.UTF8.GetString(data, 12 + elementoLen, msgLen);
            else
                this.strMensagem = null;
        }

        //Converte a estrutura em uma sequ�ncia de bytes.
        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();

            //Os primeiros 4 bytes s�o a a��o
            result.AddRange(BitConverter.GetBytes((int)cmdAcao));

            //Adiciona o comprimento do elemento
            if (strElemento != null)
                result.AddRange(BitConverter.GetBytes(strElemento.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Adiciona o comprimento da mensagem.
            if (strMensagem != null)
                result.AddRange(BitConverter.GetBytes(strMensagem.Length));
            else
                result.AddRange(BitConverter.GetBytes(0));

            //Adiciona o elemento.
            if(strElemento != null)
                result.AddRange(Encoding.UTF8.GetBytes(strElemento));

            //Adiciona a mensagem.
            if (strMensagem != null)
                result.AddRange(Encoding.UTF8.GetBytes(strMensagem));

            // Retorna a sequ�ncia de bytes.
            return result.ToArray();
        }
    }
}
