using EasyLearn.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections;
using FormsToolkit;
using EasyLearn.Helpers;

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
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
            }
            return null;
        }
        public async Task<bool> isExists(string keyword, long langId, long translationLangId)
        {
            try
            {
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE Keyword = ? AND LangId = ? AND TranslationLangId = ? ", keyword, langId, translationLangId);
                if (items.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
            }
            return false;
        }
        public async Task<bool> isExists(Word word)
        {
            try
            {
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE Keyword = ? AND LangId = ? AND TranslationLangId = ? ", word.Keyword, word.LangId, word.TranslationLangId);
                if (items.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
            }
            return false;
        }
        public async Task<IEnumerable> searchByKeywordAndLangId(string keyword, long langId, long translationLangId)
        {
            try
            {
                keyword = "%" + keyword + "%";
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE LangId = ? AND TranslationLangId = ? AND (Keyword LIKE ? OR Text LIKE ?) ", langId, translationLangId, keyword, keyword);
                if (items.Count > 0)
                {
                    return new ObservableCollection<Word>(items);
                }
            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
            }
            return null;
        }

        public async Task<IEnumerable> searchByKeywordAndLangId(string keyword, long langId)
        {
            try
            {
                keyword = "%" + keyword + "%";
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE Keyword LIKE ? AND LangId = ? ", keyword, langId);
                if (items.Count > 0)
                {
                    return new ObservableCollection<Word>(items);
                }
            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
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
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
            }
            return null;
        }
        public async Task<IEnumerable> readAllByCurrentLanguage(long langId, long translationLangId)
        {
            try
            {
                List<Word> items = await connection.QueryAsync<Word>("SELECT * FROM Word WHERE LangId = ? AND TranslationLangId = ? ", langId, translationLangId);
                if (items.Count > 0)
                {
                    return new ObservableCollection<Word>(items);
                }
            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });

            }
            return null;
        }

        public async Task<bool> deleteByLangId(Language item)
        {
            try
            {
                int i = await connection.ExecuteAsync("DELETE FROM Word WHERE LangId = ? OR TranslationLangId = ? ", item.Id, item.Id);
                return i > 0;
            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
            }
            return false;
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
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
                {
                    Title = Titles.ERROR,
                    Message = ex.Message,
                    Cancel = Titles.CANCEL
                });
            }
            return null;
        }

    }
}
