using System;
public class PlanetaryEffect
{
    private int ticks; //number of ticks this effect lasts. 0 := only once when activated
    private int deltaSeaLevel;
    private int deltaOxygen;
    private int deltaTemperature;
    private int deltaSulphur;
    private int deltaSugar;
    private int deltaLipids;
    private int deltaAminoAcids;
    private int deltaCarbon;
    private int deltaWater;
    private int deltaSingleCellCount; 
    private int deltaMultiCellCount;  
    private int deltaAdvancedCount; 
    private int deltaPlantCount;

    public PlanetaryEffect(int _ticks, int _deltaSeaLevel, int _deltaOxygen, int _deltaTemperature, int _deltaSulphur, int _deltaSugar, int _deltaLipids, int _deltaAminoAcids, int _deltaCarbon, int _deltaWater,
        int _deltaSingleCellCount, int _deltaMultiCellCount, int _deltaAdvancedCount, int _deltaPlantCount)
    {
        ticks = _ticks;
        deltaSeaLevel = _deltaSeaLevel;
        deltaOxygen = _deltaOxygen;
        deltaTemperature = _deltaTemperature;
        deltaSulphur = _deltaSulphur;
        deltaSugar = _deltaSugar;
        deltaLipids = _deltaLipids;
        deltaAminoAcids = _deltaAminoAcids;
        deltaCarbon = _deltaCarbon;
        deltaWater = _deltaWater;
        deltaSingleCellCount = _deltaSingleCellCount;
        deltaMultiCellCount = _deltaMultiCellCount;
        deltaAdvancedCount = _deltaAdvancedCount;
        deltaPlantCount = _deltaPlantCount;
    }

    public virtual void Do(Planet _planet)
    {
        if(ticks != -1)
            ticks--;

        _planet.advancedCount += deltaAdvancedCount;
        _planet.aminoAcids += deltaAminoAcids;
        _planet.carbon += deltaCarbon;
        _planet.lipids += deltaLipids;
        _planet.oxygen += deltaOxygen;
        _planet.plantCount += deltaPlantCount;
        _planet.sealvl += deltaSeaLevel;
        _planet.singleCellCount += deltaSingleCellCount;
        _planet.sugar += deltaSugar;
        _planet.sulphur += deltaSulphur;
        _planet.temperature += deltaTemperature;
        _planet.water += deltaWater;
    }

    public int GetTicks()
    {
        return ticks;
    }
}

namespace AdvancedEffects
{
    class SingleCelledEffect : PlanetaryEffect
    {
        public SingleCelledEffect() : base (0 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        {
            
        }

        public override void Do(Planet _planet)
        {
            base.Do(_planet);

            int counter = _planet.singleCellCount;

            //creating new single celled organisms
            while (_planet.carbon > 100 && _planet.lipids > 200 && _planet.aminoAcids > 200 && _planet.sulphur > 20 && _planet.water > 1000 && _planet.sealvl > 0 && counter > 0)
            {
                _planet.singleCellCount++;
                _planet.carbon -= 5;
                _planet.lipids -= 10;
                _planet.aminoAcids -= 10;
                _planet.sulphur -= 1;
                counter--;
            }

            //letting one die off
            if(_planet.singleCellCount > 0)
                _planet.singleCellCount--;

            if (_planet.water == 0)
                _planet.singleCellCount /= 2;
        }
    }

    class MultiCelledEffect : PlanetaryEffect
    {
        public MultiCelledEffect() : base(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public override void Do(Planet _planet)
        {
            base.Do(_planet);

            int counter = _planet.multiCellCount - _planet.multiCellCount / 2; //this calculation rounds up

            //creating new multi celled organisms
            while (_planet.singleCellCount > 100 && _planet.oxygen > 100 && _planet.water > 1000 && counter > 0)
            {
                _planet.multiCellCount++;
                _planet.oxygen -= 5;
                _planet.water -= 50;
                _planet.singleCellCount--;
                counter--;
            }

            //letting one die off
            if(_planet.multiCellCount > 0)
                _planet.multiCellCount--;

            if (_planet.water == 0)
                _planet.multiCellCount /= 3;
        }
    }

    class PlantEffect : PlanetaryEffect
    {
        public PlantEffect() : base(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public override void Do(Planet _planet)
        {
            base.Do(_planet);

            int counter = _planet.plantCount;

            //creating new single celled organisms
            while (_planet.carbon > 1000  && _planet.water > 1000 && _planet.sealvl > 0 && counter > 0)
            {
                _planet.plantCount++;
                _planet.carbon -= 5;
                counter--;
            }

            _planet.oxygen += _planet.plantCount * 5;

            //letting one die off
            if(_planet.plantCount > 0)
                _planet.plantCount--;

            if (_planet.water == 0)
                _planet.plantCount /= 2;
        }
    }

    class AdvancedEffect : PlanetaryEffect
    {
        public AdvancedEffect() : base(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public override void Do(Planet _planet)
        {
            base.Do(_planet);

            int counter = _planet.advancedCount - _planet.advancedCount / 2; //this calculation rounds up

            //creating new multi celled organisms
            while (_planet.multiCellCount > 100 && _planet.plantCount > 200 && _planet.oxygen > 1000 && _planet.water > 10000 && counter > 0)
            {
                _planet.advancedCount++;
                _planet.oxygen -= 5;
                _planet.water -= 50;
                _planet.multiCellCount--;
                _planet.plantCount -= 2;
                counter--;
            }

            //letting one die off
            if(_planet.advancedCount > 0)
                _planet.advancedCount -= 2;
    
            if (_planet.water == 0)
                _planet.advancedCount /= 4;
        }
    }
}