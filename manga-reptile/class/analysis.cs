using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manga_reptile
{
    class analysis : IAnalysis
    {
        //漫画名称
        public static string name = "";
        //漫画主页链接
        public static string url = "";
        //漫画网站标识
        public static string webStieMark = "";
        //章节合集
        public List<ChapterItem> chapters = new List<ChapterItem>();
        //章节页合集
        public List<string> chapterPages = new List<string>();

        analysis()
        {
            
        }
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
