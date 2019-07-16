using System;
using CustomTypes;
using ChatRoomServer;

public class UTIL
{
    public static bool GetRandomBool()
    {
        Random rand = new Random();
        return rand.Next(2) == 0;
    }

    public static int GetMaxPlanetPing()
    {
        return 10;
    }

    public static int GetMaxPlanetTick()
    {
        return 30;
    }

    public static DecisionAnswer ParseDecisionAnswer(string[] _answer)
    {
        string planetaryId = "";
        bool vote = false;
        string playerId = "";

        for (int i = 1; i < _answer.Length; i++)
        {
            if (_answer[i].Contains("pId:"))
            {
                planetaryId = _answer[i];
                planetaryId = planetaryId.Replace("pId:", "");
                planetaryId = planetaryId.Replace("\r\n", "");
  
            }
            else if (_answer[i].Contains("plId:"))
            {
                playerId = _answer[i];
                playerId = playerId.Replace("plId:", "");
                playerId = playerId.Replace("\r\n", "");
                
            }
            else if (_answer[i].Contains("v:"))
            {
                if (_answer[i].Contains("t"))
                {
                    vote = true;
                }
                //else its false as stated above
            }
        }

        //after parse the dicision broadcast to all players
        // playerId is head has voted yes/no is body
        Notification notifacation = new Notification("Decision in Planet " + planetaryId, " [" + vote + "] ", playerId + " has voted ");
        Program.BroadcastMeseage(notifacation.ToMessageString());




        //for self test
        //Console.WriteLine("**notification is: " + notifacation.ToMessageString());
        //Program.BroadcastMeseage("get new decision");
        //Console.WriteLine("**Decision Answer is : " + vote);
        return new DecisionAnswer(planetaryId, playerId, vote);
    }

    public static Vector2 ParseLocation(string[] _location)
    {
        /*
        *      ***location
        *      plId:<id>
        *      x:<x>
        *      y:<y>
       */

        float x;
        float y;
        string plId = "";

        for (int i = 0; i < _location.Length; i++)
        {
            

            if (_location[i].Contains("plId:"))
            {
                _location[i] = _location[i].Replace("plId:", "");
                _location[i] = _location[i].Replace("\r\n", "");
                plId = _location[i];
            }
            else if (_location[i].Contains("x:"))
            {
                _location[i] = _location[i].Replace("x:", "");
                _location[i] = _location[i].Replace("\r\n", "");
                x = float.Parse(_location[i]);
            }
            else if (_location[i].Contains("y:"))
            {
                _location[i] = _location[i].Replace("y:", "");
                _location[i] = _location[i].Replace("\r\n", "");
                y = float.Parse(_location[i]);
            }
        }

        //notify players
        Notification notifacation = new Notification(plId, "  add to the planet automatically.", "Player ");
        Program.BroadcastMeseage(notifacation.ToMessageString());
        return null;
    }

    public static Notification ParseNotification(string[] _notific) {

        string head = "";
        string body = "";
        string planetId = "";

        for (int i = 0; i < _notific.Length; i++) {
            if (_notific[i].Contains("pId:"))
            {
                planetId = _notific[i];
                planetId.Replace("pId:", "");
                planetId.Replace("\r\n", "");
            }

            if (_notific[i].Contains("h:"))
            {
                head = _notific[i];
                head.Replace("h:", "");
                head.Replace("\r\n", "");
            }

            if (_notific[i].Contains("b:"))
            {
                body = _notific[i];
                body.Replace("b:", "");
                body.Replace("\r\n", "");
            }
        }

        return new Notification(head, body, planetId);
    }
}