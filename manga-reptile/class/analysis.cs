using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manga_reptile
{
    class JinMan : analysis
    {
        JinMan(string url)
        {
            //获取链接
            this.url = url;
            //设置漫画网站名称
            this.webSiteName = "禁漫天堂";
            //设置漫画网站标识
            this.webSiteMark = "jinman";
            //执行初始化方法
            this.init();
        }

        protected override ChapterItem get_chapter_images(string html)
        {
            throw new NotImplementedException();
        }

        protected override List<string> get_chapter_pages(string html)
        {
            throw new NotImplementedException();
        }

        protected override List<string> get_chapter_url(string html)
        {
            throw new NotImplementedException();
        }

        protected override string get_manga_name(string html)
        {
            throw new NotImplementedException();
        }
    }
}
