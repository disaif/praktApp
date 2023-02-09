using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testAnd;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            StackUserform.BindingContext = Global.CurrentUser;
        }

        private void ExitBut_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new AutorizationPage());
        }
    }
}