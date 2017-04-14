using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    class DepthFirstSearch : SearchStrategy {

        private Stack<Node> Nodes;

        public DepthFirstSearch() : base() {
            Nodes = new Stack<Node>();
        }

        public void AddNodes(List<Node> NewNodes) {
            for (int i = NewNodes.Count - 1; i >= 0; i--)
                Nodes.Push(NewNodes[i]);
        }

        public Node Solve(Node Initial) {
            Node Current = Initial;
            while (!Current.IsValid()) {
                AddNodes(Current.Expand());
                Current = Nodes.Pop();
            }
            return Current;
        }
    }
}
