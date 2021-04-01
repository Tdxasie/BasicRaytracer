using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRaytracer
{
    static class RNG
    {
        public static Random val = new Random();
        public static double RandomDouble(double min, double max) => min + val.NextDouble() * (max - min);
    }
}
