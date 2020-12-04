using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILearnPlayer
{
    public partial class SubtitleBloacker : Form
    {
        bool m_bMoveWithMouse=false;
        private Point m_mouseOrgPnt;
        private int m_Bottom=0;
        internal int m_TopLimit;
        // the following codes are instance safe as in https://www.cnblogs.com/leolion/p/10241822.html

        public static SubtitleBloacker Instance { get { return Nested.instance; } }

        private class Nested
        {
            // 显式静态构造告诉C＃编译器
            // 未标记类型BeforeFieldInit
            static Nested()
            {
            }

            internal static readonly SubtitleBloacker instance = new SubtitleBloacker();
        }

        public SubtitleBloacker()
        {
            InitializeComponent();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // click this button, the window will move with the mouse
            //m_bMoveWithMouse = !true;
            this.Close();
        }

        

        private void SubtitleBloacker_MouseMove(object sender, MouseEventArgs e)
        {
            //move the window vertically 
            int dltY = MousePosition.Y - m_mouseOrgPnt.Y;
            label1.Text = (m_Bottom-Height -Top).ToString();

            if (e.Button == MouseButtons.Left && Bottom -(Top - dltY) <=m_Bottom && dltY > m_TopLimit)
            {
                Top = dltY;
                //Left = MousePosition.X - m_mouseOrgPnt.X;
            }
            if(Bottom > m_Bottom) { Top = m_Bottom-Height; }
        }

        private void SubtitleBloacker_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_mouseOrgPnt.X = e.X;
                m_mouseOrgPnt.Y = e.Y;
            }
        }

        private void SubtitleBloacker_SizeChanged(object sender, EventArgs e)
        {
            //btnPin.Left = this.Width - btnPin.Width - 2;
            btnClose.Left = this.Width - btnClose.Width - 2;
        }

        private void SubtitleBloacker_Activated(object sender, EventArgs e)
        {
            if (m_Bottom == 0)
            {
                m_Bottom = Bottom;
            }
        }
    }
}
