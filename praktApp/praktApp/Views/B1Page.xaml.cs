using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using praktApp.Views.BookPages.B1;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class B1Page : ContentPage
    {
        public B1Page()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page3());
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page4());
        }

        private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page5());
        }

        private async void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page6());
        }

        private async void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page7());
        }

        private async void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page8());
        }

        private async void TapGestureRecognizer_Tapped_8(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page9());
        }

        private async void TapGestureRecognizer_Tapped_9(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page10());
        }

        private async void TapGestureRecognizer_Tapped_10(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page11());
        }

        private async void TapGestureRecognizer_Tapped_11(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page12());
        }

        private async void TapGestureRecognizer_Tapped_12(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page13());
        }
    }
}