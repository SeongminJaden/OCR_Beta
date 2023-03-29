using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace captureOcr
{
    public partial class Form1 : Form
    {

        Bitmap btMain;
        private int time = 0;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void time_tick(object sender, EventArgs e)
        {
             time++;
             Rectangle rect = new Rectangle(30, 300, 250, 250);
            int bitsperPixel = Screen.PrimaryScreen.BitsPerPixel;
            PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
            if (bitsperPixel <= 16)
            {
                pixelFormat = PixelFormat.Format16bppRgb565;
            }
            if (bitsperPixel == 24)
            {
                pixelFormat = PixelFormat.Format24bppRgb;
            }

            btMain = new Bitmap(rect.Width, rect.Height, pixelFormat);

            //화면 크기만큼의 Bitmap 생성
            using (Graphics g = Graphics.FromImage(btMain))
            //bitmap 이미지 변경을 위해 Graphics 객체 생성
            {
                g.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
                 g.InterpolationMode = InterpolationMode.High;
                 g.CompositingQuality = CompositingQuality.HighQuality;
                 g.SmoothingMode = SmoothingMode.AntiAlias;

                //Picture Box Display 
               btMain = new Bitmap(btMain, new Size(1500, 1500));
               // pictureBox1.Image = btMain; 
                btMain.Save(@"D:\exam\image.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(time_tick);
            timer.Start();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var menu = new string[] {"컵", "너", "믿", "오", "태"};
            int flag = 7;
            int flag2 = 0;
            Bitmap img = new Bitmap(pictureBox1.Image);
            var ocr = new TesseractEngine("./tessdata", "kor", EngineMode.Default);
            var texts = ocr.Process(img);
            Console.WriteLine(texts.GetText());
            //MessageBox.Show(texts.GetText());
            String data = texts.GetText();
            for (int i = 0; i < menu.Length; i++)
            {
                if (data.Contains(menu[i]))
                { flag = i; flag2 = 1; }
            }

            if(flag2 == 1) {

                    switch (flag)
                    {
                        case 0:
                            Console.WriteLine("업체 : 행컵");
                            break;
                        case 1:
                            Console.WriteLine("업체 : 워너비 박스");
                            break;
                        case 2:
                            Console.WriteLine("업체 : 믿고");
                            break;
                        case 3:
                            Console.WriteLine("업체 : 오떡후");
                            break;
                        case 4:
                            Console.WriteLine("업체 : 이태리돈까스");
                            break;
                    }
                    flag2 = 0;
            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
          
            for (int i = 0; i < btMain.Width; i++) { 
                for (int j = 0; j < btMain.Height; j++) { 
                    Color c = btMain.GetPixel(i, j); 
                    int binary = (c.R + c.G + c.B) / 3; 
                    if (binary > 185) 
                            btMain.SetPixel(i, j, Color.Black); 
                    else btMain.SetPixel(i, j, Color.White); 
                } 
            } 
            pictureBox1.Image = btMain; 

            btMain.Save(@"D:\exam\imagee.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

 
    }
}
