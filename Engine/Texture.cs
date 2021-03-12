using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sanjigen.Engine.MathHelpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace sanjigen.Engine
{
    public class Texture
    {
        private byte[] internalBuffer;
        private int _width;
        private int _height;

        public string _filename { get; }

        public Texture(string filename, int width, int height)
        {
            _width = width;
            _height = height;
            _filename = filename;

            Load(_filename);
        }

        public void Load(string filename)
        {
            try
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
            catch (Exception) { }
        }

        public void Load(byte[] buffer)
        {
            try
            {
                using var image = Image.Load<Rgba32>(buffer);

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
            catch (Exception) { }
        }

        public Color4 Map(float tu, float tv)
        {
            if (internalBuffer == null)
                return Color4.White;

            int u = Math.Abs((int)(tu * _width) % _width);
            int v = Math.Abs((int)(tv * _height) % _height);

            int pos = (u + v * _width) * 4;
            byte b = internalBuffer[pos];
            byte g = internalBuffer[pos + 1];
            byte r = internalBuffer[pos + 2];
            byte a = internalBuffer[pos + 3];

            return new Color4(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
        }
    }
}
