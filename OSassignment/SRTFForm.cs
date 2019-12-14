using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSassignment
{
    public partial class SRTFForm : Form
    {
        public SRTFForm(List<Process> timeAxis, double awt, double atat, List<Process> finishedProcs)
        {
            InitializeComponent();
            Console.WriteLine(timeAxis.Count);
            int i = 0;

            chart1.ChartAreas[0].AxisY.RoundAxisValues();

            foreach (Process proc in timeAxis)
            {
                if (proc != null)
                {
                    string s = "Process " + proc.pId.ToString();

                    if (chart1.Series.FindByName(s) == null)
                    {
                        System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
                        series.ChartArea = "ChartArea1";
                        series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar;
                        series.CustomProperties = "DrawSideBySide=False";
                        series.Legend = "Legend1";
                        series.Name = s;
                        series.YValuesPerPoint = 2;

                        chart1.Series.Add(series);
                    }

                    byte[] rgb = proc.pColor.GetRGB();
                    chart1.Series[s].Color = System.Drawing.Color.FromArgb(rgb[0], rgb[1], rgb[2]); 
                    //Console.WriteLine(i + " " + proc.pId);
                    chart1.Series[s].Points.AddXY(proc.pId, i, i + 1);
                }
                i++;
            }
            chart1.ChartAreas[0].AxisY.RoundAxisValues();
            label3.Text = awt.ToString();
            label4.Text = atat.ToString();

            foreach(Process proc in finishedProcs)
            {
                listBox1.Items.Add(proc.ToString());
            }
        }

        private void SRTFForm_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.AForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.PForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.S1Form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.S2Form.Show();
        }
    }
}
