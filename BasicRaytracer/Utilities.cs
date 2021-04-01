using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRaytracer
{
    public static class Utilities
    {
        public static double DegreesToRadians(double degrees) => (Math.PI / 180.0) * degrees;

        public static double Clamp(double x, double min, double max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }
    }
}
