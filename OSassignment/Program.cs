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

        static List<Process> procs;

        static List<Process> getProcs()
        {
            List<Process> test = new List<Process>();
            foreach (Process proc in procs)
                test.Add(new Process(proc.pId, proc.pArrivalTime, proc.pBurstTime, proc.pPriority, proc.pColor));
            return test;
        }

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

            int roundRobin = 4;

            procs = new List<Process>();

            int ctxTime = 0;
            
            Console.Write("Number of Processes : ");
            int cnt = int.Parse(Console.ReadLine());
            Console.Write("Round Robin : ");
            roundRobin = int.Parse(Console.ReadLine());
            Console.Write("Context Switch : ");
            ctxTime = int.Parse(Console.ReadLine());
            
            for(int i=0; i<cnt; ++i)
            {
                Console.Write("Process Name: ");
                String pName = Console.ReadLine();
                Console.Write("Red: ");
                byte red = byte.Parse(Console.ReadLine());
                Console.Write("Green: ");
                byte green = byte.Parse(Console.ReadLine());
                Console.Write("Blue: ");
                byte blue = byte.Parse(Console.ReadLine());
                Console.Write("Arrival Time : ");
                int arrivalTime = int.Parse(Console.ReadLine());
                Console.Write("Burst Time : ");
                int burstTime = int.Parse(Console.ReadLine());
                Console.Write("Priority : ");
                int priority = int.Parse(Console.ReadLine());
                Process proc = new Process(i, arrivalTime, burstTime, priority, new Color(red, green, blue));
                procs.Add(proc);
            }
            

           


            Process proc1 = new Process(0, 10, 11, 15, new Color(250, 200, 150));
            Process testProc2 = new Process(1, 7, 3, 13, new Color(150, 69, 200));
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

            Console.WriteLine("SJF :");
            SJF2 sjf = new SJF2(getProcs(), ctxTime);
            sjf.Simulate();
            sjf.Print();
            sjf.Display();
            Console.WriteLine("\n");

            Console.WriteLine("SRTF :");
            SRTF srtf = new SRTF(getProcs(), ctxTime);
            srtf.Simulate();
            srtf.Print();
            srtf.Display();
            Console.WriteLine("\n");

            Console.WriteLine("AG :");
            AG ag = new AG(getProcs(),roundRobin);
            ag.Simulate();
            ag.print();
            ag.Display();
            Console.WriteLine("\n");

            Console.WriteLine("PS :");
            PS ps = new PS(getProcs());
            ps.Simulate();
            ps.print();
            ps.Display();

            //Application.Run(myform);
            Application.Run(PForm);

            Console.WriteLine("\n");

        }
    }
}
