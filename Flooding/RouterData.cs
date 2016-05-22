using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class RouterData : IEquatable<RouterData>
    {
        private char _id;
        private readonly int _maxCapacity = 100;
        private List<Packet> _packets;

        public RouterData(char id)
        {
            _id = id;
        }

        #region Properties

        public char ID => _id;
        /// <summary>
        /// Maximum packets the router can hold
        /// </summary>
        public int MaxCapacity => _maxCapacity;

        public List<Packet> Packets => _packets;

        public bool HasPackets => Packets?.Any() ?? false;

        #endregion


        #region Methods

        public bool ReceivePacket(Packet packet)
        {
            if (_packets == null)
                _packets = new List<Packet>();

            if (_packets.Count == _maxCapacity)
            {
                Console.WriteLine($"Node {ID} is currently at max capacity.");
                return false;
            }
            else
            {
                _packets.Add(packet);
                return true;
            }
        }

        public void ClearPackets(int sequenceID)
        {
            if (_packets != null)
            {
                _packets.RemoveAll(p => p.SequenceID == sequenceID);
                _packets.RemoveAll(p => p.IsDisposed);
            }
        }

        public bool Equals(RouterData other) => _id == other.ID;

        public override string ToString() => _id.ToString();

        #endregion
    }
}
