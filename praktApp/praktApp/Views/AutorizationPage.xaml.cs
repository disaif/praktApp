using praktApp.Data;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutorizationPage : ContentPage
    {
        public AutorizationPage()
        {
            InitializeComponent();
           
        }

        private void EmailTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTB.Text) || string.IsNullOrEmpty(PasswordTB.Text))
                butEn.IsEnabled = false;
            butEn.IsEnabled = true;
        }

        private void PasswordTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTB.Text) || string.IsNullOrEmpty(PasswordTB.Text))
                butEn.IsEnabled = false;
            butEn.IsEnabled = true;
        }

        private async void butEn_Clicked(object sender, EventArgs e)
        {
            App.CurrentUser = App.PraktDB.GetUserAsync().Result.Where(p => p.email == EmailTB.Text && p.password == PasswordTB.Text).FirstOrDefault();
            if (App.CurrentUser == null)
                return;

            SaveClass.serialize(SaveClass.pathCurUser);
            await Navigation.PushAsync(new menuPage());
        }
    }
}