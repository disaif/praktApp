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
        private List<Word> words;
        public glossaryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                if (App.SaveChangedCategory.categories.Count == 0)
                    return;
                words = App.PraktDB.GetWordAsync().Result.Where(p => p.Category.MessageFileId == 1).ToList();
                collectionWordView.ItemsSource = words;
                base.OnAppearing();
            }
            catch
            {

            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (App.SaveChangedCategory.categories.Count == 0)
                    return;
                collectionWordView.ItemsSource = words.Where(p => p.Term.ToLower().Contains(SearchTB.Text.ToLower()) || p.Category.Name.ToLower().Contains(SearchTB.Text.ToLower()) || p.Translation.ToLower().Contains(SearchTB.Text.ToLower()));
            }
            catch
            {

            }
        }
    }
}