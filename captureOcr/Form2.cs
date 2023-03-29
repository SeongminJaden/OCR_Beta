using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;



namespace captureOcr
{
    public partial class Form2 : Form
    {
        Bitmap btMain;
        System.Windows.Forms.Timer timer;
        public Form2()
        {
            InitializeComponent();
            timer = new Timer();
            // 타이머 선언 
            timer.Interval = 500; 
            // 타이머 실행간격 500ms(0.5초) 
            timer.Tick += new EventHandler(button1_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(30, 190, 200, 100);
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
                //Picture Box Display 
                btMain = new Bitmap(btMain, new Size(400, 200));
                pictureBox1.Image = btMain;
            }

            if (btMain != null)
            {
                string save_route = @"D:\TestImageForder";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "JPG File(*.jpg) | *.jpg";
                pictureBox1.Image.Save(save_route + "\\test_image.png", System.Drawing.Imaging.ImageFormat.Png);
  
            }


        }
    }
}
