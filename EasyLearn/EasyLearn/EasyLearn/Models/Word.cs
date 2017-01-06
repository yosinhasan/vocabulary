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
        public string Keyword { get; set; }
        public long LangId { get; set; }

        public override string ToString()
        {
            return "[Translation: " + Keyword + "]";
        }
    }
}
