using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Models
{
    /// <summary>File type</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public enum FileType
    {
        DIRECTORY, FILE
    }
    /// <summary>File data</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class FileData
    {
        public String Name { get; set; }
        public String Path { get; set; }
        public FileType Type { get; set; }
    }
}
