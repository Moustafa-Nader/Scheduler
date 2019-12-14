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
    public partial class Form1 : Form
    {   
        public Form1(List<Process> myPs)
        {
            InitializeComponent();
            int i = 0;
            
            chart1.ChartAreas[0].AxisY.RoundAxisValues();

            foreach (Process P in myPs)
            {
                string s = "Process " + i.ToString();
                chart1.Series[s].Points.AddXY(i+1, (myPs[i].pArrivalTime + myPs[i].pWaitingTime), myPs[i].pTurnAroundTime + myPs[i].pArrivalTime);
                i++;
            }

            chart1.ChartAreas[0].AxisY.RoundAxisValues();

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
