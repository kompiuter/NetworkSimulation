using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class Graph
    {
        private List<Node> _nodes;

        public Graph(List<Node> nodes = null)
        {
            if (nodes == null)
                _nodes = new List<Node>();
            else
                _nodes = nodes;
        }

        public List<Node> Nodes => _nodes;

        #region Methods

        public void AddNode(Node node)
        {
            if (!_nodes.Contains(node))
                _nodes.Add(node);
        }

        public void AddNote(IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
                AddNode(node);
        }

        public void AddNode(RouterData data)
        {
            AddNode(new Node(data));
        }

        public void AddEdge(Node from, Node to)
        {
            from.AddNeighbour(to);
            to.AddNeighbour(from);
        }

        public Node NodeAt(int index) => _nodes[index];

        public Node NodeWithID(char id) => _nodes.FirstOrDefault(n => n.Data.ID == id);

        public static Graph GetSampleTopology()
        {
            Graph graph = new Graph();

            foreach (var num in Enumerable.Range(0, 10))
                graph.AddNode(new RouterData((char)(65 + num))); // Generates nodes A to J

            graph.AddEdge(graph.NodeAt(0), graph.NodeAt(1)); // A connected to B
            graph.AddEdge(graph.NodeAt(0), graph.NodeAt(2)); // A connected to C
            graph.AddEdge(graph.NodeAt(0), graph.NodeAt(4)); // A connected to E
            graph.AddEdge(graph.NodeAt(0), graph.NodeAt(5)); // A connected to E
            graph.AddEdge(graph.NodeAt(1), graph.NodeAt(8)); // B connected to I
            graph.AddEdge(graph.NodeAt(2), graph.NodeAt(3)); // C connected to D
            graph.AddEdge(graph.NodeAt(2), graph.NodeAt(4)); // C connected to E
            graph.AddEdge(graph.NodeAt(3), graph.NodeAt(6)); // D connected to G
            graph.AddEdge(graph.NodeAt(4), graph.NodeAt(6)); // E connected to G
            graph.AddEdge(graph.NodeAt(5), graph.NodeAt(7)); // F connected to H
            graph.AddEdge(graph.NodeAt(6), graph.NodeAt(7)); // G connected to H
            graph.AddEdge(graph.NodeAt(6), graph.NodeAt(9)); // G connected to J
            graph.AddEdge(graph.NodeAt(7), graph.NodeAt(9)); // H connected to J
            graph.AddEdge(graph.NodeAt(8), graph.NodeAt(9)); // I connected to J

            return graph;
        }

        #endregion

    }
}
