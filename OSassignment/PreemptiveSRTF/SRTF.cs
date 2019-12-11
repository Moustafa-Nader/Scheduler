using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSassignment
{
    class SRTF
    {
        List<Process> processes;
        List<Process> readyQueue;
        List<Process> finishedProcs;
        Process currentProc;
        public SRTF(List<Process> procs)
        {
            processes = procs.ToList(); // take a copy
            readyQueue = new List<Process>();
            finishedProcs = new List<Process>();
            currentProc = null;
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
            }
            // replace current process if a shorter remaining process exists in ready queue (context switch)
            if (currentProc != null && readyQueue.Count > 0)
            {
                if(readyQueue[0].pRemTime < currentProc.pRemTime)
                {
                    readyQueue.Add(currentProc);
                    currentProc = readyQueue[0];
                    readyQueue.Remove(currentProc);
                }
            }
            // decrease remaining time of current process
            if (currentProc != null)
            {
                // Console.WriteLine(time.ToString() + " " + currentProc.pId.ToString() + " " + currentProc.pWaitingTime.ToString());
                currentProc.pRemTime--;
                if (currentProc.pRemTime == 0)
                {
                    finishedProcs.Add(currentProc);
                    currentProc = null;
                }
            }
            // update waiting time of each process
            foreach (Process proc in readyQueue)
            {
                proc.pWaitingTime++;
            }
        }

        public void Print()
        {
            double avgTurnAround = 0, avgWaiting = 0;
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
    }
}
