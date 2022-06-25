using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace praktApp.Models
{
    public class Word
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
        [ForeignKey(typeof(Category)), NotNull]
        public int CategoryId { get; set; }
        [NotNull]
        public string Term { get; set; }
        [NotNull]
        public string Translation { get; set; }
        [ManyToOne, NotNull]
        public Category Category { get; set; }
    }
}
