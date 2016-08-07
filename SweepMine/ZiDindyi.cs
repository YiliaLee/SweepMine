using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SweepMine
{
    public partial class ZiDindyi : Form
    {
        public ZiDindyi()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string txtHeight = this.txtHeight.Text;
            string txtWidth = this.txtWidth.Text;
            string txtMines = this.txtMines.Text;
            int resHeight = 0;
            int resWidth = 0;
            int resMines = 0;
            if(!int.TryParse(txtHeight,out resHeight)
                || !int.TryParse(txtWidth, out resWidth)
                || !int.TryParse(txtMines, out resMines))
            {
                MessageBox.Show("请确保输入的值为纯数字!");
                return;
            }

            if(resMines >= 400)
            {
                MessageBox.Show("雷的数量不得超过400!");
                return;
            }
            this.Owner.Tag = resMines; 
            
            this.Close();
        }

        private void ZiDindyi_Load(object sender, EventArgs e)
        {
            int parWidth = this.Owner.Width;
            int parHeight = this.Owner.Height;
            this.txtHeight.Text = parHeight.ToString();
            this.txtWidth.Text = parWidth.ToString();
        }
    }
}
