using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SweepMine
{
    public partial class MainFrom : Form
    {
        private enum Level
        {
            Sample,
            Normal,
            Difficult
        }
        public MainFrom()
        {
            InitializeComponent();
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            this.mineField.InitMineField(9, 10);
            this.mineField.MineSweepingFailed += this.MineSweepFailed;
            this.mineField.MineSweepingSuccessful += this.MineSweepSuccess;
            this.mineField.mf = this;
            this.timer1.Tag = 0;
            this.lblMines.Tag = 9;
            this.lblMines.Text = "9";
            this.Tag = Level.Sample;
        }

        private void mineField_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.mineField.DisplayAll();
        }

        private void btnStartOrPaused_Click(object sender, EventArgs e)
        {
            this.btnStartOrPaused.BackgroundImage = Properties.Resources.smile;
            if (this.mineField.Controls.Count != 0)
            {
                this.mineField.Controls.Clear();
            }
            if ((Level)this.Tag == Level.Sample)
            {
                this.mineField.InitMineField(9, 10);
                this.lblTime.Text = "0";
                this.timer1.Tag = 0;
                this.lblMines.Tag = 9;
                this.lblMines.Text = "9";
   
            }else if((Level)this.Tag == Level.Normal)
            {
                this.mineField.InitMineField(16, 40);
                this.lblTime.Text = "0";
                this.timer1.Tag = 0;
                this.lblMines.Tag = 16;
                this.lblMines.Text = "16";

            }else if((Level)this.Tag == Level.Difficult)
            {
                this.mineField.InitMineField(22, 99);
                this.lblTime.Text = "0";
                this.timer1.Tag = 0;
                this.lblMines.Tag = 99;
                this.lblMines.Text = "99";
            }
            else
            {
                this.mineField.InitMineField(22, (int)this.Tag);
                this.lblTime.Text = "0";
                this.timer1.Tag = 0;
                this.lblMines.Tag = (int)this.Tag;
                this.lblMines.Text = this.Tag.ToString();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗口大小改变时设置 中间按钮位置
        /// </summary>
        private void SetBtnLocation()
        {
            int left = (this.panel1.Width - this.btnStartOrPaused.Width)/ 2;
            int top = (this.panel1.Height-this.btnStartOrPaused.Height)/2;
            this.btnStartOrPaused.Location = new Point(left, top);
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            SetBtnLocation();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int seconds = (int)this.timer1.Tag;
            this.lblTime.Text = (seconds + 1).ToString();
            this.timer1.Tag = seconds + 1;
        }

        private void 关于扫雷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        }

        /// <summary>
        /// 扫雷失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineSweepFailed(object sender,EventArgs e)
        {
            this.btnStartOrPaused.BackgroundImage = Properties.Resources.sad;
            this.timer1.Enabled = false;
            MessageBox.Show(null, "扫雷失败", "失败鸟~", MessageBoxButtons.OK);
        }
        /// <summary>
        /// 扫雷成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MineSweepSuccess(object sender, EventArgs e)
        {
            this.btnStartOrPaused.BackgroundImage = Properties.Resources.pride;
            this.timer1.Enabled = false;
           
            Success ss = new Success();
            ss.ShowDialog();
        }

        /// <summary>
        /// 初级为9*9个方块10个雷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 初级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.mineField.Controls.Count != 0)
            {
                this.mineField.Controls.Clear();
            }
            this.初级ToolStripMenuItem.Checked = true;
            this.中级ToolStripMenuItem.Checked = false;
            this.高级ToolStripMenuItem.Checked = false;
            this.自定义ToolStripMenuItem.Checked = false;
            this.lblTime.Text = "00";
            this.lblMines.Tag = 9;
            this.lblMines.Text = "9";
            this.Tag = Level.Sample;
            this.timer1.Tag = 0;
            this.btnStartOrPaused.BackgroundImage = Properties.Resources.smile;
       
            this.mineField.InitMineField(9, 10);
        }

        /// <summary>
        /// 中级为16*16个方块40个雷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 中级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mineField.Controls.Count != 0)
            {
                this.mineField.Controls.Clear();
            }
            this.lblTime.Text = "00";
            this.lblMines.Tag = 40;
            this.初级ToolStripMenuItem.Checked = false;
            this.中级ToolStripMenuItem.Checked = true;
            this.高级ToolStripMenuItem.Checked = false;
            this.自定义ToolStripMenuItem.Checked = false;
            this.lblMines.Text = "40";
            this.Tag = Level.Normal;
            this.timer1.Tag = 0;
            this.btnStartOrPaused.BackgroundImage = Properties.Resources.smile;
            
            this.mineField.InitMineField(16, 40);
        }

        /// <summary>
        /// 高级为22*22个方块99个雷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 高级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mineField.Controls.Count != 0)
            {
                this.mineField.Controls.Clear();
            }
            this.lblTime.Text = "00";
            this.lblMines.Tag = 99;
            this.初级ToolStripMenuItem.Checked = false;
            this.中级ToolStripMenuItem.Checked = false;
            this.高级ToolStripMenuItem.Checked = true;
            this.自定义ToolStripMenuItem.Checked = false;
            this.lblMines.Text = "99";
            this.Tag = Level.Difficult;
            this.timer1.Tag = 0;
            this.btnStartOrPaused.BackgroundImage = Properties.Resources.smile;
         
            
            this.mineField.InitMineField(22,99);
        }

        private void 自定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.初级ToolStripMenuItem.Checked = false;
            this.中级ToolStripMenuItem.Checked = false;
            this.高级ToolStripMenuItem.Checked = false;
            this.自定义ToolStripMenuItem.Checked = true;
            ZiDindyi zd = new ZiDindyi();
            zd.ShowDialog(this);
        }

        private void 使用帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help hp = new Help();
            hp.ShowDialog();
        }

        private void mineField_MouseClick(object sender, MouseEventArgs e)
        {
    
        }

        private void 扫雷英雄榜ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void MainFrom_SizeChanged(object sender, EventArgs e)
        {
           
        }
    }
}
