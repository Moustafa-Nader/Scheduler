using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSassignment
{
    class SRTF
    {
        List<Process> processes;
        List<Process> readyQueue;
        List<Process> finishedProcs;
        List<Process> timeAxis;
        Process currentProc;
        double avgTurnAround, avgWaiting;
        int ctxTime;
        public SRTF(List<Process> procs, int _ctxTime)
        {
            processes = procs.ToList(); // take a copy
            readyQueue = new List<Process>();
            finishedProcs = new List<Process>();
            timeAxis = new List<Process>();
            currentProc = null;
            avgTurnAround = 0;
            avgWaiting = 0;
            ctxTime = _ctxTime;
        }

        public void Simulate()
        {
            int totalTime = 0;
            foreach (Process proc in processes)
            {
                totalTime += proc.pBurstTime + proc.pArrivalTime;
            }

            // loop over each time unit and simulate scheduling step
            for (int time = 1; time <= totalTime; ++time)
            {
                SchedulingStep(time);
            }
        }

        private void SchedulingStep(int time)
        {
            List<Process> arrivedProcs = new List<Process>();
            foreach (Process proc in processes)
            {
                if (proc.pArrivalTime == time)
                    arrivedProcs.Add(proc);
            }
            // add arrived procs to ready queue and remove from processes list
            foreach (Process proc in arrivedProcs)
            {
                readyQueue.Add(proc);
                processes.Remove(proc);
            }
            // sort based on remaining time
            readyQueue.Sort(Comparer<Process>.Create((a, b) => a.pRemTime.CompareTo(b.pRemTime)));
            // add next process if no process is executing
            if (currentProc == null && readyQueue.Count > 0)
            {
                currentProc = readyQueue[0];
                readyQueue.Remove(currentProc);
                // add ctx switch time
                for (int i = 0; i < ctxTime; ++i)
                    timeAxis.Add(null);
            }
            // replace current process if a shorter remaining process exists in ready queue (context switch)
            if (currentProc != null && readyQueue.Count > 0)
            {
                if(readyQueue[0].pRemTime < currentProc.pRemTime)
                {
                    readyQueue.Add(currentProc);
                    currentProc = readyQueue[0];
                    readyQueue.Remove(currentProc);
                    // add ctx switch time
                    for (int i = 0; i < ctxTime; ++i)
                        timeAxis.Add(null);
                }
            }
            // decrease remaining time of current process
            if (currentProc != null)
            {
                timeAxis.Add(currentProc);
                // Console.WriteLine(time.ToString() + " " + currentProc.pId.ToString() + " " + currentProc.pWaitingTime.ToString());
                currentProc.pRemTime--;
                if (currentProc.pRemTime == 0)
                {
                    finishedProcs.Add(currentProc);
                    currentProc = null;
                }
            }
            else
            {
                timeAxis.Add(null);
            }
            // update waiting time of each process
            foreach (Process proc in readyQueue)
            {
                proc.pWaitingTime++;
            }
        }

        public void Print()
        {
            foreach (Process proc in finishedProcs)
            {
                avgTurnAround += proc.pTurnAroundTime;
                avgWaiting += proc.pWaitingTime;
                Console.WriteLine(proc.ToString() + '\n');
            }
            avgTurnAround /= (double)finishedProcs.Count;
            avgWaiting /= (double)finishedProcs.Count;
            Console.WriteLine("Average Waiting Time : " + avgWaiting.ToString());
            Console.WriteLine("Average Turn Around Time : " + avgTurnAround.ToString());
        }

        public void Display()
        {
            Application.Run(new SRTFForm(timeAxis, avgWaiting, avgTurnAround, finishedProcs));
        }
    }
}
