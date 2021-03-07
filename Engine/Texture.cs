using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace sanjigen.Engine
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

        private void Load(string filename)
        {
            using var image = Image.Load<Rgba32>(filename);

            internalBuffer = new byte[image.Width * image.Height * 4];

            if (image.TryGetSinglePixelSpan(out var pixelSpan))
            {
                int i = 0;
                foreach (var pixel in pixelSpan)
                {
                    internalBuffer[i++] = pixel.B;
                    internalBuffer[i++] = pixel.G;
                    internalBuffer[i++] = pixel.R;
                    internalBuffer[i++] = pixel.A;
                }
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
