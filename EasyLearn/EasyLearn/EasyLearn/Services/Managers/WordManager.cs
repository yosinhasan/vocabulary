using EasyLearn.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections;

namespace EasyLearn.Services.Managers
{
    /// <summary>Word manager</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class WordManager : GenericManager<Word>
    {

        public WordManager(SQLiteAsyncConnection connection) : base(connection)
        {
            this.connection.CreateTableAsync<Word>();
        }

        public async Task<Word> read(long id)
        {
            try
            {
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE Id = ? LIMIT 1", id);
                if (items.Count > 0)
                {
                    Word item = items[0];
                    return item;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<Word> readByKeywordAndLangId(string keyword, long langId)
        {
            try
            {
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE Keyword = ? AND LangId = ? LIMIT 1", keyword, langId);
                if (items.Count > 0)
                {
                    Word item = items[0];
                    return item;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<IEnumerable> searchByKeywordAndLangId(string keyword, long langId, long translationLangId)
        {
            try
            {
                keyword = "%" + keyword + "%";
                List<Word> items = await connection.QueryAsync<Word>("SELECT Word.* FROM Word INNER JOIN Translation ON Word.Id = Translation.WordId WHERE Word.LangId = ? AND Translation.LangId = ? AND Word.Keyword LIKE ? ", langId, translationLangId, keyword);
                if (items.Count > 0)
                {
                    return new ObservableCollection<Word>(items);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<IEnumerable> searchByKeywordAndLangId(string keyword, long langId)
        {
            try
            {
                keyword = "%" + keyword + "%";
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE Keyword LIKE ? AND LangId = ? LIMIT 1", keyword, langId);
                if (items.Count > 0)
                {
                    return new ObservableCollection<Word>(items);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<IEnumerable> readAll(long id)
        {
            try
            {
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE LangId = ?", id);
                if (items.Count > 0)
                {
                    return new ObservableCollection<Word>(items);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<IEnumerable> readAllByCurrentLanguage(long langId, long translationLangId)
        {
            try
            {
                List<Word> items = await connection.QueryAsync<Word>("SELECT Word.* FROM Word INNER JOIN Translation ON Word.Id = Translation.WordId WHERE Word.LangId = ? AND Translation.LangId = ? ", langId, translationLangId);
                if (items.Count > 0)
                {
                    return new ObservableCollection<Word>(items);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<ObservableCollection<Word>> readAll()
        {
            try
            {
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word");
                if (items.Count > 0)
                {
                    return new ObservableCollection<Word>(items);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<bool> safeDelete(long id)
        {
            try
            {
                var c = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Translation  WHERE WordId = ? ", id);
                if (c > 0)
                {
                    return false;
                }
                else
                {
                    int i = await connection.ExecuteAsync("DELETE FROM Word WHERE Id = ?", id);
                    return i > 0;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
