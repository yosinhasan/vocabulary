using EasyLearn.Interfaces;
using EasyLearn.Models;
using EasyLearn.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Services
{
    /// <summary>Local service</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public sealed class LocalService
    {
        private static LocalService _LocalService;
        public Language Current { get; set; }
        public Language CurrentTranslation { get; set; }


        private LocalService()
        {
            Database db = Database.getDatabase();
            LanguageManager = new LanguageManager(db.getConnection());
            VocabularyManager = new VocabularyManager(db.getConnection());
            WordManager = new WordManager(db.getConnection());
        }
        public async Task<bool> SetUp()
        {
            bool cond = false;
            var items = await SqliteService.LanguageManager.readCurrent();
            if (items != null && items.Count == 2)
            {
                Current = items[0];
                CurrentTranslation = items[1];
                cond = true;
            }
            return cond;
        }
        public LanguageManager LanguageManager
        {
            get;
            private set;
        }

        public VocabularyManager VocabularyManager
        {
            get;
            private set;
        }

        public WordManager WordManager
        {
            get;
            private set;
        }

        public static LocalService SqliteService
        {
            get
            {
                return _LocalService ?? (_LocalService = new LocalService());
            }
            private set { }

        }

    }
}
