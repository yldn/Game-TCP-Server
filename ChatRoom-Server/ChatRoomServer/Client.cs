using ChatRoomServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomTypes;


//client thread class
public class Client
{
    private Socket clientSocket;
    //Core
    private Thread t;
    private byte[] data = new byte[1024];
    private Vector2 coordinates;
    

    public Client ( Socket s)
    {
        clientSocket = s;
        // start a  thread deal with client receive
        t = new Thread(ReceiveMessage);
        t.Start();
    }

    private void ReceiveMessage()
    {
        //thread real with received info from client
        while (true)
        {
            if (Connected)
            {

                try { 
                    int length = clientSocket.Receive(data);

                string message = Encoding.UTF8.GetString(data, 0, length);
                //群发给客户端
                //boardcasting
                //Program.BroadcastMeseage(message);

                Console.WriteLine("received: " + message);

                Program.ParseMessage(message);
                }
                catch (SocketException e)
                {
                    continue;
                    throw e;
                }

            } 
        }
    }

    public void SendMessage(string message)
    {
       
        if (Connected)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            if (clientSocket.Poll(10, SelectMode.SelectRead))
            {
                clientSocket.Close();
                return;//thread stopped
            }
            clientSocket.Send(data);
            //Console.WriteLine("message sent successful: " + message);
        }
       
    }

    public bool Connected
    {
        get { return clientSocket.Connected; }
    }

    public Vector2 GetCoordinates()
    {
        return coordinates;
    }

    public void SetCoodrinates(Vector2 _coordinates)
    {
        coordinates = _coordinates;
    }
}