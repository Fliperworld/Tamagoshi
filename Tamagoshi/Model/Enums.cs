using System;
using Tamagoshi.ApiPokemon;

namespace Tamagoshi.Model
{
    public enum EGrowthRate
    {
        Undefined,
        Slow,
        Medium,
        Fast,
        MediumSlow,
        SlowThenVeryFast,
        FastThenVerySlow
    }

    public enum EHabitat
    {
        Undefined,
        Cave,
        Forest,
        GrassLand,
        Mountain,
        Rare,
        RoughTerrain,
        Sea,
        Urban,
        WatersEdge
    }

    internal static class Enums
    {
        public static EGrowthRate GetGrowthRate(NameURL nu)
        {
            EGrowthRate ret = EGrowthRate.Medium;
            if(nu == null)
                return ret;
            Enum.TryParse(nu.name.Replace("-",string.Empty),true, out ret);
            return ret;
        }

        public static EHabitat GetHabitat(NameURL nu)
        {
            EHabitat ret = EHabitat.Urban;
            if (nu == null)
                return ret;
            Enum.TryParse(nu.name.Replace("-", string.Empty), true, out ret);
            return ret;
        }
    }

}
