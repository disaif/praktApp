using praktApp.Data;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using testAnd;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praktApp
{
    public partial class App : Application
    {
        static PraktDB praktDB;

        public static SaveChangedCategory SaveChangedCategory;
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
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
           
                

            try
            {
                if (File.Exists(SaveClass.pathChCa))
                    SaveClass.deserialize();
                else
                    SaveChangedCategory = new SaveChangedCategory();

                if (App.PraktDB.GetCategoryAsync().Result.Count == 0)
                {
                    Category categoryME = new Category() { IsPublic = true, Name = "Мэкэникал Инжинеринг" };
                    App.PraktDB.SaveCategoryAsync(categoryME).Wait();
                    categoryME = App.PraktDB.GetCategoryAsync().Result.Where(p => p.Name == categoryME.Name).FirstOrDefault();
                    List<Word> words = new List<Word>(){
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "involves", Translation = "включать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "maintence", Translation = "эксплуатация"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "request", Translation = "требовать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "predict", Translation = "предсказывать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "employ", Translation = "frame"},
                    };
                    foreach (Word word in words)
                        App.PraktDB.SaveWordAsync(word);
                    categoryME.Words = words;
                    App.PraktDB.SaveCategoryAsync(categoryME);

                    categoryME = new Category() { IsPublic = true, Name = "НеМэкэникал Инжинеринг" };
                    App.PraktDB.SaveCategoryAsync(categoryME).Wait();
                    categoryME = App.PraktDB.GetCategoryAsync().Result.Where(p => p.Name == categoryME.Name).FirstOrDefault();
                    words = new List<Word>(){
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "involves", Translation = "включать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "maintence", Translation = "эксплуатация"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "request", Translation = "требовать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "predict", Translation = "предсказывать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "employ", Translation = "frame"},
                    };
                    foreach (Word word in words)
                        App.PraktDB.SaveWordAsync(word);
                    categoryME.Words = words;
                    App.PraktDB.SaveCategoryAsync(categoryME);
                }
            }
            catch
            {

            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
