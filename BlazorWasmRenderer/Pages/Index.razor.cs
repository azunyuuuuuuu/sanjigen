using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions;
using Microsoft.JSInterop;
using sanjigen.Engine;
using sanjigen.Engine.MathHelpers;
using Microsoft.AspNetCore.Components;

namespace sanjigen.BlazorWasmRenderer.Pages
{
    public partial class Index
    {
        private Canvas2DContext _context;

        protected BECanvasComponent _canvasReference;
        private bool _running;
        private float previousTimestamp = 0;
        private Device _device;
        private Mesh[] _meshes = new Mesh[0];
        private Camera _camera = new Camera();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) await FirstRender();
        }

        private async Task FirstRender()
        {
            await _js.InvokeAsync<object>("setDrawCallback", DotNetObjectReference.Create(this));

            _context = await createScaledCanvasContext(_canvasReference, false);

            await _context.SetFillStyleAsync("red");
            await _context.FillRectAsync(0, 0, _canvasReference.Width, _canvasReference.Height);

            _device = new Device((int)_canvasReference.Width, (int)_canvasReference.Height);

            var tempdata = await _http.GetStringAsync("monkey.babylon");
            _meshes = _device.LoadFromBabylonFile(tempdata);

            foreach (var mesh in _meshes)
                mesh.Texture.Load(await _http.GetByteArrayAsync(mesh.Texture._filename));

            _camera.Position = new Vector3(0, 0, 10.0f);
            _camera.Target = Vector3.Zero;
        }

        async ValueTask<Canvas2DContext> createScaledCanvasContext(BECanvasComponent canvas, bool scale)
        {
            var context = await canvas.CreateCanvas2DAsync();

            await context.SetTextBaselineAsync(TextBaseline.Top);

            if (scale)
                await context.ScaleAsync(3, 3);

            return context;
        }


        ElementReference rendertarget;
        private string _rendertarget = "";

        [JSInvokable]
        public async ValueTask RenderLoop(float timeStamp)
        {
            if (_running)
                return;

            _running = true;

            _device.Clear(0, 0, 0, 255);

            var currentFps = 1000 / (timeStamp - previousTimestamp);

            foreach (var mesh in _meshes)
                mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y + 0.015f, mesh.Rotation.Z);

            _device.Render(_camera, _meshes);

            _rendertarget = _device.GetBackbufferAsBase64();

            await DrawImage(_canvasReference.CanvasReference, _rendertarget, 0, 0, 320, 240);

            await _context.SetFontAsync("10px sans-serif");
            await _context.SetFillStyleAsync("white");
            await _context.FillTextAsync($"{string.Format("{0:0.0}", currentFps)} fps", 10, 10);

            previousTimestamp = timeStamp;

            _running = false;
        }

        public async Task<object> DrawImage(ElementReference canvas, string url, double dx, double dy, double dw, double dh)
            => await _js.InvokeAsync<object>("DrawImage", canvas, url, dx, dy, dw, dh);
    }
}
