using praktApp.Data;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using praktApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class menuPage : ContentPage
    {
        public static bool swbtn = false;
        public menuPage()
        {
            InitializeComponent();
            switch (Settings.Theme)
            {
                case 0:
                    RadioButtonSystem.IsChecked = true;
                    break;
                case 1:
                    RadioButtonLight.IsChecked = true;
                    break;
                case 2:
                    RadioButtonDark.IsChecked = true;
                    break;
            }
        }
        bool loaded;
   
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new settingsPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (App.CurrentUser == null)
            {
                await Navigation.PushAsync(new AutorizationPage());
            }
            else
            {
                App.CurrentUser = null;
                SaveClass.DeleteAll();
                ButEnt.Text = "войти";
                ImageU.Source = ImageSource.FromResource("praktApp.Images.profile.png");
            }
        }

        protected override void OnAppearing()
        {
            if (App.CurrentUser != null)
            {
                ButEnt.Text = "выход";
                if (App.CurrentUser.Photo != null)
                {
                    var stream1 = new MemoryStream(App.CurrentUser.Photo);
                    ImageU.Source = ImageSource.FromStream(() => stream1);
                }
            }
            else
                ButEnt.Text = "войти";
            loaded = true;
        }

        private void RadioButtonSystem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!loaded)
                return;

            if (!e.Value)
                return;

            var val = (sender as RadioButton)?.Value as string;
            if (string.IsNullOrWhiteSpace(val))
                return;

            switch (val)
            {
                case "System":
                    Settings.Theme = 0;
                    break;
                case "Light":
                    Settings.Theme = 1;
                    break;
                case "Dark":
                    Settings.Theme = 2;
                    break;
            }

            TheTheme.SetTheme();
        }
    }
}