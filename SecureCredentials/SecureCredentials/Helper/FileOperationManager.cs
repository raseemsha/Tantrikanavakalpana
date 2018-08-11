using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecureCredentials
{
    /// <summary>
    /// This class helps in sqllite data base call and updation
    /// </summary>
    public class FileOperationManager
    {
        // Internal Filepath      
        static string internalSqlLiteFilepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        static string folder = null;
        static string folderName="/PasswordTracker/";
        static string DBName = "PasswordTracker.db";
        Java.IO.File dir = null;
        public FileOperationManager()
        {
            dir = new Java.IO.File(internalSqlLiteFilepath + folderName);
            folder = dir.ToString();
        }
      /// <summary>
      /// This method create folder for creating database.The folder will be created in root folder of app
      /// </summary>
		public void CreateFolder()
        {            
            if (!dir.Exists())
                dir.Mkdirs();                      
        }
        public bool CreateDataBaseFolder() 
        {
            CreateFolder();
            return true;
        }
        /// <summary>
        /// This method creates database table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int TableCreate<T>()
        {
            
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName)))
                {
                    return connection.CreateTable<T>();
                }
           
            
        }
        /// <summary>
        /// This method drops table from database 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int DropTable<T>()
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName)))
            {
                return connection.DropTable<T>();
            }
        }
        /// <summary>
        /// This method deletes data from table 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool DeleteFromDevice<T>()
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName)))
            {
                connection.DeleteAll<T>();
                return true;
            }
        }
        /// <summary>
        /// This method deletes single table entry as per the query passed
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool DeleteSingleEntryFromTable(string tableName, string whereCondition)
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName)))
            {
                try
                {
                    string query = string.Format("delete from {0} where {1}", tableName, whereCondition);
                    return connection.Query<bool>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// This method checks whether data is present in passed table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool DataExists<T>() where T : new()
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName)))
            {
                if (connection.Query<T>("select * from "+ typeof(T).Name).Count > 0) // Local Db has data
                {
                    return true;
                }
                else // Local Db is empty
                {
                    return false;
                }
            }
        }   
        /// <summary>
        /// This method writes data to device database table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        public bool WriteToDevice<T>(T dataValue) 
        {
            var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName));
            connection.Insert(dataValue);
            return true;
        }
        /// <summary>
        /// This method update record of a table in device database
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool UpdateToDevice(string query) 
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName)))
            {
                try
                {
                    return connection.Query<bool>(query).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public List<T> ReadFromDatabase<T>() where T : new()
        {
            using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName)))
            {
                return connection.Query<T>("select * from " + typeof(T).Name).ToList(); // Local Db has data
            }
        }
        public bool InsertOrReplace<T>(T dataValue)
        {
            var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DBName));
            connection.InsertOrReplace(dataValue);
            return true;
        }
    }
}