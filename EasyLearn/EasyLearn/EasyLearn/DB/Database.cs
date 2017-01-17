using EasyLearn.Interfaces;
using SQLite;
using Xamarin.Forms;

namespace EasyLearn.DB
{
    /// <summary>Sqlite Database class</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public sealed class Database
    {
        private SQLiteAsyncConnection connection;
        private static Database db;
        private Database()
        {
            connection = DependencyService.Get<ISQLite>().GetConnection();
        }
        /// <summary>
        /// Gets the instance of the database class. 
        /// </summary>
        /// <returns>The instance of database</returns>
        public static Database getDatabase()
        {
            if (db == null)
            {
                db = new Database();
            }
            return db;
        }
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <returns>The database connection</returns>
        public SQLiteAsyncConnection getConnection()
        {
            return db.connection;
        }
   
    }
}
