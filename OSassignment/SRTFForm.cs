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
        public SRTFForm(List<Process> timeAxis)
        {
            InitializeComponent();
            int i = 0;

            chart1.ChartAreas[0].AxisY.RoundAxisValues();

            foreach (Process proc in timeAxis)
            {
                if (proc != null)
                {
                    string s = "Process " + proc.pId.ToString();
                    //Console.WriteLine(i + " " + proc.pId);
                    chart1.Series[s].Points.AddXY(proc.pId, i, i + 1);
                }
                i++;
            }
            chart1.ChartAreas[0].AxisY.RoundAxisValues();
        }

        private void SRTFForm_Load(object sender, EventArgs e)
        {

        }
    }
}
