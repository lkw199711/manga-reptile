namespace manga_reptile
{
    partial class FormIndex
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textUrl = new System.Windows.Forms.TextBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.labelMessage = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.textMangaName = new System.Windows.Forms.TextBox();
            this.textChapterIncludes = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textChapterExcludes = new System.Windows.Forms.TextBox();
            this.textImageIncludes = new System.Windows.Forms.TextBox();
            this.textImageExcludes = new System.Windows.Forms.TextBox();
            this.btnSingleDownload = new System.Windows.Forms.Button();
            this.textPrefix = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.butJiu = new System.Windows.Forms.Button();
            this.comWebset = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelMangaName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelImageExcludes = new System.Windows.Forms.Label();
            this.labelImageIncludes = new System.Windows.Forms.Label();
            this.labelChapterExcludes = new System.Windows.Forms.Label();
            this.labelChapterIncludes = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTimerStatus = new System.Windows.Forms.Button();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.timerSubscribe = new System.Windows.Forms.Timer(this.components);
            this.btnHand = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textUrl
            // 
            this.textUrl.Location = new System.Drawing.Point(14, 31);
            this.textUrl.Name = "textUrl";
            this.textUrl.Size = new System.Drawing.Size(461, 21);
            this.textUrl.TabIndex = 0;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(14, 20);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 1;
            this.buttonTest.Text = "调试";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // labelMessage
            // 
            this.labelMessage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.Location = new System.Drawing.Point(29, 490);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(481, 403);
            this.labelMessage.TabIndex = 2;
            this.labelMessage.Text = "消息提示栏";
            this.labelMessage.Click += new System.EventHandler(this.labelMessage_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(516, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(624, 1020);
            this.webBrowser1.TabIndex = 3;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(106, 20);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnBrowser.TabIndex = 4;
            this.btnBrowser.Text = "button1";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // textMangaName
            // 
            this.textMangaName.Location = new System.Drawing.Point(92, 73);
            this.textMangaName.Name = "textMangaName";
            this.textMangaName.Size = new System.Drawing.Size(186, 21);
            this.textMangaName.TabIndex = 6;
            // 
            // textChapterIncludes
            // 
            this.textChapterIncludes.Location = new System.Drawing.Point(106, 26);
            this.textChapterIncludes.Name = "textChapterIncludes";
            this.textChapterIncludes.Size = new System.Drawing.Size(369, 21);
            this.textChapterIncludes.TabIndex = 7;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textChapterExcludes
            // 
            this.textChapterExcludes.Location = new System.Drawing.Point(106, 53);
            this.textChapterExcludes.Name = "textChapterExcludes";
            this.textChapterExcludes.Size = new System.Drawing.Size(369, 21);
            this.textChapterExcludes.TabIndex = 9;
            // 
            // textImageIncludes
            // 
            this.textImageIncludes.Location = new System.Drawing.Point(106, 88);
            this.textImageIncludes.Name = "textImageIncludes";
            this.textImageIncludes.Size = new System.Drawing.Size(369, 21);
            this.textImageIncludes.TabIndex = 10;
            // 
            // textImageExcludes
            // 
            this.textImageExcludes.Location = new System.Drawing.Point(106, 115);
            this.textImageExcludes.Name = "textImageExcludes";
            this.textImageExcludes.Size = new System.Drawing.Size(369, 21);
            this.textImageExcludes.TabIndex = 11;
            // 
            // btnSingleDownload
            // 
            this.btnSingleDownload.Location = new System.Drawing.Point(380, 20);
            this.btnSingleDownload.Name = "btnSingleDownload";
            this.btnSingleDownload.Size = new System.Drawing.Size(95, 23);
            this.btnSingleDownload.TabIndex = 12;
            this.btnSingleDownload.Text = "通过子页下载";
            this.btnSingleDownload.UseVisualStyleBackColor = true;
            this.btnSingleDownload.Click += new System.EventHandler(this.btnSingleDownload_Click);
            // 
            // textPrefix
            // 
            this.textPrefix.Location = new System.Drawing.Point(284, 73);
            this.textPrefix.Name = "textPrefix";
            this.textPrefix.Size = new System.Drawing.Size(191, 21);
            this.textPrefix.TabIndex = 13;
            // 
            // butJiu
            // 
            this.butJiu.Location = new System.Drawing.Point(207, 20);
            this.butJiu.Name = "butJiu";
            this.butJiu.Size = new System.Drawing.Size(75, 23);
            this.butJiu.TabIndex = 14;
            this.butJiu.Text = "久久漫画";
            this.butJiu.UseVisualStyleBackColor = true;
            this.butJiu.Click += new System.EventHandler(this.butJiu_Click);
            // 
            // comWebset
            // 
            this.comWebset.FormattingEnabled = true;
            this.comWebset.Items.AddRange(new object[] {
            "绅士漫画(hm07.lol)",
            "绅士漫画-仿真下载(hm07.lol)",
            "久久漫画(freexcomic.com)"});
            this.comWebset.Location = new System.Drawing.Point(14, 20);
            this.comWebset.Name = "comWebset";
            this.comWebset.Size = new System.Drawing.Size(461, 20);
            this.comWebset.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.labelMangaName);
            this.groupBox1.Controls.Add(this.textUrl);
            this.groupBox1.Controls.Add(this.textMangaName);
            this.groupBox1.Controls.Add(this.textPrefix);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 114);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "url";
            // 
            // labelMangaName
            // 
            this.labelMangaName.AutoSize = true;
            this.labelMangaName.Location = new System.Drawing.Point(9, 76);
            this.labelMangaName.Name = "labelMangaName";
            this.labelMangaName.Size = new System.Drawing.Size(77, 12);
            this.labelMangaName.TabIndex = 14;
            this.labelMangaName.Text = "设置漫画名称";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.labelImageExcludes);
            this.groupBox2.Controls.Add(this.labelImageIncludes);
            this.groupBox2.Controls.Add(this.labelChapterExcludes);
            this.groupBox2.Controls.Add(this.labelChapterIncludes);
            this.groupBox2.Controls.Add(this.textChapterIncludes);
            this.groupBox2.Controls.Add(this.textChapterExcludes);
            this.groupBox2.Controls.Add(this.textImageIncludes);
            this.groupBox2.Controls.Add(this.textImageExcludes);
            this.groupBox2.Location = new System.Drawing.Point(22, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(488, 160);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "包含与排除项";
            // 
            // labelImageExcludes
            // 
            this.labelImageExcludes.AutoSize = true;
            this.labelImageExcludes.Location = new System.Drawing.Point(23, 118);
            this.labelImageExcludes.Name = "labelImageExcludes";
            this.labelImageExcludes.Size = new System.Drawing.Size(77, 12);
            this.labelImageExcludes.TabIndex = 15;
            this.labelImageExcludes.Text = "图片排除匹配";
            // 
            // labelImageIncludes
            // 
            this.labelImageIncludes.AutoSize = true;
            this.labelImageIncludes.Location = new System.Drawing.Point(23, 91);
            this.labelImageIncludes.Name = "labelImageIncludes";
            this.labelImageIncludes.Size = new System.Drawing.Size(77, 12);
            this.labelImageIncludes.TabIndex = 14;
            this.labelImageIncludes.Text = "图片包含匹配";
            // 
            // labelChapterExcludes
            // 
            this.labelChapterExcludes.AutoSize = true;
            this.labelChapterExcludes.Location = new System.Drawing.Point(23, 56);
            this.labelChapterExcludes.Name = "labelChapterExcludes";
            this.labelChapterExcludes.Size = new System.Drawing.Size(77, 12);
            this.labelChapterExcludes.TabIndex = 13;
            this.labelChapterExcludes.Text = "章节排除匹配";
            // 
            // labelChapterIncludes
            // 
            this.labelChapterIncludes.AutoSize = true;
            this.labelChapterIncludes.Location = new System.Drawing.Point(23, 29);
            this.labelChapterIncludes.Name = "labelChapterIncludes";
            this.labelChapterIncludes.Size = new System.Drawing.Size(77, 12);
            this.labelChapterIncludes.TabIndex = 12;
            this.labelChapterIncludes.Text = "章节包含匹配";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnHand);
            this.groupBox3.Controls.Add(this.btnTimerStatus);
            this.groupBox3.Controls.Add(this.btnSubscribe);
            this.groupBox3.Controls.Add(this.btnAddTask);
            this.groupBox3.Controls.Add(this.buttonTest);
            this.groupBox3.Controls.Add(this.btnBrowser);
            this.groupBox3.Controls.Add(this.butJiu);
            this.groupBox3.Controls.Add(this.btnSingleDownload);
            this.groupBox3.Location = new System.Drawing.Point(22, 378);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(488, 100);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作按钮";
            // 
            // btnTimerStatus
            // 
            this.btnTimerStatus.Location = new System.Drawing.Point(207, 61);
            this.btnTimerStatus.Name = "btnTimerStatus";
            this.btnTimerStatus.Size = new System.Drawing.Size(75, 23);
            this.btnTimerStatus.TabIndex = 17;
            this.btnTimerStatus.Text = "订阅状态";
            this.btnTimerStatus.UseVisualStyleBackColor = true;
            this.btnTimerStatus.Click += new System.EventHandler(this.btnTimerStatus_Click);
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(106, 61);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
            this.btnSubscribe.TabIndex = 16;
            this.btnSubscribe.Text = "订阅";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // btnAddTask
            // 
            this.btnAddTask.Location = new System.Drawing.Point(14, 62);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(75, 23);
            this.btnAddTask.TabIndex = 15;
            this.btnAddTask.Text = "添加任务";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comWebset);
            this.groupBox4.Location = new System.Drawing.Point(22, 132);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(488, 74);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "站点选择";
            // 
            // timerSubscribe
            // 
            this.timerSubscribe.Interval = 10000;
            this.timerSubscribe.Tick += new System.EventHandler(this.timerSubscribe_Tick);
            // 
            // btnHand
            // 
            this.btnHand.Location = new System.Drawing.Point(311, 61);
            this.btnHand.Name = "btnHand";
            this.btnHand.Size = new System.Drawing.Size(125, 23);
            this.btnHand.TabIndex = 18;
            this.btnHand.Text = "手动执行一次订阅";
            this.btnHand.UseVisualStyleBackColor = true;
            this.btnHand.Click += new System.EventHandler(this.btnHand_Click);
            // 
            // FormIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 1044);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.labelMessage);
            this.Name = "FormIndex";
            this.Text = "爬虫";
            this.Load += new System.EventHandler(this.FormIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textUrl;
        private System.Windows.Forms.Button buttonTest;
        public System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button btnBrowser;
        public System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.TextBox textChapterIncludes;
        public System.Windows.Forms.TextBox textChapterExcludes;
        public System.Windows.Forms.TextBox textImageIncludes;
        public System.Windows.Forms.TextBox textImageExcludes;
        private System.Windows.Forms.Button btnSingleDownload;
        public System.Windows.Forms.TextBox textPrefix;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button butJiu;
        public System.Windows.Forms.TextBox textMangaName;
        private System.Windows.Forms.ComboBox comWebset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelImageExcludes;
        private System.Windows.Forms.Label labelImageIncludes;
        private System.Windows.Forms.Label labelChapterExcludes;
        private System.Windows.Forms.Label labelChapterIncludes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Label labelMangaName;
        private System.Windows.Forms.Timer timerSubscribe;
        private System.Windows.Forms.Button btnTimerStatus;
        private System.Windows.Forms.Button btnHand;
    }
}

