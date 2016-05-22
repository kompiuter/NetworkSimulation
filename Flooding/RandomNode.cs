using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    /// <summary>
    /// Used to obtain a random node which isn't the same as the node passed
    /// </summary>
    class RandomNode
    {
        private readonly List<char> _nodeIds;
        public RandomNode(int noOfNodes = 10)
        {
            if (noOfNodes < 2)
                throw new ArgumentException("Need to have at least two nodes to get a random node");

            _nodeIds = Enumerable.Range(0, noOfNodes).Select(a => (char)(65 + a)).ToList();
        }

        private Random rnd = new Random();

        public char Get(char nodeToExclude)
        {
            char nodeId;
            do
            {
                nodeId = _nodeIds.ElementAt(rnd.Next(0, _nodeIds.Count - 1));
            } while (nodeId == nodeToExclude);

            return nodeId;
        }
        
    }
}
