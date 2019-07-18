using System.Collections.Generic;
using System;

public class Planet
{
    private List<Player> players;
    private List<PlanetaryEffect> effects;
    private Decision currentDecision;
    private int pings;
    private int ticks;
    private string id;

    public int oxygen;				//changed by decissions
    public int sealvl;              //changed by decissions
    public int temperature;     //changed by decissions
    public int sulphur;
    public int sugar;
    public int lipids;
    public int aminoAcids;
    public int carbon;
    public int water;
    public int singleCellCount; //increase of this requires specific amount of sealvl and temperature (+ time)
    public int multiCellCount;      //increase of this reduces singleCellCount, requires more temperature than singleCells (++ time), while alive regularly reduces singleCellCount
    public int advancedCount;       //increase of this reduces oxygen, multiCellCount (+++ time), while alive regularly reduces multiCellCount and plantCount
    public int plantCount;          //increase of this reduces advanceCount (+++ time)

    public Planet()
    {
        oxygen = 0;
        sealvl = 0;
        temperature = 1000;
        sulphur = 100000;
        sugar = 0;
        lipids = 0;
        aminoAcids = 0;
        carbon = 10000;
        water = 0;
        singleCellCount = 0;
        multiCellCount = 0;
        advancedCount = 0;
        plantCount = 0;
        id = "testForTheOnlyPlanet";
        Init();
    }

	//private void animateLogic()
	//{
	//	// assumptions by paul:
	//	//		temperature around normal celcius
	//	//		sealvl from 0 to 100 (50 normal ?)
	//	//		oxygen from 0 to 100 (also 50 normal ?)
	//	Random r = new Random();
	//	double amountFailing = 0;
	//	if (temperature < 0 || temperature > 40)
	//	{
	//		amountFailing++;
	//		if (temperature < -30 || temperature > 70)
	//			amountFailing++;
	//	}
	//	if (sealvl < 35 || sealvl > 65)
	//	{
	//		amountFailing++;
	//		if (sealvl < 15 ||sealvl > 85)
	//			amountFailing++;
	//	}
	//	amountFailing = (amountFailing -0.35) + r.nextDouble()/2d;
	//	//try to keep singleCellCount somewhat capped (if too many, around 12.5 % will die)
	//	double newSingleCellCount = singleCellCount * (1 + ((1 + r.nextDouble() * 2) - amountFailing)/100d);
	//	if(newSingleCellCount > 20000 && newSingleCellCount > singleCellCount)
	//		newSingleCellCount = singleCellCount * 1.01+(1/Math.exp(singleCellCount));
	//	if(newSingleCellCount > 100000 && r.nextDouble() > 0.95)
	//		newSingleCellCount = (long)(newSingleCellCount * 0.875);
	//	if(newSingleCellCount < 2)
	//	{
	//		newSingleCellCount = r.nextDouble() > 0.3 ? 2+((r.nextDouble() > 0.3) ? 4 : 1) : newSingleCellCount;
	//	}
	//	singleCellCount = ((newSingleCellCount - singleCellCount) > 0.05 && (newSingleCellCount - singleCellCount) < 1 ) ? singleCellCount + 1 : (long)newSingleCellCount;
	//	int newMultiCellCount = ((temperature > 10 && temperature < 30) && r.nextDouble() > 0.95 && singleCellCount > 100) ? (multiCellCount + (int)(singleCellCount>10000 ? (int)(singleCellCount/1000) : 1)) : ((temperature < 10 || temperature > 30 || singleCellCount < 50 * multiCellCount) && r.nextDouble() > 0.35 ? Math.max(0, multiCellCount-1) : multiCellCount);
	//	if (newMultiCellCount > multiCellCount)
	//	{
	//		singleCellCount -= 20 * (newMultiCellCount - multiCellCount);
	//		singleCellCount = Math.max(singleCellCount, 0);
	//	}
	//	singleCellCount -= (int)(newMultiCellCount/5);
	//	singleCellCount = Math.max(singleCellCount, 0);
	//	multiCellCount = newMultiCellCount;
	//	//TODO plants and advanced
	//	//status: the single and multi are looking good for me (tried for loops until 1000 with different conditions), so i think it would be good to call this function maybe randomized 10-30 times per hour?
	//}

    //include seperate classes of life that work like planetary effects over several ticks
    //class also stores number of subjects
    //each tick subjects die and are born
    //birth and death rate is determined by resources
    //stores needs of species/type
    //single celled animals
    //single celled plants
    //...
    //intelligent life
    //destruction

    public void Init()
    {
        effects = new List<PlanetaryEffect>();
        effects.Add(new AdvancedEffects.AdvancedEffect());
        effects.Add(new AdvancedEffects.PlantEffect());
        effects.Add(new AdvancedEffects.MultiCelledEffect());
        effects.Add(new AdvancedEffects.SingleCelledEffect());
        players = new List<Player>();
    }

    public void Ping()
    {
        if (pings == UTIL.GetMaxPlanetPing())
        {
            pings = 0;
            Tick();
            System.Threading.Thread.Sleep(20);

        }
        else
        {
            pings++;
        }
    }

    private void Tick()
    {
        if (currentDecision != null)
        {
            if (!(currentDecision.GetVotesYes() + currentDecision.GetVotesNo() < players.Count))
            {
                if (currentDecision.GetVotesYes() > currentDecision.GetVotesNo())
                {

                    effects.Add(currentDecision.GetEffect());
                    this.MapDecisionToPlanetStats(effects[0]);
                }


                currentDecision = null;
            }
            else if (currentDecision.remainingTicks != 0)
            {
                currentDecision.remainingTicks--;
            }
            else
            {
                if (currentDecision.GetVotesYes() > currentDecision.GetVotesNo())
                {

                    effects.Add(currentDecision.GetEffect());
                    this.MapDecisionToPlanetStats(effects[0]);
                }


                currentDecision = null;
            }
        }

        if (ticks == UTIL.GetMaxPlanetTick())
        {
            ticks = 0;
            currentDecision = DecisionPool.GetDecision(this);
            
            SendDecision(currentDecision);
        }
        else
        {
            ticks++;

            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Do(this);
                if (effects[i].GetTicks() - 1000 == 0)
                {
                    effects.Remove(effects[i]);
                    i--;
                }
            }
        }

        foreach (Player p in players)
        {
            p.GetClient().SendMessage(ToMessageString());
        }
    }

    private void SendDecision(Decision _decision)
    {
        //decision to display message via location is made by the client
        foreach (Player p in players)
        {
            if(p.GetClient() != null)
            {
                
                p.GetClient().SendMessage(_decision.ToMessageString());
                //Console.WriteLine(_decision.ToMessageString());
            }
                
        }
    }

    private void SendNotification(Notification _notification)
    {
        foreach (Player p in players)
        {
            if(p.GetClient() != null)
                p.GetClient().SendMessage(_notification.ToMessageString());
        }
    }

    public string GetId()
    {
        return id;
    }

    public void AddVote(bool _vote)
    {
        if (_vote)
        {
            currentDecision.VoteYes();
        }
        else
        {
            currentDecision.VoteNo();
        }
    }

    public string ToMessageString()
    {

        return "***planet\r\n"
        + "ox:" + oxygen + "\r\n"
        + "sl:" + sealvl + "\r\n"
        + "tmp:" + temperature + "\r\n"
        + "sul:" + sulphur + "\r\n"
        + "sug:" + sugar + "\r\n"
        + "lp:" +  lipids + "\r\n"
        + "aa:" + aminoAcids + "\r\n"
        + "carbon:" + carbon + "\r\n"
        + "water:" + water + "\r\n"
        + "sc:" + singleCellCount + "\r\n"
        + "mc:" + multiCellCount + "\r\n"
        + "ac:" + advancedCount + "\r\n"
        + "pc:" + plantCount + "\r\n";
    }

    public void AddPlayer(Player _player)
    {
        if (players == null)
            players = new List<Player>();

        players.Add(_player);
    }

    void MapDecisionToPlanetStats(PlanetaryEffect effect)
    {
        effect.Do(this);

    }

    public void StatsRegulation()
    {
        if (oxygen < 0)
            oxygen = 0;
        if (sealvl < 0)
            sealvl = 0;
        if (temperature < 0)
            temperature = 0;
        if (sulphur < 0)
            sulphur = 0;
        if (sugar < 0)
            sugar = 0;
        if (lipids < 0)
            lipids = 0;
        if (aminoAcids < 0)
            aminoAcids = 0;
        if (carbon < 0)
            carbon = 0;
        if (water < 0)
            water = 0;
        if (singleCellCount < 0)
            singleCellCount = 0;
        if (multiCellCount < 0)
            multiCellCount = 0;
        if (advancedCount < 0)
            advancedCount = 0;
        if (plantCount < 0)
            plantCount = 0;
    }

}