using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OSassignment
{
    public class AG
    {
        public List<Process> processes;
        List<Process> readyqueue;
        List<Process> tmpList;
        List<Process> mylist;
        List<Process> timeAxis;
        List<Process> finishedProcs;
        double avgWaiting, avgTurnAround;
        int listiterator = 0;
        int quantam = 4;
        int timecount = 0;
        int totaltime = 0;
        public AG(List<Process> pList)
        {
            processes = pList;
            readyqueue = new List<Process>();
            finishedProcs = new List<Process>();
            foreach (Process p in processes)
            {
                p.AGquantam = quantam;
                totaltime += p.pBurstTime;
            }
            processes.Sort((x, y) => x.pArrivalTime.CompareTo(y.pArrivalTime));
            mylist = new List<Process>();
            tmpList = new List<Process>(processes);
        }

        public AG(List<Process> pList , int inputQuantam)
        {
            processes = pList;
            readyqueue = new List<Process>();
            finishedProcs = new List<Process>();
            quantam = inputQuantam;
            foreach (Process p in processes)
            {
                p.AGquantam = quantam;
                totaltime += p.pBurstTime;
            }
            processes.Sort((x, y) => x.pArrivalTime.CompareTo(y.pArrivalTime));
            mylist = new List<Process>();
            tmpList = new List<Process>(processes);

        }

        public void Simulate()
        {

            for (int AGTime = 0; AGTime < 1000; AGTime++)
            {
                updateReadyQueue();
                if (readyqueue.Count != 0)
                {
                    Process current = readyqueue[0];
                    readyqueue.Remove(current);
                    mylist.Add(current);
                    double nonP = Math.Ceiling(((double)(current.AGquantam) / 2.0));
                    for (int i = 0; i <= current.AGquantam; i++)
                    {
                        updateReadyQueue();
                        if (current.Finished())
                        {
                            finishedProcs.Add(current);
                            printQuantam();
                            break;
                        }
                        if (i < nonP)
                        {
                            current.pRemTime--;
                            foreach (Process p in readyqueue)
                            {
                                p.pWaitingTime++;
                            }
                            timecount++;
                        }
                        if (i >= nonP && i != current.AGquantam)
                        {
                            Process temp = current;
                            if (readyqueue.Count != 0)
                                temp = getMinAGfactor();
                            if (temp.AGfactor < current.AGfactor)
                            {
                                current.AGquantam += (current.AGquantam - (i));
                                printQuantam();
                                readyqueue.Add(current);
                                readyqueue.Remove(temp);
                                readyqueue.Insert(0, temp);
                                break;
                            }
                            else
                            {
                                current.pRemTime--;
                                foreach (Process p in readyqueue)
                                {
                                    p.pWaitingTime++;
                                }
                                timecount++;
                            }

                        }
                        if (i == current.AGquantam)
                        {
                            double total = 0.0;
                            foreach (Process p in readyqueue)
                                total += p.AGquantam;
                            if (readyqueue.Count != 0)
                            {
                                current.AGquantam += (int)Math.Ceiling((total / (double)readyqueue.Count) * 0.1);
                                printQuantam();
                            }
                            if (current.pRemTime > 0) readyqueue.Add(current);
                            break;
                        }
                    }

                }
                else timecount++;
            }
        }

        private void updateReadyQueue()
        {

            for (int i = 0; i < tmpList.Count; ++i)
            {
                if (timecount == tmpList[i].pArrivalTime)
                {
                    Process t = tmpList[i];
                    readyqueue.Add(tmpList[i]);
                    tmpList.Remove(t);
                }

            }

        }

        private Process getMinAGfactor()
        {
            int min = 1000000000;
            int idx = 0;
            for (int i = 0; i < readyqueue.Count; i++)
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
        }
        void printQuantam()
        {
            Console.Write("(");
            for (int i = 0; i < processes.Count; i++)
            {
                if (processes[i].Finished())
                    Console.Write("0");
                else Console.Write(processes[i].AGquantam);
                if (i != processes.Count - 1)
                    Console.Write(",");
            }
            Console.WriteLine(")");

        }

        public void Display()
        {
            Tuple<float, float> tuple = EvaluateAvgTime();
            avgWaiting = tuple.Item1;
            avgTurnAround = tuple.Item2;
            timeAxis = mylist;

            Application.Run(new SRTFForm(timeAxis, avgWaiting, avgTurnAround, finishedProcs));
        }
    }

}