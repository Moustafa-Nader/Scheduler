﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSassignment
{
    class Program
    {
        static void Main(string[] args)
        {

            Process proc1 = new Process(0, 10, 11, 15, new Color(250, 200, 150));
            Process testProc2 = new Process(1, 7, 3, 13, new Color(250, 200, 150));
            Process testProc3 = new Process(2, 15, 15, 19, new Color(250, 200, 150));
            Process testProc4 = new Process(3, 9, 9, 15, new Color(250, 200, 150));
            List<Process> p = new List<Process> { proc1, testProc2, testProc3, testProc4 };

            SJF2 sjf = new SJF2(p);
            sjf.Simulate();
            sjf.Print();
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
            Console.ReadLine();
        }
    }
}