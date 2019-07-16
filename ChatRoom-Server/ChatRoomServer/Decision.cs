using ChatRoomServer;
using System;

public class Decision
{
    private int votesYes;
    private int votesNo;
    private string description = "uninitialized Decision.";
    private PlanetaryEffect effect;
    private string planetId;
    public int remainingTicks;

    public Decision(PlanetaryEffect _effect, string _description)
    {
        votesYes = 0;
        votesNo = 0;

        effect = _effect;
        description = _description;
        remainingTicks = 60000;
    }

    public void VoteYes()
    {
        votesYes++;
    }

    public void VoteNo()
    {
        votesNo++;
    }

    public int GetVotesYes()
    {
        return votesYes;
    }

    public int GetVotesNo()
    {
        return votesNo;
    }

    public bool GetResult()
    {
        if (votesNo == votesYes)
        {
            return UTIL.GetRandomBool();
        }
        else
        {
            return votesYes > votesNo;
        }
    }

    public string GetDescription()
    {
        return description;
    }

    public PlanetaryEffect GetEffect()
    {
        return effect;
    }

    public string ToMessageString()
    {
        return "***decision\r\n"
            + "pId:" + getPlanetId() + "\r\n"
            + "description:" + description;
    }

    public string getPlanetId()
    {
        return Program.getPlanets()[0].GetId();
    }
}