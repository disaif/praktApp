using praktApp.Data;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if(App.CurrentUser != null)
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

        void BtnSwitch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}