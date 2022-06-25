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
    public partial class glossaryPage : ContentPage
    {
        public glossaryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionWordView.ItemsSource = App.PraktDB.GetWordAsync().Result.Where(p => p.Category.MessageFileId == 1).ToList();
            base.OnAppearing();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}