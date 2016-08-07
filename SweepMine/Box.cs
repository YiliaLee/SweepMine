using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SweepMine
{
    public class Box: Button
    {
        public Box()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.State = BoxState.Closed;
        }
        /// <summary>
        /// 当前方块状态
        /// </summary>
       public enum BoxState
        {
            /// <summary>
            /// 打开状态
            /// </summary>
            Opened,
            /// <summary>
            /// 标记状态
            /// </summary>
            Marked,
            /// <summary>
            /// 关闭状态
            /// </summary>
            Closed
        }
        /// <summary>
        /// 获取或设置当前方块周围地雷的数量
        /// </summary>
        public int AroundMIneCount;
        /// <summary>
        /// 获取或设置当前方块是否有雷
        /// </summary>
        public bool HasMine;
        /// <summary>
        ///获取或设置当前方块的状态
        /// </summary>
        public BoxState State;

        /// <summary>
        /// 标记当前方块为‘有雷’状态，插上小红旗
        /// </summary>
        public void Mark()
        {
            this.BackgroundImage = Properties.Resources.flag;
            this.State = BoxState.Marked;
        }
        /// <summary>
        /// 打开方块
        /// </summary>
        public void Open()
        {
            if(this.HasMine)
            {
                this.BackgroundImage = Properties.Resources.bomb0;
            }
            else
            {
                switch(this.AroundMIneCount)
                {
                    case 0:
                        this.BackgroundImage = Properties.Resources.s;
                        break;
                    case 1:
                         this.BackgroundImage = Properties.Resources._1;
                        break;
                    case 2:
                         this.BackgroundImage = Properties.Resources._2;
                        break;
                    case 3:
                         this.BackgroundImage = Properties.Resources._3;
                        break;
                    case 4:
                         this.BackgroundImage = Properties.Resources._4;
                        break;
                    case 5:
                         this.BackgroundImage = Properties.Resources._5;
                        break;
                    case 6:
                         this.BackgroundImage = Properties.Resources._6;
                        break;
                    case 7:
                         this.BackgroundImage = Properties.Resources._7;
                        break;
                    case 8:
                         this.BackgroundImage = Properties.Resources._8;
                        break;
                }
                this.State = BoxState.Opened;
                this.Enabled = false;
            }
          
        }
        /// <summary>
        /// 取消Mark()操作结果，即去掉小红旗
        /// </summary>
        public void Reset()
        {
            this.BackgroundImage = null;
            this.State = BoxState.Closed;
        }
    }

}
