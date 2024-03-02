using lkw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static manga_reptile.FormIndex;

namespace manga_reptile
{

    class HMsingle : analysis
    {
        public FormIndex form;
        private TaskParams taskParams;
        Lkw lkw = new Lkw();

        private string targetDomain = "";

        string currentChapter = "";
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formIndex"></param>
        public HMsingle(string url, TaskParams taskParams, FormIndex formIndex)
        {
            this.form = formIndex;
            this.taskParams = taskParams;
            //获取链接
            this.url = url;
            //设置漫画网站名称
            this.webSiteName = "绅士漫画";
            //设置漫画网站标识
            this.webSiteMark = "hm";
            this.targetDomain = "hm07.lol";
            //设置漫画网站域名
            this.webSiteDomain = get_domain();

            if (this.webSiteDomain == "")
            {
                this.write_log("网站无法访问");
                return;
            }


            this.url = this.url.Replace(this.targetDomain, this.webSiteDomain);


            //执行初始化方法
            this.init();
        }

        /// <summary>
        /// 域名有很多 是动态的
        /// </summary>
        /// <returns></returns>
        public string get_domain()
        {
            for (int i = 1; i < 10; i++)
            {
                string webSiteDomain = $"hm{i.ToString("D2")}.lol";
                string url = $"https://www.{webSiteDomain}/albums-index-cate-20.html";

                string res = get_html_by_request(url);

                if (res != "") return webSiteDomain;
            }

            return "";
        }

        protected override ChapterItem get_chapter_images(ChapterItem item)
        {
            List<string> list = new List<string>();
            //获取页面的html代码
            this.show_message("正在获取章节\"" + item.name + " 图片.");
            this.currentChapter = item.name;

            string html = this.get_browser_html(item.url,item.imageNum,0);
            //string html = this.get_html_by_request(item.url);

            //添加第一页
            var listOne = get_subpage_images(html);
            list.AddRange(listOne);

            //截取页码部分
            string pageBox = new Regex("(?<=paginator).+?(?=f_right)", RegexOptions.Singleline).Match(html).Value;

            //通过后一页的方式
            string nextPage = new Regex("(?<=href=\").+?(?=\">後頁)", RegexOptions.Singleline).Match(pageBox).Value;
            if (nextPage != "")
            {
                list = this.get_nextpage_images(html, item.imageNum, list);
                item.images = list;
                return item;
            }


            

            //获取页码链接
            MatchCollection src = new Regex("(?<=href=\").+?(?=\")", RegexOptions.Singleline).Matches(pageBox);

            foreach (Match m in src)
            {
                if (m.Value == "/themes/weitu/images/bg/shoucang.jpg") continue;
                string page = "https://" + this.webSiteDomain+ m.Value;

                string html2 = this.get_browser_html(page, item.imageNum, list.Count);
                var listTwo = get_subpage_images(html2);
                list.AddRange(listTwo);
            }

            item.images = list;

            /*
             生成章节实体类 (章节名,链接,图片扩展名,图片链接list)*/
            return item;
        }

        protected List<string> get_nextpage_images(string html,int imageNum, List<string> list)
        {
            //截取页码部分
            string pageBox = new Regex("(?<=paginator).+?(?=f_right)", RegexOptions.Singleline).Match(html).Value;

            string nextPage = new Regex("(?<=next\"><a\\shref=\").+?(?=\">後頁)", RegexOptions.Singleline).Match(pageBox).Value;
            
            if (nextPage != "")
            {
                string page = "https://" + this.webSiteDomain + nextPage;
                string html2 = this.get_browser_html(page, imageNum, list.Count);

                list.AddRange(get_subpage_images(html2));

                if(new Regex("(?<=href=\").+?(?=\">後頁)", RegexOptions.Singleline).Match(html2).Value != "")
                {
                    return get_nextpage_images(html2, imageNum, list);
                }

                return list;
            }

            return list;
        }
        protected List<string> get_subpage_images(string html) { 
            List<string> list = new List<string>();

            // 获取图片链接临时前缀
            string textPrefix = this.taskParams.prefix;

            //截取图片链接部分
            string imageBox = new Regex("(?<=gallary_wrap).+?(?=comment_wrap)", RegexOptions.Singleline).Match(html).Value;

            //获取所有图片链接
            //MatchCollection src = new Regex("(?<=src=\").+?(?=\")", RegexOptions.Singleline).Matches(imageBox);
            MatchCollection src = new Regex("(?<=gallary_item).+?(?=pic_ctl)", RegexOptions.Singleline).Matches(imageBox);

            foreach (Match m in src)
            {
                string str = m.Value;
                string view = new Regex("(?<=src=\").+?(?=\")", RegexOptions.Singleline).Match(str).Value;
                string imgTag = new Regex("(?<=data/t)\\/(\\d+\\/)(\\d+\\/)(?=\\b+)").Match(view).Value;
                string prefix = new Regex("t.+?\\/data").Match(view).Value;
                string suffix = Path.GetExtension(view);

                // 使用临时前缀
                if (textPrefix != "") prefix = textPrefix;

                string imgName = new Regex("(?<=name\\stb\">).+?(?=<)").Match(str).Value;
                string img = "https://" + Regex.Replace(prefix, "^t", "img") + imgTag + imgName + suffix;

                list.Add(img);
            }

            return list;
        
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

            //首先添加第一页
            list.Add(html);

            string pageBox = new Regex("(?<=thispage).+?(?=/div)", RegexOptions.Singleline).Match(html).Value;

            if(pageBox=="") return list;

            MatchCollection pages = new Regex("(?<=href=\").+?(?=\")", RegexOptions.Singleline).Matches(pageBox);

            foreach (Match m in pages)
            {
                string cUrls = "https://" + this.webSiteDomain + m.Value;
                string cHTML = this.get_html_by_request(cUrls);

                list.Add(cHTML);
            }

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
            string chapterBox = new Regex("(?<=gallary_wrap).+?(?=bot_toolbar)", RegexOptions.Singleline).Match(html).Value;

            //获取所有章节内容
            MatchCollection chapterList = new Regex("(?<=<li).+?(?=</li>)", RegexOptions.Singleline).Matches(chapterBox);
            //MatchCollection chapterList = new Regex("<a\\shref.+?</a>", RegexOptions.Singleline).Matches(chapterBox);

            for (int i = 0, l = chapterList.Count; i < l; i++)
            {
                //章节内容
                string str = chapterList[i].Value;
                
                //章节链接
                string href = new Regex("/photos-index-aid-[\\d]+.html").Match(str).Value;
                //章节名
                string name = new Regex("(?<=title=\").+?(?=\")").Match(str).Value;
                name = new Regex("<[^>]+>").Replace(name, "");

                name = this.format_file_name(name);

                if (Directory.Exists(this.downloadRoute + name))
                {
                    lkw.log("存在 跳过" + name);
                    continue;
                }
                
                // 正则排除章节
                string chapterExcludes = this.taskParams.chapterExcludes;

                if (chapterExcludes != "" && new Regex(chapterExcludes).IsMatch(name))
                {
                    lkw.log("跳过"+ name);
                    continue;
                }

                //图片数量
                string imageNum = new Regex("[\\d]+(?=張圖片)").Match(str).Value;

                //拼接处理,形成完整链接
                string url = "https://" + this.webSiteDomain + href;

                //解析所有页面时间过长 加入提示机制
                show_message("正在解析章节 " + i.ToString() + " " + name);

                list.Add(new ChapterItem(url: url, name: name, imageNum: int.Parse(imageNum)));
            }

            return list;
        }

        protected override string get_manga_name(string name) { return this.taskParams.title; }
        /// <summary>
        /// 输出消息信息
        /// </summary>
        /// <param name="msg">消息内容</param>
        protected override void show_message(string msg)
        {
            FormIndex.set_label_text(this.form.labelMessage, msg);
        }

        public string get_browser_html(string url,int imageNum, int baseNum)
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

                if (html.Length > 1000 && check_html_ready(html, imageNum, baseNum) && html.Length==oldHtml.Length) {
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
        public bool check_html_ready(string html,int num,int baseNum)
        {
            //截取图片链接部分
            string imageBox = new Regex("(?<=gallary_wrap).+?(?=comment_wrap)", RegexOptions.Singleline).Match(html).Value;

            //获取所有图片链接
            MatchCollection src = new Regex("(?<=src=\").+?(?=\")", RegexOptions.Singleline).Matches(imageBox);
            this.show_message("正在解析章节 " + this.currentChapter  + (src.Count + baseNum).ToString() + "/" + num.ToString());
            return src.Count+ baseNum >= num || src.Count>=12;
        }
        public new void download()
        {
            this.chapters.ForEach((ChapterItem i) => { this.download_chapter_images(i); });

            this.show_message("全章节\"" + " 下载完毕");
        }

        /// <summary>
        /// 下载当前章节的图片
        /// </summary>
        /// <param name="chapter">当前章节的实体类</param>
        /// <returns>返回当前章节的图片数量</returns>
        protected new int download_chapter_images(ChapterItem chapter)
        {
            //图片下载链接
            List<string> images = chapter.images;
            //下载路径
            string route = this.downloadRoute + chapter.name + "\\";
            //后缀名
            string suffix = chapter.suffix;

            //执行下载
            for (int i = 0, l = images.Count; i < l; i++)
            {
                //输出提示信息
                this.show_message("正在下载章节\"" + chapter.name + "+\"" + "第" + (i.ToString()+"/"+ images.Count.ToString()) + "张图片.");

                string saveName = route + Path.GetFileName(images[i]);

                if (System.IO.File.Exists(saveName))
                {
                    continue;
                }

                //下载图片
                download_image_by_http(images[i], saveName);
            }

            this.delete(route + "temp");

            this.show_message("章节\"" + chapter.name + " 下载完毕");

            this.write_log($"下载章节 {chapter.name}, 共{images.Count.ToString()}张图片.");

            //返回图片数量
            return images.Count;
        }
    }
}
