﻿using praktApp.Data;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praktApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordStudyPage : ContentPage
    {
        private List<Category> categories;
        private int currentCategory = 0;
        private int currentWord = 0;
        private bool[] isError;
        private int Counterror;
        private double step;
        public WordStudyPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            categories = App.PraktDB.GetCategoryAsync().Result.Where(p => App.SaveChangedCategory.categories[p.Id - 1]).ToList();
            isError = new bool[categories.Count];
            LabelTerm.Text = categories[0].Words[0].Translation;

            double countWord = 0;
            foreach(Category category in categories)
            {
                countWord += category.Words.Count;
            }
            step = 1 / countWord;
        }

        private async void EntTB_Completed(object sender, EventArgs e)
        {
            if (EntTB.Text.ToLower().Trim(' ') == categories[currentCategory].Words[currentWord].Term.ToLower().Trim(' '))
            {
                //Верно
            }
            else
            {
                isError[currentCategory] = true;
                Counterror++;
                await DisplayAlert("Ошибка", "Верный термин " + categories[currentCategory].Words[currentWord].Term, "Ок");
            }
            Progress.Progress += step;
            if (currentWord == (categories[currentCategory].Words.Count - 1))
            {
                if(currentCategory == (categories.Count - 1))
                {
                    int i = 0;
                    foreach(Category cat in categories)
                    {
                        if (isError[i] == false)
                            App.SaveStudedCategory.categories.Add(cat.Id);
                        i++;
                    }
                    SaveClass.serialize(SaveClass.pathStCa);
                    await DisplayAlert("Обучение завершено", $"Категорий без ошибок { isError.Where(p => p == false).ToArray().Count()}.\nВсего ошибок:{Counterror}", "Ок");
                    await Shell.Current.GoToAsync("..");
                    return;
                }
                currentWord = -1;
                currentCategory++;
            }
            currentWord++;
            LabelTerm.Text = categories[currentCategory].Words[currentWord].Translation;
            EntTB.Text = "";
        }
    }
}