using System;
namespace OSassignment
{
    public class Process : IComparable<Process>
    {
        public int pId;
        public Color pColor;
        public int pArrivalTime;
        public int pBurstTime;
        public int pPriority;
        public int pWaitingTime;
        public int pRemTime;
        public bool isFinished;
        public int AGquantam;       //process quantam for AG
        public int AGfactor;        //process factor for AG

        public Process(int _pid, int _pat, int _pbt, int _pp, Color _color)
        {
            pId = _pid;
            pColor = _color;
            pArrivalTime = _pat;
            pBurstTime = pRemTime = _pbt;
            pPriority = _pp;
            isFinished = false;
            pWaitingTime = 0;
            AGfactor = _pbt + _pat + _pp;
        }

        public int pTurnAroundTime
        {
            get
            {
                return pBurstTime + pWaitingTime;
            }
        }

        public int CompareTo(Process other)
        {
            return pBurstTime.CompareTo(other.pBurstTime);
        }

        public override string ToString()
        {
            string res = string.Format("Process {0}  :  Arrived {1}  :  Burst {2}  :  Priority {3}  :  Color ({4}, {5}, {6})", pId, pArrivalTime, pBurstTime, pPriority, pColor[0].ToString(), pColor[1].ToString(), pColor[2].ToString()) ;
            res += "\n" + string.Format("Waiting Time {0} : Turn Around Time {1}", pWaitingTime, pTurnAroundTime);
            return res;
        }

        public bool Finished()
        {
            return pRemTime == 0;
        }

    }
}
