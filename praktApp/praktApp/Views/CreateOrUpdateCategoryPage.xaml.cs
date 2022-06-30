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
    public partial class CreateOrUpdateCategoryPage : ContentPage
    {
        public static Category CurrentCategory { get; set; }
        public Word CurrentWord;
        private bool Updatemode;
        public CreateOrUpdateCategoryPage(bool v)
        {
            InitializeComponent();
            rename();
            Updatemode = v;
        }

        public CreateOrUpdateCategoryPage(bool v, Category cat)
        {
            InitializeComponent();
            if (!cat.IsUser)
            {
                TB1.IsEnabled = false;
                TB2.IsEnabled = false;
                BtnIn.IsVisible = false;
            }
            Updatemode = v;
            CurrentCategory = cat;
            CategoryName.Title = CurrentCategory.Name;
            collectionWordView.ItemsSource = CurrentCategory.Words;
        }

        private async void rename()
        {
            try
            {
                string s = "";
                do
                {
                    if (s != "")
                        await DisplayAlert("Ошибка создания", "Имя категории занято", "Ок");
                    s = await DisplayPromptAsync("Имя категории", "Введите наименование категории", "Ок", "Отмена");
                    if (s == null)
                    {
                        await Shell.Current.GoToAsync("..");
                        return;
                    }

                }
                while (App.PraktDB.GetCategoryAsync().Result.Where(p => p.Name == s).Count() != 0);
                CategoryName.Title = s;
                if (Updatemode)
                    CurrentCategory.Name = s;

                if (!Updatemode)
                {
                    CurrentCategory = new Category() { IsUser = true, Name = s, Words = new List<Word>() };
                }

                await App.PraktDB.SaveCategoryAsync(CurrentCategory);

                if (!Updatemode)
                {
                    CurrentCategory.Words.Add(new Word() { Category = CurrentCategory, CategoryId = CurrentCategory.Id, Term = "", Translation = "" });
                    collectionWordView.ItemsSource = CurrentCategory.Words;
                    collectionWordView.ScrollTo(CurrentCategory.Words.Count);
                    BtnIn.IsVisible = false;
                }
            }
            catch
            {
                await Shell.Current.GoToAsync("..");
            }
        }
        protected override void OnDisappearing()
        {
            if (CurrentCategory == null)
                return;
            App.SaveStudedCategory.categories.Remove(CurrentCategory.Id);
            SaveClass.serialize(SaveClass.pathStCa);
            if (CurrentCategory != null)
            {
                App.PraktDB.SaveCategoryAsync(CurrentCategory).Wait();
            }
            CurrentCategory = null;
        }

        private void CategoryNameTB_Clicked(object sender, EventArgs e)
        {
            rename();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Подтвердить действие", "Вы хотите удалить категорию?", "Да", "Нет");
            if (result == true)
            {
                await App.PraktDB.DeleteCategoryAsync(CurrentCategory);
                App.SaveChangedCategory.categories.Remove(CurrentCategory.Id);
                SaveClass.serialize(SaveClass.pathChCa);
                await Shell.Current.GoToAsync("..");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CurrentCategory.Words.Add(new Word() {Category = CurrentCategory, CategoryId = CurrentCategory.Id, Term = "", Translation = ""});
            collectionWordView.ItemsSource = null;
            collectionWordView.ItemsSource = CurrentCategory.Words;
            collectionWordView.ScrollTo(CurrentCategory.Words.Count);
            BtnIn.IsVisible = false;
        }

        private async void Editor_Completed(object sender, EventArgs e)
        {
            CurrentWord = CurrentCategory.Words.Where(p=>p.Term== (sender as Entry).Text || p.Translation == (sender as Entry).Text).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(CurrentCategory.Words[CurrentCategory.Words.Count - 1].Term) || string.IsNullOrWhiteSpace(CurrentCategory.Words[CurrentCategory.Words.Count - 1].Translation))
            {
                return;
            }


            foreach (Word w in CurrentCategory.Words)
            {
                if (w == CurrentWord)
                    continue;
                if(w.Term.ToLower().Trim(' ') == (sender as Entry).Text.ToLower().Trim(' '))
                {
                    await DisplayAlert("Ошибка создания", "Такой термин уже существует", "Ок");

                    return;
                }
                if (w.Translation.ToLower().Trim(' ') == (sender as Entry).Text.ToLower().Trim(' '))
                {
                    await DisplayAlert("Ошибка создания", "Такой перевод уже существует", "Ок");
                    return;
                }
            }


            App.PraktDB.SaveWordAsync(CurrentWord).Wait();
            collectionWordView.ScrollTo(CurrentCategory.Words.Count);
            BtnIn.IsVisible = true;
        }

        private async void BtnDel_Clicked(object sender, EventArgs e)
        {
            CurrentWord = (sender as ImageButton).BindingContext as Word;
            bool result = await DisplayAlert("Подтвердить действие", "Вы хотите удалить слово?", "Да", "Нет");
            if (result == true)
            {
                CurrentCategory.Words.Remove(CurrentWord);
                App.PraktDB.DeleteWordAsync(CurrentWord).Wait();
                collectionWordView.ItemsSource = null;
                collectionWordView.ItemsSource = CurrentCategory.Words;
                collectionWordView.ScrollTo(CurrentCategory.Words.Count);
            }

        }
    }
}