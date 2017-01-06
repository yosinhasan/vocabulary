using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Models
{
    /// <summary>Translation</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class Translation : Entity
    {
        public string Text { get; set; }
        public string Transript { get; set; }
        public long LangId { get; set; }
        public long WordId { get; set; }


        public override string ToString()
        {
            return "[Translation: " + Text + ", Transcript: " + Transript + "]";
        }
    }
}
