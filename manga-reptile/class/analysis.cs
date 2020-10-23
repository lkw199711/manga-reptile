using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manga_reptile
{
    class analysis : IAnalysis
    {
        public int check_chapter_files(ChapterItem chapter)
        {
            throw new NotImplementedException();
        }

        public int download_chapter_images(ChapterItem chapter)
        {
            throw new NotImplementedException();
        }

        public List<string> get_chapter_images(string html)
        {
            throw new NotImplementedException();
        }

        public List<string> get_chapter_list(string html)
        {
            throw new NotImplementedException();
        }

        public List<string> get_chapter_pages(string html)
        {
            throw new NotImplementedException();
        }

        public string get_html_by_browser(string url)
        {
            throw new NotImplementedException();
        }

        public string get_html_by_request(string url)
        {
            throw new NotImplementedException();
        }
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
