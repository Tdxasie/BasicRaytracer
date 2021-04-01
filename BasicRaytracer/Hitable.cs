using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicRaytracer.Materials;

namespace BasicRaytracer
{
    public struct HitRecord
    {
        public double t { get; set; }
        public Vec P { get; set; }
        public Vec Normal { get; set; }

        public bool FrontFace;

        public Material Material;

        public void SetFaceNormal(Ray r, Vec outwardNormal)
        {
            FrontFace = r.Direction.Dot(outwardNormal) < 0;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
        }
    }
    public abstract class Hitable
    {
        public virtual bool Hit(Ray r, double tmin, double tmax, ref HitRecord rec) => false;
    }

    public class HitableList : Hitable
    {
        private List<Hitable> _list;
        public HitableList()
        {
            _list = new List<Hitable>();
        }
        public HitableList(List<Hitable> l)
        {
            _list = l;
        }

        public override bool Hit(Ray r, double tmin, double tmax, ref HitRecord rec)
        {
            HitRecord temp_rec = new HitRecord();
            bool hitAnything = false;
            double closestSoFar = tmax;

            for (int i = 0; i < _list.Count; i++)
            {
                if(_list[i].Hit(r, tmin, closestSoFar, ref temp_rec))
                {
                    hitAnything = true;
                    closestSoFar = temp_rec.t;
                    rec = temp_rec;
                }
            }
            return hitAnything;
        }

        public void Add(Hitable h)
        {
            _list.Add(h);
        }
    }
}
