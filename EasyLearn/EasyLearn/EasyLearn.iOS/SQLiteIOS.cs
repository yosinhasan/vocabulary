using System;
using Xamarin.Forms;
using EasyLearn.iOS;
using System.IO;
using SQLite;
using EasyLearn.Interfaces;

[assembly: Dependency (typeof (SQLiteIOS))]

namespace EasyLearn.iOS
{
	public class SQLiteIOS : ISQLite
	{
		public SQLiteIOS()
		{
		}

		#region ISQLite implementation
		public SQLiteAsyncConnection GetConnection ()
		{
			var sqliteFilename = "EasyLearnSQLite.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
            var conn = new SQLiteAsyncConnection(path);
            return conn;
		}
		#endregion
	}
}
