using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace VimanaPoi
{
    class TcpActions
    {
        Logger log;
        private TcpListener tcpListener;
        private Thread listenThread;
        private ArrayList clients;
        private int count;

        public delegate void ClientCountChangedEventHandler(int clientCount);
        public event ClientCountChangedEventHandler ClientCountChanged;
        
        public int ClientCount
        {
            get { return this.count; }
            set
            {
                this.count = value;
                if (ClientCountChanged != null)
                {
                    this.ClientCountChanged(count);
                }
            }
        }

        public TcpActions()
        {
            log = new Logger();
            int port = Properties.Settings.Default.port;
            this.tcpListener = new TcpListener(IPAddress.Any, port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            clients = new ArrayList();            
            this.listenThread.Start();
        }

        //method that actually listens for calls from the clients
        private void ListenForClients()
        {
            this.tcpListener.Start();
            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();                
                clients.Add(client);
                ClientCount++;
                sndExistData(client);         
                Thread a = new Thread(new ParameterizedThreadStart(CheckClientActive));
                a.Start(client);
            }
        }

        //Handles the data sending to the various clients that are connected.
        public void sndData(string cmdTxt)
        {            
            int a = clients.Count;
            log.WriteLog(cmdTxt);
            try
            {
                foreach (object ob in clients)
                {
                    try
                    {
                        
                        TcpClient tcpClient = (TcpClient)ob;
                        NetworkStream clientStream = tcpClient.GetStream();
                        ASCIIEncoding encoder = new ASCIIEncoding();
                        byte[] buffer = encoder.GetBytes(cmdTxt);
                        clientStream.Write(buffer, 0, buffer.Length);
                        clientStream.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex + "j");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "p2");
            }
        }

        private void sndExistData(object client)
        {
            try
            {
                TcpClient tcpClient = (TcpClient)client;
                NetworkStream clientStream = tcpClient.GetStream();
                ASCIIEncoding encoder = new ASCIIEncoding();
                foreach (string tmpa in allCmds)
                {
                    try
                    {
                        byte[] buffer = encoder.GetBytes(tmpa);
                        clientStream.Write(buffer, 0, buffer.Length);
                        clientStream.Flush();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                //allCmds.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        string[] allCmds = new string[13];
        
        public void HoldBuffer(string str,int ind)
        {
            allCmds[ind] = str;
        }

        public void PalletBuffer(string str)
        {

        }

        public void CheckClientActive(object ob)
        {
            TcpClient tcpClient = (TcpClient)ob;
            NetworkStream clientStream = tcpClient.GetStream();
            while (true)
            {
                try
                {
                    byte[] message = new byte[4096];
                    int bytesRead;
                    bytesRead = 0;
                    try
                    {
                        //blocks until a client sends a message   
                        bytesRead = clientStream.Read(message, 0, 4096);
                    }
                    catch
                    {
                        //a socket error has occured
                        //break;

                    }
                    if (bytesRead == 0)
                    {
                        //the client has disconnected from the server                        
                        --ClientCount;                        
                        clients.Remove(ob);
                        break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Thread.Sleep(2000);
            }
        }

    }
}
