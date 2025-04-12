using lkw;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static manga_reptile.FormIndex;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Formatting = Newtonsoft.Json.Formatting;

namespace manga_reptile
{
    public partial class FormIndex : Form
    {
        Lkw lkw = new Lkw();
        public bool running = false;

        /// <summary>
        /// 窗口初始化
        /// </summary>
        public FormIndex()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormIndex_Load(object sender, EventArgs e)
        {
            config_load();

            task_run();

            //timerSubscribe.Start();

            // 设置默认值
            comWebset.Text = "绅士漫画(hm07.lol)";
        }

        public void config_load()
        {
            // 读取 JSON 文件
            string json = File.ReadAllText("./config.json");

            // 反序列化 JSON 到对象
            Config globalJson = JsonConvert.DeserializeObject<Config>(json);

            global.downloadRoute = globalJson.downloadRoute;
            global.subscribeLastRun = globalJson.subscribeLastRun;
            global.textPrefix = globalJson.textPrefix;
            if (globalJson.subscribeInterval > 0) global.subscribeInterval = globalJson.subscribeInterval;

            timerSubscribe.Enabled = globalJson.subscribeEnabeld;

            btnTimerStatus.Text = timerSubscribe.Enabled ? "关闭订阅" : "开启订阅";
        }

        public void config_update(string downloadRoute="",DateTime subscribeLastRun=default, bool subscribeEnabeld = true,int subscribeInterval=0)
        {
            if(downloadRoute == "")
            {
                downloadRoute = global.downloadRoute;
            }
            else
            {
                global.downloadRoute = downloadRoute;
            }

            if(subscribeLastRun == default)
            {
                subscribeLastRun = global.subscribeLastRun;
            }
            else
            {
                global.subscribeLastRun = subscribeLastRun;
            }

            if(subscribeInterval == 0)
            {
                subscribeInterval = global.subscribeInterval;
            }

            Config config = new Config(downloadRoute, subscribeLastRun, subscribeEnabeld, subscribeInterval);

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);

            // 将格式化后的 JSON 写入文本文件
            File.WriteAllText("./config.json", json);
        }

        /// <summary>
        /// 调试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTest_Click(object sender, EventArgs e)
        {
            lkw.msbox(get_domain());
        }

        public string get_domain()
        {
            for (int i = 1; i < 10; i++)
            {
                string webSiteDomain = $"hm{i.ToString("D2")}.lol";
                string url = $"https://www.{webSiteDomain}/albums-index-cate-20.html";

                lkw.log(url);

                string res = get_html_by_request(url);

                if (res != "") return webSiteDomain;
            }

            return "";
        }

        /// <summary>
        /// 通过委托事件 更改label控件的文本
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        public static void set_label_text(Label label, string text)
        {
            Action<String> AsyncUIDelegate = delegate (string n) { label.Text = n; };//定义一个委托

            label.Invoke(AsyncUIDelegate, new object[] { text });

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("about:blank");
            webBrowser1.Navigate(textUrl.Text);

        }

        public string GetWebPageHtml()
        {
            if (webBrowser1.Document != null)
            {
                try{
                    return webBrowser1.Document.Body.OuterHtml;
                }catch (Exception ex) {
                    return "";
                }
                
            }
            else
            {
                return string.Empty;
            }
        }

        private void btnSingleDownload_Click(object sender, EventArgs e)
        {
            HMsingle jiman = null;

            string url = textUrl.Text;
            string name = textMangaName.Text;


            //新线程下载
            lkw.NewWork(() => {
                jiman = new HMsingle(url, create_params(), this);
                jiman.download();

                jiman.download();
            });
        }

        private void butJiu_Click(object sender, EventArgs e)
        {
            Jiujiu jiujiu = null;

            string url = textUrl.Text;
            string name = textMangaName.Text;


            //新线程下载
            lkw.NewWork(() => {
                jiujiu = new Jiujiu(url, create_params(), this);
                jiujiu.download();

                jiujiu.download();
            });
        }

        private void labelMessage_Click(object sender, EventArgs e)
        {

        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            task_add();
        }

        public class Config
        {
            public string downloadRoute;
            public DateTime subscribeLastRun;
            public bool subscribeEnabeld;
            public int subscribeInterval;
            public string textPrefix;

            public Config(string downloadRoute, DateTime subscribeLastRun, bool subscribeEnabeld = true, int subscribeInterval = 10)
            {
                this.downloadRoute = downloadRoute;
                this.subscribeLastRun = subscribeLastRun;
                this.subscribeEnabeld = subscribeEnabeld;
                this.subscribeInterval = subscribeInterval;
                this.textPrefix = global.textPrefix;
            }   
        }

        public TaskParams create_params()
        {
            return new TaskParams(
                url: textUrl.Text,
                title:textMangaName.Text,
                prefix:textPrefix.Text,
                website:comWebset.Text,
                chapterIncludes: textChapterIncludes.Text,
                chapterExcludes: textChapterExcludes.Text,
                imageIncludes: textImageIncludes.Text,
                imageExcludes: textImageExcludes.Text
            );
        }

        public class TaskJson
        {
            public List<TaskParams> tasks;

            public TaskJson( List<TaskParams> tasks)
            {
                this.tasks = tasks;
            }
        }

        public class TaskParams
        {
            public string url;
            public string title;
            public string prefix;
            public string website;
            public string chapterIncludes;
            public string chapterExcludes;
            public string imageIncludes;
            public string imageExcludes;

            public TaskParams(string url, string title, string prefix,string website,string chapterIncludes,string chapterExcludes,
                string imageIncludes,string imageExcludes)
            {
                this.title = title;
                this.prefix = prefix;
                this.url = url;
                this.website = website;
                this.chapterIncludes = chapterIncludes;
                this.chapterExcludes = chapterExcludes;
                this.imageIncludes=imageIncludes;
                this.imageExcludes=imageExcludes;
            }
        }

        public List<TaskParams> task_load() {
            // 读取 JSON 文件
            string json = File.ReadAllText("./task.json");

            // 反序列化 JSON 到对象
            List<TaskParams> taskJson = JsonConvert.DeserializeObject<List<TaskParams>>(json);

            return taskJson;
        }

        public void task_add(TaskParams task=null,List<TaskParams> taskArr=null) {
            // 读取 JSON 文件
            string json = File.ReadAllText("./task.json");

            // 反序列化 JSON 到对象
            List<TaskParams> taskJson = JsonConvert.DeserializeObject<List<TaskParams>>(json);


            if (taskArr!=null) taskJson.AddRange(taskArr);

            task = task == null ? create_params() : task;
            // 添加任务
            taskJson.Add(task);

            // 写入json
            task_write_json(taskJson);

            // 执行任务
            task_run();
        }

        public void task_run()
        {
            var tasks = task_load();

            if (tasks.Count == 0) { return; }

            if (running) return;

            running = true;

            TaskParams task = tasks[0];

            lkw.NewWork(() =>
            {
                if (task.website == "绅士漫画(hm07.lol)")
                {
                    HMsingle jiman = new HMsingle(task.url, task, this);

                    jiman.download();

                    jiman.get_cover();
                }

                if (task.website == "绅士漫画-仿真下载(hm07.lol)")
                {
                    HM jiman = new HM(task.url, task, this);

                    jiman.download();

                    jiman.download();
                }

                if (task.website == "久久漫画(freexcomic.com)")
                {
                    Jiujiu jiujiu = new Jiujiu(task.url, task, this);

                    jiujiu.download();

                    jiujiu.download();
                }

                // 任务结束
                running = false;

                // 重新读取任务列表
                tasks = task_load();

                // 移除任务
                tasks.RemoveAt(0);

                // 写入json
                task_write_json(tasks);

                // 继续执行任务
                task_run();
            });
        }

        public void task_write_json(List<TaskParams> tasks)
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);

            // 将格式化后的 JSON 写入文本文件
            File.WriteAllText("./task.json", json);
        }

        public void subscribe_add(TaskParams task) {
            string json;
            List<TaskParams> taskJson = new List<TaskParams>();

            if (File.Exists("./subscribe.json"))
            {
                // 读取 JSON 文件
                json = File.ReadAllText("./subscribe.json");

                // 反序列化 JSON 到对象
                taskJson = JsonConvert.DeserializeObject<List<TaskParams>>(json);

                foreach(TaskParams item in taskJson)
                {
                    if (item.url == task.url) {
                        lkw.msbox("此订阅已添加,请查看订阅文件.");
                        return;
                    }
                }
            }

            taskJson.Add(task);

            json = JsonConvert.SerializeObject(taskJson, Formatting.Indented);

            // 将格式化后的 JSON 写入文本文件
            File.WriteAllText("./subscribe.json", json);

            // 添加订阅之后立即执行一次
            task_add(task);
        }

        public void subscribe_run()
        {
            // 获取两个时间点
            DateTime lastRun = global.subscribeLastRun; // 第一个时间点
            DateTime now = DateTime.Now; // 当前时间点

            // 两小时执行一次
            if ((now - lastRun).TotalHours < global.subscribeInterval) return;

            write_log("开始执行订阅任务");

            // 读取 JSON 文件
            string json = File.ReadAllText("./subscribe.json");

            // 反序列化 JSON 到对象
            List<TaskParams> taskJson = JsonConvert.DeserializeObject<List<TaskParams>>(json);

            task_add(taskArr: taskJson);

            config_update(subscribeLastRun: now, subscribeEnabeld:timerSubscribe.Enabled);
        }

        private void timerSubscribe_Tick(object sender, EventArgs e)
        {
            subscribe_run();
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            subscribe_add(create_params());
        }
        public void write_log(string msg)
        {
            msg = DateTime.Now.ToString() + " " + msg;

            Console.WriteLine(msg);
            lkw.WriteLine("./log.txt", msg);
        }

        private void btnTimerStatus_Click(object sender, EventArgs e)
        {
            timerSubscribe.Enabled = !timerSubscribe.Enabled;

            config_update(subscribeEnabeld: timerSubscribe.Enabled);

            if(timerSubscribe.Enabled)
            {
                labelMessage.Text = "已开启订阅事件";
                btnTimerStatus.Text = "关闭订阅";
            }
            else
            {
                labelMessage.Text = "已关闭订阅事件";
                btnTimerStatus.Text = "开启订阅";
            }
        }

        /// <summary>
        /// 根据链接获取页面html代码-使用请求
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <returns>html代码</returns>
        protected string get_html_by_request(string url, string cookie = "", string referer = "")
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
                write_log("请求html错误 " + url);
                write_log(ex.ToString());
            }
            return "";
        }

        private void btnHand_Click(object sender, EventArgs e)
        {
            write_log("开始执行订阅任务");

            // 获取两个时间点
            DateTime lastRun = global.subscribeLastRun; // 第一个时间点
            DateTime now = DateTime.Now; // 当前时间点

            // 读取 JSON 文件
            string json = File.ReadAllText("./subscribe.json");

            // 反序列化 JSON 到对象
            List<TaskParams> taskJson = JsonConvert.DeserializeObject<List<TaskParams>>(json);

            task_add(taskArr: taskJson);

            config_update(subscribeLastRun: now, subscribeEnabeld: timerSubscribe.Enabled);
        }
    }
}

    
