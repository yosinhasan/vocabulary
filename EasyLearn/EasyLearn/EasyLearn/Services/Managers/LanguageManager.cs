using EasyLearn.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Services.Managers
{
    /// <summary>Language manager</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class LanguageManager : GenericManager<Language>
    {

        public LanguageManager(SQLiteAsyncConnection connection) : base (connection)
        {
            this.connection.CreateTableAsync<Language>();
        }
        public async Task<Language> read(long id)
        {
            List<Language> items = await connection.QueryAsync<Language>("SELECT * FROM Language WHERE Id = ? LIMIT 1", id);
            if (items.Count > 0)
            {
                Language item = items[0];
                return item;
            }
            return null;
        }

        public async Task<ObservableCollection<Language>> readAll()
        {
            List<Language> items = await connection.QueryAsync<Language>("SELECT * FROM Language");
            if (items.Count > 0)
            {
                return new ObservableCollection<Language>(items);
            }
            return null;
        }

        public async Task<ObservableCollection<Language>> readCurrent()
        {
            List<Language> items = await connection.QueryAsync<Language>("SELECT * FROM Language WHERE Current = 1 OR CurrentTranslation = 1");
            if (items.Count > 0)
            {
                return new ObservableCollection<Language>(items);
            }
            return null;
        }

        public async Task<bool> resetFlags()
        {
            try
            {
                int i = await connection.ExecuteAsync("UPDATE Language SET Current = 0, CurrentTranslation = 0");
                return i > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }


    }
}
