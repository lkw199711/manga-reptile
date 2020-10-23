using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manga_reptile
{

    interface IAnalysis
    {
        
    }
    class analysis
    {
        IDictionary<string, string> imageItem = new Dictionary<string, string>();
        
    }
    /// <summary>
    /// 章节类
    /// name 章节名称
    /// imageList 章节中的图片列表 记录图片的下载链接
    /// </summary>
    class ChapterItem
    {
        public static string name;
        public List<string> imageList;
    }
}
