using SQLiteNetExtensions.Attributes;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace praktApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
        [NotNull]
        public string nickname { get; set; }
        [NotNull]
        public string email { get; set; }
        [NotNull]
        public string password { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<Category> Categories { get; set; }
        public byte[] Photo { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<int> ChosingCategorits { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<int> StudedCategories { get; set; }
    }
}
