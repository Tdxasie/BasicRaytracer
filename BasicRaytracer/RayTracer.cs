using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicRaytracer.Materials;

namespace BasicRaytracer
{
    public class RayTracer
    {
        private int _imageWidth;
        private int _imageHeight;
        private double _aspectRatio;
        private Bitmap _map;
        public RayTracer(int width, int height, double aspectRatio)
        {
            _imageWidth = width;
            _imageHeight = height;
            _aspectRatio = aspectRatio;

            _map = new Bitmap(_imageWidth, _imageHeight);
        }

        public Bitmap Render()
        {
            int nbSamples = 100;
            int maxDepth = 10;

            HitableList world = new HitableList();

            var groundMaterial = new Lambertian(new Vec(0.8, 0.8, 0.8));
            var pinkMaterial = new Lambertian(new Vec(1, 0, 0.57));
            var redMaterial = new Metal(new Vec(0.8, 0.3, 0.3));
            var mirrorMaterial = new Metal(new Vec(0.8, 0.8, 0.8));
            var blueMaterial = new Metal(new Vec(0.3, 0.7, 1));
            var greenMaterial = new Metal(new Vec(0.3, 0.7, 0.3));

            world.Add(new Sphere(new Vec( 0.0, -100.5, -1.0), 100.0, groundMaterial)); // ground
            world.Add(new Sphere(new Vec( 0.0,   -0.0, -1.5),   0.5, mirrorMaterial)); // center
            world.Add(new Sphere(new Vec(-1.0,   -0.0, -1.0),   0.5, redMaterial)); // left
            world.Add(new Sphere(new Vec( 1.0,   -0.2, -1.0),   0.3, blueMaterial)); // rsight
            world.Add(new Sphere(new Vec( 0.5,   -0.0, 10.5),    10, greenMaterial)); // back
            world.Add(new Sphere(new Vec( 0.3,   -0.4, -1.0),   0.1, pinkMaterial)); // back
            world.Add(new Sphere(new Vec( 0.4,   -0.4, -0.2),   0.1, pinkMaterial)); // back

            Camera cam = new Camera(90.0, _aspectRatio);

            for (int j = _imageHeight-1; j >=0; j--)
            {
                Console.WriteLine($"Computing line {_imageHeight - j} of {_imageHeight}");
                for (int i = 0; i < _imageWidth; i++)
                {
                    Vec col = new Vec(0, 0, 0);
                    for (int sample = 0; sample < nbSamples; sample++)
                    {
                        double u = (double)(i + RNG.val.NextDouble()) / (double)_imageWidth;
                        double v = (double)(j + RNG.val.NextDouble()) / (double)_imageHeight;

                        Ray r = cam.GetRay(u, v);
                        col += GetRayColor(r, ref world, maxDepth);
                    }
                    _map.SetPixel(i, _imageHeight-j-1, GetRGBColor(col, nbSamples));
                }
            }
            return _map;
        }

        public Vec GetRayColor(Ray r, ref HitableList world, int depth)
        {
            HitRecord rec = new HitRecord();

            if (depth <= 0) return new Vec(0, 0, 0); // no more color contribution

            if (world.Hit(r, 0.001, Double.MaxValue, ref rec))
            {
                Ray scattered = new Ray();
                Vec attenuation = new Vec();

                if (rec.Material.Scatter(r, ref rec, ref attenuation, ref scattered))
                {
                    return attenuation * GetRayColor(scattered, ref world, depth - 1);
                }
                return new Vec(0, 0, 0);
            }
            
            Vec dir = r.Direction.Normalize();
            double t = 0.5 * (dir.Y + 1.0);
            return (1.0 - t) * new Vec(1.0, 1.0, 1.0) + t * new Vec(0.5, 0.7, 1.0);
        }

        public Color GetRGBColor(Vec color, int nbSamples)
        {
            double scale = 1.0 / nbSamples;

            // Divide by number of samples and gamma correct with a value of 2 (raise to the power of 1/gamma) 
            color.R = Math.Sqrt(color.R * scale);
            color.G = Math.Sqrt(color.G * scale);
            color.B = Math.Sqrt(color.B * scale);

            int ir = (int)(255.99 * Utilities.Clamp(color.R, 0.0, 0.999));
            int ig = (int)(255.99 * Utilities.Clamp(color.G, 0.0, 0.999));
            int ib = (int)(255.99 * Utilities.Clamp(color.B, 0.0, 0.999));

            return Color.FromArgb(ir, ig, ib);
        }

        public Bitmap ColorWorld()
        {
            for (int i = 0; i < _imageWidth; i++)
            {
                for (int j = 0; j < _imageHeight; j++)
                {
                    float r = (float)i / (float)_imageHeight;
                    float g = (float)j / (float)_imageWidth;
                    float b = (float)0.2;
                    int ir = (int)(255.99 * r);
                    int ig = (int)(255.99 * g);
                    int ib = (int)(255.99 * b);
                    _map.SetPixel(i, j, Color.FromArgb(ir, ig, ib));
                }
            }
            return _map;
        }

        public void VecTests()
        {
            Vec v1 = new Vec(0, 0, 0);
            Vec v2 = new Vec(1, 1, 1);

            v1.Print();
            v2.Print();

            (-v2).Print();
            Vec v3 = v2 + v2;
            v3.Print();
            v3 = v2 - v2;
            v3.Print();
            v3 = v2 * 2;
            v3.Print();
            v3 = v2 / 2;
            v3.Print();
        }
    }
}
