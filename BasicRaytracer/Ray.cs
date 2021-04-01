using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRaytracer
{
    public class Ray
    {
        public Vec Origin { get; set; }
        public Vec Direction { get; set; }

        public Ray() { }
        public Ray(Vec origin, Vec direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vec PointAtParameter(double t) => Origin + t * Direction;

    }
}
