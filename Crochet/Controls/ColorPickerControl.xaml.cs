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
        private SKPoint _lastTouchPoint = new SKPoint();
        private readonly float _density;
        
        private readonly float _cornerRadius = 60f;
        private float _canvasHeight;
        private bool _isTheFristTime;
        private CardState _cardState = CardState.Collapsed;
        private SKRect _colorPickerRect;
        public ColorPickerControl()
        {
            InitializeComponent();

            _density = (float)Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
            _cornerRadius = 30f * _density;
            _colorPickerRect = new SKRect();
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
                Color color = PickedColor != null ? PickedColor : (Color)Application.Current.Resources["colorPrimary"];
                paint.Color = color.ToSKColor();

                // draw top hero color
                skCanvas.DrawRoundRect(
                    rect: new SKRect(0, (float)_cardTopAnimPosition, skCanvasWidth, _canvasHeight),
                    r: new SKSize(_cornerRadius, _cornerRadius),
                    paint: paint);
            }

            _colorPickerRect.Left = skCanvasWidth * 0.05f;
            _colorPickerRect.Top = _cardTopAnimPosition + (_canvasHeight * 0.05f);
            _colorPickerRect.Right = skCanvasWidth * 0.95f;
            _colorPickerRect.Bottom = _canvasHeight * 0.95f;

            if (_colorPickerRect.Top > _colorPickerRect.Bottom)
                _colorPickerRect.Bottom = _colorPickerRect.Top;

            CreateColorPicker(skImageInfo, skSurface, skCanvas, _colorPickerRect);
        }

        private void CreateColorPicker(SKImageInfo skImageInfo, SKSurface skSurface, SKCanvas skCanvas, SKRect ImagePickerRect)
        {
            ///Rainbow Gradiente
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;

                var colors = new SKColor[]
                {
                    new SKColor(255,0,0),
                    new SKColor(255,255,0),
                    new SKColor(0,255,0),
                    new SKColor(0,255,255),
                    new SKColor(0,0,255),
                    new SKColor(255,0,255),
                    new SKColor(255,0,0)
                };

                
                using (var shader = SKShader.CreateLinearGradient(
                    new SKPoint(ImagePickerRect.Left, 0),
                    new SKPoint(ImagePickerRect.Right, 0),
                    colors,
                    null,
                    SKShaderTileMode.Clamp))
                {
                    paint.Shader = shader;
                    //Put 10% Margin
                    skCanvas.DrawRect(
                        ImagePickerRect,
                        paint);
                }

            }

            //Dark and Ligth Gradient
            
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;

                var colors = new SKColor[]
                {
                    SKColors.White,
                    SKColors.Transparent,
                    SKColors.Black
                };

                using (var shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, ImagePickerRect.Top),
                    new SKPoint(0, ImagePickerRect.Bottom),
                    colors,
                    null,
                    SKShaderTileMode.Clamp))
                {
                    paint.Shader = shader;
                    skCanvas.DrawRect(
                        ImagePickerRect,
                        paint);
                }
            }            

            // Represent the color of the current Touch point
            SKColor touchPointColor;

            // Efficient and fast
            // https://forums.xamarin.com/discussion/92899/read-a-pixel-info-from-a-canvas
            // create the 1x1 bitmap (auto allocates the pixel buffer)
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
                    (ImagePickerRect.Right /
                        ImagePickerRect.Bottom) * (float)18;
                skCanvas.DrawCircle(
                    _lastTouchPoint.X,
                    _lastTouchPoint.Y,
                    outerRingRadius, paintTouchPoint);

                // Draw another circle with picked color
                paintTouchPoint.Color = touchPointColor;

                // Outer circle (Ring)
                var innerRingRadius =
                    (ImagePickerRect.Right /
                        ImagePickerRect.Bottom) * (float)12;
                skCanvas.DrawCircle(
                    _lastTouchPoint.X,
                    _lastTouchPoint.Y,
                    innerRingRadius, paintTouchPoint);
            }

            PickedColor = touchPointColor.ToFormsColor();
            PickedColorChanged?.Invoke(this, PickedColor);
        }

        private void SkCanvasView_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if (_cardState == CardState.Collapsed)
                return;

            _lastTouchPoint = e.Location;

            // Check for each touch point XY position to be inside Canvas
            // Ignore any Touch event ocurred outside the Canvas region 
            if ((e.Location.X > _colorPickerRect.Left && e.Location.X < _colorPickerRect.Right) &&
                (e.Location.Y > _colorPickerRect.Top && e.Location.Y < _colorPickerRect.Bottom))
            {
                e.Handled = true;

                // update the Canvas as you wish
                SkCanvasView.InvalidateSurface();
            }
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