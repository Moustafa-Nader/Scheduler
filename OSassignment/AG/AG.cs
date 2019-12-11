using System;
using System.Collections.Generic;

namespace OSassignment
{ 
    public class AG
    {
        List<Process> processes;
        List<Process> readyqueue;
        int listiterator = 0;
        int quantam = 4;
        int timecount = 0;
        int totaltime = 0;
        public AG(List<Process> pList)
        {
            processes = pList;
            readyqueue = new List<Process>();
            foreach (Process p in processes)
            { 
                p.AGquantam = quantam;
                totaltime += p.pBurstTime;
            }
            processes.Sort((x, y) => x.pArrivalTime.CompareTo(y.pArrivalTime));
        }

        public void Simulate()
        {
            for(int AGTime = 0; AGTime < totaltime; AGTime++)
            {
                updateReadyQueue();
                if(readyqueue.Count != 0)
                {
                    Process current = readyqueue[0];
                    readyqueue.Remove(current);
                    for(int i = 1; i <= current.AGquantam; i++,timecount++)
                    {
                        updateReadyQueue();
                        current.pRemTime--;
                        foreach (Process p in readyqueue)
                        {
                            p.pWaitingTime++;
                        }
                        if (current.Finished())
                            break;
                        if (i == current.AGquantam)
                        {
                            double total = 0.0;
                            foreach (Process p in readyqueue)
                                total += p.AGquantam;
                            current.AGquantam += (int)Math.Ceiling(total / (double)readyqueue.Count);
                        }
                        if(i > Math.Ceiling(((double)(current.AGquantam)/2.0)))
                        {
                            Process temp = current;
                            if (readyqueue.Count != 0)
                               temp = getMinAGfactor();
                            if(temp.AGfactor < current.AGfactor)
                            {
                                current.AGfactor += (current.AGfactor - i);
                                readyqueue.Add(current);
                                readyqueue.Remove(temp);
                                readyqueue.Insert(0, temp);
                                break;
                            }
                        }

                    }

                }
                timecount++;
            }
        }

        private void updateReadyQueue()
        {
            
            if (true)
            {
                while (listiterator < processes.Count)
                {
                    if (timecount == processes[listiterator].pArrivalTime)
                    {
                        readyqueue.Add(processes[listiterator]);
                    }
                    else break;
                    listiterator++;
                }
            }

        }

        private Process getMinAGfactor()
        {
                int min = 1000000000;
                int idx = 0;
                for(int i = 0; i < readyqueue.Count; i++)
                    if (readyqueue[i].AGfactor < min)
                    { 
                        min = readyqueue[i].AGfactor;
                        idx = i;
                    }
                return readyqueue[idx];
        }

        public System.Tuple<float, float> EvaluateAvgTime()
        {
            int totalWaitTime = 0, totalTurnAroundTime = 0;
            for (int i = 0; i < GetProcNum(); ++i)
            {
                totalWaitTime += processes[i].pWaitingTime;
                totalTurnAroundTime += processes[i].pBurstTime + processes[i].pWaitingTime;
            }
            float avgWaitTime = (float)totalWaitTime / (float)GetProcNum();
            float avgTurnAroundTime = (float)totalTurnAroundTime / (float)GetProcNum();
            return new System.Tuple<float, float>(avgWaitTime, avgTurnAroundTime);
        }
        public int GetProcNum()
        {
            return processes.Count;
        }

        public void print()
        {
            Tuple<float, float> tuple = EvaluateAvgTime();
            Console.WriteLine("Average Wait Time: " + tuple.Item1);
            Console.WriteLine("Average TurnAround Time: " + tuple.Item2);
            foreach(Process p in processes)
            {
                Console.WriteLine(p.pWaitingTime);
                Console.WriteLine(p.pTurnAroundTime);
            }
        }
    }

}