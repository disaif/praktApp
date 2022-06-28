using praktApp.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktApp.Data
{
    public class PraktDB
    {
        readonly SQLiteAsyncConnection db;

        public PraktDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            
            db.CreateTableAsync<User>().Wait();
            db.CreateTableAsync<Category>().Wait();
            db.CreateTableAsync<Word>().Wait();
        }

        // Методы для Пользователей
        public Task<List<User>> GetUserAsync()
        {
            return db.GetAllWithChildrenAsync<User>();
        }

        public async Task<User> GetUserAsync(int Id)
        {
            List<User> users = await GetUserAsync();
            return users.Where(p => p.Id == Id).FirstOrDefault();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id == 0)
                return db.InsertAsync(user);
            return db.UpdateAsync(user);
        }

        public Task<int> DeleteUserAsync(User user)
        {
            List<Category> categories = user.Categories;
            var v = db.DeleteAsync(user);
            foreach (Category c in categories)
                DeleteCategoryAsync(c);
            return v;
        }

        // Методы для категорий

        public Task<List<Category>> GetCategoryAsync()
        {
            return db.GetAllWithChildrenAsync<Category>();
        }

        public async Task<Category> GetCategoryAsync(int Id)
        {
            List<Category> categories = await GetCategoryAsync();
            return categories.Where(p => p.Id == Id).FirstOrDefault();
        }

        public Task<int> SaveCategoryAsync(Category category)
        {
            if (category.Id == 0)
                return db.InsertAsync(category);
            return db.UpdateAsync(category);
        }

        public Task<int> DeleteCategoryAsync(Category category)
        {
            List<Word> words = category.Words;
            var v = db.DeleteAsync(category);
            foreach (Word w in words)
                DeleteWordAsync(w);
            return v;
        }

        // Методы для слов

        public Task<List<Word>> GetWordAsync()
        {
            return db.GetAllWithChildrenAsync<Word>();
        }

        public async Task<Word> GetWordAsync(int Id)
        {
            List<Word> words = await GetWordAsync();
            return words.Where(p => p.Id == Id).FirstOrDefault();
        }

        public Task<int> SaveWordAsync(Word word)
        {
            if (word.Id == 0)
                return db.InsertAsync(word);
            return db.UpdateAsync(word);
        }

        public Task<int> DeleteWordAsync(Word word)
        {
            return db.DeleteAsync(word);
        }
    }
}
