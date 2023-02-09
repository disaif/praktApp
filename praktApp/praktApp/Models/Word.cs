﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ЛР7_ВПКС.Data;

namespace ЛР7_ВПКС.models
{
    public class Word
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
        [NotNull]
        public string Term { get; set; }
        [NotNull]
        public string Translate { get; set; }
        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        public string CategoryName
        {
            get
            {
                return ElectronicBookDB.GetContext().GetCategory(CategoryId).Name;
            }
        }
    }
}
