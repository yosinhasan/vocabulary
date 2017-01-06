using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Models;
using System.Collections.ObjectModel;

namespace EasyLearn.Services.Managers
{
    /// <summary>Vocabulary manager</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class VocabularyManager : GenericManager<Translation>
    {

        public VocabularyManager(SQLiteAsyncConnection connection) : base(connection)
        {
            this.connection.CreateTableAsync<Translation>();
        }

        public async Task<Translation> read(long id)
        {
            List<Translation> items = await connection.QueryAsync<Translation>("SELECT * FROM Translation WHERE Id = ? LIMIT 1", id);
            if (items.Count > 0)
            {
                Translation item = items[0];
                return item;
            }
            return null;
        }

        public async Task<Translation> readByWordId(long id, long translationId)
        {
            List<Translation> items = await connection.QueryAsync<Translation>("SELECT * FROM Translation WHERE WordId = ? AND LangId = ? LIMIT 1", id, translationId);
            if (items.Count > 0)
            {
                Translation item = items[0];
                return item;
            }
            return null;
        }

        public async Task<Translation> readByWordId(long id)
        {
            List<Translation> items = await connection.QueryAsync<Translation>("SELECT * FROM Translation WHERE WordId = ? LIMIT 1", id);
            if (items.Count > 0)
            {
                Translation item = items[0];
                return item;
            }
            return null;
        }

        public async Task<ObservableCollection<Translation>> readAll()
        {
            List<Translation> items = await connection.QueryAsync<Translation>("SELECT * FROM Translation");
            if (items.Count > 0)
            {
                return new ObservableCollection<Translation>(items);
            }
            return null;
        }

        public async Task<bool> delete(long id)
        {
            try
            {
                int i = await connection.ExecuteAsync("DELETE FROM Translation WHERE WordId = ?", id);
                return i > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<bool> delete(long id, long langId)
        {
            try
            {
                int i = await connection.ExecuteAsync("DELETE FROM Translation WHERE WordId = ? AND LangId = ?", id, langId);
                return i > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<Translation> readByWordIdAndLangId(long wordId, long langId)
        {
            try
            {
                List<Translation> items = await connection.QueryAsync<Translation>("SELECT * FROM Translation WHERE WordId = ? AND LangId = ? LIMIT 1", wordId, langId);
                if (items.Count > 0)
                {
                    Translation item = items[0];
                    return item;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
