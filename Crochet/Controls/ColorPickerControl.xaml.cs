using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crochet.Controls
{
    public enum CardState
    {
        Expanded,
        Collapsed        
    }

    //Inspiration https://theconfuzedsourcecode.wordpress.com/2020/02/26/i-built-an-interactive-color-picker-control-for-xamarin-forms/
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerControl : ContentView
    {
        #region Property
        public event EventHandler<Color> PickedColorChanged;

        public static readonly BindableProperty PickedColorProperty
            = BindableProperty.Create(
                nameof(PickedColor),
                typeof(Color),
                typeof(ColorPickerControl),
                null,
                BindingMode.TwoWay);

        public Color PickedColor 
        { 
            get { return (Color)GetValue(PickedColorProperty); } 
            set { SetValue(PickedColorProperty, value); } 
        }

        public static readonly BindableProperty CardStatedProperty
            = BindableProperty.Create(
                nameof(CardStated),
                typeof(CardState),
                typeof(ColorPickerControl),
                null,
                propertyChanged: OnCardStatedChanged);

        public CardState CardStated
        {
            get { return (CardState)GetValue(CardStatedProperty); }
            set { SetValue(CardStatedProperty, value); }
        }
        #endregion

        private float _cardTopAnimPosition;
        private SKPoint _lastTouchPointHue = new SKPoint();
        private SKPoint _lastTouchPoint = new SKPoint();
        private SKPoint _circleCenter = new SKPoint();
        private float _circleRadios;
        private SKPoint _trinangleV1 = new SKPoint();
        private SKPoint _trinangleV2 = new SKPoint();
        private SKPoint _trinangleV3 = new SKPoint();
        private readonly float _density;
        private Color _pickedColorHue;
        
        private readonly float _cornerRadius = 60f;
        private float _canvasHeight;
        private bool _isTheFristTime;
        private CardState _cardState = CardState.Collapsed;
        public ColorPickerControl()
        {
            InitializeComponent();

            _density = (float)Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
            _cornerRadius = 30f * _density;
            _cardTopAnimPosition = 1f * _density;
            _isTheFristTime = true; 
        }

        private void SkCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var skImageInfo = e.Info;
            var skSurface = e.Surface;
            var skCanvas = skSurface.Canvas;

            var skCanvasWidth = skImageInfo.Width;
            _canvasHeight = skImageInfo.Height;

            if (_isTheFristTime)
            {
                _cardTopAnimPosition = _canvasHeight;
                _isTheFristTime = false;
                this.IsVisible = false;
            }

            skCanvas.Clear(SKColors.Transparent);

            using(var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                Color color = PickedColor.IsDefault ? (Color)Application.Current.Resources["colorPrimary"] : PickedColor;
                paint.Color = color.ToSKColor();

                skCanvas.DrawRoundRect(
                    rect: new SKRect(0, (float)_cardTopAnimPosition, skCanvasWidth, _canvasHeight),
                    r: new SKSize(_cornerRadius, _cornerRadius),
                    paint: paint);
            }

            CreateColorPicker(skImageInfo, skSurface, skCanvas, _cardTopAnimPosition);
        }
        private void CreateColorPicker(SKImageInfo skImageInfo, SKSurface skSurface, SKCanvas skCanvas, float CardTopAnimPosition)
        {
            const int strokeWidth = 50;
            _circleCenter = new SKPoint(skImageInfo.Rect.MidX, ((skImageInfo.Height - CardTopAnimPosition) / 2f) + CardTopAnimPosition);
            //Circle Gradient
            using (SKPaint paint = new SKPaint())
            {
                paint.IsAntialias = true;
                // Define an array of rainbow colors
                SKColor[] colors = new SKColor[8];

                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = SKColor.FromHsl(i * 360f / 7, 100, 50);
                }
                // Create sweep gradient based on center of canvas
                paint.Shader = SKShader.CreateSweepGradient(_circleCenter, colors, null);

                // Draw a circle with a wide line
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = strokeWidth;

                _circleRadios = (Math.Min(skImageInfo.Width, (skImageInfo.Height - CardTopAnimPosition)) - strokeWidth) / 2;
                skCanvas.DrawCircle(_circleCenter, _circleRadios, paint);
            }

            //Triangulo
            if(CardTopAnimPosition <= 0.7f * skImageInfo.Height)
                using (SKPaint fillPaint = new SKPaint())
            {
                fillPaint.IsAntialias = true;
                SKPath path = new SKPath();

                _trinangleV1.X = _circleCenter.X;
                _trinangleV1.Y = (float)strokeWidth + 10 + CardTopAnimPosition;

                _trinangleV2.X = 0.4f * _circleCenter.X + 10;
                _trinangleV2.Y = 0.7f * skImageInfo.Height;

                _trinangleV3.X = 1.6f * _circleCenter.X - 10;
                _trinangleV3.Y = 0.7f * skImageInfo.Height;
                path.MoveTo(_trinangleV1.X, _trinangleV1.Y);
                path.LineTo(_trinangleV2.X, _trinangleV2.Y);
                path.LineTo(_trinangleV3.X, _trinangleV3.Y);
                path.Close();

                fillPaint.Style = SKPaintStyle.Fill;

                var colorsBW = new SKColor[]
                {
                    SKColors.Black,
                    SKColors.White
                };

                var colorsPCT = new SKColor[]
                {
                    (_pickedColorHue.Hue != 0 ? _pickedColorHue.ToSKColor() : SKColors.Transparent),
                    SKColors.Transparent
                };

                fillPaint.Shader = SKShader.CreateLinearGradient(
                        new SKPoint(_trinangleV2.X, 0),
                        new SKPoint(_trinangleV3.X, 0),
                        colorsBW,
                        new float[] { 0.25f, 0.95f },
                        SKShaderTileMode.Clamp);

                skCanvas.DrawPath(path, fillPaint);

                fillPaint.Shader = SKShader.CreateLinearGradient(
                        new SKPoint(0, _trinangleV1.Y),
                        new SKPoint(0, _trinangleV2.Y),
                        colorsPCT,
                        new float[] { 0.45f, 1 },
                        SKShaderTileMode.Clamp);

                skCanvas.DrawPath(path, fillPaint);
            }

            SKColor touchPointColor = new SKColor();

            // Efficient and fast
            // https://forums.xamarin.com/discussion/92899/read-a-pixel-info-from-a-canvas
            // create the 1x1 bitmap (auto allocates the pixel buffer)
            if (_lastTouchPointHue.X != 0 && CardTopAnimPosition <= _lastTouchPointHue.Y)
            {
                using (SKBitmap bitmap = new SKBitmap(skImageInfo))
                {
                    // get the pixel buffer for the bitmap
                    IntPtr dstpixels = bitmap.GetPixels();

                    // read the surface into the bitmap
                    skSurface.ReadPixels(skImageInfo,
                        dstpixels,
                        skImageInfo.RowBytes,
                        (int)_lastTouchPointHue.X, (int)_lastTouchPointHue.Y);

                    // access the color
                    touchPointColor = bitmap.GetPixel(0, 0);
                }

                //Dont painting the Touch if card is collapsed
                if (_cardState == CardState.Collapsed)
                    return;

                // Painting the Touch point
                using (var paintTouchPoint = new SKPaint())
                {
                    paintTouchPoint.Style = SKPaintStyle.Fill;
                    paintTouchPoint.Color = SKColors.White;
                    paintTouchPoint.IsAntialias = true;

                    // Outer circle (Ring)
                    var outerRingRadius =
                        (skImageInfo.Width /
                            skImageInfo.Height) * (float)18;
                    skCanvas.DrawCircle(
                        _lastTouchPointHue.X,
                        _lastTouchPointHue.Y,
                        outerRingRadius, paintTouchPoint);

                    // Draw another circle with picked color
                    paintTouchPoint.Color = touchPointColor;

                    // Outer circle (Ring)
                    var innerRingRadius =
                        (skImageInfo.Width /
                            skImageInfo.Height) * (float)12;
                    skCanvas.DrawCircle(
                        _lastTouchPointHue.X,
                        _lastTouchPointHue.Y,
                        innerRingRadius, paintTouchPoint);
                }

                _pickedColorHue = touchPointColor.ToFormsColor();
            }

            if (_lastTouchPoint.X != 0 && CardTopAnimPosition <= _lastTouchPoint.Y)
            {
                using (SKBitmap bitmap = new SKBitmap(skImageInfo))
                {
                    // get the pixel buffer for the bitmap
                    IntPtr dstpixels = bitmap.GetPixels();

                    // read the surface into the bitmap
                    skSurface.ReadPixels(skImageInfo,
                        dstpixels,
                        skImageInfo.RowBytes,
                        (int)_lastTouchPoint.X, (int)_lastTouchPoint.Y);

                    // access the color
                    touchPointColor = bitmap.GetPixel(0, 0);
                }

                //Dont painting the Touch if card is collapsed
                if (_cardState == CardState.Collapsed)
                    return;

                // Painting the Touch point
                using (var paintTouchPoint = new SKPaint())
                {
                    paintTouchPoint.Style = SKPaintStyle.Fill;
                    paintTouchPoint.Color = SKColors.White;
                    paintTouchPoint.IsAntialias = true;

                    // Outer circle (Ring)
                    var outerRingRadius =
                        (skImageInfo.Width /
                            skImageInfo.Height) * (float)14;
                    skCanvas.DrawCircle(
                        _lastTouchPoint.X,
                        _lastTouchPoint.Y,
                        outerRingRadius, paintTouchPoint);

                    // Draw another circle with picked color
                    paintTouchPoint.Color = touchPointColor;

                    // Outer circle (Ring)
                    var innerRingRadius =
                        (skImageInfo.Width /
                            skImageInfo.Height) * (float)12;
                    skCanvas.DrawCircle(
                        _lastTouchPoint.X,
                        _lastTouchPoint.Y,
                        innerRingRadius, paintTouchPoint);
                }

                PickedColor = touchPointColor.ToFormsColor();
                PickedColorChanged?.Invoke(this, PickedColor);
            }
        }
        private void SkCanvasView_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if (_cardState == CardState.Collapsed)
                return;

            var point = e.Location;

            // Check for each touch point XY position to be inside Canvas
            // Ignore any Touch event ocurred outside the Canvas region 
            var a = Math.Pow(point.X - _circleCenter.X,2);
            var b = Math.Pow(point.Y - _circleCenter.Y,2);
            var c = Math.Pow(_circleRadios + 25f,2);

            //internal circle
            var d = Math.Pow(_circleRadios - 25f, 2);
            if ((a + b) <= c && (a + b) > d)
            {
                e.Handled = true;
                _lastTouchPointHue = point;

                // update the Canvas as you wish
                SkCanvasView.InvalidateSurface();
            }

            //Point a Triangle
            if (ClickInTriangle(point, _trinangleV1, _trinangleV2, _trinangleV3))
            {
                e.Handled = true;
                _lastTouchPoint = point;

                // update the Canvas as you wish
                SkCanvasView.InvalidateSurface();
            }
        }
        private bool ClickInTriangle(SKPoint pt, SKPoint v1, SKPoint v2, SKPoint v3)
        {
            float d1, d2, d3;
            bool has_neg, has_pos;

            d1 = Sing(pt, v1, v2);
            d2 = Sing(pt, v2, v3);
            d3 = Sing(pt, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }
        private float Sing(SKPoint p1, SKPoint p2, SKPoint p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }
        static void OnCardStatedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ColorPickerControl)bindable).GoToState((CardState)newValue);
        }
        private void GoToState(CardState cardState)
        {
            if (_cardState == cardState)
                return;

            _cardState = cardState;
            
            AnimateTransition(_cardState);
        }
        private void AnimateTransition(CardState cardState)
        {
            this.IsVisible = true;
            var parentAnimation = new Animation();

            if (cardState == CardState.Expanded)
                parentAnimation.Add(0, 1, CreateCardAnimation(cardState));
            else
                parentAnimation.Add(0, 1, CreateCardAnimation(cardState));

            parentAnimation.Commit(this, "CardExpand", 16, 1000, null, (v, c) => this.IsVisible = cardState == CardState.Expanded?true:false);
        }
        private Animation CreateCardAnimation(CardState cardState)
        {
            // work out where the top of the card should be
            var cardAnimStart = cardState == CardState.Expanded ? _canvasHeight : 0;
            var cardAnimEnd = cardState == CardState.Expanded ? 0 : _canvasHeight;
            
            var cardAnim = new Animation(
                v =>
                {
                    _cardTopAnimPosition = (float)v;
                    SkCanvasView.InvalidateSurface();
                },
                cardAnimStart,
                cardAnimEnd,
                Easing.CubicIn
                );
            return cardAnim;
        }
    }
}