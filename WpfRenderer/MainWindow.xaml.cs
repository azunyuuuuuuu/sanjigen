using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using sanjigen.Engine;

namespace sanjigen.WpfRenderer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private WriteableBitmap _bmp;
        private Device _device;
        private Mesh[] _meshes = new Mesh[0];
        private Camera _camera = new Camera();

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _bmp = new WriteableBitmap(1280, 720, 96, 96, PixelFormats.Bgra32, BitmapPalettes.WebPalette);

            _device = new Device((int)_bmp.Width, (int)_bmp.Height);

            // Our XAML Image control
            FrontBuffer.Source = _bmp;
            _meshes = await _device.LoadJSONFileAsync("monkey.babylon");

            _camera.Position = new Vector3(0, 0, 10.0f);
            _camera.Target = Vector3.Zero;

            // Registering to the XAML rendering loop
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        DateTime previousDate;

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            // calculate fps
            var now = DateTime.Now;
            var currentFps = 1000.0 / (now - previousDate).TotalMilliseconds;
            _device.Clear(0, 0, 0, 255);
            previousDate = now;

            FPStext.Text = string.Format("{0:0.00} fps", currentFps);

            // rotating slightly the cube during each frame rendered
            foreach (var mesh in _meshes)
                mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y + 0.015f, mesh.Rotation.Z);

            // Doing the various matrix operations
            _device.Render(_camera, _meshes);

            // Flushing the back buffer into the front buffer
            var buffer = _device.GetBackBuffer();
            _bmp.WritePixels(new Int32Rect(0, 0, _device.Width, _device.Height), buffer, _bmp.BackBufferStride, 0);
        }
    }
}
