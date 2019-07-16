using System;
using System.Collections.Generic;

public class DecisionPool
{

    private static int ticks, deltaSeaLevel, deltaOxygen, deltaTemperature,
            deltaSulphur, deltaSugar, deltaLipids, deltaAminoAcids, deltaCarbon, deltaWater,
         deltaSingleCellCount, deltaMultiCellCount, deltaAdvancedCount, deltaPlantCount;

    //TODO: balancing

    public static Decision GetDecision(Planet _planet)
    {
        Random random = new Random();

        List<Decision> possibleDecisions = new List<Decision>();

        possibleDecisions.Add(nextDecision());

        if (_planet.singleCellCount >= 10)
        {
            possibleDecisions.Add(nextCellDecision());
        }
        if (_planet.multiCellCount >= 10)
        {
            possibleDecisions.Add(nextMultiCellDecision());
        }
        if (_planet.plantCount >= 10)
        {
            possibleDecisions.Add(nextPlantDecision());
        }
        if (_planet.advancedCount >= 10)
        {
            possibleDecisions.Add(nextAdvancedDecision());
        }

        return possibleDecisions[random.Next(0, possibleDecisions.Count)];
    }

    //general
    private static Decision nextDecision()
    {
        ticks = deltaSeaLevel = deltaOxygen = deltaTemperature = deltaSulphur = deltaSugar
            = deltaLipids = deltaAminoAcids = deltaCarbon = deltaWater = deltaSingleCellCount
            = deltaMultiCellCount = deltaAdvancedCount = deltaPlantCount = 0;
        string descr = "";

        Random random = new Random();
        int select = random.Next(0, 5);
        switch (select)
        {
            case 0:
                descr = "meteorite shower";
                deltaWater = 100;
                deltaTemperature = -2;  //cloud -> temp down; more water -> temp down
                deltaWater = 2;
                deltaSeaLevel = 1;
                ticks = random.Next(0, 10);
                break;
            case 1:
                descr = "volcanic eruption";
                deltaTemperature = 4;
                deltaSeaLevel = 1;
                deltaSulphur = 2;
                deltaWater = 2;
                deltaCarbon = 10;
                deltaAdvancedCount = -50;
                deltaMultiCellCount = -100;
                deltaPlantCount = -1000;
                deltaSingleCellCount = -10000;
                break;
            case 2:
                descr = "decreased sun activity";
                deltaTemperature = -4;
                deltaSeaLevel = 1;
                deltaWater = 1;
                deltaPlantCount = -100;
                ticks = random.Next(0, 10);
                break;
            case 3:
                descr = "increased sun activity";
                deltaTemperature = 4;
                deltaSeaLevel = 1;
                deltaWater = -1;
                deltaPlantCount = 100;
                ticks = random.Next(0, 10);
                break;
            case 4:
                descr = "asteroid impact";
                deltaSeaLevel = 2;
                deltaWater = random.Next(50, 100);
                deltaTemperature = 20;
                deltaCarbon = 500;
                deltaWater = 1;
                deltaSingleCellCount = random.Next(0, 2);
                break;
        }

        PlanetaryEffect eff = new PlanetaryEffect(ticks, deltaSeaLevel, deltaOxygen, deltaTemperature,
            deltaSulphur, deltaSugar, deltaLipids, deltaAminoAcids, deltaCarbon, deltaWater,
         deltaSingleCellCount, deltaMultiCellCount, deltaAdvancedCount, deltaPlantCount);

        Decision dec = new Decision(eff, descr);

        return dec;
    }


    //cell
    private static Decision nextCellDecision()
    {
        ticks = deltaSeaLevel = deltaOxygen = deltaTemperature = deltaSulphur = deltaSugar
            = deltaLipids = deltaAminoAcids = deltaCarbon = deltaWater = deltaSingleCellCount
            = deltaMultiCellCount = deltaAdvancedCount = deltaPlantCount = 0;
        string descr = "";

        Random random = new Random();
        int select = random.Next(0, 3);
        switch (select)
        {
            case 0:
                descr = "algal bloom";
                deltaOxygen = 100;
                deltaSingleCellCount = 4;
                ticks = random.Next(0, 5);
                break;
            case 1:
                descr = "simple cell colony";
                deltaMultiCellCount = 5;
                break;
            case 2:
                descr = "develop photosynthesis";
                deltaPlantCount = 5;
                break;
            case 3:
                descr = "";
                break;
        }

        PlanetaryEffect eff = new PlanetaryEffect(ticks, deltaSeaLevel, deltaOxygen, deltaTemperature,
            deltaSulphur, deltaSugar, deltaLipids, deltaAminoAcids, deltaCarbon, deltaWater,
         deltaSingleCellCount, deltaMultiCellCount, deltaAdvancedCount, deltaPlantCount);

        Decision dec = new Decision(eff, descr);

        return dec;
    }


    //plants
    private static Decision nextPlantDecision()
    {
        ticks = deltaSeaLevel = deltaOxygen = deltaTemperature = deltaSulphur = deltaSugar
            = deltaLipids = deltaAminoAcids = deltaCarbon = deltaWater = deltaSingleCellCount
            = deltaMultiCellCount = deltaAdvancedCount = deltaPlantCount = 0;
        string descr = "";

        Random random = new Random();
        int select = random.Next(0, 1);
        switch (select)
        {
            case 0:
                descr = "wildfire";
                deltaTemperature = 1;
                deltaCarbon = 5;
                deltaPlantCount = -2;
                ticks = random.Next(0, 5);
                break;
            case 1:
                descr = "";
                break;
            case 2:
                descr = "";
                break;
            case 3:
                descr = "";
                break;
        }

        PlanetaryEffect eff = new PlanetaryEffect(ticks, deltaSeaLevel, deltaOxygen, deltaTemperature,
            deltaSulphur, deltaSugar, deltaLipids, deltaAminoAcids, deltaCarbon, deltaWater,
         deltaSingleCellCount, deltaMultiCellCount, deltaAdvancedCount, deltaPlantCount);

        Decision dec = new Decision(eff, descr);

        return dec;
    }

    //multi Celled
    private static Decision nextMultiCellDecision()
    {
        ticks = deltaSeaLevel = deltaOxygen = deltaTemperature = deltaSulphur = deltaSugar
            = deltaLipids = deltaAminoAcids = deltaCarbon = deltaWater = deltaSingleCellCount
            = deltaMultiCellCount = deltaAdvancedCount = deltaPlantCount = 0;
        string descr = "";

        Random random = new Random();
        int select = random.Next(0, 1);
        switch (select)
        {
            case 0:
                descr = "advance evolution";
                deltaAdvancedCount = 5;
                break;
            case 1:
                descr = "";
                break;
            case 2:
                descr = "";
                break;
            case 3:
                descr = "";
                break;
        }

        PlanetaryEffect eff = new PlanetaryEffect(ticks, deltaSeaLevel, deltaOxygen, deltaTemperature,
            deltaSulphur, deltaSugar, deltaLipids, deltaAminoAcids, deltaCarbon, deltaWater,
         deltaSingleCellCount, deltaMultiCellCount, deltaAdvancedCount, deltaPlantCount);

        Decision dec = new Decision(eff, descr);

        return dec;
    }

    //advanced
    private static Decision nextAdvancedDecision()
    {
        ticks = deltaSeaLevel = deltaOxygen = deltaTemperature = deltaSulphur = deltaSugar
            = deltaLipids = deltaAminoAcids = deltaCarbon = deltaWater = deltaSingleCellCount
            = deltaMultiCellCount = deltaAdvancedCount = deltaPlantCount = 0;
        string descr = "";

        Random random = new Random();
        int select = random.Next(0, 1);
        switch (select)
        {
            case 0:
                descr = "";
                break;
            case 1:
                descr = "";
                break;
            case 2:
                descr = "";
                break;
            case 3:
                descr = "";
                break;
        }

        PlanetaryEffect eff = new PlanetaryEffect(ticks, deltaSeaLevel, deltaOxygen, deltaTemperature,
            deltaSulphur, deltaSugar, deltaLipids, deltaAminoAcids, deltaCarbon, deltaWater,
         deltaSingleCellCount, deltaMultiCellCount, deltaAdvancedCount, deltaPlantCount);

        Decision dec = new Decision(eff, descr);

        return dec;
    }

}