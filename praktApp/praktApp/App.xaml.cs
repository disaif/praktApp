using praktApp.Data;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using testAnd;
using Xamarin.Forms;

namespace praktApp
{
    public partial class App : Application
    {
        static PraktDB praktDB;

        public static SaveChangedCategory SaveChangedCategory;
        public static User CurrentUser;
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
                    SaveClass.deserialize(SaveClass.pathChCa);
                else
                    SaveChangedCategory = new SaveChangedCategory();

                if (File.Exists(SaveClass.pathCurUser))
                    SaveClass.deserialize(SaveClass.pathCurUser);
                else
                    CurrentUser = null;

                if (App.PraktDB.GetCategoryAsync().Result.Count == 0)
                {
                    User user = new User() { email = "Nikitos613@mail.ru", nickname = "Ebola", password = "123", Photo = ImageDataFromResource("praktApp.Data.ava.jpg")};
                    PraktDB.SaveUserAsync(user);

                    Category categoryME = new Category() { IsPublic = true, Name = "Мэкэникал Инжинеринг" };
                    App.PraktDB.SaveCategoryAsync(categoryME).Wait();
                    categoryME = App.PraktDB.GetCategoryAsync().Result.Where(p => p.Name == categoryME.Name).FirstOrDefault();
                    List<Word> words = new List<Word>(){
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "involves", Translation = "включать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "maintence", Translation = "эксплуатация"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "request", Translation = "требовать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "predict", Translation = "предсказывать"},
                    new Word() { Category = categoryME, CategoryId = categoryME.Id, Term = "employ", Translation = "использовать"},
                    };
                    foreach (Word word in words)
                        App.PraktDB.SaveWordAsync(word);
                    categoryME.Words = words;
                    App.PraktDB.SaveCategoryAsync(categoryME);

                    categoryME = new Category() { IsPublic = true, Name = "Электроника" };
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
            }
            catch(Exception ex)
            {
            }
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
        }

        protected override void OnResume()
        {
        }
    }
}
