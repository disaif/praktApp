
using praktApp.Data;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Shell.SetTabBarIsVisible(this, false);
        }


        protected override async void OnAppearing()
        {
            collectionCategoryView.ItemsSource = await App.PraktDB.GetCategoryAsync();
            base.OnAppearing();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Category currentCategory = (sender as CheckBox).BindingContext as Category;
            if (currentCategory == null)
                return;
            if ((sender as CheckBox).IsChecked)
            {
                App.SaveChangedCategory.categories[currentCategory.Id-1] = true;
            }
            else
            {
                App.SaveChangedCategory.categories[currentCategory.Id-1] = false;
            }
            SaveClass.serialize(SaveClass.pathChCa);
        }

    }
}