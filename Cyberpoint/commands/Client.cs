using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cyberpoint.commands
{
    public class Client
    {

        public static IPHostEntry host = Dns.GetHostEntry("localhost");
        public static IPAddress ipAddress = host.AddressList[0];
        public static IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

        // Create a TCP/IP  socket.    
        public static Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        private string endstr = "*&*";

        public void StartClient()
        {try
            {
                // Connect to a Remote server  
                // Get Host IP Address that is used to establish a connection  
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
                // If a host has multiple addresses, you will get a list of addresses  
                

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());
                }
                catch (ArgumentNullException ane)
                {
                    //Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    //Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }
        }

        public void Stop()
        {
            // Release the socket.
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        public void Send(string message)
        {
            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(message + endstr);

            // Send the data through the socket.
            int bytesSent = sender.Send(msg);
        }

        public string Recive()
        {
            byte[] bytes = new byte[1024];
            // Receive the response from the remote device.
            int bytesRec = sender.Receive(bytes);
            char[] str = Encoding.ASCII.GetString(bytes, 0, bytesRec).ToCharArray();
            string Out = "";

            for(int i = 0; i < str.Length-endstr.Length; i++)
            {
                Out += str[i].ToString();
            }

            return Out;
        }
    }
}
