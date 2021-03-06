using Diiage2020.CsharpGame.Entities;
using Diiage2020.CSharpGame.Server.Services;
using SerializeJson;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Diiage2020.CsharpGame.Server.Core
{
    public class AsynchronousSocketListener
    {
        // Thread signal.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        //List de socket client
        private static List<Socket> socketsClient;
        //Class contenant les méthodes
        private static Service service;
        public static void Main(String[] args)
        {
            service = new Service();
            StartListening();
        }

        public static void StartListening()
        {

            // Establish the local endpoint for the socket.  
            //IPAddress ipAddress = IPAddress.Any;
            IPAddress ipAddress = IPAddress.Loopback;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            socketsClient = new List<Socket>();
            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    connectDone.Reset();
                    // Start an asynchronous socket to listen for connections.
                    ServerLog.Debug($"Waiting for a connection... On IP {ipAddress}");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);
                    connectDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                ServerLog.Error(e);
            }

        }
        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            connectDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            socketsClient.Add(handler);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            try
            {
                while (true)
                {
                    // Set the event to nonsignaled state.  
                    receiveDone.Reset();

                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);

                    // Wait until a connection is made before continuing.  
                    receiveDone.WaitOne();
                    state.sb.Clear();
                }
            }
            catch (Exception ex)
            {
                if (ex == null)
                {
                    socketsClient.Remove(handler);
                    ServerLog.Log("A client as disconect");
                }
                else
                {
                    ServerLog.Error(ex);
                }
            }
        }
        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

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
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    content = content.Substring(0, content.Length - 5);
                    // All the data has been read from the
                    // client. Display it on the console.
                    ServerLog.Debug($"Read {0} bytes from socket. \n Data : {1} {bytesRead}{content}");


                    ///Do something on data send by client for simulate an serveur action. 
                    SendObject sendObject = Serialise.FromJson<SendObject>(content);
                    SelectGoodMethod(sendObject, handler);
                    ///
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }
        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data + "<EOF>");

            sendDone.Reset();

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);

            sendDone.WaitOne();
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                ServerLog.Debug($"Sent bytes {bytesSent} to client.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                sendDone.Set();
            }
        }
        private static void SelectGoodMethod(SendObject sendObject, Socket handler)
        {
            Game game = sendObject.Game;
            MethodInfo method = service.GetType().GetMethod(sendObject.Action);
            if (game == null)
            {
                game = (Game)method.Invoke(service, null);
            }
            else if (sendObject.ObjectToAdd == null)
            {
                game = (Game)method.Invoke(service, new object[] { game });
            }
            else if(sendObject.ObjectToAdd != null && sendObject.ObjectWhereAdd == null)
            {
                game = (Game)method.Invoke(service, new object[] { game, sendObject.ObjectToAdd });
            }
            else
            {
                game = (Game)method.Invoke(service, new object[] { game, sendObject.ObjectToAdd, sendObject.ObjectWhereAdd });
            }

            if (sendObject.Action == "StartGame")
            {
                ServerLog.Log($"Send to {socketsClient.Count} client");
                foreach (var socket in socketsClient)
                {
                    ServerLog.Log($"Send to one client");
                    receiveDone.Set();
                    string content = Serialise.ToJson(game);
                    Send(socket, content);
                } 
            }
            else
            {
                receiveDone.Set();
                string content = Serialise.ToJson(game);
                Send(handler, content);
            }
        }
    }
}
