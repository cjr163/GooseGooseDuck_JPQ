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
    public partial class Entrance :Form
    {
        public Entrance()
        {
            InitializeComponent();
        }

        //有时间的话,想添加如下功能:
        //独立颜色不跟全盘色
        //透明度拉条
        //下拉框显示角色图标
        //角色字体颜色
        //任务栏图标,退出
        //带刀牌中立牌可勾选不显示

        static Form_main main_form =new Form_main();
        public static Point game_win ;//api读取游戏位置
        static Point game_win_bk ;//bk
        public static Color 透明色 = Color.DarkKhaki;

        IntPtr hWnd_Game = IntPtr.Zero;//游戏窗口句柄

        static bool is_main_show=false;

        private void entrance_Load(object sender, EventArgs e)
        {
            this.Width = 180;
            this.Height = 30;
            //this.Left += 600;
            //this.Top -= 300;
            this.BackColor = 透明色;//透明
            this.TransparencyKey = 透明色;//透明 (R值不等于B值,鼠标不穿透)            
            this.TopMost = true;

            game_win = new Point { X = this.Left - 800, Y = this.Top - 600 };//取默认位置
            game_win_bk = game_win;


            main_form.Show();
            main_form.Hide();

            this.Left = game_win.X + 1185;
            this.Top = game_win.Y + 60;

            main_form.Left = game_win.X + 70;
            main_form.Top = game_win.Y + 210;

            timer1.Interval = 200;
            timer1.Enabled = true;

        }

        void main_form_show()
        {
            is_main_show = true;
            main_form.Show();
            button1.Text = "隐藏";
            button1.BackColor = Color.Gold;
        }
        void main_form_hide()
        {
            is_main_show = false;
            main_form.Hide();
            button1.Text = "显示";
            button1.BackColor = Color.LightGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(is_main_show == false)
                main_form_show();
            else
                main_form_hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            main_form.Close();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer_follow_win();
        }

        void timer_follow_win()
        {
            hWnd_Game = win_api.FindWindow("UnityWndClass", "Goose Goose Duck");
            if(hWnd_Game != IntPtr.Zero)
            {
                if(win_api.find_win(hWnd_Game, ref game_win_bk) == false)   //if(game_win.X >= -1500)//|| game_win.Y >= 0 )
                {
                    game_win_bk.X = 400;
                    game_win_bk.Y = 500;
                }

                if(game_win != game_win_bk)
                {
                    game_win = game_win_bk;

                    this.Left = game_win.X + 1185;
                    this.Top = game_win.Y + 60;

                    main_form.Left = game_win.X + 70;
                    main_form.Top = game_win.Y + 210;
                }
            }
        }

    }
}
