using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class NumberExtensions
    {
        public static int RoundOff(this int i)
        {
            return ((int)Math.Round(i / 100.0)) * 100;
        }
        public static int RoundOff(this double i)
        {
            return ((int)Math.Round(i / 10.0)) * 10;
        }
    }
}
