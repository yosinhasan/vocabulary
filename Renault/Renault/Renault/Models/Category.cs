using System;
using Xamarin.Forms;

namespace Renault.Models
{
    /// <summary>Category model.</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class Category
    {
        public ImageSource image { get; set; }
        public string label { get; set; }
        public Type targetType { get; set; }
    }
}
