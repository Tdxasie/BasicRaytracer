using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Diagnostics;

namespace BasicRaytracer
{
    public partial class ImageDisplay : Form
    {
        private double _aspectRatio;
        private int _imageHeight;
        private int _imageWidth;
        private Bitmap _map;
        private RayTracer _rt;
        public ImageDisplay()
        {
            _aspectRatio = 16.0 / 9.0;
            _imageWidth = 1200;
            _imageHeight = (int)(_imageWidth / _aspectRatio);

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RenderImage();
        }

        public void RenderImage()
        {
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(_imageWidth, _imageHeight)
            };

            this.Controls.Add(pictureBox);

            _rt = new RayTracer(_imageWidth, _imageHeight, _aspectRatio);
            _map = _rt.Render();

            pictureBox.Image = _map;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\..\\",
                Filter = "image files (*.jpg)|*.jpg|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _map.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
