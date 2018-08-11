using System.Collections.Generic;
namespace PasswordTracker
{
    public class FileOperations
    {

        private static FileOperationManager fileOperationManager;
         static FileOperations()
        {
            if(fileOperationManager==null)
            {
                fileOperationManager = new FileOperationManager();
            }
        }
        /// <summary>
        /// This method helps in the creation of database folder
        /// </summary>
        /// <returns></returns>
        public static bool CreateDatabaseFolder()
        {
            
            return fileOperationManager.CreateDataBaseFolder();
        }
        /// <summary>
        /// This method helps in the creation of table 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool CreateTable<T>()
        {
            if (CreateDatabaseFolder())
            {
                fileOperationManager.TableCreate<T>();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method helps in dropping a table from device database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int DropTable<T>()
        {
            if (CreateDatabaseFolder())
            {
                return fileOperationManager.DropTable<T>();
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// This method helps in deleting single table entry as per the query passed
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool DeleteSingleEntryFromTable(string tableName, string whereCondition)
        {
            return fileOperationManager.DeleteSingleEntryFromTable(tableName, whereCondition);
        }
        /// <summary>
        /// This method deletes all data from table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool DeleteFromDevice<T>()
        {
            return fileOperationManager.DeleteFromDevice<T>();
        }
        /// <summary>
        /// This methods helps in writing data to database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datavalue"></param>
        /// <returns></returns>
        public static bool WriteToDevice<T>(T datavalue)
        {
            return fileOperationManager.WriteToDevice(datavalue);
        }
        /// <summary>
        /// This method checks whether data is available in database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool DataExists<T>() where T : new()
        {
            return fileOperationManager.DataExists<T>();
        }
        /// <summary>
        /// This method will update the table in device database
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool UpdateToDevice(string query)
        {
            return fileOperationManager.UpdateToDevice(query);
        }
        public static List<T> ReadFromDevice<T>() where T : new()
        {
            return fileOperationManager.ReadFromDatabase<T>();
        }
        public static bool InsertOrReplace<T>(T dataValue)
        {
            return fileOperationManager.InsertOrReplace(dataValue);
        }
    }
}
