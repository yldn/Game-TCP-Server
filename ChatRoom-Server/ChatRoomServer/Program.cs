using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomTypes;

namespace ChatRoomServer
{
    public class Program
    {
        static List<Client> clientlist = new List<Client>();
        static List<Planet> planets = new List <Planet>();


        public static void BroadcastMeseage(string _message)
        {
            var notConnectedList = new List<Client>();

            foreach(var client in clientlist)
            {
                Console.WriteLine("client connected"+ client.Connected);

                if (client.Connected)
                {
                    //Console.WriteLine("ready to send" + _message);
                    client.SendMessage(_message);
                }
                else
                {
                    notConnectedList.Add(client);
                }
            }

            //移除所有断开连接的客户端
            //remove all the clients
            foreach (var temp in notConnectedList)
            {
                clientlist.Remove(temp);
            }

        }

        public static void ParseMessage(string _message)
        {
            string[] lines = _message.Split("\r\n".ToCharArray());
            
            switch (lines[0])
            {
                case "***decision-answer":
                case "***decision-answer\r\n":
                   
                    DecisionAnswer d = UTIL.ParseDecisionAnswer(lines);
                    EnforceDecisionAnswer(d);
                    break;
                case "***location":
                case "***location\r\n":
                    Vector2 c = UTIL.ParseLocation(lines);
                    //TODO find player and update location
                    break;
                case "***notification":
                case "***notification\r\n":
                    Notification n = UTIL.ParseNotification(lines);
                    //TODO find player and update location
                    break;
                default:
                    BroadcastMeseage(_message);
                    //Console.WriteLine("defaultttt" + _message);
                    break;
            }
        }



        private static void EnforceDecisionAnswer(DecisionAnswer _answer)
        {
            //Console.WriteLine("***" + _answer.GetPlanetaryId());
            for (int i = 0; i < planets.Count; i++)
            {
                //Console.WriteLine("***" );
                //Console.WriteLine(planets[i].GetId());
                if (planets[i].GetId().Equals(_answer.GetPlanetaryId()))
                {
                    i = planets.Count - 1;

                    planets[i].AddVote(_answer.GetVote());
                }
            }
        }

        private static void RegisterClients()
        {
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //for testing: cmd -> ipconfig -all -> ip v4
            //tcpServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.104"), 7788));
            tcpServer.Listen(100);
            Console.WriteLine("server running.");

            while (true)
            {
				Socket clientSocket = tcpServer.Accept();
				Console.WriteLine("a client is connected !");
				//parse into CLient Obj
				Client client = new Client(clientSocket);
				//add to the list 
				clientlist.Add(client);

				//generates a new player every time a client is registered
				//should be enough for the presentation
				//very basic and definitely hackey from here on:
				//even BASIC AUTHENTIFICATION would require more message type + handling + sending + STORING + loading ....
				Player p = new Player();
				p.SetClient(client);
				//only works because there will be only one planet
				//officially we would need some locks e.g. here for the planets, because theyre used in the main thread
				planets[0].AddPlayer(p);

                client.SendMessage(planets[0].ToMessageString());

            }
        }

        static void Main(string[] args)
        {
            //TODO Load all planets/players from Database

            //the one planet needed for the presentation
            Planet planet = new Planet();
            planets.Add(planet);

			Thread t = new Thread(RegisterClients);
			t.Start();
            while (true)
            {
                //RegisterClient(tcpServer);

                foreach (Planet p in planets)
                {
                    p.Ping();

                }
                System.Threading.Thread.Sleep(5000);
                ////过**s开始生成 Decision 
                sendDecesion(); 
            }

        }


        static void sendDecesion()
        {
            //send Decision
            if (clientlist.Count >= 1)
            {
                Decision newDecision = DecisionPool.GetDecision(planets[0]);

                //Console.WriteLine("new Decision :" + newDecision.ToMessageString());

                BroadcastMeseage(newDecision.ToMessageString());

            }
        }


        public static List<Planet> getPlanets()
        {
            return planets;
        }
    }
}
