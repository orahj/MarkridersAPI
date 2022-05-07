using System;

namespace MarkriderAPI.Extensions
{
    public static class NumberExtension
    {
        public static int RoundOff(this int i)
        {
            return ((int)Math.Round(i / 10.0)) * 10;
        }
        public static int RoundOff(this double i)
        {
            return ((int)Math.Round(i / 10.0)) * 10;
        }
    }
}
