using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SweepMine
{
    public partial class MineField : UserControl
    {
        public delegate void MineSweepingSuccessfulEventHandler(object sender, EventArgs e);
        public event MineSweepingSuccessfulEventHandler MineSweepingSuccessful;

        public delegate void MineSweepingFailedEventHandler(object sender, EventArgs e);
        public event MineSweepingFailedEventHandler MineSweepingFailed;

        /// <summary>
        /// 主窗体的引用
        /// </summary>
        public MainFrom mf;
        private void MS_Success(object sender,EventArgs e)
        {

        }
        private void MS_Failed(object sender, EventArgs e)
        {

        }
        public MineField()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化雷区。布局方块并随机放置地雷
        /// </summary>
        /// <param name="boxLineNumber">一行中含有的方块数量</param>
        /// <param name="mineNumber">地雷数目</param>
        public void InitMineField(int boxLineNumber,int mineNumber)
        {
            //根据方块数构建方块实例
            for(int i = 0;i < boxLineNumber*boxLineNumber;i++)
            {
                Box box = new Box();
                box.MouseDown += new MouseEventHandler(box_MouseDown);
                this.Controls.Add(box);
            }
            //布局方块
            this.LayoutBoxes();

            //随机生成地雷
            this.LayMines(mineNumber);

            //设置每个方块周边的地雷数
            foreach(Box box in this.Controls)
            {
                int mineCount = 0;
                List<Box> boxes = this.GetMineAroundCount(box);
                foreach(Box b in boxes)
                {
                    if(b.HasMine)
                    {
                        mineCount++;
                    }

                }
                box.AroundMIneCount = mineCount;
            }
        }

 
        /// <summary>
        /// 踩雷后显示所有方块
        /// </summary>
        public void DisplayAll()
        {
            foreach(Box box in this.Controls)
            {
                if(box.State !=  Box.BoxState.Opened)
                {
                    box.Open();
                }
            }

        }

        /// <summary>
        /// 当无雷方块被点击后，显示与当前方块相关的无地雷的方块
        /// </summary>
        /// <param name="box">被点击的方块</param>
        public void DisplayAround(Box box)
        {
            if(box.State == Box.BoxState.Opened || box.HasMine)
            {
                return;
            }
            box.Open();

            List<Box> boxes = this.GetMineAroundCount(box);
            foreach(Box b in boxes)
            {
                if(this.GetMineAroundCount(b).Count == 0)
                {
                    this.DisplayAround(b);
                }
                else
                {
                    if(b.State != Box.BoxState.Opened && !b.HasMine)
                    {
                        b.Open();
                    }
                }
            }
        }

        /// <summary>
        /// 布局当前雷区中的方块
        /// </summary>
        private void LayoutBoxes()
        {
            if (this.Controls.Count == 0)
                return;
            int boxLineNum = (int)Math.Sqrt(this.Controls.Count);
            int boxWidth = this.Width / boxLineNum;
            int boxHeight = this.Height / boxLineNum;

            int boxIndex = 0;
            int boxTop = 0;
            int boxLeft = 0;

            for(int colIndex = 0;colIndex < boxLineNum;colIndex++)
            {
                boxTop = colIndex * boxHeight;
                for(int rowIndex = 0;rowIndex < boxLineNum;rowIndex++)
                {
                    boxLeft = rowIndex * boxWidth;
                    Box box = this.Controls[boxIndex] as Box;
                    box.Size = new Size(boxWidth, boxHeight);

                    box.Location = new Point(boxLeft, boxTop);
                    boxIndex++;
                }
            }
        }
        private void LayMines(int mineNumber)
        {
            Random ran = new Random();
            for(int i = 0;i < mineNumber;i++)
            {
                int index = ran.Next(0, this.Controls.Count);
                Box box = (Box)this.Controls[index];
                box.HasMine = true;
            }
        }

        private void MineField_SizeChanged(object sender, EventArgs e)
        {
            this.LayoutBoxes();
        }

        private void box_MouseDown(object sender, MouseEventArgs e)
        {
            //只处理鼠标左右键事件  忽略其他按键
            if(e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
            {
                return;
            }
            Box box = sender as Box;
            
            this.mf.timer1.Enabled = true;
            if(e.Button == MouseButtons.Left)
            {
                if(box.HasMine)
                {
                    box.Open();
                    this.DisplayAll();
                    if(this.MineSweepingFailed != null)
                    {
                        this.MineSweepingFailed(this, EventArgs.Empty);
                    }
                }
                else
                {
                    this.DisplayAround(box);
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                if(box.State ==  Box.BoxState.Marked)
                {
                    box.Reset();
                    int mineCount = Convert.ToInt32(this.mf.lblMines.Text);
                    if (mineCount < Convert.ToInt32(this.mf.lblMines.Tag)) mineCount++;
                    this.mf.lblMines.Text = mineCount.ToString();
                }
                else
                {
                    box.Mark();
                    int mineCount = Convert.ToInt32(this.mf.lblMines.Text);
                    if(mineCount>0) mineCount--;
                    this.mf.lblMines.Text = mineCount.ToString();
                }
            }
            if(this.IsSweepSuccessed())
            {
                if(this.MineSweepingSuccessful != null)
                {
                    this.MineSweepingSuccessful(this, EventArgs.Empty);
                }
            }
        }

        private List<Box> GetMineAroundCount(Box box)
        {
            List<Box> boxes = new List<Box>();

            int boxHeight = box.Height;
            int boxWidth = box.Width;

            foreach(Box b in this.Controls)
            {
                if((b.Left == box.Left) && Math.Abs(b.Top-box.Top)==boxHeight
                    || (b.Top == box.Top) && Math.Abs(b.Left-box.Left)==boxWidth
                    || Math.Abs(b.Top-box.Top)==boxHeight && Math.Abs(b.Left-box.Left)==boxWidth)
                {
                    boxes.Add(b);
                }
            }

            return boxes;

        }

        private bool IsSweepSuccessed()
        {
            int markedCount = 0;
            int mineCount = 0;

            foreach(Box box in this.Controls)
            {
                if(box.HasMine)
                {
                    mineCount++;
                }
                if(box.State == Box.BoxState.Marked)
                {
                    markedCount++;
                    //如果存在做了标记但没有地雷的方格，扫雷失败
                    if(!box.HasMine)
                    {
                        return false;
                    }
                }
            }
            return mineCount == markedCount;
        }
    }
}
