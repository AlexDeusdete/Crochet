using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crochet.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YarnSpoolControl : ContentView
    {
        public static readonly BindableProperty ColorsProperty
            = BindableProperty.Create(
                nameof(Colors),
                typeof(IEnumerable<Color>),
                typeof(YarnSpoolControl),
                null,
                BindingMode.TwoWay,
                propertyChanged: (b, o, n) =>
                {
                    var yarnSpoolControl = (YarnSpoolControl)b;
                    if (n is ObservableCollection<Color> colors)
                    {
                        colors.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
                        {
                            yarnSpoolControl.skColors.InvalidateSurface();
                        };
                    }
                });
        public IEnumerable<Color> Colors
        {
            get { return (IEnumerable<Color>)GetValue(ColorsProperty); }
            set { SetValue(ColorsProperty, value);}
        }
        public YarnSpoolControl()
        {
            InitializeComponent();
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            if (Colors == null || Colors.Count() == 0)
                return;

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKRect rect = new SKRect(0f, 0f, info.Width, info.Height);
            using (SKPaint paint = new SKPaint())
            {
                paint.Shader = SKShader.CreateLinearGradient(
                                new SKPoint(0, rect.Top),
                                new SKPoint(0, rect.Bottom),
                                Colors.Select(x => x.ToSKColor()).ToArray(),
                                null,
                                SKShaderTileMode.Repeat);

                // Draw the gradient on the rectangle
                canvas.DrawRect(rect, paint);
            }
        }
    }
}