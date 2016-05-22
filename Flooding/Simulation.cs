using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flooding
{
    class Simulation
    {
        private readonly Graph topology;
        private readonly byte hopCount;
        private readonly double packetChance;
        private readonly int tickInterval;
        private readonly Mutex mtx = new Mutex(false);
        private readonly Random rnd = new Random((int)DateTime.Now.Ticks);

        public Simulation(SimulationOptions options)
        {
            if (options.Topology == null)
                throw new ArgumentNullException("Topology cannot be null", nameof(options.Topology));
            if (options.PacketHopCount <= 0)
                throw new ArgumentException("Hop count cannot be 0", nameof(options.PacketHopCount));
            if (options.PacketGenerationChance <= 0)
                throw new ArgumentException("Packet generation chance cannot be less or equal than 0", nameof(options.PacketGenerationChance));
            if (options.PacketGenerationChance > 1)
                throw new ArgumentException("Packet generation chance cannot be greater than 1", nameof(options.PacketGenerationChance));
            if (options.TickInterval <= 0)
                throw new ArgumentException("Tick interval cannot be less or equal than 0", nameof(options.TickInterval));

            topology = options.Topology;
            hopCount = options.PacketHopCount;
            packetChance = options.PacketGenerationChance;
            tickInterval = options.TickInterval;
        }

        public void Run()
        {
            var timer = new System.Timers.Timer()
            {
                Interval = tickInterval,
                AutoReset = true,
            };

            int tick = 0;
            timer.Elapsed += (s, e) =>
            {
                // Use mutex for cases where interval is lower than processing speed, so two threads don't access a common collection concurrently
                mtx.WaitOne();

                Console.WriteLine($"Tick: {tick}");

                if (AreLinesFlooded())
                {
                    Console.WriteLine("All lines are flooded, ending simulation.");
                    timer.Stop();
                }
                else
                {
                    // 10% chance for each node to create a new packet
                    foreach (var srcNd in topology.Nodes)
                    {
                        if (rnd.NextDouble() < packetChance)
                        {
                            RandomNode rndNode = new RandomNode();
                            Node dstNd = new Node(new RouterData(rndNode.Get(srcNd.Data.ID)));
                            topology.NodeWithID(srcNd.Data.ID).Data.ReceivePacket(new Packet(srcNd.Data.ID, dstNd.Data.ID, tick, hopCount));
                        }
                    }

                    foreach (var node in topology.Nodes.Where(n => n.Data.HasPackets).ToList())
                        node.Flood(tick);

                    ++tick;
                }

                mtx.ReleaseMutex();
            };

            timer.Start();
        }


        /// <summary>
        /// Checks if all lines are flooded (packet max capacity reached)
        /// </summary>
        private bool AreLinesFlooded()
        {
            bool allLinesFlooded = true;

            foreach (var node in topology.Nodes.Select(n => n.Data))
            {
                // If at least one node isnt at max capacity, then not all lines are flooded
                if (!node.IsAtMaxCapcity)
                    allLinesFlooded = false;
            }

            return allLinesFlooded;
        }
    }
}
