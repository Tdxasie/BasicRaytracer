using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRaytracer.Materials
{
    public class Metal : Material
    {
        private Vec _albedo;
        public Metal(Vec a)
        {
            _albedo = a;
        }

        public override bool Scatter(Ray rIn, ref HitRecord rec, ref Vec attenuation, ref Ray scattered)
        {
            Vec reflected = rIn.Direction.Normalize().Reflect(rec.Normal);
            scattered = new Ray(rec.P, reflected);
            attenuation = _albedo;
            return (scattered.Direction.Dot(rec.Normal) > 0);
        }
    }
}
