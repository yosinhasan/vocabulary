using EasyLearn.Helpers;
using EasyLearn.Models;
using EasyLearn.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.DB
{
    public sealed class Seeder
    {

        public static async Task startLanguagePopulation(LanguageManager manager)
        {
            int count = await manager.count();
            if (count == 0)
            {
                List<Language> list = new List<Language>();
                list.Add(new Language()
                {
                    Name = "English",
                    Current = Constants.ACTIVE,
                    CurrentTranslation = Constants.INACTIVE
                });
                list.Add(new Language()
                {
                    Name = "Russian",
                    Current = Constants.INACTIVE,
                    CurrentTranslation = Constants.ACTIVE
                });
                list.Add(new Language()
                {
                    Name = "French",
                    Current = Constants.INACTIVE,
                    CurrentTranslation = Constants.INACTIVE
                });
                await manager.createMultiple(list);
            }
          


        }
    }
}
