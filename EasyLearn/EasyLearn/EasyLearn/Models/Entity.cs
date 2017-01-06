using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Models
{
    /// <summary>Entity</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public abstract class Entity
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

    }
}
