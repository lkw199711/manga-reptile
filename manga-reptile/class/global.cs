using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manga_reptile
{
    
    class global
    {
        public static string website;
        public static string downloadRoute= @"E:\9临时\1漫画\";
    }

    class utils
    {
        public static string format_file_name(string str)
        {
            return str.Replace("\n", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("_", "");
        }
    }
}
