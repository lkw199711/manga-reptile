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
        public List<TaskParams> tasks = new List<TaskParams>();
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

            task_load();

            timerSubscribe.Start();

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
        }

        public void config_update(string downloadRoute="",DateTime subscribeLastRun=default)
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

            Config config = new Config(downloadRoute, subscribeLastRun);

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
            DateTime now = DateTime.Now;

            Config config = new Config(@"C:\Users\lkw\OneDrive\0\01manga\00韩漫", now);

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);

            // 将格式化后的 JSON 写入文本文件
            File.WriteAllText("./config.json", json);

            lkw.msbox(now.ToString());
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

            public Config(string downloadRoute, DateTime subscribeLastRun)
            {
                this.downloadRoute = downloadRoute;
                this.subscribeLastRun = subscribeLastRun;
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

        public void task_load() {
            // 读取 JSON 文件
            string json = File.ReadAllText("./task.json");

            // 反序列化 JSON 到对象
            List<TaskParams> taskJson = JsonConvert.DeserializeObject<List<TaskParams>>(json);

            tasks.AddRange(taskJson);

            task_run();
        }

        public void task_add(TaskParams task=null,List<TaskParams> taskArr=null) {
            if(taskArr!=null) tasks.AddRange(taskArr);

            task = task == null ? create_params() : task;
            // 添加任务
            tasks.Add(task);

            // 写入json
            task_write_json();

            // 执行任务
            task_run();
        }

        public void task_run()
        {
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

                    jiman.download();
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

                // 移除任务
                tasks.RemoveAt(0);

                // 写入json
                task_write_json();

                // 继续执行任务
                task_run();
            });
        }

        public void task_write_json()
        {
            TaskJson taskJson = new TaskJson(tasks);

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
            if ((now - lastRun).TotalHours < 2) return;

            write_log("开始执行订阅任务");

            // 读取 JSON 文件
            string json = File.ReadAllText("./subscribe.json");

            // 反序列化 JSON 到对象
            List<TaskParams> taskJson = JsonConvert.DeserializeObject<List<TaskParams>>(json);

            task_add(taskArr: taskJson);

            config_update(subscribeLastRun: now);
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
    }
}

    
