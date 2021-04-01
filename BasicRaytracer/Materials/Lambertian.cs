using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRaytracer.Materials
{
    public class Lambertian : Material
    {
        private Vec _albedo;

        public Lambertian(Vec a)
        {
            _albedo = a;
        }

        public override bool Scatter(Ray rIn, ref HitRecord rec, ref Vec attenuation, ref Ray scattered)
        {
            //Vec scatterDirection = rec.Normal + new Vec().RandomInUnitSphere(); // Diffuse using approached Lambertian
            //Vec scatterDirection = rec.Normal + new Vec().RandomUnitVector(); // Diffuse using True Lambertian
            Vec scatterDirection = new Vec().RandomInHemisphere(rec.Normal); // Diffuse using intuitive Lambertian

            if (scatterDirection.NearZero()) // avoid scatterDirection cancelling with rec.normal
                scatterDirection = rec.Normal;

            scattered = new Ray(rec.P, scatterDirection);
            attenuation = _albedo;
            return true;
        }
    }
}
