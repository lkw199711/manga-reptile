﻿using System;
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
            JinMan jiman = new JinMan("https://18comic2.biz/album/223523/");
        }
    }
}
