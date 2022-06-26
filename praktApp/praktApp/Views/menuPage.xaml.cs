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
        public menuPage()
        {
            InitializeComponent();
        }

        

    private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new settingsPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AutorizationPage());
        }

        protected override async void OnAppearing()
        {
            if(App.CurrentUser != null)
            {
                if (App.CurrentUser.Photo != null)
                {
                    var stream1 = new MemoryStream(App.CurrentUser.Photo);
                    ImageU.Source = ImageSource.FromStream(() => stream1);
                }
            }
            base.OnAppearing();
        }
    }
}