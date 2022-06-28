using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace praktApp.Views
{
    public static class Settings
    {
        const int theme = 0;
        public static int Theme
        {
            //0-дефоалт, 1-светлая, 2-темная
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }
    }
}
