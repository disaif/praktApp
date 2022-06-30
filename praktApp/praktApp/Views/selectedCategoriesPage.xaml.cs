
using praktApp.Data;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class selectedCategoriesPage : ContentPage
    {
        public selectedCategoriesPage()
        {
            InitializeComponent();
        }
        private List<Category> categories;
        protected override async void OnAppearing()
        {
            categories = await App.PraktDB.GetCategoryAsync();
            collectionCategoryView.ItemsSource = categories;
            base.OnAppearing();
        }
        public static List<CheckBox> checkBoxes = new List<CheckBox>();
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            Category currentCategory = (sender as CheckBox).BindingContext as Category;
            if (currentCategory == null)
                return;
            if ((sender as CheckBox).IsChecked)
            {
                App.SaveChangedCategory.categories.Where(p => p.Id == currentCategory.Id).FirstOrDefault().flag = true;
            }
            else
            {
                App.SaveChangedCategory.categories.Where(p => p.Id == currentCategory.Id).FirstOrDefault().flag = false;
            }
        }
        bool flag = true;
        protected override void OnDisappearing()
        {
            SaveClass.serialize(SaveClass.pathChCa);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateOrUpdateCategoryPage(false));
        }

        private async void collectionCategoryView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
                await Navigation.PushAsync(new CreateOrUpdateCategoryPage(true, (Category)e.CurrentSelection.FirstOrDefault()));
        }

        private void searchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (App.SaveChangedCategory.categories.Count == 0)
                    return;
                collectionCategoryView.ItemsSource = categories.Where(p => p.Name.ToLower().Contains(searchTB.Text.ToLower()));
            }
            catch
            {

            }
        }

        private void checkboxCV_Focused(object sender, FocusEventArgs e)
        {
            flag = false;
        }
    }
}