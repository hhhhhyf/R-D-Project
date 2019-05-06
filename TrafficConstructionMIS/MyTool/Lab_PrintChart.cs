using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyModel;
//引用word对象类库和命名空间
using MSWord = Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;

namespace MyTool
{
    /// <summary>
    /// 
    /// </summary>
    public class Lab_PrintChart
    {
        public void Paint(object wordFilePath,Lab_PrintPoint curves, int[] xArrary, int[] yArrary)//xArrary、yArrary分别装载x轴和y轴的坐标
        {
            //MSWord.Application wordApp;            //Word应用程序变量
            //MSWord.Document wordDoc;              //Word文档变量
            //wordApp = new MSWord.ApplicationClass();  //初始化
            int curveNum=curves.curveNum;
            int height = 270, width = 800;
            Font font = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            Font font1 = new System.Drawing.Font("宋体", 20, FontStyle.Regular);
            Font font2 = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
            Font font3 = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 800, 270), Color.Gray, Color.Gray, 0.5f, true);//网格颜色
            Brush brush1 = new SolidBrush(Color.Black);//SolidBrush单色画刷
            Brush brush2 = new SolidBrush(Color.Black);
            SolidBrush mybrush = new SolidBrush(Color.Black);
            Pen mypen = new Pen(brush, 1);
            Pen mypen1 = new Pen(Color.Black, 2);//数字代表线段粗细,绘制了x轴和y轴
            Pen mypen2 = new Pen(brush2, 1.8f);

            //一号曲线
            if (curveNum > 0)
            {
                Bitmap image1 = new Bitmap(width, height);
                Graphics g1 = Graphics.FromImage(image1);
                g1.Clear(Color.White);//清空图片背景色

                g1.FillRectangle(Brushes.AliceBlue, 0, 0, width, height);//图表背景色
                //g.DrawString("2", font1, brush1, new PointF(85, 30));//标题
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Black), 0, 0, image.Width - 1, image.Height - 1);

                g1.SmoothingMode = SmoothingMode.AntiAlias;//使绘图质量最高，即消除锯齿
                g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g1.CompositingQuality = CompositingQuality.HighQuality;

                //绘制线条
                //绘制纵向线条
                int x = 60;
                x = 60;
                g1.DrawLine(mypen1, x, 10, x, 255);//y轴。  后面4个参数分别为Y轴上端到左边距距离，上端到上边距距离，下端到左边距距离，下端到上边距距离
                //绘制横向线条
                int y = 10;
                for (int i = 0; i < 10; i++)
                {
                    g1.DrawLine(mypen, 60, y, 780, y);//表内网格
                    y = y + 24;
                }
                g1.DrawLine(mypen1, 55, y, 780, y);//x轴

                //绘制x和y坐标轴上的数字
                x = 53;//x轴第一个坐标值离左边的距离
                for (int i = 0; i < 11; i++)
                {
                    g1.DrawString(xArrary[i].ToString(), font, Brushes.Black, x, 253); //设置文字内容及输出位置
                    x = x + 70;//X轴数字之间的距离
                }
                y = 245 - 24;
                for (int i = 1; i < 11; i++)
                {
                    g1.DrawString(yArrary[i].ToString(), font, Brushes.Black, 8, y); //设置文字内容及输出位置
                    y = y - 24;
                }

                //显示折线效果
                
 
                int num;//在曲线中要绘制的点的个数
                if (curves.x_Arrary1.Count <= 720) { num = curves.x_Arrary1.Count; }//数据库中该曲线点少于720个
                else { num = 720; }//数据库中点该曲线点多于720个
                Point[] points1 = new Point[num];//points1为System.Drawing,代表一个个点
                for (int i = 0; i < num; i++)
                {
                    points1[i].X = curves.x_Arrary1[i]; points1[i].Y = curves.y_Arrary1[i];
                }
                g1.DrawLines(mypen2, points1); //绘制折线

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //image1.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的流中
                image1.Save("E://jtjsypt_data//YuHuan_TCMIS//lab//curve1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的文件中
                //System.Web.HttpContext.Current.Response.ClearContent();
                //System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                //System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

                g1.Save();
                g1.Dispose();
                image1.Dispose();
                //image1.Save("c:\\myBitmap.bmp");
                curveNum--;
            }

            //二号曲线
            if (curveNum > 0)
            {
                Bitmap image1 = new Bitmap(width, height);
                Graphics g1 = Graphics.FromImage(image1);
                g1.Clear(Color.White);//清空图片背景色

                g1.FillRectangle(Brushes.AliceBlue, 0, 0, width, height);//图表背景色
                //g.DrawString("2", font1, brush1, new PointF(85, 30));//标题
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Black), 0, 0, image.Width - 1, image.Height - 1);

                g1.SmoothingMode = SmoothingMode.AntiAlias;//使绘图质量最高，即消除锯齿
                g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g1.CompositingQuality = CompositingQuality.HighQuality;

                //绘制线条
                //绘制纵向线条
                int x = 60;
                x = 60;
                g1.DrawLine(mypen1, x, 10, x, 255);//y轴。  后面4个参数分别为Y轴上端到左边距距离，上端到上边距距离，下端到左边距距离，下端到上边距距离
                //绘制横向线条
                int y = 10;
                for (int i = 0; i < 10; i++)
                {
                    g1.DrawLine(mypen, 60, y, 780, y);//表内网格
                    y = y + 24;
                }
                g1.DrawLine(mypen1, 55, y, 780, y);//x轴

                //绘制x和y坐标轴上的数字
                x = 53;//x轴第一个坐标值离左边的距离
                for (int i = 0; i < 11; i++)
                {
                    g1.DrawString(xArrary[i].ToString(), font, Brushes.Black, x, 253); //设置文字内容及输出位置
                    x = x + 70;//X轴数字之间的距离
                }
                y = 245 - 24;
                for (int i = 1; i < 11; i++)
                {
                    g1.DrawString(yArrary[i].ToString(), font, Brushes.Black, 8, y); //设置文字内容及输出位置
                    y = y - 24;
                }

                //显示折线效果


                int num;//在曲线中要绘制的点的个数
                if (curves.x_Arrary2.Count <= 720) { num = curves.x_Arrary2.Count; }//数据库中该曲线点少于720个
                else { num = 720; }//数据库中点该曲线点多于720个
                Point[] points1 = new Point[num];//points1为System.Drawing,代表一个个点
                for (int i = 0; i < num; i++)
                {
                    points1[i].X = curves.x_Arrary2[i]; points1[i].Y = curves.y_Arrary2[i];
                }
                g1.DrawLines(mypen2, points1); //绘制折线

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //image1.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的流中
                image1.Save("E://jtjsypt_data//YuHuan_TCMIS//lab//curve2.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的文件中
                //System.Web.HttpContext.Current.Response.ClearContent();
                //System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                //System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

                g1.Save();
                g1.Dispose();
                image1.Dispose();
                //image1.Save("c:\\myBitmap.bmp");
                curveNum--;
            }


            //三号曲线
            if (curveNum > 0)
            {
                Bitmap image1 = new Bitmap(width, height);
                Graphics g1 = Graphics.FromImage(image1);
                g1.Clear(Color.White);//清空图片背景色

                g1.FillRectangle(Brushes.AliceBlue, 0, 0, width, height);//图表背景色
                //g.DrawString("2", font1, brush1, new PointF(85, 30));//标题
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Black), 0, 0, image.Width - 1, image.Height - 1);

                g1.SmoothingMode = SmoothingMode.AntiAlias;//使绘图质量最高，即消除锯齿
                g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g1.CompositingQuality = CompositingQuality.HighQuality;

                //绘制线条
                //绘制纵向线条
                int x = 60;
                x = 60;
                g1.DrawLine(mypen1, x, 10, x, 255);//y轴。  后面4个参数分别为Y轴上端到左边距距离，上端到上边距距离，下端到左边距距离，下端到上边距距离
                //绘制横向线条
                int y = 10;
                for (int i = 0; i < 10; i++)
                {
                    g1.DrawLine(mypen, 60, y, 780, y);//表内网格
                    y = y + 24;
                }
                g1.DrawLine(mypen1, 55, y, 780, y);//x轴

                //绘制x和y坐标轴上的数字
                x = 53;//x轴第一个坐标值离左边的距离
                for (int i = 0; i < 11; i++)
                {
                    g1.DrawString(xArrary[i].ToString(), font, Brushes.Black, x, 253); //设置文字内容及输出位置
                    x = x + 70;//X轴数字之间的距离
                }
                y = 245 - 24;
                for (int i = 1; i < 11; i++)
                {
                    g1.DrawString(yArrary[i].ToString(), font, Brushes.Black, 8, y); //设置文字内容及输出位置
                    y = y - 24;
                }

                //显示折线效果


                int num;//在曲线中要绘制的点的个数
                if (curves.x_Arrary3.Count <= 720) { num = curves.x_Arrary3.Count; }//数据库中该曲线点少于720个
                else { num = 720; }//数据库中点该曲线点多于720个
                Point[] points1 = new Point[num];//points1为System.Drawing,代表一个个点
                for (int i = 0; i < num; i++)
                {
                    points1[i].X = curves.x_Arrary3[i]; points1[i].Y = curves.y_Arrary3[i];
                }
                g1.DrawLines(mypen2, points1); //绘制折线

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //image1.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的流中
                image1.Save("E://jtjsypt_data//YuHuan_TCMIS//lab//curve3.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的文件中
                //System.Web.HttpContext.Current.Response.ClearContent();
                //System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                //System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

                g1.Save();
                g1.Dispose();
                image1.Dispose();
                //image1.Save("c:\\myBitmap.bmp");
                curveNum--;
            }



            //四号曲线
            if (curveNum > 0)
            {
                Bitmap image1 = new Bitmap(width, height);
                Graphics g1 = Graphics.FromImage(image1);
                g1.Clear(Color.White);//清空图片背景色

                g1.FillRectangle(Brushes.AliceBlue, 0, 0, width, height);//图表背景色
                //g.DrawString("2", font1, brush1, new PointF(85, 30));//标题
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Black), 0, 0, image.Width - 1, image.Height - 1);

                g1.SmoothingMode = SmoothingMode.AntiAlias;//使绘图质量最高，即消除锯齿
                g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g1.CompositingQuality = CompositingQuality.HighQuality;

                //绘制线条
                //绘制纵向线条
                int x = 60;
                x = 60;
                g1.DrawLine(mypen1, x, 10, x, 255);//y轴。  后面4个参数分别为Y轴上端到左边距距离，上端到上边距距离，下端到左边距距离，下端到上边距距离
                //绘制横向线条
                int y = 10;
                for (int i = 0; i < 10; i++)
                {
                    g1.DrawLine(mypen, 60, y, 780, y);//表内网格
                    y = y + 24;
                }
                g1.DrawLine(mypen1, 55, y, 780, y);//x轴

                //绘制x和y坐标轴上的数字
                x = 53;//x轴第一个坐标值离左边的距离
                for (int i = 0; i < 11; i++)
                {
                    g1.DrawString(xArrary[i].ToString(), font, Brushes.Black, x, 253); //设置文字内容及输出位置
                    x = x + 70;//X轴数字之间的距离
                }
                y = 245 - 24;
                for (int i = 1; i < 11; i++)
                {
                    g1.DrawString(yArrary[i].ToString(), font, Brushes.Black, 8, y); //设置文字内容及输出位置
                    y = y - 24;
                }

                //显示折线效果


                int num;//在曲线中要绘制的点的个数
                if (curves.x_Arrary4.Count <= 720) { num = curves.x_Arrary4.Count; }//数据库中该曲线点少于720个
                else { num = 720; }//数据库中点该曲线点多于720个
                Point[] points1 = new Point[num];//points1为System.Drawing,代表一个个点
                for (int i = 0; i < num; i++)
                {
                    points1[i].X = curves.x_Arrary4[i]; points1[i].Y = curves.y_Arrary4[i];
                }
                g1.DrawLines(mypen2, points1); //绘制折线

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //image1.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的流中
                image1.Save("E://jtjsypt_data//YuHuan_TCMIS//lab//curve4.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的文件中
                //System.Web.HttpContext.Current.Response.ClearContent();
                //System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                //System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

                g1.Save();
                g1.Dispose();
                image1.Dispose();
                //image1.Save("c:\\myBitmap.bmp");
                curveNum--;
            }



            //五号曲线
            if (curveNum > 0)
            {
                Bitmap image1 = new Bitmap(width, height);
                Graphics g1 = Graphics.FromImage(image1);
                g1.Clear(Color.White);//清空图片背景色

                g1.FillRectangle(Brushes.AliceBlue, 0, 0, width, height);//图表背景色
                //g.DrawString("2", font1, brush1, new PointF(85, 30));//标题
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Black), 0, 0, image.Width - 1, image.Height - 1);

                g1.SmoothingMode = SmoothingMode.AntiAlias;//使绘图质量最高，即消除锯齿
                g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g1.CompositingQuality = CompositingQuality.HighQuality;

                //绘制线条
                //绘制纵向线条
                int x = 60;
                x = 60;
                g1.DrawLine(mypen1, x, 10, x, 255);//y轴。  后面4个参数分别为Y轴上端到左边距距离，上端到上边距距离，下端到左边距距离，下端到上边距距离
                //绘制横向线条
                int y = 10;
                for (int i = 0; i < 10; i++)
                {
                    g1.DrawLine(mypen, 60, y, 780, y);//表内网格
                    y = y + 24;
                }
                g1.DrawLine(mypen1, 55, y, 780, y);//x轴

                //绘制x和y坐标轴上的数字
                x = 53;//x轴第一个坐标值离左边的距离
                for (int i = 0; i < 11; i++)
                {
                    g1.DrawString(xArrary[i].ToString(), font, Brushes.Black, x, 253); //设置文字内容及输出位置
                    x = x + 70;//X轴数字之间的距离
                }
                y = 245 - 24;
                for (int i = 1; i < 11; i++)
                {
                    g1.DrawString(yArrary[i].ToString(), font, Brushes.Black, 8, y); //设置文字内容及输出位置
                    y = y - 24;
                }

                //显示折线效果


                int num;//在曲线中要绘制的点的个数
                if (curves.x_Arrary5.Count <= 720) { num = curves.x_Arrary5.Count; }//数据库中该曲线点少于720个
                else { num = 720; }//数据库中点该曲线点多于720个
                Point[] points1 = new Point[num];//points1为System.Drawing,代表一个个点
                for (int i = 0; i < num; i++)
                {
                    points1[i].X = curves.x_Arrary5[i]; points1[i].Y = curves.y_Arrary5[i];
                }
                g1.DrawLines(mypen2, points1); //绘制折线

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //image1.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的流中
                image1.Save("E://jtjsypt_data//YuHuan_TCMIS//lab//curve5.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的文件中
                //System.Web.HttpContext.Current.Response.ClearContent();
                //System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                //System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

                g1.Save();
                g1.Dispose();
                image1.Dispose();
                //image1.Save("c:\\myBitmap.bmp");
                curveNum--;
            }



            //六号曲线
            if (curveNum > 0)
            {
                Bitmap image1 = new Bitmap(width, height);
                Graphics g1 = Graphics.FromImage(image1);
                g1.Clear(Color.White);//清空图片背景色

                g1.FillRectangle(Brushes.AliceBlue, 0, 0, width, height);//图表背景色
                //g.DrawString("2", font1, brush1, new PointF(85, 30));//标题
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Black), 0, 0, image.Width - 1, image.Height - 1);

                g1.SmoothingMode = SmoothingMode.AntiAlias;//使绘图质量最高，即消除锯齿
                g1.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g1.CompositingQuality = CompositingQuality.HighQuality;

                //绘制线条
                //绘制纵向线条
                int x = 60;
                x = 60;
                g1.DrawLine(mypen1, x, 10, x, 255);//y轴。  后面4个参数分别为Y轴上端到左边距距离，上端到上边距距离，下端到左边距距离，下端到上边距距离
                //绘制横向线条
                int y = 10;
                for (int i = 0; i < 10; i++)
                {
                    g1.DrawLine(mypen, 60, y, 780, y);//表内网格
                    y = y + 24;
                }
                g1.DrawLine(mypen1, 55, y, 780, y);//x轴

                //绘制x和y坐标轴上的数字
                x = 53;//x轴第一个坐标值离左边的距离
                for (int i = 0; i < 11; i++)
                {
                    g1.DrawString(xArrary[i].ToString(), font, Brushes.Black, x, 253); //设置文字内容及输出位置
                    x = x + 70;//X轴数字之间的距离
                }
                y = 245 - 24;
                for (int i = 1; i < 11; i++)
                {
                    g1.DrawString(yArrary[i].ToString(), font, Brushes.Black, 8, y); //设置文字内容及输出位置
                    y = y - 24;
                }

                //显示折线效果


                int num;//在曲线中要绘制的点的个数
                if (curves.x_Arrary6.Count <= 720) { num = curves.x_Arrary6.Count; }//数据库中该曲线点少于720个
                else { num = 720; }//数据库中点该曲线点多于720个
                Point[] points1 = new Point[num];//points1为System.Drawing,代表一个个点
                for (int i = 0; i < num; i++)
                {
                    points1[i].X = curves.x_Arrary6[i]; points1[i].Y = curves.y_Arrary6[i];
                }
                g1.DrawLines(mypen2, points1); //绘制折线

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //image1.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的流中
                image1.Save("E://jtjsypt_data//YuHuan_TCMIS//lab//curve6.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//以指定格式保存到指定的文件中
                //System.Web.HttpContext.Current.Response.ClearContent();
                //System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                //System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

                g1.Save();
                g1.Dispose();
                image1.Dispose();
                //image1.Save("c:\\myBitmap.bmp");
                curveNum--;
            }

        }
    }
}
