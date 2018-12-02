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
    }
}
