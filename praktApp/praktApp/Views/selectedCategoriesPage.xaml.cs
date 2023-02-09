using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using testAnd;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ЛР7_ВПКС.Data;
using ЛР7_ВПКС.models;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedCategoriesPage : ContentPage
    {
        public SelectedCategoriesPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            collectionCategoryView.ItemsSource = Global.completeCategoriesUser;
            base.OnAppearing();
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if ((sender as CheckBox).BindingContext is CompleteCategory CurrentCompleteCategory)
                ElectronicBookDB.GetContext().SaveComplCatAsync(CurrentCompleteCategory);
        }
       

        private async void Button_Clicked(object sender, EventArgs e)
        {
         //   await Navigation.PushAsync(new CreateOrUpdateCategoryPage(false));
        }

        private async void collectionCategoryView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (e.CurrentSelection != null)
                await Navigation.PushAsync(new CreateOrUpdateCategoryPage(true, (Category)e.CurrentSelection.FirstOrDefault()));
            */
        }

        private void searchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            collectionCategoryView.ItemsSource = Global.completeCategoriesUser.Where(p => p.Category.Name.ToLower().Contains(searchTB.Text.ToLower()));
        }

        private void checkboxCV_Focused(object sender, FocusEventArgs e)
        {
        }
    }
}