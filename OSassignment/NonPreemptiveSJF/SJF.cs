using System.Collections.Generic;

namespace OSassignment
{
    class SJF
    {
        List<Process> processes;
        List<int> pWaitTime;
        List<int> pTurnAroundTime;

        public SJF(List<Process> schedulingList)
        {
            processes = schedulingList;
            pWaitTime = new List<int>();
            pWaitTime.Capacity = processes.Count;
            pTurnAroundTime = new List<int>();
            pTurnAroundTime.Capacity = processes.Count;
        }

        // order of execution is the sorted List
        public void SortProcesses()
        {
            processes.Sort();
        }

        private void EvaluateTurnAroundTime()
        {
            // turn around time is just the total time that particular process needed to finish execution since its ""conception""
            for (int i = 0; i < GetProcNum(); ++i)
                pTurnAroundTime.Add(processes[i].pBurstTime + pWaitTime[i]);
        }

        private void EvaluateWaitTime()
        {
            // trivially, the first process doesn't have to wait.
            pWaitTime.Add(0);
            for (int i = 1; i < GetProcNum(); ++i)
                pWaitTime.Add(processes[i - 1].pBurstTime + pWaitTime[i - 1]);
        }

        public System.Tuple<float, float> EvaluateAvgTime()
        {
            this.EvaluateWaitTime();
            this.EvaluateTurnAroundTime();

            int totalWaitTime = 0, totalTurnAroundTime = 0;

            for(int i = 0; i < GetProcNum(); ++i)
            {
                totalWaitTime += pWaitTime[i];
                totalTurnAroundTime += pTurnAroundTime[i];
            }

            float avgWaitTime = (float)totalWaitTime / (float)GetProcNum();
            float avgTurnAroundTime = (float)totalTurnAroundTime / (float)GetProcNum();
            return new System.Tuple<float, float>(avgWaitTime, avgTurnAroundTime);
        }

        public string print()
        {
            string output = "";
            foreach(Process p in processes)
            {
                output += p.ToString() + "\n";
            }
            return output;
        }

        public int GetProcNum()
        {
            return processes.Count;
        }

        public Process GetProc(int idx)
        {
            return processes[idx];
        }
    }
}
