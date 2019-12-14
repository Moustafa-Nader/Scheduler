using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace OSassignment
{
     class PS
    {
        List<int> pWaitTime;
        List<int> pTurnAroundTime;
        public List<Process> processes;
        List<Process> tmpList;
        double avgWaiting;
        double avgTurnAround;
        List<Process> finishedProcs;
        List<Process> timeAxis;
        int time;
        public PS(List<Process> schedulingList)
        {
            processes = new List<Process>();
            tmpList = new List<Process>(schedulingList);
            pTurnAroundTime = new List<int>();
            readyQueue = new List<Process>();
            pWaitTime= new List<int>();
            finishedProcs = new List<Process>();
            timeAxis = new List<Process>();

        }
        List<Process> readyQueue;
         public void Simulate()
        {
            //aging factor = 4
            int Aging = 4;
            tmpList.Sort(Comparer<Process>.Create((a, b) => a.pArrivalTime.CompareTo(b.pArrivalTime)));
            //pShift will be used to calculate the Aging of each process
            int[] pShift = new int[tmpList.Count];
            for (int i = 0; i < tmpList.Count; ++i)
            {
                pShift[i] = 0;
               // pShift[tmpList[i].pId] = tmpList[i].pArrivalTime;
            }
            //init time with the first arrival
            time = tmpList[0].pArrivalTime;
            //add the firt element of tmplist to ready queue
            readyQueue.Add(tmpList[0]);
            //remove the first element
            tmpList.Remove(tmpList[0]);
            while (tmpList.Count > 0 || readyQueue.Count > 0)
            {
                //ready queue is empty but there is still a process in tmplist
                if (readyQueue.Count == 0 && tmpList.Count > 0)
                {
                    time = tmpList[0].pArrivalTime;
                    readyQueue.Add(tmpList[0]);
                    tmpList.Remove(tmpList[0]);  
                }
                //get first element of the queue
                Process cur = readyQueue[0];
                processes.Add(cur);
                readyQueue.Remove(cur);
                time += cur.pBurstTime;
                while (tmpList.Count > 0 && tmpList[0].pArrivalTime <= time)
                {
                    readyQueue.Add(tmpList[0]);
                    tmpList.Remove(tmpList[0]);
                }
                foreach (Process p in readyQueue)
                {   //process wait time = time - arrival time
                    p.pWaitingTime = time - p.pArrivalTime;
                    //if wait time <= current burst time (i.e process didnt start with cur process) Pshift = wait time
                    if (p.pWaitingTime <= cur.pBurstTime) pShift[p.pId] = p.pWaitingTime;
                    //process started
                    else pShift[p.pId] += cur.pBurstTime;
                    //Console.WriteLine(p.ToString()+"    Wait:"+p.pWaitTime);
                    
                    p.pPriority -= (pShift[p.pId] / Aging);
                    pShift[p.pId] %= Aging;
                    // Console.WriteLine(p.ToString()+"    Wait:"+p.pWaitTime);
                    //Console.WriteLine("----------------");
                }
                //sort ready queue based on priority after aging
                readyQueue.Sort(Comparer<Process>.Create((a, b) => a.pPriority.CompareTo(b.pPriority)));
            }
        }
        public void print()
        {
            string output = "";
            foreach (Process p in processes)
            {
                output += p.ToString() + "\n";
            }
            Console.WriteLine(output);
            ExecOrder();
            Tuple<float, float> avgTime = EvaluateAvgTime();
            Console.Write("Average wait time: " + avgTime.Item1.ToString() + "\t");
            Console.Write("Average turn around time: " + avgTime.Item2.ToString() + "\n");
        }
        public void ExecOrder()
        {
            Console.Write("Order of execution is: ");
            for (int i = 0; i < processes.Count; ++i)
                Console.Write(processes[i].pId + " ");
            Console.WriteLine("\n");
        }
        private void EvaluateWaitTime()
        {
   
            for (int i = 0; i < GetProcNum(); ++i){
       
                pWaitTime.Add(processes[i].pWaitingTime);
            }
        }
        private void EvaluateTurnAroundTime()
        {
            for (int i = 0; i < GetProcNum(); ++i)
                pTurnAroundTime.Add(processes[i].pBurstTime + processes[i].pWaitingTime);
        }
        public System.Tuple<float, float> EvaluateAvgTime()
        {
            this.EvaluateWaitTime();
            this.EvaluateTurnAroundTime();
            int totalWaitTime = 0, totalTurnAroundTime = 0;
            for (int i = 0; i < GetProcNum(); ++i)
            {
                totalWaitTime += pWaitTime[i];
                totalTurnAroundTime += pTurnAroundTime[i];
            }
            float avgWaitTime = (float)totalWaitTime / (float)GetProcNum();
            avgWaiting = (double)avgWaitTime;
            float avgTurnAroundTime = (float)totalTurnAroundTime / (float)GetProcNum();
            avgTurnAround = (double)avgTurnAroundTime;
            return new System.Tuple<float, float>(avgWaitTime, avgTurnAroundTime);
        }
        public int GetProcNum()
        {
            return processes.Count;
        }
        void maketAxis()
        {
            for(int i = 0,j=0; i < time; ++i)
            {
                if (j == processes.Count) break;
                int t = processes[j].pArrivalTime+processes[j].pWaitingTime;
                while(i< t)
                {
                    timeAxis.Add(null);
                    i++;
                }
                while(i<(processes[j].pArrivalTime + processes[j].pWaitingTime+processes[j].pBurstTime))
                {
                    timeAxis.Add(processes[j]);
                    i++;
                }
                j++;

            }
        }
        void makefProcess()
        {
            foreach(Process p in processes)
            {
                finishedProcs.Add(p);
            }
        }

        public void Display()
        {
            EvaluateAvgTime();
            maketAxis();
            makefProcess();
            Application.Run(new SRTFForm(timeAxis, avgWaiting, avgTurnAround, finishedProcs));
        }
    }
}