using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FinalTermProject
{
    public partial class TCPClient : Form
    {
        public TCPClient()
        {
            InitializeComponent();
        }

        private delegate void SafeCallDelegate(string text);
        public Dashboard dboard;
        IPEndPoint ipepServer;
        Socket serverSocket;
        public bool isURLExist = true;
        string result = "";

        private void TCPClient_Load(object sender, EventArgs e)
        {
            ipepServer = new IPEndPoint(dboard.ipServer, dboard.portServer);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                serverSocket.Connect(ipepServer);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Can't connect to server\n Something went wrong\n" +
                   ex.InnerException.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Thread listenServer = new Thread(ListenResultFromServer);
            listenServer.IsBackground = true;
            
            listenServer.Start();
        }

        private void ListenResultFromServer()
        {
            Byte[] recvData = new byte[1024 * 5000];
            while (serverSocket.Connected)
            {
                if (!serverSocket.Connected)
                {
                    serverSocket.Close();
                    dboard.client.Close();
                }
                try
                {
                    serverSocket.Receive(recvData);
                }
                catch
                {
                    return;
                }
                result = DeserializeMessage(recvData);
                if (result == "")
                    return;
                DisplayResult(result);
                result = "";
                recvData = null;
            }
        }

        private void DisplayResult(string text)
        {
            if (!dboard.connectedClient.isURLExist)
            {
                if (failResult.InvokeRequired)
                {
                    var d = new SafeCallDelegate(DisplayResult);
                    failResult.Invoke(d, new object[] { text });
                }
                else
                {
                    failResult.Text = result;
                    failResult.Visible = true;
                    succeedResult.Visible = false;
                }
            }
            else
            {
                if (succeedResult.InvokeRequired)
                {
                    var d = new SafeCallDelegate(DisplayResult);
                    succeedResult.Invoke(d, new object[] { text });
                }
                else
                {
                    succeedResult.Text = result;
                    succeedResult.Visible = true;
                    failResult.Visible = false;
                }
            }
        }

        private void requestBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(URLbox.Text))
            {
                if (!URLbox.Text.StartsWith("http://") && !URLbox.Text.StartsWith("https://"))
                {
                   URLbox.Text = URLbox.Text.Insert(0, "http://");
                }
                serverSocket.Send(SerializeMessage(URLbox.Text));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(URLbox.Text) || URLbox.Text.Equals("about:blank"))
                {
                    MessageBox.Show("Enter a valid URL.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    URLbox.Focus();
                    return;
                }
            }
        }

        private byte[] SerializeMessage(object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(ms, obj);
            return ms.ToArray();
        }

        private string DeserializeMessage(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                return (string)binaryFormatter.Deserialize(ms);
            }
            catch
            {
                return "";
            }
        }

        private void closeClient(object sender, FormClosedEventArgs e)
        {
            serverSocket.Close();
            dboard.client.Close();
        }
    }
}
