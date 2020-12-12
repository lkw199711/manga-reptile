using lkw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manga_reptile
{
    public partial class FormIndex : Form
    {
        Lkw lkw = new Lkw();

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

        }

        /// <summary>
        /// 调试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTest_Click(object sender, EventArgs e)
        {
            string url = textUrl.Text;

            //新线程下载
            lkw.NewWork(() => { JinMan jiman = new JinMan(url, this); });
            
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
    }
}
