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
    public partial class sel_role :Form
    {
        public sel_role()
        {
            InitializeComponent();
        }

        private void sel_role_Load(object sender, EventArgs e)
        {
            this.Left = Entrance.game_win.X + 390;
            this.Top = Entrance.game_win.Y + 380;

            add_Role();
        }
        
        void add_Role()
        {           
            flowLayoutPanel1.Controls.Clear();
            var table =Form_main.Run_data.roles_List.Where(a=>a.camp == game_base.Camps.Neutral).ToList();
            for(int i = 0;i < table.Count;i++)
            {
                CheckBox cc =new CheckBox();
                cc.Width = 120;
                cc.Text = table[i].name;                
                cc.Enabled = table[i].game_check;

                int index =table[i].index;
                cc.Checked = Form_main.Run_data.player_check_ary[index];            
                
                //checkBox_Neutral.Add(cc);
                flowLayoutPanel1.Controls.Add(cc);                
            }

            flowLayoutPanel2.Controls.Clear();
            table =Form_main.Run_data.roles_List.Where(a=>a.camp == game_base.Camps.Positive).ToList();
            for(int i = 0;i < table.Count;i++)
            {
                CheckBox cc =new CheckBox();
                cc.Width = 120;
                cc.Text = table[i].name;
                cc.Enabled = table[i].game_check;

                int index =table[i].index;
                cc.Checked = Form_main.Run_data.player_check_ary[index];

                //checkBox_Neutral.Add(cc);
                flowLayoutPanel2.Controls.Add(cc);
            }

            flowLayoutPanel3.Controls.Clear();    
            table = Form_main.Run_data.roles_List.Where(a => a.camp == game_base.Camps.Villain).ToList();
            for(int i = 0;i < table.Count;i++)
            {
                CheckBox cc =new CheckBox();
                cc.Width = 120;
                cc.Text = table[i].name;
                cc.Enabled = table[i].game_check;

                int index =table[i].index;
                cc.Checked = Form_main.Run_data.player_check_ary[index];

                //checkBox_Neutral.Add(cc);
                flowLayoutPanel3.Controls.Add(cc);
            }
        }

        private void sel_role_FormClosing(object sender, FormClosingEventArgs e)
        {
            save_data();
        }

        void save_data()
        {
            for(int i = 0;i < flowLayoutPanel1.Controls.Count;i++)
            {
                CheckBox aa = (CheckBox) flowLayoutPanel1.Controls[i];
         
                int index = Form_main.Run_data.roles_List.Single(a=>a.name==aa.Text && a.camp == game_base.Camps.Neutral).index;
                Form_main.Run_data.player_check_ary[index] = aa.Checked;
            }

            for(int i = 0;i < flowLayoutPanel2.Controls.Count;i++)
            {
                CheckBox aa = (CheckBox) flowLayoutPanel2.Controls[i];
         
                int index = Form_main.Run_data.roles_List.Single(a=>a.name==aa.Text && a.camp == game_base.Camps.Positive).index;
                Form_main.Run_data.player_check_ary[index] = aa.Checked;
            }

            for(int i = 0;i < flowLayoutPanel3.Controls.Count;i++)
            {
                CheckBox aa = (CheckBox) flowLayoutPanel3.Controls[i];
             
                int index = Form_main.Run_data.roles_List.Single(a=>a.name==aa.Text && a.camp == game_base.Camps.Villain).index;
                Form_main.Run_data.player_check_ary[index] = aa.Checked;
            }
        }
    }
}
