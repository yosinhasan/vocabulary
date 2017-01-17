using SQLite;
namespace EasyLearn.Models
{
    /// <summary>Language</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class Language : Entity
    {
        [Unique]
        public string Name { get; set; }
        public int Current { get; set; }
        public int CurrentTranslation { get; set; }
        public override string ToString()
        {
            return "[Language: " + Name + " ]";
        }

    }
}
