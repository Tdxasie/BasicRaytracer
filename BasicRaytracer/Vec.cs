using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRaytracer
{
    public class Vec
    {
        public double X;
        public double Y;
        public double Z;
        public double R { get => X; set => X = value; }
        public double G { get => Y; set => Y = value; }
        public double B { get => Z; set => Z = value; }
        public Vec() { }
        public Vec(double v1, double v2, double v3)
        {
            X = v1;
            Y = v2;
            Z = v3;
        }
        public static Vec operator -(Vec v) => new Vec(-v.X, -v.Y, -v.Z);
        public static Vec operator +(Vec a, Vec b) => new Vec(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vec operator -(Vec a, Vec b) => new Vec(a.X + -b.X, a.Y + -b.Y, a.Z + -b.Z);
        public static Vec operator *(Vec v, double d) => new Vec(v.X * d, v.Y * d, v.Z * d);
        public static Vec operator *(double d, Vec v) => v * d;
        public static Vec operator *(Vec a, Vec b) => new Vec(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Vec operator /(Vec v, double d) => new Vec(v.X / d, v.Y / d, v.Z / d);
        public double Dot(Vec v) => X * v.X + Y * v.Y + Z * v.Z;
        public Vec Cross(Vec v) => new Vec( Y * v.Z + Z * v.Y,
                                            Z * v.X + X * v.Z,
                                            X * v.Y + Y * v.X);
        public double Norm() => Math.Sqrt(X * X + Y * Y + Z * Z);
        public double NormSquared() => (X * X + Y * Y + Z * Z);
        public void Print() => Console.WriteLine($"v1={X} v2={Y} v3={Z}");

        public Vec Normalize()
        {
            double norm = this.Norm();
            return new Vec(X / norm, Y / norm, Z / norm);
        }

        public Vec Random(double min, double max) => new Vec(RNG.RandomDouble(min, max), RNG.RandomDouble(min, max), RNG.RandomDouble(min, max));

        public Vec RandomInUnitSphere()
        {
            Vec p;
            while (true)
            {
                p = Random(-1, 1);
                if (p.NormSquared() >= 1) continue;
                return p;
            }
        }

        public Vec RandomUnitVector() => RandomInUnitSphere().Normalize(); // true lambertian reflection

        public Vec RandomInHemisphere(Vec normal)
        {
            Vec inUnitSphere = RandomInUnitSphere();
            if (inUnitSphere.Dot(normal) > 0.0) // in the same hemisphere as the normal
                return inUnitSphere;
            else
                return -inUnitSphere;
            
        }

        public bool NearZero() // test if vector has very close to zero values
        {
            double s = 1e-8;
            return (Math.Abs(X) < s) && (Math.Abs(Y) < s) && (Math.Abs(Z) < s);
        }

        public Vec Reflect(Vec n) => this - 2 * this.Dot(n) * n;


    }
}
