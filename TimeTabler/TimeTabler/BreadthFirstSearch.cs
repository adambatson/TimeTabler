using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    class BreadthFirstSearch : SearchStrategy {

        private Queue<Node> Nodes;

        public BreadthFirstSearch() : base() {
            Nodes = new Queue<Node>();
        }

        public void AddNodes(List<Node> NewNodes) {
            for (int i = NewNodes.Count - 1; i >= 0; i--)
                Nodes.Enqueue(NewNodes[i]);
        }

        public Node Solve(Node Initial) {
            Node Current = Initial;
            while (!Current.IsValid()) {
                AddNodes(Current.Expand());
                Current = Nodes.Dequeue();
            }
            return Current;
        }

    }
}
