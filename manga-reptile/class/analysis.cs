using lkw;
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
        Lkw lkw = new Lkw();
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

        protected override ChapterItem get_chapter_images(string url)
        {
            List<string> list = new List<string>();
            //获取页面的html代码
            string html = this.get_html_by_request(url);
            //获取章节名称
            string chapterName = new Regex("(?<=pull-left\\shidden\">)[^<]+", RegexOptions.Singleline).Match(html).Value;
            chapterName = this.format_file_name(chapterName);

            //截取图片链接部分
            string imageBox = new Regex("(?<=row\\sthumb-overlay-albums).+?(?=tab-content\\sm-b-20\\sm-t-15)", RegexOptions.Singleline).Match(html).Value;

            //获取所有图片链接
            MatchCollection src = new Regex("(?<=data-original=\")http[^\"]+[.png|.jpg]*(?=\")", RegexOptions.Singleline).Matches(imageBox);

            //获取图片的扩展名
            string suffix = this.get_image_suffix(src[0].Value);

            foreach (Match m in src)
            {
                list.Add(m.Value);
            }

            /*
             生成章节实体类 (章节名,链接,图片扩展名,图片链接list)*/
            return new ChapterItem(chapterName, url, suffix, list);
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

            //获取所有章节内容
            MatchCollection chapterList = new Regex("<a\\shref.+?</a>", RegexOptions.Singleline).Matches(chapterBox);

            for (int i = 0, l = chapterList.Count; i < l; i++)
            {
                //章节内容
                string str = chapterList[i].Value;
                //章节链接
                string href = new Regex("/photo/[\\d]+").Match(str).Value;
                //章节名
                string name = new Regex("(?<=<li[^>]+>)[^<]+").Match(str).Value;

                name = this.format_file_name(name);

                //拼接处理,形成完整链接
                string url = "http://" + this.webSiteDomain + href;

                //解析所有页面时间过长 加入提示机制
                lkw.log("正在解析章节 " + i.ToString() + " " + name);

                list.Add(url);
                lkw.log(url);
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
