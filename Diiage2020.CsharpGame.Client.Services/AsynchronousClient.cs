using Diiage2020.CsharpGame.Entities;
using Diiage2020.CSharpGame.Client.Entities;
using SerializeJson;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Diiage2020.CsharpGame.Client.Services
{
    public static class AsynchronousClient
    {
        private const string ipServer = "172.16.1.4";
        // The port number for the remote device.  
        private const int port = 11000;
        // ManualResetEvent instances signal completion
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        // The response from the remote device.  
        private static String response = String.Empty;
        //Socket client
        private static Socket Client;

        public static event EventHandler DataReceived;
        public static event EventHandler GameAsStart;
        private static void OnDataReceived(string data, EventArgs e)
        {
            Game game = Serialise.FromJson<Game>(data);
            if (game.Etat == "En Cours")
            {
                GameAsStart?.Invoke(game, e);
            }

            DataReceived?.Invoke(game, e);
        }
        public static void StartClient()
        {
            // Connect to a remote device. 
            try
            {
                //IPAddress ipAddress = IPAddress.Parse(ipServer);
                IPAddress ipAddress = IPAddress.Loopback;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                // Create a TCP/IP socket.  
                Client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                connectDone.Set();
                // Connect to the remote endpoint.  
                Client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), Client);
                connectDone.WaitOne();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static void SendToServer(string action)
        {
            string data = Serialise.ToJson<SendObject>(new SendObject { Action = action });

            Send(Client, data);
        }
        public static void SendToServer(Game game, string action, object objectToAdd = null, object objectWhereAdd = null)
        {
            string data = Serialise.ToJson<SendObject>(new SendObject { Action = action, Game = game, ObjectToAdd = objectToAdd, ObjectWhereAdd = objectWhereAdd });
            Send(Client, data);
        }
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                connectDone.Set();

                while (true)
                {
                    receiveDone.Reset();
                    // Receive the response from the remote device.  
                    Receive(client);
                    receiveDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.  
                response = state.sb.ToString();
                if (response.IndexOf("<EOF>") > -1)
                {
                    response = response.Substring(0, response.Length - 5);
                    // Write the response to the console.  
                    Console.WriteLine("Response received : {0}", response);
                    OnDataReceived(response, EventArgs.Empty);
                    receiveDone.Set();
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
                }
            }
        }
        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data + "<EOF>");

            sendDone.Set();
            // Begin sending the data to the remote device.  
                client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
            sendDone.WaitOne();
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
