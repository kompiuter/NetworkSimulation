using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class Node
    {
        private RouterData _data;
        private LinkedList<Node> _neighbours;

        public Node(RouterData data)
        {
            _data = data;
        }

        public Node(RouterData data, LinkedList<Node> neighbours) : this(data)
        {
            _neighbours = neighbours;
        }

        #region Properties

        public RouterData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public LinkedList<Node> Neighbours => _neighbours;

        #endregion

        #region Methods

        public void AddNeighbour(Node neighbour)
        {
            if (_neighbours == null)
                _neighbours = new LinkedList<Node>();

            // Only add neighbour if it's not already a neighbour
            if (!_neighbours.Contains(neighbour))
                _neighbours.AddLast(neighbour);
        }

        public void AddNeighbour(RouterData data)
        {
            Node node = new Node(data);
            AddNeighbour(node);
        }

        public void DisplayNeighbours()
        {
            if (_neighbours == null || _neighbours.Count == 0)
                Console.WriteLine($"{_data.ToString()} has no neighbours");
            else
            {
                Console.Write($"{_data.ToString()} neighbours: ");
                foreach (var neighbour in _neighbours)
                    Console.Write($"{neighbour._data.ToString()} ");
                Console.WriteLine();
            }
        }

        public void RemoveNeighbour(Node neighbour)
        {
            if (_neighbours == null)
                return;

            _neighbours.Remove(neighbour);
        }

        public void Flood(int sequenceID)
        {
            foreach (var packet in Data.Packets.Where(p => p.SequenceID == sequenceID))
            {
                foreach (var neighbour in Neighbours)
                {
                    var newPacket = packet.Clone();
                    if (newPacket.SendTo(neighbour.Data.ID))
                        neighbour.Data.ReceivePacket(newPacket);
                }
            }
            Data.ClearPackets(sequenceID);
        }

        #endregion

        public bool Equals(Node other) => _data.Equals(other.Data);

    }
}
