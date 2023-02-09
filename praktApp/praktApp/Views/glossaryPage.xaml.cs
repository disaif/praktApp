using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ЛР7_ВПКС.Data;
using ЛР7_ВПКС.models;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlossaryPage : ContentPage
    {
        private List<Word> Words;
        public GlossaryPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            Words = new List<Word>();
            List<Category> categories = ElectronicBookDB.GetContext().GetCategoriesAsync().Result.Where(x => Global.completeCategoriesUser.FirstOrDefault(y => y.Id == x.Id && y.IsChoose) != null).ToList();
            foreach (Category category in categories)
            {
                Words.AddRange(category.Words);
            }
            collectionWordView.ItemsSource = Words;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            collectionWordView.ItemsSource = Words.Where(p => p.Term.ToLower().Contains(SearchTB.Text.ToLower()) || p.CategoryName.ToLower().Contains(SearchTB.Text.ToLower()) || p.Translate.ToLower().Contains(SearchTB.Text.ToLower()));
        }
    }
}