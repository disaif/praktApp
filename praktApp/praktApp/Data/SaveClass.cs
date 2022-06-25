using Newtonsoft.Json;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace praktApp.Data
{
    [Serializable]
    public class SaveChangedCategory
    {
        public bool[] categories { get; set; } = new bool[App.PraktDB.GetCategoryAsync().Result.Count];
    }


    public class SaveClass
    {
        public static readonly string pathChCa = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SaveFileChCa.json");



        public static void serialize()
        {
            File.WriteAllText(pathChCa, JsonConvert.SerializeObject(App.SaveChangedCategory));
        }

        public static void deserialize()
        {
            App.SaveChangedCategory = JsonConvert.DeserializeObject<SaveChangedCategory>(File.ReadAllText(pathChCa));
        }
    }
}
