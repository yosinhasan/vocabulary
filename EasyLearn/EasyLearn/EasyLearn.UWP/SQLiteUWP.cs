using System.IO;
using EasyLearn.UWP;
using Xamarin.Forms;
using Windows.Storage;
using EasyLearn.Interfaces;
using SQLite;
using System;

[assembly: Dependency(typeof(SQLiteUWP))]

namespace EasyLearn.UWP
{
    public class SQLiteUWP : ISQLite
	{
		public SQLiteUWP ()
		{
		}

        #region ISQLite implementation

        public SQLiteAsyncConnection GetConnection()
        {
            var sqliteFilename = "EasyLearnSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);

            var conn = new SQLiteAsyncConnection(path);

            // Return the database connection 
            return conn;
        }
        #endregion
    }
}

