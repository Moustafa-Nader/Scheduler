using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSassignment
{
    class Program
    {
        static Form1 myform;
        //[STAThread]
        public static SRTFForm PForm;
        public static SRTFForm S1Form;
        public static SRTFForm S2Form;
        public static SRTFForm AForm;

        static List<Process> testList()
        {
            Process proc1 = new Process(0, 10, 11, 15, new Color(250, 200, 150));
            Process testProc2 = new Process(1, 7, 9, 13, new Color(150, 69, 200));
            Process testProc3 = new Process(2, 15, 15, 19, new Color(100, 200, 0));
            Process testProc4 = new Process(3, 8, 5, 15, new Color(0, 50, 200));
            Process testProc5 = new Process(4, 8, 5, 15, new Color(0, 50, 200));
            return new List<Process> { proc1, testProc2, testProc3, testProc4,testProc5};
        }

        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int ctxTime = 1;

            Console.WriteLine("SJF :");
            SJF2 sjf = new SJF2(testList(), ctxTime);

            sjf.Simulate();
            sjf.Print();
            sjf.Display();
            Console.WriteLine("\n");

            Process proc1 = new Process(0, 10, 11, 15, new Color(250, 200, 150));
            Process testProc2 = new Process(1, 7, 3, 13, new Color(250, 200, 150));
            Process testProc3 = new Process(2, 15, 15, 19, new Color(250, 200, 150));
            Process testProc4 = new Process(3, 9, 9, 15, new Color(250, 200, 150));
            Process AGtestProc1 = new Process(0, 0, 17, 4, new Color(250, 200, 150));
            Process AGtestProc2 = new Process(1, 3, 6, 9, new Color(150, 69, 200));
            Process AGtestProc3 = new Process(2, 4, 10, 3, new Color(100, 200, 0));
            Process AGtestProc4 = new Process(3, 29, 4, 8, new Color(0, 50, 200));
            List<Process> p = new List<Process> { proc1, testProc2, testProc3, testProc4 };
            List<Process> pr = new List<Process> { proc1, testProc2, testProc3, testProc4 };
            List<Process> AGpr = new List<Process> { AGtestProc1, AGtestProc2, AGtestProc3, AGtestProc4 };
            List<Process> psp = new List<Process>(AGpr);
            
            Console.WriteLine("SRTF :");
            SRTF srtf = new SRTF(testList(), ctxTime);
            srtf.Simulate();
            srtf.Print();
            srtf.Display();
            Console.WriteLine("\n--\n");

            AG ag = new AG(AGpr);
            ag.Simulate();
            ag.print();

            /*
            SJF sjf = new SJF(p);
            Console.WriteLine(sjf.print());

            Console.WriteLine("initialized SJF object\n");

            sjf.SortProcesses();

            Console.Write("Order of execution is: ");
            for (int i = 0; i < sjf.GetProcNum(); ++i)
                Console.Write(sjf.GetProc(i).pId + " ");
            Console.WriteLine("\n");

            Tuple<float, float> avgTime = sjf.EvaluateAvgTime();

            Console.Write("Average wait time: " + avgTime.Item1.ToString() + "\t");
            Console.Write("Average turn around time: " + avgTime.Item2.ToString() + "\n");
            //Console.WriteLine(String.Format("Average wait time: {0}\tAverage turn around time: {1}"), avgTime.Item1.ToString(), avgTime.Item2.ToString());
            //Console.WriteLine(sjf.print());
            */

            //myform = new Form1(ps.processes);
            PS ps = new PS(testList());
            ps.Simulate();
            ps.print();
            ps.Display();
            //Application.Run(myform);
            Application.Run(PForm);
        }
    }
}
