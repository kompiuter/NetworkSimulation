using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class SimulationOptions
    {
        /// <summary>
        /// How long each simulation tick should last (in ms)
        /// </summary>
        public int TickInterval { get; set; }

        /// <summary>
        /// Maximum number of hops a packet should do before being discarded
        /// </summary>
        public byte PacketHopCount { get; set; }

        /// <summary>
        /// Chance for packet to be created by a node
        /// </summary>
        public double PacketGenerationChance { get; set; } 

        /// <summary>
        /// The graph of nodes the simulation will run on
        /// </summary>
        public Graph Topology { get; set; }
    }
}
