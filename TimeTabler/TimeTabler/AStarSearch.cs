using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    class AStarSearch : SearchStrategy {

        private SortedList<int, Node> OpenNodes, ClosedNodes;
        private HashSet<Node> AllNodes;

        private Heuristic H;

        public AStarSearch(Heuristic H) : base() {
            this.H = H;
            OpenNodes = new SortedList<int, Node>(new DuplicateKeyComparer<int>());
            ClosedNodes = new SortedList<int, Node>(new DuplicateKeyComparer<int>());
            AllNodes = new HashSet<Node>();
        }

        public void AddNodes(List<Node> Nodes) {
            foreach (Node B in Nodes) {
                if (!AllNodes.Contains(B)) {
                    OpenNodes.Add(H.Evaluate(B), B);
                    AllNodes.Add(B);
                } else {
                    int keyIndex;
                    if (ClosedNodes.ContainsValue(B))
                        keyIndex = ClosedNodes.IndexOfValue(B);
                    else keyIndex = OpenNodes.IndexOfValue(B);
                    int key = ClosedNodes.Keys[keyIndex];
                    if (H.Evaluate(B) < key) {
                        UpdateKey(B, key - H.Evaluate(B));
                    }
                }
            }
        }

        private void UpdateKey(Node B, int Difference) {
            int OldKey = OpenNodes.Keys[OpenNodes.IndexOfValue(B)];
            OpenNodes.Remove(OldKey);
            ClosedNodes.Remove(OldKey);
            OpenNodes.Add(OldKey - Difference, B);
            ClosedNodes.Add(OldKey - Difference, B);
            foreach (Node Br in AllNodes)
                if (Br.Parent.Equals(B))
                    UpdateKey(Br, Difference);
        }

        public Node Solve(Node Initial) {
            OpenNodes.Add(H.Evaluate(Initial), Initial);
            AllNodes.Add(Initial);
            Node Current = Initial;
            while (!Current.IsValid()) {
                OpenNodes.RemoveAt(0);
                AddNodes(Current.Expand());
                ClosedNodes.Add(H.Evaluate(Current), Current);
                Current = OpenNodes.Values[0];
                //Console.WriteLine(Current);
                //Console.ReadLine();
            }
            return Current;
        }

    }
}
