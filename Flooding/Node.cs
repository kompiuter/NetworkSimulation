using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class Node<T> : IEquatable<Node<T>> where T : IEquatable<T>
    {
        private T _data;
        private LinkedList<Node<T>> _neighbours;

        public Node(T data)
        {
            _data = data;
        }

        public Node(T data, LinkedList<Node<T>> neighbours) : this(data)
        {
            _neighbours = neighbours;
        }

        #region Properties

        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public LinkedList<Node<T>> Neighbours => _neighbours;

        #endregion

        #region Methods

        public void AddNeighbour(Node<T> neighbour)
        {
            if (_neighbours == null)
                _neighbours = new LinkedList<Node<T>>();

            // Only add neighbour if it's not already a neighbour
            if (!_neighbours.Contains(neighbour))
                _neighbours.AddLast(neighbour);
        }

        public void AddNeighbour(T data)
        {
            Node<T> node = new Node<T>(data);
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

        public void RemoveNeighbour(Node<T> neighbour)
        {
            if (_neighbours == null)
                return;

            _neighbours.Remove(neighbour);
        }

        #endregion

        public bool Equals(Node<T> other) => _data.Equals(other.Data);

    }
}
