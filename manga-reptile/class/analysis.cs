﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace manga_reptile
{
    class analysis
    {
        //漫画名称
        public string name;
        //漫画主页链接
        public string url;
        //漫画主页html代码
        private string html;
        //漫画网站名称
        public string webSiteName;
        //漫画网站标识
        public string webSiteMark;
        //漫画下载路径
        private string downloadRoute;
        //章节合集
        public List<ChapterItem> chapters = new List<ChapterItem>();
        //章节页合集
        public List<string> chapterPages = new List<string>();
        //章节链接合集-下载用
        private List<string> chapterUrls = new List<string>();

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="url">漫画首页的链接</param>
        analysis(string url)
        {
            this.url = url;
            this.downloadRoute = global.downloadRoute + "\\" + this.webSiteName + "\\";
            //设置网站标识
            global.website = this.webSiteMark;
            //获取漫画主页数据
            this.name = this.get_manga_name(html);
            //获取所有页码页面
            this.chapterPages = this.get_chapter_pages(url);
            //获取所有章节链接
            this.chapterPages.ForEach((string i) => { this.chapterUrls = chapterUrls.Union(this.get_chapter_url(i)).ToList(); });
            //获取章节图片
            this.chapterUrls.ForEach((string i) => { this.chapters.Add(this.get_chapter_images(i)); });
            //下载章节图片
            this.chapters.ForEach((ChapterItem i) => { this.download_chapter_images(i); });
            
        }

        /// <summary>
        /// 校验当前章节的图片数量
        /// </summary>
        /// <param name="chapter">当前章节的实体类</param>
        /// <returns>返回当前章节下载的错误数量</returns>
        private int check_chapter_files(ChapterItem chapter, string route)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 下载当前章节的图片
        /// </summary>
        /// <param name="chapter">当前章节的实体类</param>
        /// <returns>返回当前章节的图片数量</returns>
        private int download_chapter_images(ChapterItem chapter)
        {
            List<string> images = chapter.imageList;
            string route = this.downloadRoute + chapter.name;

            images.ForEach((string i) => {
                http_download(i, route);
            });

            this.check_chapter_files(chapter,route);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取页面内所有图片的链接
        /// </summary>
        /// <param name="html">当前漫画浏览页的html代码</param>
        /// <returns>当前页面所有图片的链接</returns>
        private ChapterItem get_chapter_images(string html)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有章节的链接
        /// </summary>
        /// <param name="html">当前漫画目录页的html代码</param>
        /// <returns>当前页面所有章节的链接</returns>
        private List<string> get_chapter_url(string html)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取章节分页
        /// 有些网站在漫画章节目录也会进行分页，使得其在章节首页只展出一部分内容
        /// 调用此方法获取全部的页面
        /// </summary>
        /// <param name="html">漫画首页的html代码</param>
        /// <returns>所有的页码的html代码</returns>
        private List<string> get_chapter_pages(string html)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取漫画名称
        /// </summary>
        /// <param name="html">漫画主页的html数据</param>
        /// <returns>漫画名称</returns>
        private string get_manga_name(string html)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据链接获取页面html代码-使用请求
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns>html代码</returns>
        private static string get_html_by_request(string url, string cookie = "", string referer = "")
        {
            var data = new byte[4];
            new Random().NextBytes(data);
            string ip = new IPAddress(data).ToString();

            Encoding encode = Encoding.GetEncoding("utf-8");
            try
            {
                //构造httpwebrequest对象，注意，这里要用Create而不是new
                HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(url);

                //伪造浏览器数据，避免被防采集程序过滤
                //wReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50215; CrazyCoder.cn;www.aligong.com)";
                wReq.UserAgent = "Mozilla/5.0 (Linux; Android 9; Mi Note 3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Mobile Safari/537.36";
                wReq.Accept = "*/*";
                wReq.KeepAlive = true;
                wReq.Headers.Add("cookie", cookie);

                wReq.Headers.Add("origin", referer);
                wReq.Headers.Add("x-requested-with", "XMLHttpRequest");

                wReq.Headers.Add("pragma", "no-cache");
                wReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                wReq.Headers.Add("sec-fetch-dest", "document");
                wReq.Headers.Add("sec-fetch-mode", "navigate");
                wReq.Headers.Add("sec-fetch-site", "none");
                wReq.Headers.Add("sec-fetch-user", "?1");

                //wReq.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; U; Android 9; zh-cn; Mi Note 3 Build/PKQ1.181007.001) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/71.0.3578.141 Mobile Safari/537.36 XiaoMi/MiuiBrowser/11.8.14");
                //wReq.Headers.Add("Pragma", "no-cache");
                //wReq.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                //wReq.Headers.Add("Connection", "Keep-Alive");
                //随机ip
                //wReq.Headers.Add("CLIENT-IP", ip);
                //wReq.Headers.Add("X-FORWARDED-FOR", ip);
                //注意，为了更全面，可以加上如下一行，避开ASP常用的POST检查              

                wReq.Referer = referer;//您可以将这里替换成您要采集页面的主页

                HttpWebResponse wResp = wReq.GetResponse() as HttpWebResponse;
                // 获取输入流
                System.IO.Stream respStream = wResp.GetResponseStream();

                System.IO.StreamReader reader = new System.IO.StreamReader(respStream, encode);

                string content = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();

                return content;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return "";
        }

        /// <summary>
        /// 根据链接获取页面html代码-使用浏览器
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns>html代码</returns>
        private static string get_html_by_browser(string url, string cookie = "", string referer = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        private static bool http_download(string url, string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);    //存在则删除
            }

            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp"; //临时文件
            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);    //存在则删除
            }
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                System.IO.File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
    /// <summary>
    /// 章节类
    /// name 章节名称
    /// url 章节链接
    /// imageList 章节中的图片列表 记录图片的下载链接
    /// </summary>
    class ChapterItem
    {
        public string name;
        public string url;
        public List<string> imageList;
    }
}
