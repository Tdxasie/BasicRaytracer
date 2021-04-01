using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRaytracer
{
    class Camera
    {
        public Vec Origin { get; set; }
        public Vec LowerLeftCorner { get; set; }
        public Vec Horizontal { get; set; }
        public Vec Vertical { get; set; }

        public Camera(double vfov, double aspectRatio)
        {
            double theta = Utilities.DegreesToRadians(vfov);
            double h = Math.Tan(theta / 2);
            double viewportHeight = 2.0 * h;
            double viewportWidth = aspectRatio * viewportHeight;
            
            double focalLength = 1.0;

            Origin = new Vec(0, 0, 0);
            Horizontal = new Vec(viewportWidth, 0, 0);
            Vertical = new Vec(0, viewportHeight, 0);

            LowerLeftCorner = Origin - Horizontal / 2 - Vertical / 2 - new Vec(0, 0, focalLength);
        }

        public Ray GetRay(double u, double v) => new Ray(Origin, LowerLeftCorner + (u * Horizontal) + (v * Vertical) - Origin);
    }
}
