using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoControlTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.uiMode = "none";
        }

        string currentprompt;

        private void button1_Click(object sender, EventArgs e)
        {
            // Set the URL property to the file path obtained from the text box. 
            axWindowsMediaPlayer1.URL = textBox1.Text;

            

            // Play the media file. 
            axWindowsMediaPlayer1.Ctlcontrols.play();

            timer1.Start();
        }

        private void axWindowsMediaPlayer1_PositionChange(object sender, AxWMPLib._WMPOCXEvents_PositionChangeEvent e)
        {
            label1.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            label4.Text = Convert.ToString(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            label4.Text = Convert.ToString(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
            var clip = axWindowsMediaPlayer1.newMedia(textBox1.Text);
            label5.Text = Convert.ToString(TimeSpan.FromSeconds(clip.duration));
            label6.Text = Convert.ToString(axWindowsMediaPlayer1.Ctlcontrols.currentPosition / clip.duration);

            this.trackBar1.Value = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition / clip.duration*1000);
            panel1.Refresh();

            double currentposition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

            promptdelivery(currentposition);
        }

        public void promptdelivery(double position)
        {
            if(position < 120 && position > 50 && currentprompt != "promptID01")
            {
                currentprompt = "promptID01";
                promptcontent("promptID01");
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
        }

        public void promptcontent(string promptid)
        {
            if (promptid == "promptID01")
            {
                // Display prompt
                richTextBox1.Text = "Prompt ID 01 is displayed here from markers 50 seconds to 120 seconds...";
            }
            else
            {
                // Nothing happens
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            int width = panel1.Width;
            int height = panel1.Height;
            var clip = axWindowsMediaPlayer1.newMedia(textBox1.Text);

            // Draw target area for prompts
            Rectangle rect = ClientRectangle;
            int recx1 = Convert.ToInt32(50 / clip.duration * width);
            int recwidth = Convert.ToInt32(70 / clip.duration * width);
            rect.Location = new Point(recx1, 0);                  // specify rectangle relative position here (relative to parent container)
            rect.Size = new Size(recwidth, height);                       // specify rectangle size here

            using (Brush brush = new SolidBrush(Color.DeepSkyBlue))    // specify color here and brush type here
            {
                g.FillRectangle(brush, rect);
            }

            // Draw line for the trackbar
            Pen mypen = new Pen(System.Drawing.Color.DarkGray, 5);
            int x1 = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition / clip.duration*width);
            int x2 = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition / clip.duration*width);
            int y1 = 0;
            int y2 = height;
            g.DrawLine(mypen, x1, y1, x2, y2);
        }
    }
}
