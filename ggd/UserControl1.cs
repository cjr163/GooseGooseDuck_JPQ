using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ggd
{
    public partial class UserControl1 :UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public PictureBox pictureBox1;
        public int Control_index;//本控件数组索引
        Color frame_c = color_frame.def_color;//框颜色
        
        /// <summary>
        /// 设置下拉框样式
        /// </summary>
        void set_combobox_size()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.ItemHeight = 24;
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;

            comboBox1.DrawItem += new DrawItemEventHandler(delegate (object sender, DrawItemEventArgs e)
            {
                if(e.Index < 0)
                {
                    return;
                }
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(comboBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 4);
            });
            //e.Graphics.DrawString(comboBox1.Items[e.Index].ToString(), new Font("黑体", 9), new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 4);

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.ItemHeight = 24;
            comboBox2.DrawMode = DrawMode.OwnerDrawFixed;

            comboBox2.DrawItem += new DrawItemEventHandler(delegate (object sender, DrawItemEventArgs e)
            {
                if(e.Index < 0)
                {
                    return;
                }
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(comboBox2.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 4);
            });

            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.ItemHeight = 24;
            comboBox3.DrawMode = DrawMode.OwnerDrawFixed;

            comboBox3.DrawItem += new DrawItemEventHandler(delegate (object sender, DrawItemEventArgs e)
            {
                if(e.Index < 0)
                {
                    return;
                }
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(comboBox3.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 4);
            });
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            set_combobox_size();

            label1.BackColor = Entrance.透明色;
            label2.BackColor = Entrance.透明色;
            label3.BackColor = Entrance.透明色;

            color_frame.bmp_k[Control_index] = new Bitmap(this.Width, this.Height);
            pictureBox1 = new PictureBox();
            this.Controls.Add(pictureBox1);

            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;
            pictureBox1.SendToBack();//将控件放到所有控件最低端

            pictureBox1.BackColor = Entrance.透明色;

            color_frame.bmp_sel[Control_index] = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox2.BackColor = Entrance.透明色;

            s_k();

            set_textbox();
        }

        /// <summary>
        /// 刷框
        /// </summary>
        void s_k()
        {
            if(Form_main.是否显示色框)
            {
                color_frame.draw_frame(ref color_frame.bmp_k[Control_index], frame_c);
                pictureBox1.Image = color_frame.bmp_k[Control_index];

                color_frame.draw_frame(ref color_frame.bmp_sel[Control_index], frame_c);
                pictureBox2.Image = color_frame.bmp_sel[Control_index];
            }
            else
            {
                pictureBox1.Image = null;
                pictureBox2.Image = null;
            }
        }
        /// <summary>
        /// 清框后恢复原所选项 不清备注
        /// </summary>
        public void reset_value_and_recover()
        {
            string combe1 = comboBox1.Text;
            string combe2 = comboBox2.Text;
            string combe3 = comboBox3.Text;

            reset_combe();

            foreach(string i in comboBox1.Items)
            {
                if(i == combe1)
                    comboBox1.SelectedItem = i;
            }
            foreach(string i in comboBox2.Items)
            {
                if(i == combe2)
                    comboBox2.SelectedItem = i;
            }
            foreach(string i in comboBox3.Items)
            {
                if(i == combe3)
                    comboBox3.SelectedItem = i;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void reset_value()
        {
            reset_combe();
            textBox1.Text = "";


            frame_c = color_frame.def_color;//框颜色
            s_k();
        }

        void reset_combe()
        {
            comboBox1.Items.Clear();
            foreach(var a in Form_main.本图角色)
            {
                comboBox1.Items.Add(a);
            }

            comboBox2.Items.Clear();
            foreach(var a in Form_main.本图角色)
            {
                comboBox2.Items.Add(a);
            }

            comboBox3.Items.Clear();
            foreach(var a in Form_main.本图角色)
            {
                comboBox3.Items.Add(a);
            }
        }

        public void set_textbox()
        {
            textBox1.Visible = Form_main.是否显示备注框;
        }
                
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ColorDialog a =new ColorDialog();
           
            a.Color = Entrance.透明色;
            a.ShowDialog();

            if(a.Color != Entrance.透明色)
            {
                frame_c = a.Color;
                s_k();
            }

            a.Dispose();
        }

        public void set_frame(bool is_show, int i)
        {
            if(is_show)
            {
                pictureBox1.Image = color_frame.bmp_k[i];                
                pictureBox2.Image = color_frame.bmp_sel[i];
            }
            else
            {
                pictureBox1.Image = null;
                pictureBox2.Image = null;
            }
        }

        /// <summary>
        /// 如有设置,不恢复色
        /// </summary>
        public void is_reset_frame(Color ne)
        {
            if(frame_c != color_frame.def_color) //如有设置,不恢复色
                return;

            frame_c = ne;
            s_k();
        }
    }
}
