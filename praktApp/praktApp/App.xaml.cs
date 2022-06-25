using praktApp.Data;
using System;
using System.IO;
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
