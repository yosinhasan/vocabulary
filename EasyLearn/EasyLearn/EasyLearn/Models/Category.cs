using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EasyLearn.Models
{
    /// <summary>Category model</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public class Category
    {
        public string Label { get; set; }
        public ImageSource Image { get; set; }
        public Type TargetType { get; set; }

    }
}
