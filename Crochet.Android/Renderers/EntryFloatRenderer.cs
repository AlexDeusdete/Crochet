using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Crochet.Controls;
using Crochet.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryFloat), typeof(EntryFloatRenderer))]
namespace Crochet.Droid.Renderers
{
    public class EntryFloatRenderer : EntryRenderer
    {
        public EntryFloatRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.KeyListener = Android.Text.Method.DigitsKeyListener.GetInstance(string.Format("1234567890,."));
                this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
            }
        }
    }
}