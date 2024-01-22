using lkw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static manga_reptile.FormIndex;

namespace manga_reptile
{

    class Jiujiu : analysis
    {
        public FormIndex form;
        private TaskParams taskParams;

        Lkw lkw = new Lkw();

        string currentChapter = "";

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formIndex"></param>
        public Jiujiu(string url, TaskParams taskParams, FormIndex formIndex)
        {
            this.form = formIndex;
            this.taskParams = taskParams;
            //获取链接
            this.url = url;
            //设置漫画网站名称
            this.webSiteName = "久久漫画";
            //设置漫画网站标识
            this.webSiteMark = "jiujiu";
            //设置漫画网站域名
            this.webSiteDomain = "mxs6.cc";
            //执行初始化方法
            this.init();
        }

        protected override ChapterItem get_chapter_images(ChapterItem item)
        {
            List<string> list = new List<string>();
            string suffix = ".jpg";
            //获取页面的html代码
            string html = this.get_browser_html(item.url);

            //截取图片链接部分
            string imageBox = new Regex("(?<=comicpage).+?(?=fanye)", RegexOptions.Singleline).Match(html).Value;

            //获取所有图片链接
            MatchCollection src = new Regex("(?<=data-original=\")http[^\"]+[.png|.jpg]*(?=\")", RegexOptions.Singleline).Matches(imageBox);

            //获取图片的扩展名
            if(src.Count>0)
                suffix = Path.GetExtension(src[0].Value);

            lkw.log(item.name);

            foreach (Match m in src)
            {
                lkw.log(m.Value);
                list.Add(m.Value);
            }

            item.images = list;
            item.suffix = suffix;

            /*
             生成章节实体类 (章节名,链接,图片扩展名,图片链接list)*/
            return item;
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
        protected override List<ChapterItem> get_chapter_url(string html)
        {
            List<ChapterItem> list = new List<ChapterItem>();

            //截取包含章节列表的部分
            string chapterBox = new Regex("(?<=tempc).+?(?=detail-fix-bottom)", RegexOptions.Singleline).Match(html).Value;

            //获取所有章节内容
            MatchCollection chapterList = new Regex("<a\\shref.+?</a>", RegexOptions.Singleline).Matches(chapterBox);

            for (int i = 0, l = chapterList.Count; i < l; i++)
            {
                //章节内容
                string str = chapterList[i].Value;
                //章节链接
                string href = new Regex("/chapter/[\\d]+").Match(str).Value;

                if (href == "") continue;

                //章节名
                string name = new Regex("(?<=title=\").+?(?=\")").Match(str).Value;

                name = this.format_file_name(name);

                if (Directory.Exists(this.downloadRoute + name))
                {
                    lkw.log("存在 跳过" + name);
                    continue;
                }

                if (name == "") continue;

                //拼接处理,形成完整链接
                string url = "http://" + this.webSiteDomain + href;

                //解析所有页面时间过长 加入提示机制
                show_message("正在解析章节 " + i.ToString() + " " + name);

                if(list.Count<8)
                list.Add(new ChapterItem(url: url, name: name));
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
            Regex getName = new Regex("(?<=normal-top-title>)[^<]+");

            //获取漫画名
            string mangaName = getName.Match(html).Value;

            //替换掉 换行符 等不能命名为文件夹的特殊字符
            mangaName = this.format_file_name(mangaName);

            return mangaName;
        }

        public string get_browser_html(string url)
        {
            this.form.webBrowser1.Navigate(url);
            string html = "";
            string oldHtml = "";
            int loop = 0;
            while (true)
            {
                this.form.Invoke(new Action(() =>
                {
                    html = this.form.GetWebPageHtml();
                }));

                if (html.Length > 1000 && check_html_ready(html, 4) && html.Length == oldHtml.Length)
                {
                    this.form.webBrowser1.Navigate("about:blank");
                    return html;
                }

                loop++;

                if (loop > 10)
                {
                    this.form.webBrowser1.Navigate("about:blank");
                    return html;

                }


                lkw.sleep(1000);

                oldHtml = html;
            }
        }
        public bool check_html_ready(string html, int num=1)
        {
            //截取图片链接部分
            string imageBox = new Regex("(?<=comicpage).+?(?=fanye)", RegexOptions.Singleline).Match(html).Value;

            //获取所有图片链接
            MatchCollection src = new Regex("(?<=data-original=\")http[^\"]+[.png|.jpg]*(?=\")", RegexOptions.Singleline).Matches(imageBox);
            this.show_message(src.Count.ToString() + "/" + num.ToString());
            return src.Count>num;
        }

        /// <summary>
        /// 输出消息信息
        /// </summary>
        /// <param name="msg">消息内容</param>
        protected override void show_message(string msg)
        {
            FormIndex.set_label_text(this.form.labelMessage, msg);
        }
    }
}