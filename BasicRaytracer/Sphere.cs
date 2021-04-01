using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicRaytracer.Materials;

namespace BasicRaytracer
{
    public class Sphere : Hitable
    {
        private Vec _center;
        private double _radius;
        private Material _mat;
        public Sphere(Vec center, double radius, Material material)
        {
            _center = center;
            _radius = radius;
            _mat = material;
        }

        public override bool Hit(Ray r, double tmin, double tmax, ref HitRecord rec)
        {
            Vec oc = r.Origin - _center;
            double a = r.Direction.Dot(r.Direction);
            double b = oc.Dot(r.Direction);
            double c = oc.Dot(oc) - _radius * _radius;
            double discriminant = b * b - a * c;

            if (discriminant < 0) return false;

            double sqrtd = Math.Sqrt(discriminant);

            // Find the nearest root that lies in the acceptable range.
            double root = (-b - sqrtd) / a;
            if (root < tmin || tmax < root)
            {
                root = (-b + sqrtd) / a;
                if (root < tmin || tmax < root)
                    return false;
            }

            rec.Material = _mat;

            rec.t = root;
            rec.P = r.PointAtParameter(rec.t);

            Vec outwardNormal = (rec.P - _center) / _radius;
            rec.SetFaceNormal(r, outwardNormal);

            return true;
        }
    }
}
