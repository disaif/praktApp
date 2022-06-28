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
    [Serializable]
    public class SaveStudedCategory
    {
        public List<int> categories { get; set; } = new List<int>();
    }

    public class SaveClass
    {
        public static readonly string pathChCa = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SaveFileChCa.json");
        public static readonly string pathCurUser = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SaveFileCurUser.json");
        public static readonly string pathStCa = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SaveFileStCa.json");



        public static void serialize(string path)
        {
            if (path == pathChCa)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(App.SaveChangedCategory));
                return;
            }
            if (path == pathCurUser)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(App.CurrentUser));
                return;
            }
            if (path == pathStCa)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(App.SaveStudedCategory));
                return;
            }
        }

        public static void deserialize(string path)
        {
            if (path == pathChCa)
            {
                App.SaveChangedCategory = JsonConvert.DeserializeObject<SaveChangedCategory>(File.ReadAllText(path));
                return;
            }
            if (path == pathCurUser)
            {
                App.CurrentUser = JsonConvert.DeserializeObject<User>(File.ReadAllText(path));
                return;
            }
            if(path == pathStCa)
            {
                App.SaveStudedCategory = JsonConvert.DeserializeObject<SaveStudedCategory>(File.ReadAllText(path));
                return;
            }
        }

        public static void DeleteAll()
        {
            if(File.Exists(pathCurUser))
            File.Delete(pathCurUser);
            if(File.Exists(pathChCa))
            File.Delete(pathChCa);
            if (File.Exists(pathStCa))
            File.Delete(pathStCa);
        }
    }
}
