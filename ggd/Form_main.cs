using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ggd
{
    public partial class Form_main :Form
    {
        public Form_main()
        {
            InitializeComponent();
        }

        public PictureBox pictureBox1;//边框
        public IntPtr this_Handle =IntPtr.Zero;//本窗句柄
        public static bool 是否显示色框 = true;

        /// <summary>
        /// 角色属性
        /// </summary>
        public struct ST_run_data
        {
            /// <summary>
            /// 所选地图
            /// </summary>
            public string map_name;
            /// <summary>
            /// 游戏设定角色
            /// </summary>
            public List<game_base.ST_role_attribute> roles_List;
            /// <summary>
            /// 玩家勾选设定
            /// </summary>
            public bool[] player_check_ary;
            /// <summary>
            /// 所选人数
            /// </summary>
            public int sel_cnt;
        }

        public static ST_run_data Run_data  = new ST_run_data();
        public static List<string> 本图角色=new List<string>();

        public static List<UserControl1> uc_player =new List<UserControl1>();

        game_base game基数 =new game_base();
        List<game_base.ST_map> game_Maps ;

        public static bool 是否显示备注框=true;

        /// <summary>
        /// 设置下拉框样式
        /// </summary>
        void set_combobox_size()
        {
            comboBox_map.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_map.ItemHeight = 24;
            comboBox_map.DrawMode = DrawMode.OwnerDrawFixed;

            comboBox_map.DrawItem += new DrawItemEventHandler(delegate (object sender, DrawItemEventArgs e)
            {
                if(e.Index < 0)
                {
                    return;
                }
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(comboBox_map.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 4);
            });

            comboBox_cnt.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_cnt.ItemHeight = 24;
            comboBox_cnt.DrawMode = DrawMode.OwnerDrawFixed;

            comboBox_cnt.DrawItem += new DrawItemEventHandler(delegate (object sender, DrawItemEventArgs e)
            {
                if(e.Index < 0)
                {
                    return;
                }
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(comboBox_cnt.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 4);
            });
        }


        private void Form_main_Load(object sender, EventArgs e)
        {
            set_combobox_size();

            //this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Entrance.透明色;
            this.TransparencyKey = Entrance.透明色;
            this.TopMost = true;
            label1.BackColor = Entrance.透明色;
            label2.BackColor = Entrance.透明色;

            this_Handle = this.Handle;
            game_Maps = game基数.Map_List;

            ccc_init();

            color_frame.bmp_kk_main = new Bitmap(this.Width, this.Height);
            pictureBox1 = new PictureBox();
            this.Controls.Add(pictureBox1);

            pictureBox1.Left = 0;
            pictureBox1.Top = 0;
            pictureBox1.Width = this.Width;
            pictureBox1.Height = this.Height;
            //pictureBox1.BringToFront();
            pictureBox1.SendToBack();//将控件放到所有控件最低端

            pictureBox1.BackColor = Entrance.透明色;

            color_frame.draw_frame(ref color_frame.bmp_kk_main, color_frame.def_color);
            pictureBox1.Image = color_frame.bmp_kk_main;


            pictureBox2.BackColor = color_frame.def_color;

        }
        /// <summary>
        /// 初始化
        /// </summary>
        void ccc_init()
        {
            uc_player.Clear();
            for(int i = 0;i < 16;i++)
            {
                uc_player.Add(new UserControl1());
                uc_player[i].Location = new Point(133 + i % 4 * 395, 64 + i / 4 * 181);
                uc_player[i].BackColor = Entrance.透明色;
                uc_player[i].Visible = false;
                uc_player[i].Control_index = i;
                this.Controls.Add(uc_player[i]);
            }
            for(int i = 0;i < Run_data.sel_cnt;i++)
            {
                uc_player[i].Visible = true;
            }
            comboBox_cnt.SelectedItem = comboBox_cnt.Items[comboBox_cnt.Items.Count - 1];
            Run_data.sel_cnt = Convert.ToInt32(comboBox_cnt.SelectedItem);

            ////////

            comboBox_map.Items.Clear();
            foreach(string s in Enum.GetNames(typeof(game_base.ENUM_Maps)))
            {
                comboBox_map.Items.Add(s);
            }
            comboBox_map.SelectedItem = comboBox_map.Items[0];
            Run_data.map_name = comboBox_map.Text;

            /////////               

            get_role_data();
            refresh_data();
            foreach(var uu in uc_player)
            {
                uu.reset_value();//更新框
            }
        }
        /// <summary>
        /// 读取基础数据
        /// </summary>
        void get_role_data()
        {
            var mmm = game_Maps.Single(a => a.name == Run_data.map_name);
            Run_data.roles_List = mmm.roles_List.ToList(); //复制,防修改源

            Run_data.player_check_ary = new bool[Run_data.roles_List.Count];

            for(int i = 0;i < Run_data.roles_List.Count;i++)
            {
                Run_data.player_check_ary[i] = Run_data.roles_List[i].game_check; //初始化玩家勾选
            }

        }
        /// <summary>
        /// 刷新地图角色
        /// </summary>
        void refresh_data()
        {
            List<game_base.ST_role_attribute> check_list =new List<game_base.ST_role_attribute>();

            for(int i = 0;i < Run_data.player_check_ary.Count();i++)
            {
                if(Run_data.player_check_ary[i])
                {
                    var a=  Run_data.roles_List[i];
                    check_list.Add(a);                  //所有勾选角色到新表
                }
            }

            本图角色.Clear();
            本图角色.Add("");
            本图角色.Add("带刀牌");
            本图角色.Add("单走牌");
            本图角色.Add("----中----");

            var table = check_list.Where(a=>a.camp == game_base.Camps.Neutral).ToList();
            foreach(var a in table)
            {
                本图角色.Add(a.name);
            }

            本图角色.Add("----好----");
            table = check_list.Where(a => a.camp == game_base.Camps.Positive).ToList();
            foreach(var a in table)
            {
                本图角色.Add(a.name);
            }

            本图角色.Add("----坏----");
            table = check_list.Where(a => a.camp == game_base.Camps.Villain).ToList();
            foreach(var a in table)
            {
                本图角色.Add(a.name);
            }
        }

        private void comboBox_map_SelectedIndexChanged(object sender, EventArgs e)
        {
            Run_data.map_name = comboBox_map.Text;
            get_role_data();
            refresh_data();
            foreach(var uu in uc_player)
            {
                uu.reset_value_and_recover();
            }

        }

        private void comboBox_cnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            Run_data.sel_cnt = Convert.ToInt32(comboBox_cnt.SelectedItem);

            if(uc_player.Count > 0)
            {
                for(int i = 0;i < 16;i++)
                {
                    if(i < Run_data.sel_cnt)
                        uc_player[i].Visible = true;
                    else
                        uc_player[i].Visible = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //清空
        {
            foreach(var uu in uc_player)
            {
                uu.reset_value();//更新框
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(是否显示备注框)
            {
                是否显示备注框 = false;
                button2.Text = "显示备注";
            }
            else
            {
                是否显示备注框 = true;
                button2.Text = "隐藏备注";
            }

            foreach(var uu in uc_player)
            {
                uu.set_textbox();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sel_role aa =new sel_role();
            aa.ShowDialog();

            //返回刷新勾选

            refresh_data();
            foreach(var uu in uc_player)
            {
                uu.reset_value_and_recover();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(是否显示色框 == false)
            {
                是否显示色框 = true;
                button4.BackColor = Color.LightGray;
                pictureBox1.Image = color_frame.bmp_kk_main;
                for(int i = 0;i < 16;i++)
                {
                    Form_main.uc_player[i].set_frame(true, i);
                }

                pictureBox2.Visible = true;
            }
            else
            {
                是否显示色框 = false;
                button4.BackColor = Color.LightGreen;
                pictureBox1.Image = null;
                for(int i = 0;i < 16;i++)
                {
                    Form_main.uc_player[i].set_frame(false, i);
                }

                pictureBox2.Visible = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ColorDialog a =new ColorDialog();

            a.Color = Entrance.透明色;
            a.ShowDialog();

            if(a.Color != Entrance.透明色)
            {               
                color_frame.draw_frame(ref color_frame.bmp_kk_main, a.Color);
                pictureBox1.Image = color_frame.bmp_kk_main;

                for(int i = 0;i < 16;i++)
                {
                    uc_player[i].is_reset_frame(a.Color);
                }

                color_frame.def_color = a.Color;
            }

            a.Dispose();

            pictureBox2.BackColor = color_frame.def_color;
        }
    }
}
