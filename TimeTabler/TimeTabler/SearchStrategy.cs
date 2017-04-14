using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    interface SearchStrategy {

        Node Solve(Node Initial);
        void AddNodes(List<Node> NewNodes);

    }
}
