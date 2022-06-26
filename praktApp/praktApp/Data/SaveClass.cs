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
        public static readonly string pathCurUser = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SaveFileCurUser.json");


        public static void serialize(string path)
        {
            

            if (path == pathChCa)
                File.WriteAllText(path, JsonConvert.SerializeObject(App.SaveChangedCategory));
            if (path == pathCurUser)
                File.WriteAllText(path, JsonConvert.SerializeObject(App.CurrentUser));
        }

        public static void deserialize(string path)
        {
            if(path==pathChCa)
            App.SaveChangedCategory = JsonConvert.DeserializeObject<SaveChangedCategory>(File.ReadAllText(path));
            if(path==pathCurUser)
                App.CurrentUser = JsonConvert.DeserializeObject<User>(File.ReadAllText(path));
        }
    }
}
