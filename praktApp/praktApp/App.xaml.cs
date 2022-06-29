using praktApp.Data;
using praktApp.Models;
using praktApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using testAnd;
using Xamarin.Forms;
using Xamarin.Essentials;
using praktApp.Views;

namespace praktApp
{
    public partial class App : Application
    {
        static PraktDB praktDB;

        public static SaveChangedCategory SaveChangedCategory;
        public static SaveStudedCategory SaveStudedCategory;
        public static PraktDB PraktDB
        {
            get
            {
                if (praktDB == null)
                {
                    praktDB = new PraktDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HRDatabase.db3"));
                }
                return praktDB;
            }
        }
        public App()
        {
            InitializeComponent();
            TheTheme.SetTheme();
            MainPage = new AppShell();
        }


        protected override void OnStart()
        {
            try
            {
                if (App.PraktDB.GetCategoryAsync().Result.Count == 0)
                {
                    Category categoryME = new Category() { IsUser = false, Name = "Мэкэникал Инжинеринг" };
                    App.PraktDB.SaveCategoryAsync(categoryME).Wait();
                    categoryME = App.PraktDB.GetCategoryAsync().Result.Where(p => p.Name == categoryME.Name).FirstOrDefault();
                    List<Word> words = new List<Word>(){
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "involves", Translation = "включать"}
                    ,
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "maintence", Translation = "эксплуатация"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "request", Translation = "требовать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "predict", Translation = "предсказывать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "employ", Translation = "использовать"},
                    };
                    foreach (Word word in words)
                        App.PraktDB.SaveWordAsync(word);
                    categoryME.Words = words;
                    App.PraktDB.SaveCategoryAsync(categoryME);

                    categoryME = new Category() { IsUser = false, Name = "Электроника" };
                    App.PraktDB.SaveCategoryAsync(categoryME).Wait();
                    categoryME = App.PraktDB.GetCategoryAsync().Result.Where(p => p.Name == categoryME.Name).FirstOrDefault();
                    words = new List<Word>(){
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "capability", Translation = "способность"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "request", Translation = "преимущество"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "benefit", Translation = "польза"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "fineline", Translation = "прецезионный"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "pattern", Translation = "шаблон"}
                    };
                    foreach (Word word in words)
                        App.PraktDB.SaveWordAsync(word);
                    categoryME.Words = words;
                    App.PraktDB.SaveCategoryAsync(categoryME);
                }

                if (File.Exists(SaveClass.pathChCa))
                    SaveClass.deserialize(SaveClass.pathChCa);
                else
                {
                    SaveChangedCategory = new SaveChangedCategory();
                    SaveChangedCategory.categories = new List<int>();
                }

                if (File.Exists(SaveClass.pathStCa))
                    SaveClass.deserialize(SaveClass.pathStCa);
                else
                {
                    SaveStudedCategory = new SaveStudedCategory();
                    SaveStudedCategory.categories = new List<int>();
                }

            }
            catch(Exception ex)
            {
            }
            OnResume();
        }

        public byte[] ImageDataFromResource(string r)
        {
            // Ensure "this" is an object that is part of your implementation within your Xamarin forms project
            var assembly = this.GetType().GetTypeInfo().Assembly;
            byte[] buffer = null;

            using (System.IO.Stream s = assembly.GetManifestResourceStream(r))
            {
                if (s != null)
                {
                    long length = s.Length;
                    buffer = new byte[length];
                    s.Read(buffer, 0, (int)length);
                }
            }

            return buffer;
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;

            if (CreateOrUpdateCategoryPage.CurrentCategory != null)
                PraktDB.SaveCategoryAsync(CreateOrUpdateCategoryPage.CurrentCategory);
            CreateOrUpdateCategoryPage.CurrentCategory = null;

            foreach (CheckBox checkBox in selectedCategoriesPage.checkBoxes)
            {
                Category currentCategory = checkBox.BindingContext as Category;
                if (currentCategory == null)
                    return;
                if (checkBox.IsChecked)
                {
                    App.SaveChangedCategory.categories.Add(currentCategory.Id);
                }
                else
                {
                    App.SaveChangedCategory.categories.Remove(currentCategory.Id);
                }
            }
            SaveClass.serialize(SaveClass.pathChCa);
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }
        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
