using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ggd
{
    public class color_frame
    {

        public static Bitmap bmp_kk_main ;//主框框
        public static Bitmap[] bmp_k =new Bitmap[16];//框框*16
        public static Bitmap[] bmp_sel =new Bitmap[16];//选择色盘*16
        public static Color def_color =Color.Green;//默认色

        public static void draw_frame(ref Bitmap bmp, Color ccc)
        {
            //bmp.MakeTransparent();
            //bmp_kk_main = new Bitmap(Width, Height);

            Graphics aa = Graphics.FromImage(bmp);
            //aa.Clear(Color.Red);//透明
            //aa.DrawLine(Pens.Gold, new Point { X = 0, Y = 0 }, new Point { X = 500, Y = 500 });
          
            aa.DrawRectangle(new Pen(ccc), 0, 0, bmp.Width - 1, bmp.Height - 1);//Pens.Green
            aa.DrawRectangle(new Pen(ccc), 1, 1, bmp.Width - 3, bmp.Height - 3);

            //Brush bbb =new SolidBrush(Color.Green);
            //aa.FillRectangle(bbb, 0, 0, this.Width - 1, 2);
            aa.Dispose();
        }

        public static void fill_frame(ref Bitmap bmp, Color ccc)
        {
            //bmp.MakeTransparent();
            //bmp_kk_main = new Bitmap(Width, Height);

            Graphics aa = Graphics.FromImage(bmp);
            //aa.Clear(Color.Red);//透明
            //aa.DrawLine(Pens.Gold, new Point { X = 0, Y = 0 }, new Point { X = 500, Y = 500 });

            aa.DrawRectangle(new Pen(ccc), 0, 0, bmp.Width - 1, bmp.Height - 1);//Pens.Green
            aa.DrawRectangle(new Pen(ccc), 1, 1, bmp.Width - 3, bmp.Height - 3);

            //Brush bbb =new SolidBrush(Color.Green);
            //aa.FillRectangle(bbb, 0, 0, this.Width - 1, 2);
            aa.Dispose();
        }

    }
}
