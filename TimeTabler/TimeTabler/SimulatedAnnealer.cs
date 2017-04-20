using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {

    /// <summary>
    /// Simulates the annealing process
    /// </summary>
    class SimulatedAnnealer {

        public Node Initial;

        private int InitialTemp = 2500;
        private int FinalTemp = 1;
        private int MaxSteps = 10000;
        private List<Node> AllNodes;

        public SimulatedAnnealer(Node Initial) {
            this.Initial = Initial;
            AllNodes = GetAllNodes();
            Console.WriteLine("Node Count = " + AllNodes.Count);
        }

        public List<Node> GetAllNodes() {
            return ExpandNode(Initial);
        }

        private List<Node> ExpandNode(Node Curr) {
            List<Node> Nodes = new List<Node>();
            foreach(Node Descendant in Curr.Expand()) {
                if (Nodes.Count > 5000) break;
                Nodes.Add(Descendant);
                if(!Descendant.IsValid()) {
                    Nodes.AddRange(ExpandNode(Descendant));
                }
            }
            return Nodes;
        }

        private int StateEnergy(Node State) {
            //Energy should be high if not a valid state
            int Energy = (State.TasksToSched.Count == 0) ? 0 : 1000;
            Energy += State.Week.GetGapTime();
            return Energy;
        }

        private Node GetRandomNode() {
            Random Randy = new Random();
            return AllNodes[Randy.Next(AllNodes.Count)];
        }

        public Node Anneal() {
            double TempFactor = -Math.Log(InitialTemp / FinalTemp);
            Random Randy = new Random();
            //Initialize
            int Steps = 0;
            double Temp = InitialTemp;
            Node Curr = Initial;
            int Energy = StateEnergy(Curr);

            Node PrevState = Curr;
            int PrevEnergy = Energy;

            Node BestState = Curr;
            int BestEnergy = Energy;

            while(Steps < MaxSteps) {
                Steps++;
                Temp = InitialTemp * Math.Exp(TempFactor * Steps / MaxSteps);
                Curr = GetRandomNode();
                Energy = StateEnergy(Curr);
                int dE = Energy - PrevEnergy;

                if(dE > 0 && Math.Exp(-dE/ Temp) < Randy.NextDouble()) {
                    //Abort to previous state
                    Curr = PrevState;
                    Energy = PrevEnergy;
                } else { //Move to this state
                    PrevState = Curr;
                    PrevEnergy = Energy;
                    if(Energy < BestEnergy) {
                        BestEnergy = Energy;
                        BestState = Curr;
                    }
                }
            }
            return BestState;
        }
    }
}
