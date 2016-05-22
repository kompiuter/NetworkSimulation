using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class Program
    {
        static void Main(string[] args)
        {
            var topology = Graph<RouterData>.GetSampleTopology();

            foreach (var node in topology.Nodes)
                node.DisplayNeighbours();

            Console.ReadLine();
        }
    }
}
