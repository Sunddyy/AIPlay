namespace ILearnPlayer
{
    partial class IlearnPlayer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IlearnPlayer));
            this.spCntnerForm = new System.Windows.Forms.SplitContainer();
            this.spltcLeftPane = new System.Windows.Forms.SplitContainer();
            this.dgvPlayList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSchMedia = new System.Windows.Forms.TextBox();
            this.btnSchPlayList = new System.Windows.Forms.Button();
            this.dgvSub = new System.Windows.Forms.DataGridView();
            this.pnlSrtControl = new System.Windows.Forms.Panel();
            this.lblSubNotice = new System.Windows.Forms.Label();
            this.txtSearchText = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cxPlayOnSub = new System.Windows.Forms.CheckBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.pboxCover2 = new System.Windows.Forms.PictureBox();
            this.pboxCover1 = new System.Windows.Forms.PictureBox();
            this.rtxtSub1 = new System.Windows.Forms.RichTextBox();
            this.pbxVideo = new System.Windows.Forms.PictureBox();
            this.pnlForButton = new System.Windows.Forms.Panel();
            this.pnlCtrlBox1 = new System.Windows.Forms.Panel();
            this.lblTimeElps = new System.Windows.Forms.Label();
            this.dtpPlay = new System.Windows.Forms.DateTimePicker();
            this.btnSubTrans = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnShowSub2 = new System.Windows.Forms.Button();
            this.btnShowSub1 = new System.Windows.Forms.Button();
            this.btnMaxMin = new System.Windows.Forms.Button();
            this.tkBarVol = new System.Windows.Forms.TrackBar();
            this.tkBarTimeElapse = new System.Windows.Forms.TrackBar();
            this.txtVideo = new System.Windows.Forms.TextBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnSubttlPlay = new System.Windows.Forms.Button();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tmGetPos = new System.Windows.Forms.Timer(this.components);
            this.ctxMnuPlayList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchSubtitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.spCntnerForm)).BeginInit();
            this.spCntnerForm.Panel1.SuspendLayout();
            this.spCntnerForm.Panel2.SuspendLayout();
            this.spCntnerForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltcLeftPane)).BeginInit();
            this.spltcLeftPane.Panel1.SuspendLayout();
            this.spltcLeftPane.Panel2.SuspendLayout();
            this.spltcLeftPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayList)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSub)).BeginInit();
            this.pnlSrtControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxCover2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxCover1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVideo)).BeginInit();
            this.pnlForButton.SuspendLayout();
            this.pnlCtrlBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkBarVol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkBarTimeElapse)).BeginInit();
            this.ctxMnuPlayList.SuspendLayout();
            this.SuspendLayout();
            // 
            // spCntnerForm
            // 
            this.spCntnerForm.BackColor = System.Drawing.Color.Black;
            this.spCntnerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spCntnerForm.Location = new System.Drawing.Point(0, 0);
            this.spCntnerForm.Name = "spCntnerForm";
            // 
            // spCntnerForm.Panel1
            // 
            this.spCntnerForm.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.spCntnerForm.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.spCntnerForm.Panel1.Controls.Add(this.spltcLeftPane);
            this.spCntnerForm.Panel1.Controls.Add(this.splitter1);
            // 
            // spCntnerForm.Panel2
            // 
            this.spCntnerForm.Panel2.Controls.Add(this.txtComment);
            this.spCntnerForm.Panel2.Controls.Add(this.pboxCover2);
            this.spCntnerForm.Panel2.Controls.Add(this.pboxCover1);
            this.spCntnerForm.Panel2.Controls.Add(this.rtxtSub1);
            this.spCntnerForm.Panel2.Controls.Add(this.pbxVideo);
            this.spCntnerForm.Panel2.Controls.Add(this.pnlForButton);
            this.spCntnerForm.Panel2.SizeChanged += new System.EventHandler(this.spCntnerForm_Panel2_SizeChanged);
            this.spCntnerForm.Size = new System.Drawing.Size(984, 592);
            this.spCntnerForm.SplitterDistance = 222;
            this.spCntnerForm.TabIndex = 0;
            // 
            // spltcLeftPane
            // 
            this.spltcLeftPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltcLeftPane.Location = new System.Drawing.Point(3, 0);
            this.spltcLeftPane.Name = "spltcLeftPane";
            this.spltcLeftPane.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltcLeftPane.Panel1
            // 
            this.spltcLeftPane.Panel1.Controls.Add(this.dgvPlayList);
            this.spltcLeftPane.Panel1.Controls.Add(this.panel1);
            // 
            // spltcLeftPane.Panel2
            // 
            this.spltcLeftPane.Panel2.Controls.Add(this.dgvSub);
            this.spltcLeftPane.Panel2.Controls.Add(this.pnlSrtControl);
            this.spltcLeftPane.Size = new System.Drawing.Size(219, 592);
            this.spltcLeftPane.SplitterDistance = 296;
            this.spltcLeftPane.TabIndex = 2;
            // 
            // dgvPlayList
            // 
            this.dgvPlayList.AllowUserToAddRows = false;
            this.dgvPlayList.BackgroundColor = System.Drawing.SystemColors.WindowFrame;
            this.dgvPlayList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayList.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlayList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlayList.EnableHeadersVisualStyles = false;
            this.dgvPlayList.GridColor = System.Drawing.Color.Black;
            this.dgvPlayList.Location = new System.Drawing.Point(0, 50);
            this.dgvPlayList.MultiSelect = false;
            this.dgvPlayList.Name = "dgvPlayList";
            this.dgvPlayList.RowHeadersVisible = false;
            this.dgvPlayList.RowTemplate.Height = 23;
            this.dgvPlayList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPlayList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlayList.Size = new System.Drawing.Size(219, 246);
            this.dgvPlayList.TabIndex = 3;
            this.dgvPlayList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlayList_CellClick);
            this.dgvPlayList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlayList_CellDoubleClick);
            this.dgvPlayList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPlayList_CellMouseDown);
            this.dgvPlayList.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvPlayList_RowPrePaint);
            this.dgvPlayList.SizeChanged += new System.EventHandler(this.dgvPlayList_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSchMedia);
            this.panel1.Controls.Add(this.btnSchPlayList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 50);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Chartreuse;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Play List ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSchMedia
            // 
            this.txtSchMedia.Location = new System.Drawing.Point(3, 26);
            this.txtSchMedia.Name = "txtSchMedia";
            this.txtSchMedia.Size = new System.Drawing.Size(174, 21);
            this.txtSchMedia.TabIndex = 2;
            this.txtSchMedia.TextChanged += new System.EventHandler(this.txtSchMedia_TextChanged);
            // 
            // btnSchPlayList
            // 
            this.btnSchPlayList.Location = new System.Drawing.Point(183, 26);
            this.btnSchPlayList.Name = "btnSchPlayList";
            this.btnSchPlayList.Size = new System.Drawing.Size(33, 23);
            this.btnSchPlayList.TabIndex = 1;
            this.btnSchPlayList.Text = "Q";
            this.btnSchPlayList.UseVisualStyleBackColor = true;
            this.btnSchPlayList.Click += new System.EventHandler(this.btnSchPlayList_Click);
            // 
            // dgvSub
            // 
            this.dgvSub.AllowUserToAddRows = false;
            this.dgvSub.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSub.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSub.Location = new System.Drawing.Point(0, 71);
            this.dgvSub.Name = "dgvSub";
            this.dgvSub.ReadOnly = true;
            this.dgvSub.RowHeadersVisible = false;
            this.dgvSub.RowTemplate.Height = 23;
            this.dgvSub.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSub.Size = new System.Drawing.Size(219, 221);
            this.dgvSub.TabIndex = 2;
            this.dgvSub.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSub_CellClick);
            // 
            // pnlSrtControl
            // 
            this.pnlSrtControl.Controls.Add(this.lblSubNotice);
            this.pnlSrtControl.Controls.Add(this.txtSearchText);
            this.pnlSrtControl.Controls.Add(this.btnSearch);
            this.pnlSrtControl.Controls.Add(this.cxPlayOnSub);
            this.pnlSrtControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSrtControl.Location = new System.Drawing.Point(0, 0);
            this.pnlSrtControl.Name = "pnlSrtControl";
            this.pnlSrtControl.Size = new System.Drawing.Size(219, 71);
            this.pnlSrtControl.TabIndex = 1;
            // 
            // lblSubNotice
            // 
            this.lblSubNotice.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblSubNotice.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubNotice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubNotice.ForeColor = System.Drawing.Color.Aqua;
            this.lblSubNotice.Location = new System.Drawing.Point(0, 0);
            this.lblSubNotice.Name = "lblSubNotice";
            this.lblSubNotice.Size = new System.Drawing.Size(219, 22);
            this.lblSubNotice.TabIndex = 3;
            this.lblSubNotice.Text = "Subtitle  List";
            this.lblSubNotice.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtSearchText
            // 
            this.txtSearchText.Location = new System.Drawing.Point(3, 47);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(174, 21);
            this.txtSearchText.TabIndex = 2;
            this.txtSearchText.TextChanged += new System.EventHandler(this.txtSearchText_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(183, 47);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(33, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Q";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cxPlayOnSub
            // 
            this.cxPlayOnSub.AutoSize = true;
            this.cxPlayOnSub.Location = new System.Drawing.Point(0, 25);
            this.cxPlayOnSub.Name = "cxPlayOnSub";
            this.cxPlayOnSub.Size = new System.Drawing.Size(90, 16);
            this.cxPlayOnSub.TabIndex = 0;
            this.cxPlayOnSub.Text = "Play on Sub";
            this.cxPlayOnSub.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 592);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // txtComment
            // 
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtComment.Location = new System.Drawing.Point(0, 371);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(758, 91);
            this.txtComment.TabIndex = 9;
            this.txtComment.Visible = false;
            this.txtComment.TextChanged += new System.EventHandler(this.txtComment_TextChanged);
            // 
            // pboxCover2
            // 
            this.pboxCover2.BackColor = System.Drawing.Color.Black;
            this.pboxCover2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pboxCover2.Location = new System.Drawing.Point(0, 0);
            this.pboxCover2.Name = "pboxCover2";
            this.pboxCover2.Size = new System.Drawing.Size(758, 50);
            this.pboxCover2.TabIndex = 8;
            this.pboxCover2.TabStop = false;
            this.pboxCover2.Visible = false;
            // 
            // pboxCover1
            // 
            this.pboxCover1.BackColor = System.Drawing.Color.White;
            this.pboxCover1.Image = global::ILearnPlayer.Properties.Resources.subttleBlocker011;
            this.pboxCover1.InitialImage = null;
            this.pboxCover1.Location = new System.Drawing.Point(0, 412);
            this.pboxCover1.Name = "pboxCover1";
            this.pboxCover1.Size = new System.Drawing.Size(758, 50);
            this.pboxCover1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxCover1.TabIndex = 7;
            this.pboxCover1.TabStop = false;
            this.pboxCover1.Visible = false;
            this.pboxCover1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pboxCover1_MouseDown);
            this.pboxCover1.MouseLeave += new System.EventHandler(this.pboxCover1_MouseLeave);
            this.pboxCover1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pboxCover1_MouseMove);
            // 
            // rtxtSub1
            // 
            this.rtxtSub1.BackColor = System.Drawing.Color.DimGray;
            this.rtxtSub1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtSub1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtxtSub1.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtSub1.ForeColor = System.Drawing.Color.White;
            this.rtxtSub1.Location = new System.Drawing.Point(0, 462);
            this.rtxtSub1.Multiline = false;
            this.rtxtSub1.Name = "rtxtSub1";
            this.rtxtSub1.ReadOnly = true;
            this.rtxtSub1.Size = new System.Drawing.Size(758, 50);
            this.rtxtSub1.TabIndex = 6;
            this.rtxtSub1.Text = "";
            this.rtxtSub1.Visible = false;
            this.rtxtSub1.WordWrap = false;
            this.rtxtSub1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rtxtSub1_MouseMove);
            // 
            // pbxVideo
            // 
            this.pbxVideo.BackColor = System.Drawing.Color.Black;
            this.pbxVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxVideo.Image = global::ILearnPlayer.Properties.Resources.ILearn01;
            this.pbxVideo.InitialImage = null;
            this.pbxVideo.Location = new System.Drawing.Point(0, 0);
            this.pbxVideo.Name = "pbxVideo";
            this.pbxVideo.Size = new System.Drawing.Size(758, 512);
            this.pbxVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxVideo.TabIndex = 4;
            this.pbxVideo.TabStop = false;
            this.pbxVideo.Click += new System.EventHandler(this.pbxVideo_Click_1);
            this.pbxVideo.DoubleClick += new System.EventHandler(this.pbxVideo_DoubleClick);
            this.pbxVideo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxVideo_MouseMove);
            this.pbxVideo.Resize += new System.EventHandler(this.pbxVideo_Resize);
            // 
            // pnlForButton
            // 
            this.pnlForButton.Controls.Add(this.pnlCtrlBox1);
            this.pnlForButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlForButton.Location = new System.Drawing.Point(0, 512);
            this.pnlForButton.Name = "pnlForButton";
            this.pnlForButton.Size = new System.Drawing.Size(758, 80);
            this.pnlForButton.TabIndex = 0;
            // 
            // pnlCtrlBox1
            // 
            this.pnlCtrlBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlCtrlBox1.Controls.Add(this.lblTimeElps);
            this.pnlCtrlBox1.Controls.Add(this.dtpPlay);
            this.pnlCtrlBox1.Controls.Add(this.btnSubTrans);
            this.pnlCtrlBox1.Controls.Add(this.btnTest);
            this.pnlCtrlBox1.Controls.Add(this.btnShowSub2);
            this.pnlCtrlBox1.Controls.Add(this.btnShowSub1);
            this.pnlCtrlBox1.Controls.Add(this.btnMaxMin);
            this.pnlCtrlBox1.Controls.Add(this.tkBarVol);
            this.pnlCtrlBox1.Controls.Add(this.tkBarTimeElapse);
            this.pnlCtrlBox1.Controls.Add(this.txtVideo);
            this.pnlCtrlBox1.Controls.Add(this.btnEnd);
            this.pnlCtrlBox1.Controls.Add(this.btnBrowse);
            this.pnlCtrlBox1.Controls.Add(this.btnHome);
            this.pnlCtrlBox1.Controls.Add(this.btnSubttlPlay);
            this.pnlCtrlBox1.Controls.Add(this.btnPlayPause);
            this.pnlCtrlBox1.Controls.Add(this.btnForward);
            this.pnlCtrlBox1.Controls.Add(this.btnStop);
            this.pnlCtrlBox1.Controls.Add(this.btnBackward);
            this.pnlCtrlBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCtrlBox1.Location = new System.Drawing.Point(0, 0);
            this.pnlCtrlBox1.Name = "pnlCtrlBox1";
            this.pnlCtrlBox1.Size = new System.Drawing.Size(758, 80);
            this.pnlCtrlBox1.TabIndex = 11;
            // 
            // lblTimeElps
            // 
            this.lblTimeElps.Location = new System.Drawing.Point(3, 57);
            this.lblTimeElps.Name = "lblTimeElps";
            this.lblTimeElps.Size = new System.Drawing.Size(93, 16);
            this.lblTimeElps.TabIndex = 17;
            this.lblTimeElps.Text = "00/100";
            this.lblTimeElps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTimeElps.Click += new System.EventHandler(this.lblTimeElps_Click);
            // 
            // dtpPlay
            // 
            this.dtpPlay.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpPlay.Location = new System.Drawing.Point(3, 54);
            this.dtpPlay.Name = "dtpPlay";
            this.dtpPlay.ShowUpDown = true;
            this.dtpPlay.Size = new System.Drawing.Size(74, 21);
            this.dtpPlay.TabIndex = 19;
            this.dtpPlay.Value = new System.DateTime(2020, 11, 14, 11, 49, 35, 0);
            this.dtpPlay.ValueChanged += new System.EventHandler(this.dtpPlay_ValueChanged);
            // 
            // btnSubTrans
            // 
            this.btnSubTrans.Location = new System.Drawing.Point(517, 55);
            this.btnSubTrans.Name = "btnSubTrans";
            this.btnSubTrans.Size = new System.Drawing.Size(37, 23);
            this.btnSubTrans.TabIndex = 16;
            this.btnSubTrans.Text = "sT";
            this.btnSubTrans.UseVisualStyleBackColor = true;
            this.btnSubTrans.Click += new System.EventHandler(this.btnSubTrans_Click);
            // 
            // btnTest
            // 
            this.btnTest.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTest.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnTest.Location = new System.Drawing.Point(716, 20);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(42, 60);
            this.btnTest.TabIndex = 15;
            this.btnTest.Text = "i";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnShowSub2
            // 
            this.btnShowSub2.Location = new System.Drawing.Point(482, 55);
            this.btnShowSub2.Name = "btnShowSub2";
            this.btnShowSub2.Size = new System.Drawing.Size(33, 23);
            this.btnShowSub2.TabIndex = 14;
            this.btnShowSub2.Text = "S-";
            this.btnShowSub2.UseVisualStyleBackColor = true;
            this.btnShowSub2.Click += new System.EventHandler(this.btnShowSub2_Click);
            // 
            // btnShowSub1
            // 
            this.btnShowSub1.Location = new System.Drawing.Point(444, 55);
            this.btnShowSub1.Name = "btnShowSub1";
            this.btnShowSub1.Size = new System.Drawing.Size(34, 23);
            this.btnShowSub1.TabIndex = 13;
            this.btnShowSub1.Text = "S_";
            this.btnShowSub1.UseVisualStyleBackColor = true;
            this.btnShowSub1.Click += new System.EventHandler(this.btnShowSub_Click);
            // 
            // btnMaxMin
            // 
            this.btnMaxMin.Location = new System.Drawing.Point(565, 57);
            this.btnMaxMin.Name = "btnMaxMin";
            this.btnMaxMin.Size = new System.Drawing.Size(26, 23);
            this.btnMaxMin.TabIndex = 12;
            this.btnMaxMin.Text = "<>";
            this.btnMaxMin.UseVisualStyleBackColor = true;
            this.btnMaxMin.Click += new System.EventHandler(this.btnMaxMin_Click);
            // 
            // tkBarVol
            // 
            this.tkBarVol.Location = new System.Drawing.Point(294, 55);
            this.tkBarVol.Maximum = 100;
            this.tkBarVol.Name = "tkBarVol";
            this.tkBarVol.Size = new System.Drawing.Size(164, 45);
            this.tkBarVol.TabIndex = 11;
            this.tkBarVol.Scroll += new System.EventHandler(this.tkBarVol_Scroll);
            // 
            // tkBarTimeElapse
            // 
            this.tkBarTimeElapse.AutoSize = false;
            this.tkBarTimeElapse.Dock = System.Windows.Forms.DockStyle.Top;
            this.tkBarTimeElapse.LargeChange = 100;
            this.tkBarTimeElapse.Location = new System.Drawing.Point(0, 0);
            this.tkBarTimeElapse.Name = "tkBarTimeElapse";
            this.tkBarTimeElapse.Size = new System.Drawing.Size(758, 20);
            this.tkBarTimeElapse.SmallChange = 20;
            this.tkBarTimeElapse.TabIndex = 10;
            this.tkBarTimeElapse.Scroll += new System.EventHandler(this.tkBarTimeElapse_Scroll);
            this.tkBarTimeElapse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tkBarTimeElapse_MouseDown);
            // 
            // txtVideo
            // 
            this.txtVideo.Location = new System.Drawing.Point(3, 28);
            this.txtVideo.Name = "txtVideo";
            this.txtVideo.Size = new System.Drawing.Size(551, 21);
            this.txtVideo.TabIndex = 7;
            this.txtVideo.TextChanged += new System.EventHandler(this.txtVideo_TextChanged);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(242, 54);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(26, 23);
            this.btnEnd.TabIndex = 6;
            this.btnEnd.Text = ">|";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(565, 28);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(26, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            this.btnBrowse.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnBrowse_DragEnter);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(214, 54);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(26, 23);
            this.btnHome.TabIndex = 5;
            this.btnHome.Text = "|<";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnSubttlPlay
            // 
            this.btnSubttlPlay.Location = new System.Drawing.Point(128, 54);
            this.btnSubttlPlay.Name = "btnSubttlPlay";
            this.btnSubttlPlay.Size = new System.Drawing.Size(26, 23);
            this.btnSubttlPlay.TabIndex = 1;
            this.btnSubttlPlay.Text = "-";
            this.btnSubttlPlay.UseVisualStyleBackColor = true;
            this.btnSubttlPlay.Click += new System.EventHandler(this.btnSubttlPlay_Click);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Location = new System.Drawing.Point(102, 54);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(26, 23);
            this.btnPlayPause.TabIndex = 1;
            this.btnPlayPause.Text = ">";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(186, 54);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(26, 23);
            this.btnForward.TabIndex = 4;
            this.btnForward.Text = ">>";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(269, 55);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(26, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "[]";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Location = new System.Drawing.Point(154, 54);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(26, 23);
            this.btnBackward.TabIndex = 3;
            this.btnBackward.Text = "<<";
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tmGetPos
            // 
            this.tmGetPos.Tick += new System.EventHandler(this.tmGetPos_Tick);
            // 
            // ctxMnuPlayList
            // 
            this.ctxMnuPlayList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.addToolStripMenuItem,
            this.commentToolStripMenuItem,
            this.searchSubtitleToolStripMenuItem});
            this.ctxMnuPlayList.Name = "ctxMnuPlayList";
            this.ctxMnuPlayList.Size = new System.Drawing.Size(163, 92);
            this.ctxMnuPlayList.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMnuPlayList_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // commentToolStripMenuItem
            // 
            this.commentToolStripMenuItem.Name = "commentToolStripMenuItem";
            this.commentToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.commentToolStripMenuItem.Text = "Comment";
            this.commentToolStripMenuItem.Click += new System.EventHandler(this.commentToolStripMenuItem_Click);
            // 
            // searchSubtitleToolStripMenuItem
            // 
            this.searchSubtitleToolStripMenuItem.Name = "searchSubtitleToolStripMenuItem";
            this.searchSubtitleToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.searchSubtitleToolStripMenuItem.Text = "Search Subtitle";
            this.searchSubtitleToolStripMenuItem.Click += new System.EventHandler(this.searchSubtitleToolStripMenuItem_Click);
            // 
            // IlearnPlayer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 592);
            this.Controls.Add(this.spCntnerForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IlearnPlayer";
            this.Text = "ILearnPlayer V0.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IlearnPlayer_FormClosing);
            this.Load += new System.EventHandler(this.IlearnPlayer_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.IlearnPlayer_DragDrop);
            this.spCntnerForm.Panel1.ResumeLayout(false);
            this.spCntnerForm.Panel2.ResumeLayout(false);
            this.spCntnerForm.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spCntnerForm)).EndInit();
            this.spCntnerForm.ResumeLayout(false);
            this.spltcLeftPane.Panel1.ResumeLayout(false);
            this.spltcLeftPane.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltcLeftPane)).EndInit();
            this.spltcLeftPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSub)).EndInit();
            this.pnlSrtControl.ResumeLayout(false);
            this.pnlSrtControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxCover2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxCover1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVideo)).EndInit();
            this.pnlForButton.ResumeLayout(false);
            this.pnlCtrlBox1.ResumeLayout(false);
            this.pnlCtrlBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkBarVol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tkBarTimeElapse)).EndInit();
            this.ctxMnuPlayList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spCntnerForm;
        private System.Windows.Forms.Panel pnlForButton;
        private System.Windows.Forms.TextBox txtVideo;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer tmGetPos;
        private System.Windows.Forms.TrackBar tkBarTimeElapse;
        private System.Windows.Forms.Panel pnlCtrlBox1;
        private System.Windows.Forms.TrackBar tkBarVol;
        private System.Windows.Forms.Button btnMaxMin;
        private System.Windows.Forms.Button btnShowSub1;
        private System.Windows.Forms.Button btnShowSub2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSubTrans;
        private System.Windows.Forms.Label lblTimeElps;
        private System.Windows.Forms.DateTimePicker dtpPlay;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.SplitContainer spltcLeftPane;
        private System.Windows.Forms.DataGridView dgvSub;
        private System.Windows.Forms.Panel pnlSrtControl;
        private System.Windows.Forms.TextBox txtSearchText;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox cxPlayOnSub;
        private System.Windows.Forms.DataGridView dgvPlayList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSchMedia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSchPlayList;
        private System.Windows.Forms.ContextMenuStrip ctxMnuPlayList;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commentToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbxVideo;
        private System.Windows.Forms.PictureBox pboxCover1;
        private System.Windows.Forms.RichTextBox rtxtSub1;
        private System.Windows.Forms.PictureBox pboxCover2;
        private System.Windows.Forms.Label lblSubNotice;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ToolStripMenuItem searchSubtitleToolStripMenuItem;
        private System.Windows.Forms.Button btnSubttlPlay;
    }
}

