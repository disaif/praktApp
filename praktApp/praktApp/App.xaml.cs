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
        public static PraktDB PraktDB
        {
            get
            {
                if (praktDB == null)
                {
                    praktDB = new PraktDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HRDatabase.db3"));


                    Category categoryME = new Category() { IsPublic = true, Name = "Мэкэникал Инжинеринг"};
                    praktDB.SaveCategoryAsync(categoryME);
                    categoryME = praktDB.GetCategoryAsync().Result.ToList().FirstOrDefault();
                    List<Word> words = new List<Word>(){ 
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "involves", Translation = "Включать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "maintence", Translation = "эксплуатация"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "request", Translation = "требовать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "predict", Translation = "предсказывать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "employ", Translation = "frame"},
                    };
                    foreach (Word word in words)
                        praktDB.SaveWordAsync(word);
                    categoryME.Words = words;
                    praktDB.SaveCategoryAsync(categoryME);


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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
