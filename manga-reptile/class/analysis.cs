using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace manga_reptile
{
    class JinMan : analysis
    {
        public JinMan(string url)
        {
            //获取链接
            this.url = url;
            //设置漫画网站名称
            this.webSiteName = "禁漫天堂";
            //设置漫画网站标识
            this.webSiteMark = "jinman";
            //设置漫画网站域名
            this.webSiteDomain = "18comic2.biz";
            //执行初始化方法
            this.init();
        }

        protected override ChapterItem get_chapter_images(string html)
        {
            Console.WriteLine(html);
            System.Environment.Exit(0);

            return new ChapterItem("","","",new List<string>());
        }

        /// <summary>
        /// 获取目录的所有页码
        /// 禁止天堂仅有一页目录
        /// 直接返回包含html 的list
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected override List<string> get_chapter_pages(string html)
        {
            List<string> list = new List<string>();

            //仅有一页 直接返回包含html list
            list.Add(html);

            //返回list
            return list;
        }

        /// <summary>
        /// 获取所有章节的链接
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected override List<string> get_chapter_url(string html)
        {
            List<string> list = new List<string>();

            //截取包含章节列表的部分
            string chapterBox = new Regex("(?<=btn-toolbar).+?(?=visible-lg)", RegexOptions.Singleline).Match(html).Value;

            //获取所有章节链接
            MatchCollection hrefList = new Regex("/photo/[\\d]+").Matches(chapterBox);

            foreach (Match m in hrefList)
            {
                //拼接处理,形成完整链接
                string url = "http://" + this.webSiteDomain + m.Value;
                //获取html代码
                string chapterHtml = this.get_html_by_request(url);

                list.Add(chapterHtml);
            }

            return list;
        }

        /// <summary>
        /// 获取漫画名称
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        protected override string get_manga_name(string html)
        {
            //筛选漫画名的正则
            Regex getName = new Regex("(?<=itemprop=\"name\"\\sclass=\"pull-left\">)[^<]+");

            //获取漫画名
            string mangaName = getName.Match(html).Value;

            //替换掉 换行符 等不能命名为文件夹的特殊字符
            mangaName = this.format_file_name(mangaName);

            return mangaName;
        }
    }
}
