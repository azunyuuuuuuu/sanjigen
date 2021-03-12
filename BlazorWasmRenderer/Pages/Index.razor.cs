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

namespace sanjigen.BlazorWasmRenderer.Pages
{
    public partial class Index
    {
        private Canvas2DContext _context;

        protected BECanvasComponent _canvasReference;
        private bool _running;
        private float previousTimestamp = 0;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _js.InvokeAsync<object>("setDrawCallback", DotNetObjectReference.Create(this));

                _context = await createScaledCanvasContext(_canvasReference, false);
            }

            if (firstRender)
            {
                await _context.SetFillStyleAsync("red");
                await _context.FillRectAsync(0, 0, _canvasReference.Width, _canvasReference.Height);
            }
        }

        async ValueTask<Canvas2DContext> createScaledCanvasContext(BECanvasComponent canvas, bool scale)
        {
            var context = await canvas.CreateCanvas2DAsync();

            await context.SetTextBaselineAsync(TextBaseline.Top);

            if (scale)
                await context.ScaleAsync(3, 3);

            return context;
        }

        [JSInvokable]
        public async ValueTask RenderLoop(float timeStamp)
        {
            if (_running)
                return;

            _running = true;

            var currentFps = 1000/(timeStamp - previousTimestamp);

            await _context.SetFillStyleAsync("red");
            await _context.FillRectAsync(0, 0, _canvasReference.Width, _canvasReference.Height);

            await _context.SetFontAsync("10px sans-serif");

            await _context.SetFillStyleAsync("white");
            await _context.FillTextAsync($"{string.Format("{0:0.0}", currentFps)} fps", 10, 10);

            previousTimestamp = timeStamp;

            _running = false;
        }
    }
}
