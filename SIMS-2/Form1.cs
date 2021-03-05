using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_2
{
    public partial class Form1 : Form
    {
        const double g = 9.81;

        double dt;

        double a;
        double v0;
        double y0;

        double t;
        double x;
        double y;

        bool isModeling = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            if (isModeling==false)
            {
                    dt = (double)timer1.Interval;
                    a = (double)edAngle.Value;
                    v0 = (double)edSpeed.Value;
                    y0 = (double)edHeight.Value;

                    t = 0;
                    x = 0;
                    y = y0;
                    chart1.Series[0].Points.Clear();
                    chart1.Series[0].Points.AddXY(x, y);

                    double s = v0 * v0 * Math.Sin(2 * a * Math.PI / 180) / g;
                    double h = (v0 * v0 * Math.Sin(a * Math.PI / 180) * Math.Sin(a * Math.PI / 180)) / (2 * g);
                    chart1.ChartAreas[0].AxisX.Maximum = s * 1.1;
                    chart1.ChartAreas[0].AxisY.Maximum = (h + y0) * 1.1;

                    isModeling = true;
            }
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            timeValueLabel.Text = $"{(int)t/60000}.{(int)t/1000}.{(int)t%1000}";
            x = v0 * Math.Cos(a * Math.PI / 180) * t/1000;
            y = y0 + v0 * Math.Sin(a * Math.PI / 180) * t/1000 - g * t/1000 * t / 2000;
            chart1.Series[0].Points.AddXY(x, y);
            if (y <= 0) 
            {
                timer1.Stop();
                isModeling = false;
            } 
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
