using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ЛР7_ВПКС.Data;
using ЛР7_ВПКС.models;

namespace praktApp
{
    public static class Global
    {
        public static User CurrentUser;
        /// <summary>
        /// Список выбранных пользователем категорий
        /// </summary>
        public static List<CompleteCategory> completeCategoriesUser;
        /// <summary>
        /// Путь к айди текущего пользователя
        /// </summary>
        private static string CurUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentUser.xml");
        /// <summary>
        /// Авторизовавшийся пользователь
        /// </summary>
        /// <param name="user"></param>
        private static void InitializateUserInSystem(User user)
        {
            CurrentUser = user;
            completeCategoriesUser = ElectronicBookDB.GetContext().GetComplCatAsync().Result.Where(x => CurrentUser.CategoriesComlList.FirstOrDefault(y => y.Id == x.Id) != null).ToList();
        }

        public static void SerializateUser(User user)
        {
            File.WriteAllText(CurUserPath, JsonConvert.SerializeObject(user.Id));
            InitializateUserInSystem(user);
        }

        public static void DeserealizateUser()
        {
            int id = JsonConvert.DeserializeObject<int>(File.ReadAllText(CurUserPath));
            InitializateUserInSystem(ElectronicBookDB.GetContext().GetUsersAsync().Result.FirstOrDefault(i => i.Id == id));
        }

        public static void DeleteFileUserId()
        {
            File.Delete(CurUserPath);
        }

        public static bool IsFileWithUserExist()
        {
            return File.Exists(CurUserPath);
        }
    }
}
