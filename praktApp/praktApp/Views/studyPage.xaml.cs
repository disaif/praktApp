using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using praktApp.Models;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class studyPage : ContentPage
    {
        public studyPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new selectedCategoriesPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            foreach (Category category in App.PraktDB.GetCategoryAsync().Result.Where(p => App.SaveChangedCategory.categories.Contains(p.Id)))
            {
                if(category.Words.Count == 0)
                {
                    await DisplayAlert("Ошибка", $"В категории {category.Name} нет терминов!!!", "Ок");
                    return;
                }
            }
            if(App.SaveChangedCategory.categories.Count != 0)
                await Navigation.PushAsync(new WordStudyPage());
            else
                await DisplayAlert("Ошибка", "Изучаемые категории не выбраны!!!", "Ок");
        }
    }
}