using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class Packet : IEquatable<Packet>
    {
        static int _packetCount;
        private int _id;
        private int _sequenceId;
        private byte _hopCount;
        private char _sourceId;
        private char _destId;
        private char _currentId;
        private char _receivedFromId;
        private bool _isDisposed;

        static Packet()
        {
            _packetCount = 0;
        }

        private Packet()
        {
            ++_packetCount;

            _hopCount = 99;
            _id = _packetCount;
        }

        public Packet(char sourceId, char destId, int sequenceId) : this()
        {
            if (sourceId == destId)
                throw new ArgumentException("Source id cannot be the same as the destination id");

            _sequenceId = sequenceId;
            _sourceId = sourceId;
            _currentId = sourceId;
            _receivedFromId = sourceId;
            _destId = destId;

            Console.WriteLine($"Packet {ID} created. Source node: {_sourceId}, Destination node: {_destId}");
        }

        #region Properties

        public int ID => _id;

        public int SequenceID => _sequenceId;

        /// <summary>
        /// Maximum number of hops this packet can do. Packet is discarded when it hits 0
        /// </summary>
        public byte HopCount => _hopCount;

        /// <summary>
        /// Which node this packet originates from
        /// </summary>
        public char SourceID => _sourceId;

        /// <summary>
        /// Which node this packet is destined for
        /// </summary>
        public char DestinationID => _destId;

        /// <summary>
        /// Which node this packet is currently located at
        /// </summary>
        public char CurrentID => _currentId;

        public bool HasReachedDestination => _currentId == _destId;

        public bool IsDisposed => _isDisposed;

        #endregion

        #region Methods

        public Packet Clone() => (Packet)MemberwiseClone();

        /// <summary>
        /// Send a packet to a specified node.
        /// Packet will not be sent if any of the three following conditions hold:
        /// 1. Packet is being sent to the node it arrived from
        /// 2. Hop count is 0
        /// 3. Packet is at destination node
        /// </summary>
        /// <param name="nodeId">char: Node ID</param>
        /// <returns>True if the packet is sent, false otherwise</returns>
        public bool SendTo(char nodeId)
        {
            if (nodeId == _currentId)
                throw new ArgumentException("Cannot send a packet to it's host node");

            // Packet will not be sent if it's hop count is 0 or if it's being sent to the node it arrived from or is at destination node
            if (_hopCount == 0 || _receivedFromId == nodeId || _currentId == _destId)
            {
                _isDisposed = true;
                return false;
            }


            // Update received from
            _receivedFromId = _currentId;

            ++_sequenceId;
            _currentId = nodeId;
            --_hopCount;

            //Console.WriteLine($"Packet {ID} sent from {_receivedFromId} to {_currentId}. Hop count {_hopCount}");
            // Packet arrived at destination node
            if (_currentId == _destId)
            {
                Console.WriteLine($"Packet {ID} arrived at destination node ({_destId})");
            }

            return true;
        }

        public bool Equals(Packet other) => _id == other.ID;

        #endregion
    }
}
