using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace praktApp.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public bool IsUser { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<Word> Words { get; set; }

        public int MessageFileId
        {
            get
            {
                if (App.SaveChangedCategory.categories.Where(p => p.Id == Id).FirstOrDefault().flag)
                    return 1;
                return 0;
            }
        }
        public string IsStuded
        {
            get
            {
                if (App.SaveStudedCategory.categories.Contains(Id))
                    return "(Изучено)";
                return "(Не изучено)";
            }
        }

    }
}
