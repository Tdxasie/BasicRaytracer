using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRaytracer.Materials
{
    public abstract class Material
    {
        public virtual bool Scatter(Ray rIn, ref HitRecord rec, ref Vec attenuation, ref Ray scattered) => false;
    }
}
