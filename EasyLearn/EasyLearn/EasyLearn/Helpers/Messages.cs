using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Helpers
{
    /// <summary>Messages</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public static class Messages
    {
        public static readonly string FIELD_EMPTY = "field can not be empty";

        public static readonly string LANGUAGE_ITEM_NOT_SELECTED = "Please choose language mode in settings -> choose mode";
        public static readonly string FILE_ITEM_NOT_SELECTED = "Please choose file to import";

        public static readonly string LANGUAGES_MUST_NOT_MATCH = "selected languages must not match";

        public static readonly string SAVE_DATA = "Do you want to save data?";

        public static readonly string DELETE_ACTION = "Do you want to delete the data?";

        public static readonly string WORD_NOT_FOUND = "No word found";
        public static readonly string TRANSLATION_NOT_FOUND = "No translation found";
        public static readonly string LANGUAGE_IS_USED = "Language is selected for language mode";
        public static readonly string WORD_EXISTS = "Cannot save the data, the given word exists";
        public static readonly string REPLACE_EXISTS = "The file with given name exists, do you want to replace it?";
        internal static readonly string NOTHING_TO_EXPORT = "Nothing to export, the vocabulary data is empty";
        internal static readonly string IMPORTED = "The data is imported";
        internal static readonly string NOTHING_TO_IMPORT = "Nothing to import, the file might have dublicate data";
        internal static readonly string EXPORT_DATA = "Do you want to export the data?";
        internal static readonly string EXPORTED_DATA = "The data is exported";
    }
}
