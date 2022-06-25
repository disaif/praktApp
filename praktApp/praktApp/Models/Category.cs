using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace praktApp.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public bool IsPublic { get; set; }
        [ManyToOne]
        public User User { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<Word> Words { get; set; }
    }
}
