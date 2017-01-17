using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Models
{
    /// <summary>Word</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class Word : Entity
    {
        //  [Unique]
        [JsonProperty("word")]
        public string Keyword { get; set; }
        [JsonIgnore]
        public long LangId { get; set; }
        [JsonIgnore]
        public long TranslationLangId { get; set; }
        [JsonProperty("translation")]
        public string Text { get; set; }
        [JsonProperty("transcript")]
        public string Transript { get; set; }
        public override string ToString()
        {
            return "[Word: " + Keyword + " Translation: " + Text +"]";
        }


    }
}
