using Newtonsoft.Json;
using praktApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace praktApp.Data
{
    [Serializable]
    public class SaveChangedCategory
    {
        public List<IdCategiriesAndFlag> categories { get; set; }
    }
    public class IdCategiriesAndFlag
    {
        public int Id = 0;
        public bool flag = false;
    }

    [Serializable]
    public class SaveStudedCategory
    {
        public List<int> categories { get; set; }
    }

    public class SaveClass
    {
        public static readonly string pathChCa = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SaveFileChCa.json");
        public static readonly string pathStCa = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SaveFileStCa.json");



        public static void serialize(string path)
        {
            if (path == pathChCa)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(App.SaveChangedCategory));
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
            if(path == pathStCa)
            {
                App.SaveStudedCategory = JsonConvert.DeserializeObject<SaveStudedCategory>(File.ReadAllText(path));
                return;
            }
        }
    }
}
