using SQLite;

namespace EasyLearn.Interfaces
{
    /// <summary>Connection interface</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public interface ISQLite
	{
        /// <summary>
        /// Gets the implemented connection for respective platform.
        /// </summary>
        /// <returns>Connection for database</returns>
		SQLiteAsyncConnection GetConnection();
	}
}

