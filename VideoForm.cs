using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using System.Linq;
/// <summary>
///  2020-11-29 Installed Jumony.Core for parsering html 
/// </summary>
namespace ILearnPlayer
{


    public partial class IlearnPlayer : Form, IMessageFilter
    {
        private const int MpvFormatString = 1;
        private IntPtr _libMpvDll;
        private IntPtr _mpvHandle;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr MpvCreate();
        private MpvCreate _mpvCreate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvInitialize(IntPtr mpvHandle);
        private MpvInitialize _mpvInitialize;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvCommand(IntPtr mpvHandle, IntPtr strings);
        private MpvCommand _mpvCommand;

        //mpv_command_async, added by sund 2020-11-05 

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvCommandAsync(IntPtr mpvHandle, ulong reply_userdata, IntPtr strings);
        private MpvCommandAsync _mpvCommandAsync;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvTerminateDestroy(IntPtr mpvHandle);
        private MpvTerminateDestroy _mpvTerminateDestroy;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvSetOption(IntPtr mpvHandle, byte[] name, int format, ref long data);
        private MpvSetOption _mpvSetOption;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvSetOptionString(IntPtr mpvHandle, byte[] name, byte[] value);
        private MpvSetOptionString _mpvSetOptionString;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvGetPropertystring(IntPtr mpvHandle, byte[] name, int format, ref IntPtr data);
        private MpvGetPropertystring _mpvGetPropertyString;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvGetPropertyDouble(IntPtr mpvHandle);
        private MpvGetPropertyDouble _mpvGetPropertyInt64;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int MpvSetProperty(IntPtr mpvHandle, byte[] name, int format, ref byte[] data);
        private MpvSetProperty _mpvSetProperty;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void MpvFree(IntPtr data);
        private MpvFree _mpvFree;

        [DllImport("user32", EntryPoint = "ShowCursor")]
        public extern static bool ShowCursor(bool show);

        bool m_bMouseMoving;//用于控制mousemove的重入问题（不知道有没有？）
        string[] m_rtTextArray;//使用数据比richtext自己的函数速度要快得多
        String m_sRtText;
        public Point m_oldPnt = new Point(0, 0);

        object readOnly = true;
        // used for selected word with mouse..., in mousemove event of richtext of subtitle
        String m_subSelWord = "";

        ToolTip m_subTitleToolTip = new ToolTip();

        ToolTip m_provSubTTP = new ToolTip();
        ToolTip m_nextSubTTP = new ToolTip();

        ToolTip m_showSub1TTP = new ToolTip();
        ToolTip m_showSub2TTP = new ToolTip();
        ToolTip m_subTransTTP = new ToolTip();
        ToolTip m_postionTTP = new ToolTip();

        ToolTip m_playPauseTTP = new ToolTip();
        ToolTip m_stopTTP = new ToolTip();
        ToolTip m_browseTTP = new ToolTip();
        ToolTip m_maxMinWinTTP = new ToolTip();

        ToolTip m_provVideoTTP = new ToolTip();

        ToolTip m_nextVideoTTP = new ToolTip();

        ToolTip m_volumeTTP = new ToolTip();

        ToolTip m_clipTTP = new ToolTip();


        int m_wheelIdx = 0;
        String nm_subFont = "Arial";
        int m_subFontSize = 30;
        int m_subMargin = 20;
        double m_duration0 = 0;

        //在WIn7下DragDrop事件为响应解决方案如下：
        #region  Windows user32
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr ChangeWindowMessageFilter(uint message, uint dwFlag);
        #endregion
        //Load事件中加上:
        //uint WM_DROPFILES = 0x0233;
        //uint WM_COPYDATA = 0x4A;
        //uint MSGFLT_ADD = 1;
        //ChangeWindowMessageFilter(WM_DROPFILES, MSGFLT_ADD);
        //ChangeWindowMessageFilter(WM_COPYDATA, MSGFLT_ADD);
        //ChangeWindowMessageFilter(0x0049, MSGFLT_ADD);

        //这样 WNI7下事件便会有响应啦
        //#define WM_MOUSEMOVE                    0x0200
        //#define WM_LBUTTONDOWN                  0x0201
        //#define WM_LBUTTONUP                    0x0202
        //#define WM_LBUTTONDBLCLK                0x0203
        //#define WM_RBUTTONDOWN                  0x0204
        //#define WM_RBUTTONUP                    0x0205
        //#define WM_RBUTTONDBLCLK                0x0206
        //#define WM_MBUTTONDOWN                  0x0207
        //#define WM_MBUTTONUP                    0x0208
        //#define WM_MBUTTONDBLCLK                0x0209
        public const int WM_MOUSEMOVE = 0x0200;
        private String m_DbPath;//record the db path 
        private SQLiteHelper m_Sqlite = new SQLiteHelper();  //数据库操作
        int m_lastPos = 0;//the last played position of the video
        int m_curDgvRow = 0;
        long m_plstMaxPK;
        DataTable m_CurSsrtDtbl;// the datasource of the dgvSrt
        String m_plstTable = "playrec";
        Size m_pbxLastSize;//record the last size of the pbxVideo control 
        private Point m_mouseOrgPnt;// used for move the picture box 
        private int m_Bottom;
        private int m_TopLimit;
        double m_ratioOfCvTop2VidH;
        private int m_subCoverInitialH;
        private double m_ratioOfCvH2VidH;
        private bool m_bAdjustTop = false;
        private bool m_bAdjustBottom = false;
        private double m_aspectRatio;
        private Size m_videoSize;// the videoSize calculate based on the video window and aspect ratio 
        private bool m_bHasSubtitle;
        private double m_curSubFrom;// the start time for the current subtitle 
        private double m_curSubTo;// The end time for the current subtitle 
        public IlearnPlayer()
        {
            try
            {
                InitializeComponent();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            setToolTip();
            //added the mousewheel event and control 
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form_MouseWheel);
            //set the richtext to alignment center
            //set the height of subtitle cover control and the richtext for subtitle 
            rtxtSub1.Height = m_subFontSize + m_subMargin;
            pboxCover1.Height = rtxtSub1.Height;
            this.rtxtSub1.SelectionAlignment = HorizontalAlignment.Center;
            CRichTextSetLineSpace.SetLineSpace(rtxtSub1, rtxtSub1.Height * 11);
            this.AllowDrop = true;
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {


            if (e.Delta > 0)
                DoMpvCommand("sub-seek", "-1");
            else
                DoMpvCommand("sub-seek", "1");
            //btnTest.Text = e.Delta.ToString() +":"+m_wheelIdx.ToString();
            //m_wheelIdx++;
        }



        private void addVideoToDGV()
        {
            tmGetPos.Enabled = true;
            //set the basic volume 
            m_duration0 = 0;
            tkBarVol.Value = 100;
            pboxCover1.Visible = pboxCover2.Visible = false;
            m_lastPos = -1;
            //If it is a new one ,added to the datagridview
            DataTable dt = (DataTable)dgvPlayList.DataSource;
            DataRow[] rows = dt.Select(String.Format("mediapath='{0}'", txtVideo.Text)); // select类似where条件
            String mediaPath;
            mediaPath = txtVideo.Text;
            if (rows.Length == 0)
            {
                DataRow dr = dt.NewRow();
                m_plstMaxPK++;

                int pos;
                pos = mediaPath.LastIndexOf(@"\");
                String fileName = mediaPath.Substring(pos + 1);

                dr["fileName"] = fileName;
                dr["pk"] = m_plstMaxPK;
                dr["mediapath"] = mediaPath;

                dr["playpos"] = 0;

                dr["rowNo"] = dt.Rows.Count;
                dt.Rows.Add(dr);
                m_curDgvRow = dt.Rows.Count - 1;
                dgvPlayList.Rows[m_curDgvRow].Selected = true;


            }
            else
            {

                DataGridViewRow row = dgvPlayList.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["mediapath"].Value.ToString().Equals(mediaPath))
                    .First();

                m_curDgvRow = row.Index;
                dgvPlayList.Rows[m_curDgvRow].Selected = true;

            }
        }

        private void addVideoToDGV(String videoFile)
        {
            //If it is a new one ,added to the datagridview
            DataTable dt = (DataTable)dgvPlayList.DataSource;
            DataRow[] rows = dt.Select(String.Format("mediapath='{0}'", videoFile)); // select类似where条件
            String mediaPath;
            mediaPath = videoFile;
            if (rows.Length == 0)
            {
                DataRow dr = dt.NewRow();
                m_plstMaxPK++;

                int pos;
                pos = mediaPath.LastIndexOf(@"\");
                String fileName = mediaPath.Substring(pos + 1);

                dr["fileName"] = fileName;
                dr["pk"] = m_plstMaxPK;
                dr["mediapath"] = mediaPath;

                dr["playpos"] = 0;

                dr["rowNo"] = dt.Rows.Count;
                dt.Rows.Add(dr);


            }
            else
            {

                MessageBox.Show("This video has been in the play list.");

            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            bool bOpenFile = false;// this is used for open the filedialog, or else there is a blocking. 

            if (!IsPaused())
            {
                Pause();
                btnPlayPause.Text = "||";
                bOpenFile = true;
            }
            openFileDialog1.FileName = String.Empty;
            //*.avi *.rmvb *.rm *.asf *.divx *.mpg *.mpeg *.mpe *.wmv *.mp4 *.mkv *.vob
            //*.rmvb *.rm *.asf *.divx *.mpg *.mpeg *.mpe *.wmv;*.vob
            String filter = "视频文件(MP4,AVI,MKV,MOV,RMVB...)|*.mp4;*.avi;*.mkv;*.mov;*.rmvb;*.rm;*.asf;*.divx;*.mpg;*.mpeg;*.mpe;*.wmv;*.vob|All files|*.*";
            //"Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*"
            openFileDialog1.Filter = filter;// 
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)// if the player is not paused, this dialog will be blocked.... Why?
            {

                txtVideo.Text = openFileDialog1.FileName;

                if (bOpenFile) { playPause(); }

                playVideo(txtVideo.Text);
                btnPlayPause.Text = "||";
                //note: the duration can not be get right now
                //double duration;
                //duration = GetDuration();
                //set for pagedown , ↔ and click 

                //pBarTimeElapse.Maximum = (int)(duration*10);
                addVideoToDGV();
            }
        }

        private void setSubtitleOption()
        {
            // set the subtitle position 
            //--sub-pos=<0-150>
            //100 means the 100% of the video window height , from top (0) to (100..150) 
            //_mpvSetOptionString(_mpvHandle, GetUtf8Bytes("sub-pos"), GetUtf8Bytes("100"));
            //set the size of the subtitle 
            //--sub-scale=<0-100>
            //_mpvSetOptionString(_mpvHandle, GetUtf8Bytes("sub-scale"), GetUtf8Bytes("1"));
            // set it to the fixed font size 
            //--sub-font=<size>
            _mpvSetOptionString(_mpvHandle, GetUtf8Bytes("sub-font"), GetUtf8Bytes(nm_subFont));
            //--sub-font-size=<size>
            _mpvSetOptionString(_mpvHandle, GetUtf8Bytes("sub-font-size"), GetUtf8Bytes(m_subFontSize.ToString()));

            //--sub-margin-y=<size>
            _mpvSetOptionString(_mpvHandle, GetUtf8Bytes("sub-margin-y"), GetUtf8Bytes(m_subMargin.ToString()));

            //--sub-scale-by-window=<yes|no>
            _mpvSetOptionString(_mpvHandle, GetUtf8Bytes("sub-scale-by-window"), GetUtf8Bytes("no"));
            //set two subtitle files 
            //-sub-files=<file-list>
            //_mpvSetOptionString(_mpvHandle, GetUtf8Bytes("sub-files"), GetUtf8Bytes("MoonWalk -2.srt;MoonWalk.srt"));
            //select the second subtitle 
            //--secondary-sid=<ID|auto|no>
            _mpvSetOptionString(_mpvHandle, GetUtf8Bytes("secondary-sid"), GetUtf8Bytes("2"));
        }

        private void playVideo(String vFile)
        {
            if(vFile==null || vFile.Trim().Length==0)
            {
                return;
            }
            if (_mpvHandle == IntPtr.Zero)
            {
                if (_mpvHandle != IntPtr.Zero)
                    _mpvTerminateDestroy(_mpvHandle);

                LoadMpvDynamic();
                if (_libMpvDll == IntPtr.Zero)
                    return;

                _mpvHandle = _mpvCreate.Invoke();
                if (_mpvHandle == IntPtr.Zero)
                    return;

                _mpvInitialize.Invoke(_mpvHandle);
                _mpvSetOptionString(_mpvHandle, GetUtf8Bytes("keep-open"), GetUtf8Bytes("always"));
                int mpvFormatInt64 = 4;
                var windowId = pbxVideo.Handle.ToInt64();
                _mpvSetOption(_mpvHandle, GetUtf8Bytes("wid"), mpvFormatInt64, ref windowId);

                setSubtitleOption();

            }
            DoMpvCommand("loadfile", vFile);

            if (IsPaused()) { playPause(); }

            //import the default subtitle file(SRT file ） if it exists 
            string srtFile = vFile.Substring(0, vFile.Length - 3) + "srt";
            //List<SubtitleBlock> mySub = SubtitleReader.ParseSrt(srtFile);
            //dgvSub.DataSource = mySub;
            dgvSub.DataSource = SubtitleReader.ParseSrtFile(srtFile);
            if (dgvSub.DataSource != null)
            {
                m_CurSsrtDtbl = (DataTable)dgvSub.DataSource;
                //dgvSub.Columns[0].Width = ;
                //dgvSub.Columns[1].Visible = false;
                //dgvSub.Columns[2].Visible = false;
                //dgvSub.Columns[3].Visible = false;
                //dgvSub.Columns[4].Visible = false;
                lblSubNotice.Text = "Subtitle List";
                m_bHasSubtitle = true;
                m_provSubTTP.SetToolTip(btnBackward, "Go to last subtitle.");
                m_nextSubTTP.SetToolTip(btnForward, "Go to next subtitle.");
                btnSubTrans.Enabled = true;
            }
            else
            {
                lblSubNotice.Text = "No subtitle file found.";
                m_bHasSubtitle = !true;
                m_provSubTTP.SetToolTip(btnBackward, "Go to previous segment.");
                m_nextSubTTP.SetToolTip(btnForward, "Go to next segment.");
                btnSubTrans.Enabled = false;
            }

            //get the duration of the video
            btnPlayPause.Text = "||";
            tmGetPos.Enabled = true;
            //set the basic volume 
            m_duration0 = 0;
            tkBarVol.Value = 100;
            pboxCover1.Visible = pboxCover2.Visible = false;
            btnShowSub1.Text = "S_";
            btnShowSub2.Text = "S-";
        }
        private void LoadMpvDynamic()
        {
            //_libMpvDll = LoadLibrary(@"G:\CSharp\ILearnPlayer\ILearnPlayer\bin\Debug\mpv-1.dll"); // The dll is included in the DEV builds by lachs0r: https://mpv.srsfckn.biz/
            _libMpvDll = LoadLibrary(@"mpv-1.dll"); // The dll is included in the DEV builds by lachs0r: https://mpv.srsfckn.biz/
            int reCode = Marshal.GetLastWin32Error();
            Console.WriteLine(reCode);
            _mpvCreate = (MpvCreate)GetDllType(typeof(MpvCreate), "mpv_create");
            _mpvInitialize = (MpvInitialize)GetDllType(typeof(MpvInitialize), "mpv_initialize");
            _mpvTerminateDestroy = (MpvTerminateDestroy)GetDllType(typeof(MpvTerminateDestroy), "mpv_terminate_destroy");
            _mpvCommand = (MpvCommand)GetDllType(typeof(MpvCommand), "mpv_command");
            // added by sund 
            _mpvCommandAsync = (MpvCommandAsync)GetDllType(typeof(MpvCommandAsync), "mpv_command_async");

            _mpvSetOption = (MpvSetOption)GetDllType(typeof(MpvSetOption), "mpv_set_option");
            _mpvSetOptionString = (MpvSetOptionString)GetDllType(typeof(MpvSetOptionString), "mpv_set_option_string");
            _mpvGetPropertyString = (MpvGetPropertystring)GetDllType(typeof(MpvGetPropertystring), "mpv_get_property");
            _mpvGetPropertyInt64 = (MpvGetPropertyDouble)GetDllType(typeof(MpvGetPropertyDouble), "mpv_get_time_us");//added by sund 2020-10-28
            _mpvSetProperty = (MpvSetProperty)GetDllType(typeof(MpvSetProperty), "mpv_set_property");
            _mpvFree = (MpvFree)GetDllType(typeof(MpvFree), "mpv_free");
        }
        private static byte[] GetUtf8Bytes(string s)
        {
            return Encoding.UTF8.GetBytes(s + "\0");
        }
        private void DoMpvCommand(params string[] args)
        {
            if (_mpvHandle == IntPtr.Zero)
                return;
            IntPtr[] byteArrayPointers;
            var mainPtr = AllocateUtf8IntPtrArrayWithSentinel(args, out byteArrayPointers);
            _mpvCommand(_mpvHandle, mainPtr);
            foreach (var ptr in byteArrayPointers)
            {
                Marshal.FreeHGlobal(ptr);
            }
            Marshal.FreeHGlobal(mainPtr);
        }
        private void DoMpvCommandAsync(params string[] args)
        {
            if (_mpvHandle == IntPtr.Zero)
                return; ;
            IntPtr[] byteArrayPointers;
            var mainPtr = AllocateUtf8IntPtrArrayWithSentinel(args, out byteArrayPointers);
            UInt64 reply = 0;
            _mpvCommandAsync(_mpvHandle, reply, mainPtr);
            //_mpvCommand(_mpvHandle, mainPtr);
            foreach (var ptr in byteArrayPointers)
            {
                Marshal.FreeHGlobal(ptr);
            }
            Marshal.FreeHGlobal(mainPtr);
        }

        private object GetDllType(Type type, string name)
        {
            IntPtr address = GetProcAddress(_libMpvDll, name);
            if (address != IntPtr.Zero)
                return Marshal.GetDelegateForFunctionPointer(address, type);
            return null;
        }
        public static IntPtr AllocateUtf8IntPtrArrayWithSentinel(string[] arr, out IntPtr[] byteArrayPointers)
        {
            int numberOfStrings = arr.Length + 1; // add extra element for extra null pointer last (sentinel)
            byteArrayPointers = new IntPtr[numberOfStrings];
            IntPtr rootPointer = Marshal.AllocCoTaskMem(IntPtr.Size * numberOfStrings);
            for (int index = 0; index < arr.Length; index++)
            {
                var bytes = GetUtf8Bytes(arr[index]);
                IntPtr unmanagedPointer = Marshal.AllocHGlobal(bytes.Length);
                Marshal.Copy(bytes, 0, unmanagedPointer, bytes.Length);
                byteArrayPointers[index] = unmanagedPointer;
            }
            Marshal.Copy(byteArrayPointers, 0, rootPointer, numberOfStrings);
            return rootPointer;
        }
        //duration
        public double GetDuration()
        {
            if (_mpvHandle == IntPtr.Zero) return 0;
            var lpBuffer = IntPtr.Zero;
            _mpvGetPropertyString(_mpvHandle, GetUtf8Bytes("duration"), MpvFormatString, ref lpBuffer);
            //String duration = (String)lpBuffer;
            String duration;
            duration = Marshal.PtrToStringAnsi(lpBuffer);
            return Convert.ToDouble(duration);
        }
        // in seconds 
        public double GetTimePos()
        {
            if (_mpvHandle == IntPtr.Zero) return 0;
            var lpBuffer = IntPtr.Zero;
            _mpvGetPropertyString(_mpvHandle, GetUtf8Bytes("time-pos"), MpvFormatString, ref lpBuffer);
            //String duration = (String)lpBuffer;
            string pos;
            pos = Marshal.PtrToStringAnsi(lpBuffer);
            return Convert.ToDouble(pos);
        }

        public double GetAspectRatio()
        {
            if (_mpvHandle == IntPtr.Zero) return 0;
            var lpBuffer = IntPtr.Zero;
            _mpvGetPropertyString(_mpvHandle, GetUtf8Bytes("video-params/aspect"), MpvFormatString, ref lpBuffer);
            //String duration = (String)lpBuffer;
            String aspectRatio;
            aspectRatio = Marshal.PtrToStringAnsi(lpBuffer);
            return Convert.ToDouble(aspectRatio);
        }

        private void tmGetPos_Tick(object sender, EventArgs e)
        {
            if (m_curDgvRow == -1) return;
            double pos0 = GetTimePos();
            int pos = (int)(pos0 * 10 + 0.5);
            if (m_duration0 == 0)
            {
                m_duration0 = GetDuration();
                if (m_duration0 <= 0) return;
                //set datetime picker 
                dtpPlay.Value = DateTime.Parse("00:00:00");
                if (m_lastPos != -1)
                {
                    SetTimePos(m_lastPos);
                    m_lastPos = -1;
                }
                // get video-params/aspect 
                m_aspectRatio = GetAspectRatio();
            }
            int duration = (int)(m_duration0 * 10 + 0.5);
            tkBarTimeElapse.Maximum = duration;
            tkBarTimeElapse.LargeChange = 1;
            tkBarTimeElapse.SmallChange = duration / 10 / 5;
            if (pos > tkBarTimeElapse.Maximum) pos = tkBarTimeElapse.Maximum - 1;
            tkBarTimeElapse.Value = pos;
            // display the time elapsed in hh:mm:ss style 
            String sPos, sDuration;
            TimeSpan ts0 = new TimeSpan(0, 0, (int)(m_duration0 + 0.5));
            TimeSpan ts1 = new TimeSpan(0, 0, (int)(pos0 + 0.5));
            sDuration = ts0.Hours.ToString() + ":" + ts0.Minutes.ToString() + ":" + ts0.Seconds.ToString();
            sPos = ts1.Hours.ToString() + ":" + ts1.Minutes.ToString() + ":" + ts1.Seconds.ToString();
            lblTimeElps.Text = sPos + "/" + sDuration;
            //update pos in datagridView
            dgvPlayList.Rows[m_curDgvRow].Cells["playpos"].Value = ((int)pos0).ToString();
            if (IsPaused())
            {
                btnPlayPause.Text = ">";
                ShowCursor(true);
            }
            else {
                if (this.WindowState == FormWindowState.Maximized && !pnlCtrlBox1.Visible)
                {
                    ShowCursor(false);
                }
            }
        }

        private void tkBarTimeElapse_Scroll(object sender, EventArgs e)
        {

        }
        // value: the interval is 1s
        public void SetTimePos(double value)
        {
            if (_mpvHandle == IntPtr.Zero)
                return;
            if (value > m_duration0) value = m_duration0;
            DoMpvCommand("seek", value.ToString(CultureInfo.InvariantCulture), "absolute");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Pause();
            SetTimePos(0);
            btnPlayPause.Text = ">";
        }
        public void Pause()
        {
            if (_mpvHandle == IntPtr.Zero)
                return;

            var bytes = GetUtf8Bytes("yes");
            _mpvSetProperty(_mpvHandle, GetUtf8Bytes("pause"), MpvFormatString, ref bytes);
        }
        public void setClipRange(double start,double end)
        {
            if (_mpvHandle == IntPtr.Zero)
                return;

            SetTimePos(start);
            var bytes = GetUtf8Bytes(end.ToString());
            _mpvSetProperty(_mpvHandle, GetUtf8Bytes("end"), MpvFormatString, ref bytes);
        }
        private void playPause()
        {
            if (btnPlayPause.Text == ">")
            {
                btnPlayPause.Text = "||";
            }
            else
            {
                btnPlayPause.Text = ">";
            }
            if (IsPaused())
            {
                Play();
                rtxtSub1.Visible = false;

            }
            else
            {
                Pause();
                //get the background title and display in the richtext box.
                rtxtSub1.Text = getCurrentSubText(1);
                rtxtSub1.Visible = true;
            }
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (m_curDgvRow == -1) return;
            resetEndTime();
            if (GetTimePos() == 0)
            {
                playVideo(txtVideo.Text);
                btnPlayPause.Text = "||";
                //note: the duration can not be get right now
                //double duration;
                //duration = GetDuration();
                //set for pagedown , ↔ and click 

                //pBarTimeElapse.Maximum = (int)(duration*10);
                tmGetPos.Enabled = true;
                //set the basic volume 
                m_duration0 = 0;
                tkBarVol.Value = 100;
                pboxCover1.Visible = pboxCover2.Visible = false;
            }
            else
            {
                playPause();
            }
        }
        public bool IsPaused()
        {
            if (_mpvHandle == IntPtr.Zero)
                return true;

            var lpBuffer = IntPtr.Zero;
            _mpvGetPropertyString(_mpvHandle, GetUtf8Bytes("pause"), MpvFormatString, ref lpBuffer);
            var isPaused = Marshal.PtrToStringAnsi(lpBuffer) == "yes";
            _mpvFree(lpBuffer);
            return isPaused;
        }
        private void Play()
        {
            if (_mpvHandle == IntPtr.Zero)
                return;

            var bytes = GetUtf8Bytes("no");
            _mpvSetProperty(_mpvHandle, GetUtf8Bytes("pause"), MpvFormatString, ref bytes);
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (m_bHasSubtitle)
            {
                DoMpvCommand("sub-seek", "-1");
                if (IsPaused())
                {
                    Thread.Sleep(100);
                    rtxtSub1.Text = getCurrentSubText(1);
                }
        
            }
            else
            {
                double pos;
                pos = GetTimePos();
                double lastPos = pos - m_duration0 / 10;

                SetTimePos(lastPos > 0 ? lastPos : 0);
            }



        }

        private void tkBarTimeElapse_MouseDown(object sender, MouseEventArgs e)
        {
            // for precision of the position of the track bar  
            int borderW = 12;// key point 
            float barLen = tkBarTimeElapse.Width - borderW;
            float curPos = e.X - tkBarTimeElapse.Location.X - borderW / 2;
            if (curPos > barLen) curPos = barLen;
            if (curPos < 0) curPos = 0;
            tkBarTimeElapse.Value = (int)(curPos * Convert.ToDouble(tkBarTimeElapse.Maximum) / barLen);

            float pos;
            pos = tkBarTimeElapse.Value / 10;
            SetTimePos(pos);

        }

        private void tkBarVol_Scroll(object sender, EventArgs e)
        {
            //--volume =< value >
            SetVolume(tkBarVol.Value);

        }
        // value: the interval is 1s
        public void SetVolume(int value)
        {
            if (_mpvHandle == IntPtr.Zero)
                return;
            var bytes = GetUtf8Bytes(value.ToString());
            _mpvSetProperty(_mpvHandle, GetUtf8Bytes("volume"), MpvFormatString, ref bytes);
        }

        private void vedioSizeControl()
        {
            if (btnMaxMin.Text == "<>")

            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                btnMaxMin.Text = "><";
                spCntnerForm.Panel1Collapsed = true;
                //spCntnerForm.SplitterDistance = 0;
                //spCntnerForm.SplitterIncrement = 1;
                //spCntnerForm.Panel1.Hide();
                //hide the panel at the same time 
                pnlCtrlBox1.Hide();

            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                btnMaxMin.Text = "<>";
                //spCntnerForm.Panel1.Show();
                spCntnerForm.Panel1Collapsed = false;
                //show the button panel 
                // adjust the location of the pnlCtrlBox that make it in the center of the panel2 of main splitter

                //int pn2W = spCntnerForm.Panel2.Width;
                //int pn2H = spCntnerForm.Panel2.Height;
                //pnlCtrlBox1.Left = (pn2W - pnlCtrlBox1.Width) / 2;
                //pnlCtrlBox1.Top = pn2H - pnlCtrlBox1.Height;

                //pnlCtrlBox1.Dock = DockStyle.None;
                pnlCtrlBox1.Show();

            }
            pboxCover1.BringToFront();
        }

        private void btnMaxMin_Click(object sender, EventArgs e)
        {
            // maximum the video area to the whole screen 
            vedioSizeControl();
            // hide the left pane 


        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            //DoMpvCommand("sub-seek 1");
            //DoMpvCommand("seek", "1", "absolute");
            if (m_bHasSubtitle)
            {
                DoMpvCommand("sub-seek", "1");
                if (IsPaused())
                {
                    Thread.Sleep(100);
                    rtxtSub1.Text = getCurrentSubText(1);
                }
            }
            else
            {
                double pos;
                pos = GetTimePos();
                double lastPos = pos + m_duration0 / 10;

                SetTimePos(lastPos < m_duration0 ? lastPos : m_duration0);
            }

        }

        private void getVideoSize()
        {
            m_aspectRatio = GetAspectRatio();
            if (m_aspectRatio == 0)
                return;
            m_videoSize.Width = pbxVideo.Width;
            m_videoSize.Height = (int)(pbxVideo.Width / m_aspectRatio);
            if (m_videoSize.Height > pbxVideo.Height)
            {
                m_videoSize.Height = pbxVideo.Height;
                m_videoSize.Width = (int)(m_videoSize.Height * m_aspectRatio);
            }
        }

        private void btnShowSub_Click(object sender, EventArgs e)
        {
            Control thisCtrl;
            thisCtrl = (Control)sender;
            String mpvStr = "sub-remove";
            int subTitleTop = pbxVideo.Height - 40;
            if (thisCtrl.Text == "S_")
            {
                mpvStr = "sub-remove";
                thisCtrl.Text = "S+";
                // DoMpvCommand(mpvStr);
                //DoMpvCommandAsync(mpvStr);
                //use the control to override the subtitle 
                rtxtSub1.Visible = false;
                pboxCover1.Visible = true;
                // calculate the position of the cover control based on aspect ratio and the video window
                getVideoSize();
                if (m_aspectRatio != 0)
                {
                    setSubtitleCover();
                }
                else
                {
                    pboxCover1.Width = pbxVideo.Width;
                    pboxCover1.Top = (pbxVideo.Height - pboxCover1.Height)+pbxVideo.Top;

                }

            }
            else
            {
                mpvStr = "sub-reload";
                thisCtrl.Text = "S_";
                //DoMpvCommandAsync(mpvStr);
                pboxCover1.Visible = false;
                if (IsPaused() && m_bHasSubtitle)
                {
                    rtxtSub1.Visible = true;

                }
            }

        }

        private void btnShowSub2_Click(object sender, EventArgs e)
        {
            Control thisCtrl;
            thisCtrl = (Control)sender;
            String mpvStr = "sub-remove";
            int subTitleTop = pbxVideo.Height - 40;
            if (thisCtrl.Text == "S-")
            {
                mpvStr = "sub-remove";
                thisCtrl.Text = "S+";
                // DoMpvCommand(mpvStr);
                //DoMpvCommandAsync(mpvStr);
                //use the control to override the subtitle 
                pboxCover2.Height = 50;
                pboxCover2.Visible = true;


            }
            else
            {
                mpvStr = "sub-reload";
                thisCtrl.Text = "S-";
                //DoMpvCommandAsync(mpvStr);
                pboxCover2.Visible = false;
            }

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show($@"Thank you for using this media player which is under development.
            What you do is use it and give me your feedback.
               MaraSun From General WuDaoKou Area in BJ.  
                     My weChat is SunDDYY.", "About ILearnPlayer");
            // test subtitle blocker
            //SubtitleBloacker sBlk= SubtitleBloacker.Instance;
            ////spCntnerForm
            //sBlk.Width = pbxVideo.Width;
            //Point pnt= new Point();
            //pnt.X = pbxVideo.Left;
            //pnt.Y = pbxVideo.Height - sBlk.Height; ;

            
            //pnt = pbxVideo.PointToScreen(pnt);
            //sBlk.Top = pnt.Y ;
            //sBlk.Left = pnt.X;
            //sBlk.Show(pbxVideo);
            //sBlk.m_TopLimit = pnt.Y - (pbxVideo.Height - sBlk.Height);
            //get the current text 
            //DataTable dt;

            //dgvPlayList.DataSource= getPlayList();
            //String mediaName;
            //int pos;
            //for(int rowIdx = 0; rowIdx < dt.Rows.Count; rowIdx++)
            //{
            //    mediaName = dt.Rows[rowIdx]["mediapath"].ToString();
            //    pos = Convert.ToInt32(dt.Rows[rowIdx]["playpos"].ToString());

            //}
            // read subtitle 
            //string file = @"G:\CSharp\ILearnPlayer\ILearnPlayer\bin\Debug\MoonWalk.srt";
            ////List<SubtitleBlock> mySub  = SubtitleReader.ParseSrtFile(file);
            //dgvSub.DataSource = SubtitleReader.ParseSrtFile(file);
        }

        //sub-text 
        //https://www.cnblogs.com/wangjinming/p/7845307.html 
        //        出错代码：

        //string dec = Marshal.PtrToStringAnsi(audioOutput.psz_description);//输出 鎵０鍣?(Realtek High Definition Audio)

        //        原因：

        //查看内存后知道内存编码是UTF8，Marshal不支持UTF转换，所以必须先转成Unicode再转成UTF8

        //解决后的代码：

        //byte[] bytes = System.Text.Encoding.Unicode.GetBytes(Marshal.PtrToStringUni(audioOutput.psz_description));//转成UNICODE编码

        //        string dec = System.Text.Encoding.UTF8.GetString(bytes);//再转成UTF8
        private string getCurrentSubText(int subID)
        {
            if (_mpvHandle == IntPtr.Zero) return "";
            string subtitle = "";
            var lpBuffer = IntPtr.Zero;
            _mpvGetPropertyString(_mpvHandle, GetUtf8Bytes("sub-text"), MpvFormatString, ref lpBuffer);
            try
            {

                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(Marshal.PtrToStringUni(lpBuffer));//转成UNICODE编码

                subtitle = System.Text.Encoding.UTF8.GetString(bytes);//再转成UTF8
            }
            catch
            { }

            return subtitle;
        }

        private void btnSubTrans_Click(object sender, EventArgs e)
        {
            if (!pboxCover1.Visible)
            {
                rtxtSub1.Text = getCurrentSubText(1);
            }

        }

        public static string UnicodeToString(string srcText)
        {
            string dst = "";
            string src = srcText;
            int len = srcText.Length / 6;
            for (int i = 0; i <= len - 1; i++)
            {
                string str = "";
                str = src.Substring(0, 6).Substring(2);
                src = src.Substring(6);
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
        }

        private void rtxtSub1_MouseMove(object sender, MouseEventArgs e)
        {
            //模仿金山词霸的识词功能
            Control thisCtrl;
            thisCtrl = (Control)sender;

            if (m_bMouseMoving)
                return;
            if ((m_oldPnt.X - e.Location.X) * (m_oldPnt.X - e.Location.X) + (m_oldPnt.Y - e.Location.Y) * (m_oldPnt.Y - e.Location.Y) < 16)
            {
                m_bMouseMoving = false;

                return;
            }
            m_oldPnt = e.Location;

            RichTextBox rt = (RichTextBox)sender;
            int cursorPos = rt.GetCharIndexFromPosition(e.Location);
            int lineNo = rt.GetLineFromCharIndex(cursorPos);
            int selStart = rt.GetFirstCharIndexOfCurrentLine();
            if (rt.TextLength == 0)
            {
                m_bMouseMoving = false;

                return;
            }
            if (m_sRtText != rt.Text)
            {
                m_sRtText = rt.Text;
                m_rtTextArray = m_sRtText.Split('\n');

            }
            // get a word from the current cursor position 
            // search forward to a non alphabet character.
            int pos0, pos1;
            pos0 = pos1 = cursorPos;
            char[] chr = m_sRtText.ToCharArray();
            String curWord = "";
            if (Char.IsLetter(chr[pos0]))// the current char is no a letter, do nothing 
            {

                for (pos0 = cursorPos - 1; pos0 >= 0; pos0--)
                {
                    if (!Char.IsLetter(chr[pos0]))
                    {
                        break;
                    }
                }
                for (pos1 = cursorPos + 1; pos1 < m_sRtText.Length; pos1++)
                {
                    if (!Char.IsLetter(chr[pos1]))
                    {
                        break;
                    }
                }
                if (pos1 < 0)
                    pos0 = 0;
                else
                {
                    pos0++;
                }

                curWord = m_sRtText.Substring(pos0, pos1 - pos0);
            }
            // if the selected word is not changed , do nothing
            if (curWord == m_subSelWord) { return; }
            m_subSelWord = curWord;
            //btnTest.Text = m_subSelWord;
            //rt.Select(pos0, pos1-pos0);

            // translate this word 
            OnlineTranslator onLnTrans = new OnlineTranslator();
            String trans = onLnTrans.getTranslation(m_subSelWord);
            //m_subTitleToolTip
            m_subTitleToolTip.SetToolTip(thisCtrl, trans);

        }

        private void IlearnPlayer_DragDrop(object sender, DragEventArgs e)
        {
            //play the video file wieh draddrop ....
            string dummy = "temp";
            //获得进行"Drag"操作中拖动的字符串
            string str = (string)e.Data.GetData(dummy.GetType());


            //playVideo(txtVideo.Text);
            //btnPlayPause.Text = "||";
            ////note: the duration can not be get right now
            ////double duration;
            ////duration = GetDuration();
            ////set for pagedown , ↔ and click 

            ////pBarTimeElapse.Maximum = (int)(duration*10);
            //tmGetPos.Enabled = true;
            ////set the basic volume 

            //tkBarVol.Value = 100;
            //pboxCover1.Visible = pboxCover2.Visible = false;

        }

        private void pbxVideo_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (System.IO.File.Exists(path[0]))
                {

                    //pictureBox1.Image = Image.FromFile(path[0]);
                    txtVideo.Text = path[0];
                    playVideo(txtVideo.Text);
                    btnPlayPause.Text = "||";
                    //note: the duration can not be get right now
                    //double duration;
                    //duration = GetDuration();
                    //set for pagedown , ↔ and click 
                    //pBarTimeElapse.Maximum = (int)(duration*10);
                    //tmGetPos.Enabled = true;
                    //set the basic volume 

                    //tkBarVol.Value = 100;
                    //pboxCover1.Visible = pboxCover2.Visible = false;
                    addVideoToDGV();
                }
            }

        }

        private void pbxVideo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void btnBrowse_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void pbxVideo_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {


        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 17:
                    MessageBox.Show("哈哈，你不能关闭计算机！");
                    m.Result = (IntPtr)0;
                    break;
                case 513:
                    MessageBox.Show("哈哈，你不能点击左键！");
                    m.Result = (IntPtr)0;
                    break;
                case 516:
                    MessageBox.Show("哈哈，你不能点击右键！");
                    m.Result = (IntPtr)0;
                    break;
                //#define WM_MOUSEMOVE                    0x0200

                case 0x0200:

                    m.Result = (IntPtr)0;
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }

        }

        private void IlearnPlayer_Load(object sender, EventArgs e)
        {
            //Load事件中加上 which make the form support drag and drop 
            uint WM_DROPFILES = 0x0233;
            uint WM_COPYDATA = 0x4A;
            uint MSGFLT_ADD = 1;
            ChangeWindowMessageFilter(WM_DROPFILES, MSGFLT_ADD);
            ChangeWindowMessageFilter(WM_COPYDATA, MSGFLT_ADD);
            ChangeWindowMessageFilter(0x0049, MSGFLT_ADD);
            this.pbxVideo.DragEnter += new DragEventHandler(pbxVideo_DragEnter);
            this.pbxVideo.DragDrop += new DragEventHandler(pbxVideo_DragDrop);
            this.pbxVideo.AllowDrop = true;
            //添加消息过滤
            Application.AddMessageFilter(this);


            dtpPlay.Value = DateTime.Parse("00:00:00");

            m_curDgvRow = -1;
            getPlayList();// display the playlist

            dgvSub.BackgroundColor = Color.FromArgb(77, 77, 77);
            dgvSub.DefaultCellStyle.SelectionBackColor = Color.FromArgb(251, 176, 59);
            dgvSub.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(179, 179, 179);
            dgvSub.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 128, 128);
            dgvSub.RowHeadersDefaultCellStyle.SelectionBackColor = Color.ForestGreen;


            //dgvPlayList.DefaultCellStyle.Font = new Font("行书", 12);
            m_pbxLastSize = pbxVideo.Size;

            Point srtCoverPnt;
            srtCoverPnt = pboxCover1.Location;
            // calculate the ratio of the Y of cover to the height of the picture box for video
            m_ratioOfCvTop2VidH = (double)(srtCoverPnt.Y - pbxVideo.Top) / m_pbxLastSize.Height;
            m_subCoverInitialH = pboxCover1.Height;
            m_ratioOfCvH2VidH = (double)pboxCover1.Height / pbxVideo.Height;


        }

        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            //throw new NotImplementedException();
           // btnTest.Text = m.Msg.ToString() + "," + m.WParam.ToString() + "," + m.LParam.ToString();
            switch (m.Msg)
            {
                //#define WM_MOUSEMOVE                    0x0200

                case WM_MOUSEMOVE:
                    //get the position of the mouse 
                    Point msPnt = Control.MousePosition;
                    int formH;// the height of the video form 
                    formH = this.Height;

                    Rectangle ctrlRect = this.ClientRectangle;
                    ctrlRect = this.RectangleToScreen(ctrlRect);
                    ctrlRect.Y = ctrlRect.Height - pnlForButton.Height;
                    if (btnMaxMin.Text == "><")//the form is in maximized state , show the panel of the button while the mouse move in this area
                    {
                        if (ctrlRect.Contains(msPnt))
                        {
                            pnlCtrlBox1.Show();
                        }
                        else
                        {
                            pnlCtrlBox1.Hide();
                        }

                    }
                    // Judge is the mouse is in the rect of dateTimerPicker control 
                    ctrlRect = dtpPlay.ClientRectangle;
                    ctrlRect = dtpPlay.RectangleToScreen(ctrlRect);

                    if (dtpPlay.Visible && !ctrlRect.Contains(msPnt))
                    {
                        dtpPlay.Hide();
                        lblTimeElps.Show();
                    }

                    break;
            }

            return false;

        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            int playPos;//seconds
            playPos = dtpPlay.Value.Hour * 3600 + dtpPlay.Value.Minute * 60 + dtpPlay.Value.Second;
            SetTimePos(playPos);

        }

        private void lblTimeElps_Click(object sender, EventArgs e)
        {
            Control thisCtrl;
            thisCtrl = (Control)sender;
            TimeSpan ts0 = new TimeSpan(0, 0, (int)(GetTimePos() + 0.5));
            dtpPlay.Value = DateTime.Parse(ts0.Hours.ToString() + ":" + ts0.Minutes.ToString() + ":" + ts0.Seconds.ToString());
            thisCtrl.Hide();
            dtpPlay.Show();

        }

        private void dtpPlay_ValueChanged(object sender, EventArgs e)
        {
            int playPos;//seconds
            playPos = dtpPlay.Value.Hour * 3600 + dtpPlay.Value.Minute * 60 + dtpPlay.Value.Second;
            SetTimePos(playPos);

        }

        private void pbxVideo_DoubleClick(object sender, EventArgs e)
        {
            vedioSizeControl();
        }

        private void pbxVideo_Click(object sender, EventArgs e)
        {
            playPause();
        }
        //get the play list from sqlite db 
        private DataTable getPlayList()
        {

            // open sqlite BD which is used to record the last position and name of the movie
            String sql;
            m_DbPath = System.Environment.CurrentDirectory + @"\playRec.db";
            SQLiteHelper.SetConnectionString(m_DbPath, "");
            sql = String.Format("select 0 as rowNo,filename,mediapath,playpos,pk,comment,0 as playing from {0}  order by pk desc", m_plstTable);
            DataTable dt;

            dt = m_Sqlite.ExecuteQuery(sql);
            //get the maximum of the pk 
            if (dt.Rows.Count != 0)
            {
                sql = String.Format("select max(pk) from {0}  ", m_plstTable);
                m_plstMaxPK = Convert.ToInt32(m_Sqlite.ExecuteScalar(sql).ToString());
            }

            // display the playlist recorded 

            dgvPlayList.DataSource = dt;
            dgvPlayList.Columns[0].Width = 24;
            dgvPlayList.Columns[0].ReadOnly = true;
            dgvPlayList.Columns[1].Width = dgvPlayList.Width - dgvPlayList.Columns[0].Width;
            dgvPlayList.Columns[1].ReadOnly = false;
            dgvPlayList.Columns[2].Visible = false;
            dgvPlayList.Columns[3].Visible = false;
            dgvPlayList.Columns[4].Visible = false;
            dgvPlayList.Columns[5].Visible = false;
            dgvPlayList.Columns[6].Visible = false;

            foreach (DataGridViewRow row in dgvPlayList.Rows)
            {
                row.Cells["rowNo"].Value = row.Index;
            }
            //set custom color of back and ..
            dgvPlayList.BackgroundColor = Color.FromArgb(77, 77, 77);
            dgvPlayList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(251, 176, 59);
            dgvPlayList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(179, 179, 179);
            dgvPlayList.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 128, 128);
            dgvPlayList.RowHeadersDefaultCellStyle.SelectionBackColor = Color.ForestGreen;


            return dt;
        }

        private void playCurRow(int RowIndex)
        {
            if (m_curDgvRow == RowIndex) return;
            //remove the playing mark
            if (m_curDgvRow == -1)
            {
                m_curDgvRow = RowIndex;
            }

            dgvPlayList.Rows[m_curDgvRow].Cells["playing"].Value = 0;

            m_curDgvRow = RowIndex;

            dgvPlayList.Rows[m_curDgvRow].Cells["playing"].Value = 1;

            txtVideo.Text = dgvPlayList.Rows[RowIndex].Cells["mediapath"].Value.ToString();
            m_lastPos = Convert.ToInt32(dgvPlayList.Rows[RowIndex].Cells["playpos"].Value);



            playVideo(txtVideo.Text);
        }

        private void dgvPlayList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtVideo.Text = dgvPlayList.Rows[e.RowIndex].Cells["mediapath"].Value.ToString();
            //m_lastPos = Convert.ToInt32(dgvPlayList.Rows[e.RowIndex].Cells["playpos"].Value);
           


        }

        private void IlearnPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //update the play list 
            DoMpvCommand("quit");
            foreach (DataGridViewRow row in dgvPlayList.Rows)
            {
                String pk, pos;
                String fileName;
                String mediapath;
                pk = row.Cells["pk"].Value.ToString();
                pos = row.Cells["playpos"].Value.ToString();
                fileName = row.Cells["fileName"].Value.ToString();
                mediapath = row.Cells["mediapath"].Value.ToString();
                String comment = row.Cells["comment"].Value.ToString();
                String table = "playrec";

                String sql = String.Format("update {0} set pk={1},mediapath='{2}',playpos={3},fileName='{4}' ,comment ='{5}'where pk={1}", table, pk, mediapath, pos, fileName, comment);
                SQLiteParameter[] parameters = { new SQLiteParameter() };

                int rows = m_Sqlite.ExecuteNonQuery(sql, parameters);
                if (rows == 0)
                {
                    sql = String.Format("insert into {0} values({1},'{2}','{3}',{4},'Comment',date('now'))", table, pk, fileName, mediapath, pos);
                    rows = m_Sqlite.ExecuteNonQuery(sql, parameters);
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            int curRow=m_curDgvRow;
            if (curRow > 0)
            {
                curRow--;
                dgvPlayList.Rows[curRow].Selected = true;
                playCurRow(curRow);

            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            int curRow = m_curDgvRow;

            if (curRow < dgvPlayList.Rows.Count - 1)
            {
                curRow++;
                dgvPlayList.Rows[curRow].Selected = true;
                playCurRow(curRow);

            }

        }

        private void dgvPlayList_SizeChanged(object sender, EventArgs e)
        {
            dgvPlayList.Columns[1].Width = dgvPlayList.Width - dgvPlayList.Columns[0].Width;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // search the subtext 
            String str = txtSearchText.Text;
            DataTable dt = (DataTable)dgvSub.DataSource;
            String filter;
            filter = String.Format("text like '%{0}%'", str);
            dt.DefaultView.RowFilter = filter;

            dgvSub.DataSource = dt;

        }

        private void dgvSub_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int RowIndex = e.RowIndex;
            //txtVideo.Text = dgvSub.Rows[RowIndex].Cells["text"].Value.ToString();
            m_lastPos = Convert.ToInt32(dgvSub.Rows[RowIndex].Cells["from"].Value);
            if (m_lastPos != -1 && cxPlayOnSub.Checked)
            {
                SetTimePos(m_lastPos);
                m_lastPos = -1;
            }
        }

        private void btnSchPlayList_Click(object sender, EventArgs e)
        {
            // search the subtext 
            String str = txtSchMedia.Text;
            DataTable dt = (DataTable)dgvPlayList.DataSource;
            String filter;
            filter = String.Format("fileName like '%{0}%'", str);
            dt.DefaultView.RowFilter = filter;
            dgvPlayList.DataSource = dt;
        }

        private void txtSchMedia_TextChanged(object sender, EventArgs e)
        {
            // search the subtext 
            String str = txtSchMedia.Text;
            DataTable dt = (DataTable)dgvPlayList.DataSource;
            String filter;
            filter = String.Format("fileName like '%{0}%'", str);
            dt.DefaultView.RowFilter = filter;
            dgvPlayList.DataSource = dt;
        }

        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            // search the subtext 
            String str = txtSearchText.Text;
            DataTable dt = (DataTable)dgvSub.DataSource;
            String filter;
            filter = String.Format("text like '%{0}%'", str);
            dt.DefaultView.RowFilter = filter;
            dgvSub.DataSource = dt;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int RowIndex = dgvPlayList.SelectedRows[0].Index;
            //dgvPlayList.Rows[RowIndex].Cells["fileName"].ReadOnly = false;


        }

        private void dgvPlayList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //from https://www.cnblogs.com/liye/archive/2010/09/29/1838709.html
            DataGridView thisDgv = (DataGridView)sender;

            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (thisDgv.Rows[e.RowIndex].Selected == false)
                    {
                        thisDgv.ClearSelection();
                        thisDgv.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (thisDgv.SelectedRows.Count == 1)
                    {
                        thisDgv.CurrentCell = thisDgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    //弹出操作菜单
                    ctxMnuPlayList.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // delete it from the database first;
            int RowIndex, pk;
            RowIndex = dgvPlayList.SelectedRows[0].Index;
            pk = Convert.ToInt32(dgvPlayList.Rows[RowIndex].Cells["pk"].Value);
            //if the selected media is being played , stop it and quit 
            DataTable dt = (DataTable)dgvPlayList.DataSource;
            //dt.Rows[RowIndex].Delete();//need datatable.AccepteChanges()方法确认完全删除
            dt.Rows.RemoveAt(RowIndex);
            dgvPlayList.DataSource = dt;

            if (m_curDgvRow == RowIndex)
            {
                Pause();
                txtVideo.Text = "";
                m_curDgvRow = -1;// current row has been removed, so ...
            }else if (m_curDgvRow > RowIndex)
            {
                m_curDgvRow--;//the row before current row has been removed that make the current row -1
            }

            //dgvPlayList.Rows.Remove(dgvPlayList.SelectedRows[0]);
            //dgvPlayList.Refresh();
            String sql = String.Format("Delete from {0} where pk={1}", m_plstTable, pk);
            m_Sqlite.ExecuteNonQuery(sql);
        }

        private void dgvPlayList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            playCurRow(e.RowIndex);
            if (txtComment.Visible)
            {
                String txt;
                int RowIndex = dgvPlayList.SelectedRows[0].Index;
                txt = dgvPlayList.Rows[RowIndex].Cells["comment"].Value.ToString();
                txtComment.Text = txt;
            }
        }

        private void dgvPlayList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > dgvPlayList.Rows.Count - 1)
                return;

            DataGridViewRow dgr = dgvPlayList.Rows[e.RowIndex];
            try
            {
                if (dgr.Cells["playing"].Value.ToString() == "1")
                {
                    dgr.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    dgr.DefaultCellStyle.ForeColor = Color.Black;
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void spCntnerForm_Panel2_SizeChanged(object sender, EventArgs e)
        {
        }

        private void setSubtitleCover()
        {
            getVideoSize();
            pboxCover1.Width = m_videoSize.Width;
            int subCoverH = m_videoSize.Height / 8;
            int videoBottom = (pbxVideo.Height + m_videoSize.Height) / 2;
            // move the cover to the subtitle area of the video
            pboxCover1.Top = videoBottom - subCoverH;
            if (m_bHasSubtitle)// if there is a subtitle file , let it cover all the subtitle area including the video are.
            {
                pboxCover1.Height = (pbxVideo.Height - m_videoSize.Height) / 2 + subCoverH-5;//-5 for adjusting height and top because the control below is bottom filled...
            }
            pboxCover1.Left = (pbxVideo.Width + m_videoSize.Width) / 2 - pboxCover1.Width;
        }

        private void pbxVideo_Resize(object sender, EventArgs e)
        {
            if (m_pbxLastSize.Height == 0) return;

            //calculate the new top of the cover control 
            int newBlkTop = (int)(m_ratioOfCvTop2VidH * pbxVideo.Height + 0.5);
            //srtCoverPnt = new Point(0, newBlkTop);
            //blkLTPnt = pbxVideo.PointToScreen(blkLTPnt);
            pboxCover1.Top = newBlkTop;
            pboxCover1.Width = pbxVideo.Width;
            pboxCover1.Height =(int) (pbxVideo.Height * m_ratioOfCvH2VidH);
            //adjust the subtitle cover control 
            setSubtitleCover();


            //record the current height of the video control 
            //m_pbxLastSize.Height = pbxVideo.Height;
        }

        private void pboxCover1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_mouseOrgPnt.X = e.X;
                m_mouseOrgPnt.Y = e.Y;
                if (e.Y <= 1 )
                {
                    this.Cursor = Cursors.SizeNS;
                    m_bAdjustTop = true;
                    m_bAdjustBottom = false;
                }
                else if (e.Y >= pboxCover1.Height - 2)
                {
                    m_bAdjustTop = false;
                    m_bAdjustBottom = true;

                }
                else
                {
                    m_bAdjustTop = false;
                    m_bAdjustBottom = false;
                }
            }
        }

        private void pboxCover1_MouseMove(object sender, MouseEventArgs e)
        {
            int dltY = e.Y - m_mouseOrgPnt.Y;
            // label1.Text = (m_Bottom - Height - Top).ToString();

            if (e.Y <= 1 || e.Y>=pboxCover1.Height-2)
            {
                this.Cursor = Cursors.SizeNS;
            }
            else
            {
                this.Cursor = Cursors.SizeAll;
            }


            if (e.Button == MouseButtons.Left && pboxCover1.Bottom + dltY <= pbxVideo.Bottom && pboxCover1.Top + dltY > pbxVideo.Top)
            {
                if(m_bAdjustTop)
                {
                    pboxCover1.Top += dltY;
                    pboxCover1.Height -= dltY;
                }
                else if (m_bAdjustBottom)
                {
                    //pboxCover1.Top += dltY;
                    if (e.Y > 8)
                    {
                        pboxCover1.Height = e.Y;
                    }
                    
                    //btnTest.Text = e.Y.ToString();

                }
                else 
                {
                    pboxCover1.Top += dltY;
                }
                    //Left = MousePosition.X - m_mouseOrgPnt.X;
                    //m_mouseOrgPnt.Y = e.Y;
                Point srtCoverPnt;

                srtCoverPnt = pboxCover1.Location;
                // calculate the ratio of the Y of cover to the height of the picture box for video
                m_ratioOfCvTop2VidH = (double)(srtCoverPnt.Y - pbxVideo.Top) / pbxVideo.Height;
                m_subCoverInitialH = pboxCover1.Height;
                m_ratioOfCvH2VidH = (double)pboxCover1.Height / pbxVideo.Height;

            }
           
            //if (Bottom > m_Bottom) { Top = m_Bottom - Height; }
        }

        private void pboxCover1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            m_bAdjustTop = !true;
            m_bAdjustBottom = !true;
        }

        private void commentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtComment.Visible = true;
            String txt;
            int RowIndex = dgvPlayList.SelectedRows[0].Index;
            txt = dgvPlayList.Rows[RowIndex].Cells["comment"].Value.ToString();
            txtComment.Text = txt;
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {
            String txt;
            int RowIndex = dgvPlayList.SelectedRows[0].Index;
            dgvPlayList.Rows[RowIndex].Cells["comment"].Value = txtComment.Text;
        }

        private void ctxMnuPlayList_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void pbxVideo_Click_1(object sender, EventArgs e)
        {
            txtComment.Visible = false;
        }
        private void setToolTip()
        {
            m_subTitleToolTip.AutoPopDelay = 5000;
            m_subTitleToolTip.InitialDelay = 500;
            m_subTitleToolTip.ReshowDelay = 100;
            m_subTitleToolTip.ShowAlways = true;


            m_playPauseTTP.AutoPopDelay = 5000;
            m_playPauseTTP.InitialDelay = 500;
            m_playPauseTTP.ReshowDelay = 100;
            m_playPauseTTP.ShowAlways = true;

            m_stopTTP.AutoPopDelay = 5000;
            m_stopTTP.InitialDelay = 500;
            m_stopTTP.ReshowDelay = 100;
            m_stopTTP.ShowAlways = true;

            m_nextSubTTP.AutoPopDelay = 5000;
            m_nextSubTTP.InitialDelay = 500;
            m_nextSubTTP.ReshowDelay = 100;
            m_nextSubTTP.ShowAlways = true;

            m_provSubTTP.AutoPopDelay = 5000;
            m_provSubTTP.InitialDelay = 500;
            m_provSubTTP.ReshowDelay = 100;
            m_provSubTTP.ShowAlways = true;

            m_showSub1TTP.AutoPopDelay = 5000;
            m_showSub1TTP.InitialDelay = 500;
            m_showSub1TTP.ReshowDelay = 100;
            m_showSub1TTP.ShowAlways = true;

            m_showSub2TTP.AutoPopDelay = 5000;
            m_showSub2TTP.InitialDelay = 500;
            m_showSub2TTP.ReshowDelay = 100;
            m_showSub2TTP.ShowAlways = true;

            m_subTransTTP.AutoPopDelay = 5000;
            m_subTransTTP.InitialDelay = 500;
            m_subTransTTP.ReshowDelay = 100;
            m_subTransTTP.ShowAlways = true;


            m_postionTTP.AutoPopDelay = 5000;
            m_postionTTP.InitialDelay = 500;
            m_postionTTP.ReshowDelay = 100;
            m_postionTTP.ShowAlways = true;

            m_provVideoTTP.AutoPopDelay = 5000;
            m_provVideoTTP.InitialDelay = 500;
            m_provVideoTTP.ReshowDelay = 100;
            m_provVideoTTP.ShowAlways = true;


            m_nextVideoTTP.AutoPopDelay = 5000;
            m_nextVideoTTP.InitialDelay = 500;
            m_nextVideoTTP.ReshowDelay = 100;
            m_nextVideoTTP.ShowAlways = true;

            m_volumeTTP.AutoPopDelay = 5000;
            m_volumeTTP.InitialDelay = 500;
            m_volumeTTP.ReshowDelay = 100;
            m_volumeTTP.ShowAlways = true;

            m_maxMinWinTTP.AutoPopDelay = 5000;
            m_maxMinWinTTP.InitialDelay = 500;
            m_maxMinWinTTP.ReshowDelay = 100;
            m_maxMinWinTTP.ShowAlways = true;

            m_volumeTTP.AutoPopDelay = 5000;
            m_volumeTTP.InitialDelay = 500;
            m_volumeTTP.ReshowDelay = 100;
            m_volumeTTP.ShowAlways = true;

            m_playPauseTTP.SetToolTip(btnPlayPause, "Play/Pause...");
            m_stopTTP.SetToolTip(btnStop, "Stop.");
            m_showSub1TTP.SetToolTip(btnShowSub1, "Show/Hide the bottom subtitle.");
            m_showSub2TTP.SetToolTip(btnShowSub2, "Show/Hide the top subtitle.");
            m_subTransTTP.SetToolTip(btnSubTrans, "Set/Reset translation mode of the first subtitle.(Hover the mouse over the subtitle...)");
            m_postionTTP.SetToolTip(lblTimeElps, "Click on me to set video time position.");
            m_provVideoTTP.SetToolTip(btnHome, "Previous one.");
            m_nextVideoTTP.SetToolTip(btnEnd, "Next one.");
            m_volumeTTP.SetToolTip(tkBarVol, "Volume +-.");
            m_browseTTP.SetToolTip(btnBrowse, "Browse files.");
            m_maxMinWinTTP.SetToolTip(btnMaxMin, "Maximum/Minimum window.");

            m_provSubTTP.SetToolTip(btnBackward, "Go to previous segment/subtitle.");
            m_nextSubTTP.SetToolTip(btnForward, "Go to next segment/subtitle.");
            m_clipTTP.SetToolTip(btnSubttlPlay, "Play the current subtitle clip.");
        }

        private void searchSubtitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //String url = @"http://www.zimuku.la/search?q=%E9%BB%91%E6%9A%97%E7%89%A9%E8%B4%A8";
            String url = @"http://www.zimuku.la/search?q=战士";
            url = "http://zmk.pw/download/MTM1MzgyfDVlNzg1NWFmYjQ3MzEwYzI4NjZiZjY1M3wxNjA2NzQwNjY1fGFjNjk5M2VhfHJlbW90ZQ%3D%3D/svr/dx1";
            SubtitleDownLoader.EntryPoint = url;
            //int pageTo = integerInput1.Value;

            List<SubtitleFileItem> lst = new List<SubtitleFileItem>();

           // for (int i = 1; i <= pageTo; i++)
            {
                List<SubtitleFileItem> temp = SubtitleDownLoader.GetSunbtitle(0);
                //if (temp.Count == 0)
                //    break;
                lst.AddRange(temp);
            }
            //ShowInGrid(lst);
        }

        private void pbxVideo_MouseMove(object sender, MouseEventArgs e)
        {
            ShowCursor(true);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = String.Empty;
            String filter = "视频文件(MP4,AVI,MKV,MOV,RMVB...)|*.mp4;*.avi;*.mkv;*.mov;*.rmvb;*.rm;*.asf;*.divx;*.mpg;*.mpeg;*.mpe;*.wmv;*.vob|All files|*.*";
            openFileDialog1.Filter = filter;// 
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)// if the player is not paused, this dialog will be blocked.... Why?
            {

                String file = openFileDialog1.FileName;

                addVideoToDGV(file);
            }

        }

        private void txtVideo_TextChanged(object sender, EventArgs e)
        {

            Pause();
            playVideo(txtVideo.Text);
            btnPlayPause.Text = "||";
            //note: the duration can not be get right now
            //double duration;
            //duration = GetDuration();
            //set for pagedown , ↔ and click 

            //pBarTimeElapse.Maximum = (int)(duration*10);
            addVideoToDGV();

        }

        private void btnSubttlPlay_Click(object sender, EventArgs e)
        {
            // play the video against the subtitle  time line 
            //get the current subtitle time line range 
            if (!m_bHasSubtitle) return;
            double pos;
            pos = GetTimePos();
            //search the subtitle in the subtitle gridview
            // search the subtext 
            String str = txtSearchText.Text;
            DataTable dt = (DataTable)dgvSub.DataSource;
            String filter;
            filter = String.Format("start<= {0} and {0}<=end", pos.ToString());
            //filter = String.Format(" to<0");
            dt.DefaultView.RowFilter = filter;
            //DataTable newTable = dv.ToTable();
            if (dt.DefaultView.Count >= 1)
            {
                m_curSubFrom = Convert.ToDouble(dt.DefaultView[0]["start"].ToString());
                m_curSubTo = Convert.ToDouble(dt.DefaultView[0]["end"].ToString());
                //set the time pos of start time 
                setClipRange(m_curSubFrom, m_curSubTo);
                Play();
            }
            //--end=<relative time>
            //Stop at given time. Use--length if the time should be relative to--start.See--start for valid option values and examples.
            //dgvSub.DataSource = dt;

        }
        // reset the property of -end to normal 
        public void resetEndTime()
        {
            double pos = GetTimePos();
            setClipRange(pos, m_duration0 - pos);
            
        }
    }














//This class is copied from https://blog.csdn.net/gjban/article/details/36628211?utm_source=blogxgwz3&utm_medium=distribute.pc_relevant_download.none-task-blog-baidujs-2.nonecase&depth_1-utm_source=distribute.pc_relevant_download.none-task-blog-baidujs-2.nonecase
// is used to set the line space of richtext line.
class CRichTextSetLineSpace
    {
        public const int WM_USER = 0x0400;
        public const int EM_GETPARAFORMAT = WM_USER + 61;
        public const int EM_SETPARAFORMAT = WM_USER + 71;
        public const long MAX_TAB_STOPS = 32;
        public const uint PFM_LINESPACING = 0x00000100;
        [StructLayout(LayoutKind.Sequential)]
        private struct PARAFORMAT2
        {
            public int cbSize;
            public uint dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref PARAFORMAT2 lParam);

        /// <summary>
        /// 设置行距
        /// </summary>
        /// <param name="ctl">控件</param>
        /// <param name="dyLineSpacing">间距</param>
        public static void SetLineSpace(Control ctl, int dyLineSpacing)
        {
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.bLineSpacingRule = 4;// bLineSpacingRule;
            fmt.dyLineSpacing = dyLineSpacing;
            fmt.dwMask = PFM_LINESPACING;
            try
            {
                SendMessage(new HandleRef(ctl, ctl.Handle), EM_SETPARAFORMAT, 0, ref fmt);
            }
            catch
            {

            }
        }
    }
    //subtitle related
    //1# get the aspect ratio of the video
    //2# display the subtitles in the black area of the video window 
    //3# visible control 
    //4# subtitle index based display 
}

