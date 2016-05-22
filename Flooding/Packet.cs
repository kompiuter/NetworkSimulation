using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class Packet
    {
        static int _packetCount;
        private int _sequenceNo;
        private byte _hopCount;
        private char _sourceId;
        private char _destId;
        private char _currentId;

        static Packet()
        {
            _packetCount = 0;
        }

        private Packet(byte hopCount)
        {
            if (hopCount == 0)
                throw new ArgumentException("Hop count cannot be 0");

            _hopCount = hopCount;
            ++_packetCount;
            _sequenceNo = _packetCount;
        }

        public Packet(char sourceId, char destId, byte hopCount) : this(hopCount)
        {
            if (sourceId == destId)
                throw new ArgumentException("Source id cannot be the same as the destination id");

            _sourceId = sourceId;
            _currentId = sourceId;
            _destId = destId;
        }

        public Packet(char sourceId, char destId) : this(sourceId, destId, 4) { }

        public int SequenceNo => _sequenceNo;

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
    }
}
