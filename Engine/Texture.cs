using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TreiD
{
    public class Texture
    {
        private byte[] internalBuffer;
        private int width;
        private int height;

        public Texture(string filename, int width, int height)
        {
            this.width = width;
            this.height = height;
            Load(filename);
        }

        private async void Load(string filename)
        {
            using (var stream = new StreamReader(filename))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filename, UriKind.Relative);
                bitmapImage.EndInit();
                internalBuffer = new byte[width * height * 4];
                bitmapImage.CopyPixels(internalBuffer, width * 4, 0);
            }
        }

        public Color4 Map(float tu, float tv)
        {
            if (internalBuffer == null)
                return Color4.White;

            int u = Math.Abs((int)(tu * width) % width);
            int v = Math.Abs((int)(tv * height) % height);

            int pos = (u + v * width) * 4;
            byte b = internalBuffer[pos];
            byte g = internalBuffer[pos + 1];
            byte r = internalBuffer[pos + 2];
            byte a = internalBuffer[pos + 3];

            return new Color4(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
        }
    }
}
