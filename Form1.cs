using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private Image img = null;
        private Image img1 = null;
        private BufferedGraphicsContext bgc = BufferedGraphicsManager.Current;
        private BufferedGraphics bg;
        private BufferedGraphicsContext bgc1 = BufferedGraphicsManager.Current;
        private BufferedGraphics bg1;

        Graphics g;
        Graphics im;
        Color color = Color.Black;
        Point startloc;
        Point currentloc;
        bool paint = false;
        bool paint_line = true;
        bool ellips_is_pressed = false;
        bool rectangle_is_pressed = false;
        bool pen_is_pressed = true;
        bool brush_is_pressed = false;
        bool cut_is_pressed = false;
        bool fill_figure = false;
       
        
        ColorDialog c = new ColorDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            S_s = panel1.Size;
            startloc = e.Location;
            paint = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (img == null) return;
            if (im == null) return;
            var g = panel1.CreateGraphics();
            var bgg = bg.Graphics;

            currentloc = e.Location;
            float width = 1;
            if(brush_is_pressed)
            {
                width = (float)numericUpDown1.Value * 6;
                label14.BorderStyle = BorderStyle.Fixed3D;
                label13.BorderStyle = BorderStyle.None;
                label4.BorderStyle = BorderStyle.None;
            }
            if (pen_is_pressed)
            {
                width = (float)numericUpDown1.Value;
                label4.BorderStyle = BorderStyle.None;
                label14.BorderStyle = BorderStyle.None;
                label13.BorderStyle = BorderStyle.Fixed3D;
            }
            if (eraser_is_pressed)
            {
                width = (float)numericUpDown1.Value * 6;
                label4.BorderStyle = BorderStyle.Fixed3D;
                label14.BorderStyle = BorderStyle.None;
                label13.BorderStyle = BorderStyle.None;
            }
            var p = new Pen(color, width);
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Brush b = new SolidBrush(color);
            if (paint)
            {
                if (paint_line)
                {
                    button4.BackColor = Color.White;
                    im.DrawLine(p, startloc, currentloc);
                    g.DrawLine(p, startloc, currentloc);
                    startloc = currentloc;
                }
                if (cut_is_pressed)
                {
                    button4.BackColor = Form1.DefaultBackColor;
                    var sz = new Size(currentloc.X - startloc.X, currentloc.Y - startloc.Y);
                    bgg.DrawImage(img, 0, 0);
                    bgg.DrawLine(p, startloc, currentloc);
                    bgg.Save();
                    bg.Render();
                }
                else if (ellips_is_pressed)
                {
                    button4.BackColor = Form1.DefaultBackColor;
                    var sz = new Size(currentloc.X - startloc.X, currentloc.Y - startloc.Y);
                    bgg.DrawImage(img, 0, 0);
                    bgg.DrawEllipse(p, new Rectangle(startloc, sz));
                    if (fill_figure)
                    {
                        bgg.FillEllipse(b, new Rectangle(startloc, sz));
                    }
                    bgg.Save();
                    bg.Render();
                   
                }
                else if (rectangle_is_pressed)
                {
                    button4.BackColor = Form1.DefaultBackColor;
                    Point t = new Point();
                    Size sz = new Size();
                    if (startloc.X < currentloc.X && startloc.Y < currentloc.Y)
                    {
                        t = startloc;
                        sz = new Size(currentloc.X - startloc.X, currentloc.Y - startloc.Y);
                    }
                    if (startloc.X < currentloc.X && startloc.Y > currentloc.Y)
                    {
                        t.X = startloc.X;
                        t.Y = currentloc.Y;
                        sz = new Size(currentloc.X - startloc.X, startloc.Y - currentloc.Y);
                    }
                    if (startloc.X > currentloc.X && startloc.Y < currentloc.Y)
                    {
                        t.X = currentloc.X;
                        t.Y = startloc.Y;
                        sz = new Size(startloc.X - currentloc.X, currentloc.Y - startloc.Y);
                    }
                    if (startloc.X > currentloc.X && startloc.Y > currentloc.Y)
                    {
                        t = currentloc;
                        sz = new Size(startloc.X - currentloc.X, startloc.Y - currentloc.Y);
                    }
                    Rectangle rect = new Rectangle(t, sz);
                    bgg.DrawImage(img, 0, 0);
                    bgg.DrawRectangle(p, rect);
                    if (fill_figure)
                    {
                        bgg.FillRectangle(b, rect);
                    }
                    bgg.Save();
                    bg.Render();
                    
                }
                if (star_is_pressed)
                {
                    button4.BackColor = Form1.DefaultBackColor;
                    Point t = new Point();
                    Size sz = new Size();
                    if (startloc.X < currentloc.X && startloc.Y < currentloc.Y)
                    {
                        t = startloc;
                        sz = new Size(currentloc.X - startloc.X, currentloc.Y - startloc.Y);
                    }
                    if (startloc.X < currentloc.X && startloc.Y > currentloc.Y)
                    {
                        t.X = startloc.X;
                        t.Y = currentloc.Y;
                        sz = new Size(currentloc.X - startloc.X, startloc.Y - currentloc.Y);
                    }
                    if (startloc.X > currentloc.X && startloc.Y < currentloc.Y)
                    {
                        t.X = currentloc.X;
                        t.Y = startloc.Y;
                        sz = new Size(startloc.X - currentloc.X, currentloc.Y - startloc.Y);
                    }
                    if (startloc.X > currentloc.X && startloc.Y > currentloc.Y)
                    {
                        t = currentloc;
                        sz = new Size(startloc.X - currentloc.X, startloc.Y - currentloc.Y);
                    }
                    Rectangle rect = new Rectangle(t, sz);
                    Point[] points;
                    points = CreatePolPoints(7, rect);
                    bgg.DrawImage(img, 0, 0);
                    bgg.DrawPolygon(p, points);
                    if (fill_figure)
                    {
                        bgg.FillPolygon(b, points);
                        
                    }
                    
                    bgg.Save();
                    bg.Render();

                }
                if (poligon_is_pressed)
                {
                    button4.BackColor = Form1.DefaultBackColor;
                    Point t = new Point();
                    Size sz = new Size();
                    if (startloc.X < currentloc.X && startloc.Y < currentloc.Y)
                    {
                        t = startloc;
                        sz = new Size(currentloc.X - startloc.X, currentloc.Y - startloc.Y);
                    }
                    if (startloc.X < currentloc.X && startloc.Y > currentloc.Y)
                    {
                        t.X = startloc.X;
                        t.Y = currentloc.Y;
                        sz = new Size(currentloc.X - startloc.X, startloc.Y - currentloc.Y);
                    }
                    if (startloc.X > currentloc.X && startloc.Y < currentloc.Y)
                    {
                        t.X = currentloc.X;
                        t.Y = startloc.Y;
                        sz = new Size(startloc.X - currentloc.X, currentloc.Y - startloc.Y);
                    }
                    if (startloc.X > currentloc.X && startloc.Y > currentloc.Y)
                    {
                        t = currentloc;
                        sz = new Size(startloc.X - currentloc.X, startloc.Y - currentloc.Y);
                    }
                    Rectangle rect = new Rectangle(t, sz);
                    Point[] points;
                    points = CreateArrPoints(3, rect);
                    bgg.DrawImage(img, 0, 0);
                    bgg.DrawPolygon(p, points);
                    if (fill_figure)
                    {
                        bgg.FillPolygon(b, points);
                    }
                    bgg.Save();
                    bg.Render();
                }
            }
        }
        private Point[] CreatePolPoints(int numb, Rectangle area)
        {
            Point[] pts = new Point[numb];
            int cX = area.Width / 2;
            int cY = area.Height / 2;

            int centerX = area.X + cX;
            int centerY = area.Y + cY;

            double angle = -Math.PI / 5;
            double dangle = 6 * Math.PI / numb;
            for (int i = 0; i < numb; i++)
            {
                pts[i] = new Point((int)(centerX + cX * Math.Cos(angle)), (int)(centerY + cY * Math.Sin(angle)));
                angle += dangle;
            }
            return pts;
        }
        
        private Point[] CreateArrPoints(int count, Rectangle area)
        {
            double angle = -Math.PI * 0.5;
            int cX = area.Width / 2;
            int cY = area.Height / 2;
            int centerX = area.X + cX;
            int centerY = area.Y + cY;
            Point[] points = new Point[count];
            for (int i = 0; i < count; i++)
            {
                points[i] = new Point((int)(centerX + cX * Math.Cos(angle)), (int)(centerY + cY * Math.Sin(angle)));
                angle += angle;
            }
            return points;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            im = Graphics.FromImage(img);
            currentloc = e.Location;

            float width = 1;
            if (brush_is_pressed)
            {
                width = (float)numericUpDown1.Value * 6;
            }
            if (pen_is_pressed)
            {
                width = (float)numericUpDown1.Value;

            }
            var p = new Pen(color, width);
            var b = new SolidBrush(color);

            if (cut_is_pressed)
            {
                im.DrawLine(p, startloc, currentloc);    
            }
            if (ellips_is_pressed)
            {
                var sz = new Size(/*Math.Abs*/(currentloc.X - startloc.X),/* Math.Abs*/(currentloc.Y - startloc.Y));
                im.DrawEllipse(p, new Rectangle(startloc, sz));
                if (fill_figure)
                {
                    im.FillEllipse(b, new Rectangle(startloc, sz));
                }
            }
            if (rectangle_is_pressed)
            {
                Point t = new Point();
                Size sz = new Size();
                if (startloc.X < currentloc.X && startloc.Y < currentloc.Y)
                {
                    t = startloc;
                    sz = new Size(currentloc.X - startloc.X, currentloc.Y - startloc.Y);
                }
                if (startloc.X < currentloc.X && startloc.Y > currentloc.Y)
                {
                    t.X = startloc.X;
                    t.Y = currentloc.Y;
                    sz = new Size(currentloc.X - startloc.X, startloc.Y - currentloc.Y);
                }
                if (startloc.X > currentloc.X && startloc.Y < currentloc.Y)
                {
                    t.X = currentloc.X;
                    t.Y = startloc.Y;
                    sz = new Size(startloc.X - currentloc.X, currentloc.Y - startloc.Y);
                }
                if (startloc.X > currentloc.X && startloc.Y > currentloc.Y)
                {
                    t = currentloc;
                    sz = new Size(startloc.X - currentloc.X, startloc.Y - currentloc.Y);
                }
                Rectangle rect = new Rectangle(t, sz);
                im.DrawRectangle(p, rect);
                if (fill_figure)
                {
                    im.FillRectangle(b, rect);
                }
            }
            if (star_is_pressed)
            {
                Point t = new Point();
                Size sz = new Size();
                if (startloc.X < currentloc.X && startloc.Y < currentloc.Y)
                {
                    t = startloc;
                    sz = new Size(currentloc.X - startloc.X, currentloc.Y - startloc.Y);
                }
                if (startloc.X < currentloc.X && startloc.Y > currentloc.Y)
                {
                    t.X = startloc.X;
                    t.Y = currentloc.Y;
                    sz = new Size(currentloc.X - startloc.X, startloc.Y - currentloc.Y);
                }
                if (startloc.X > currentloc.X && startloc.Y < currentloc.Y)
                {
                    t.X = currentloc.X;
                    t.Y = startloc.Y;
                    sz = new Size(startloc.X - currentloc.X, currentloc.Y - startloc.Y);
                }
                if (startloc.X > currentloc.X && startloc.Y > currentloc.Y)
                {
                    t = currentloc;
                    sz = new Size(startloc.X - currentloc.X, startloc.Y - currentloc.Y);
                }
                Rectangle rect = new Rectangle(t, sz);
                Point[] points;
                points = CreatePolPoints(7, rect);
                if (fill_figure)
                {
                    im.FillPolygon(b, points);
                    
                }
                im.DrawPolygon(p, points);
            }
            if (poligon_is_pressed) {
                Point t = new Point();
                Size sz = new Size();
                if (startloc.X < currentloc.X && startloc.Y < currentloc.Y)
                {
                    t = startloc;
                    sz = new Size(currentloc.X - startloc.X, currentloc.Y - startloc.Y);
                }
                if (startloc.X < currentloc.X && startloc.Y > currentloc.Y)
                {
                    t.X = startloc.X;
                    t.Y = currentloc.Y;
                    sz = new Size(currentloc.X - startloc.X, startloc.Y - currentloc.Y);
                }
                if (startloc.X > currentloc.X && startloc.Y < currentloc.Y)
                {
                    t.X = currentloc.X;
                    t.Y = startloc.Y;
                    sz = new Size(startloc.X - currentloc.X, currentloc.Y - startloc.Y);
                }
                if (startloc.X > currentloc.X && startloc.Y > currentloc.Y)
                {
                    t = currentloc;
                    sz = new Size(startloc.X - currentloc.X, startloc.Y - currentloc.Y);
                }
                Rectangle rect = new Rectangle(t, sz);
                Point[] points;
                points = CreateArrPoints(3,rect);
                im.DrawPolygon(p, points);
                if (fill_figure) { im.FillPolygon(b, points); }
            }
            panel1.CreateGraphics().DrawImage(img, 0, 0);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           if (img == null)
           {
                img = new Bitmap(panel1.Width, panel1.Height, panel1.CreateGraphics());
                img1 = new Bitmap(panel1.Width, panel1.Height, panel1.CreateGraphics());
                im = Graphics.FromImage(img);
                
                bg = bgc.Allocate(panel1.CreateGraphics(), new Rectangle(0, 0, panel1.Width, panel1.Height));
                im.Clear(Color.White);
                
                e.Graphics.DrawImage(img, 0, 0);
           }
            e.Graphics.DrawImage(img, 0, 0);
        }
        private Size S_s;
        private Size S_k;
        private void Form1_Resize(object sender, EventArgs e)
        {
            S_k = panel1.Size;
            if (this.WindowState == FormWindowState.Minimized)
            { return; }
            if (S_k != S_s)
            {
                if (this.WindowState == FormWindowState.Minimized || S_k != S_s)
                {
                    if(img1 == null) { return; }
                    img1 = img;
                    img = new Bitmap(panel1.Width, panel1.Height, panel1.CreateGraphics());
                    var gimg = Graphics.FromImage(img);

                    gimg.Clear(Color.White);
                    bg = bgc.Allocate(panel1.CreateGraphics(), new Rectangle(0, 0, panel1.Width, panel1.Height));
                    gimg.DrawImage(img1, 0, 0);

                }


            }
        }
        //фигуры
        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.White;
            button5.BackColor = Form1.DefaultBackColor;
            button8.BackColor = Form1.DefaultBackColor;
            button1.BackColor = Form1.DefaultBackColor;
            button6.BackColor = Form1.DefaultBackColor;
            button7.BackColor = Form1.DefaultBackColor;
            paint_line = true;
            ellips_is_pressed = false;
            rectangle_is_pressed = false;
            cut_is_pressed = false;
            star_is_pressed = false;
            poligon_is_pressed = false;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            button4.BackColor = Form1.DefaultBackColor;
            button8.BackColor = Form1.DefaultBackColor;
            button1.BackColor = Form1.DefaultBackColor;
            button5.BackColor = Color.White;
            button6.BackColor = Form1.DefaultBackColor;
            button7.BackColor = Form1.DefaultBackColor;
            paint_line = false;
            rectangle_is_pressed = false;
            cut_is_pressed = false;
            ellips_is_pressed = true;
            star_is_pressed = false;
            poligon_is_pressed = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button5.BackColor = Form1.DefaultBackColor;
            button4.BackColor = Form1.DefaultBackColor;
            button1.BackColor = Color.White;
            button6.BackColor = Form1.DefaultBackColor;
            button8.BackColor = Form1.DefaultBackColor;
            button7.BackColor = Form1.DefaultBackColor;
            rectangle_is_pressed = true;
            paint_line = false;
            ellips_is_pressed = false;
            cut_is_pressed = false;
            star_is_pressed = false;
            poligon_is_pressed = false;
        }
        private void button6_Click(object sender, EventArgs e)
        {

            button5.BackColor = Form1.DefaultBackColor;
            button4.BackColor = Form1.DefaultBackColor;
            button1.BackColor = Form1.DefaultBackColor;
            button6.BackColor = Color.White;
            button7.BackColor = Form1.DefaultBackColor;
            button8.BackColor = Form1.DefaultBackColor;
            rectangle_is_pressed = false;
            paint_line = false;
            ellips_is_pressed = false;
            cut_is_pressed = true;
            poligon_is_pressed = false;
            star_is_pressed = false;
        }
        bool star_is_pressed = false;
        private void button7_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.White;
            button4.BackColor = Form1.DefaultBackColor;
            button5.BackColor = Form1.DefaultBackColor;
            button1.BackColor = Form1.DefaultBackColor;
            button6.BackColor = Form1.DefaultBackColor;
            button8.BackColor = Form1.DefaultBackColor;
            rectangle_is_pressed = false;
            paint_line = false;
            ellips_is_pressed = false;
            cut_is_pressed = false;
            star_is_pressed = true;
            poligon_is_pressed = false;
        }
        bool poligon_is_pressed = false;
        private void button8_Click(object sender, EventArgs e)
        {
            button8.BackColor = Color.White;
            button7.BackColor = Form1.DefaultBackColor;
            button4.BackColor = Form1.DefaultBackColor;
            button5.BackColor = Form1.DefaultBackColor;
            button1.BackColor = Form1.DefaultBackColor;
            button6.BackColor = Form1.DefaultBackColor;
            rectangle_is_pressed = false;
            paint_line = false;
            ellips_is_pressed = false;
            cut_is_pressed = false;
            star_is_pressed = false;
            poligon_is_pressed = true;
        }
        //наличие заливки
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fill_figure = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fill_figure = false;
        }
        //инструменты
        private void label13_Click(object sender, EventArgs e)
        {
            pen_is_pressed = true;
            brush_is_pressed = false;
            eraser_is_pressed = false;
            color = label5.BackColor;
        }
        private void label14_Click(object sender, EventArgs e)
        {
            pen_is_pressed = false;
            brush_is_pressed = true;
            eraser_is_pressed = false;
            color = label5.BackColor;
        }
        bool eraser_is_pressed = false;
        private void label4_Click(object sender, EventArgs e)
        {
            color = panel1.BackColor;
            eraser_is_pressed = true;
            pen_is_pressed = false;
            brush_is_pressed = false;
            paint_line = true;
            ellips_is_pressed = false;
            rectangle_is_pressed = false;
            
        }

        //всплывающая подсказка для кисти, ручки и ластика
        private void label4_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(label4, "eraser");
            
        }
        private void label14_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(label14, "brush");
            
        }
        private void label13_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(label13, "pen");
            
        }
        //выбор цвета
        private void label1_Click(object sender, EventArgs e)
        {
            color = Color.Black;
            label5.BackColor = color;
        }
        private void label6_Click(object sender, EventArgs e)
        {
            color = Color.Red;
            label5.BackColor = color;
        }
        private void label9_Click(object sender, EventArgs e)
        {
            color = Color.Yellow;
            label5.BackColor = color;
        }
        private void label8_Click(object sender, EventArgs e)
        {
            color = Color.Lime;
            label5.BackColor = color;
        }
        private void label7_Click(object sender, EventArgs e)
        {
            color = Color.Blue;
            label5.BackColor = color;
        }
        private void label12_Click(object sender, EventArgs e)
        {
            color = Color.Aqua;
            label5.BackColor = color;
        }
        private void label11_Click(object sender, EventArgs e)
        {
            color = Color.Fuchsia;
            label5.BackColor = color;
        }
        private void label10_Click(object sender, EventArgs e)
        {
            color = Color.Maroon;
            label5.BackColor = color;
        }
        //открытие диалогового окна для выбора цвета
        private void label3_Click(object sender, EventArgs e)
        {
            if (c.ShowDialog() == DialogResult.OK)
            {
                color = c.Color;
                label5.BackColor = c.Color;
            }
           
        }
        //открытие диалогового окна для сохранения нарисованной картинки
        private void label15_Click(object sender, EventArgs e)
        {
            saveFileDialog.AddExtension = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.Filter = "Файлы картинок в формате JPG|*.jpg|Файлы картинок в формате PNG|*.png";
            var res = saveFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                label1.Text = saveFileDialog.FileName;
            }
        }
        //отчистка панели
        private void label16_Click(object sender, EventArgs e)
        {
            var gimg = Graphics.FromImage(img);
            gimg.Clear(Color.White);
            gimg.DrawImage(img, 0, 0);
            var bgg = bg.Graphics;
            bgg.Clear(Color.White);
            img = new Bitmap(panel1.Width, panel1.Height, panel1.CreateGraphics());
            var gimg11 = Graphics.FromImage(img);
            gimg11.Clear(Color.White);
            bg = bgc.Allocate(panel1.CreateGraphics(), new Rectangle(0, 0, panel1.Width, panel1.Height));
            panel1.Refresh();
            im.Clear(Color.White);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            /*groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button5);
            Controls.Add(groupBox1);
        */
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //открытие диалогового окна для вставки картинки
        private void label17_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы картинок в формате JPG|*.jpg|Файлы картинок в формате PNG|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PictureBox pb = new PictureBox();
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.Parent = panel1;
                pb.Image = Image.FromFile(dialog.FileName);
            }
        }
        
    }
}
