using Crochet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crochet.Controls
{
    public enum PickerState
    {
        Expanded,
        Collapsed
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YarnPickerControl : ContentView
    {
        public static readonly BindableProperty PickerStatedProperty
            = BindableProperty.Create(
                nameof(PickerStated),
                typeof(PickerState),
                typeof(YarnPickerControl),
                PickerState.Collapsed,
                propertyChanged: OnPickerStatedChanged);

        public static readonly BindableProperty YarnsProperty
            = BindableProperty.Create(
                nameof(Yarns),
                typeof(ObservableCollection<FeedStockGroup>),
                typeof(YarnPickerControl),
                null);

        public static readonly BindableProperty ConsumptionProperty
            = BindableProperty.Create(
                nameof(Consumption),
                typeof(int),
                typeof(YarnPickerControl),
                null);

        public static readonly BindableProperty SelectedYarnProperty
            = BindableProperty.Create(
                nameof(SelectedYarn),
                typeof(FeedStock),
                typeof(YarnPickerControl),
                null);

        public static readonly BindableProperty CommandProperty 
            = BindableProperty.Create(
                nameof(Command), 
                typeof(ICommand), 
                typeof(YarnPickerControl), 
                null);

        public static readonly BindableProperty CommandParameterProperty =
          BindableProperty.Create(
              nameof(CommandParameter), 
              typeof(object), 
              typeof(YarnPickerControl), 
              null);
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public FeedStock SelectedYarn
        {
            get { return (FeedStock)GetValue(SelectedYarnProperty); }
            set { SetValue(SelectedYarnProperty, value); }
        }
        public int Consumption
        {
            get { return (int)GetValue(ConsumptionProperty); }
            set { SetValue(ConsumptionProperty, value); }
        }
        public ObservableCollection<FeedStockGroup> Yarns
        {
            get { return (ObservableCollection<FeedStockGroup>)GetValue(YarnsProperty); }
            set { SetValue(YarnsProperty, value); }
        }
        public PickerState PickerStated
        {
            get { return (PickerState)GetValue(PickerStatedProperty); }
            set { SetValue(PickerStatedProperty, value); }
        }
        public YarnPickerControl()
        {
            
            InitializeComponent();
            fWorkArea.HeightRequest = 0;
            this.IsVisible = false;
        }
        static void OnPickerStatedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((YarnPickerControl)bindable).GoToState((PickerState)newValue);
        }

        private void GoToState(PickerState state)
        {
            if(state == PickerState.Collapsed)
            {
                var animation = new Animation(v => fWorkArea.HeightRequest = v, this.Height, 0, Easing.SpringOut);
                animation.Commit(this, "FrameAnimation", 15, 1000, Easing.SinIn, (v, c) => this.IsVisible = false, () => false);
            }
            else
            {
                this.IsVisible = true;               
                var animation = new Animation(v => fWorkArea.HeightRequest = v, 0, ((View)this.Parent).Height, Easing.SpringOut);
                animation.Commit(this, "FrameAnimation", 15, 1000, Easing.SinIn, (v, c) => this.IsVisible = true, () => false);
            }
        }

        private async void MyCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count() == 0)
                return;

            string result = await Prism.PrismApplicationBase.Current.MainPage.DisplayPromptAsync("Consumo", "Consumo em gramas :", "Salvar", "Cancelar", null, -1, Keyboard.Numeric, "");

            if((!string.IsNullOrEmpty(result)) && int.TryParse(result,out var value))
            {
                SelectedYarn = (FeedStock)e.CurrentSelection[0];
                Consumption = value;
                if(Command != null)
                {
                    if(CommandParameter != null)
                    {
                        if (Command.CanExecute(CommandParameter))
                            Command.Execute(CommandParameter);
                    }
                    else
                    {
                        if (Command.CanExecute(null))
                            Command.Execute(null);
                    }
                }
            }

            PickerStated = PickerState.Collapsed;
            (sender as CollectionView).SelectedItem = null;
        }
    }
}