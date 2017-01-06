using System;
using EasyLearn;
using Xamarin.Forms;
using EasyLearn.Droid;
using System.IO;
using SQLite;
using EasyLearn.Interfaces;

[assembly: Dependency (typeof (SQLite_Android))]

namespace EasyLearn.Droid
{
	public class SQLite_Android : ISQLite
	{
		public SQLite_Android ()
		{
		}

		#region ISQLite implementation
		public SQLiteAsyncConnection GetConnection()
        {
			var sqliteFilename = "EasyLearnSQLite.db3";
			string documentsPath = Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);

            var conn = new SQLiteAsyncConnection(path);
            return conn;
		}
        #endregion
	}
}
