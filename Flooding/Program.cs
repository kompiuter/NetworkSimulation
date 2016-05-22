using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Flooding
{
    class Program
    {
        static Graph _topology;

        static void Main(string[] args)
        {
            _topology = Graph.GetSampleTopology();

            Timer timer = new Timer()
            {
                Interval = 2500,
                AutoReset = true,
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Simulate(_topology);

            Console.ReadLine();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Simulate(_topology);
        }

        private static Random rnd = new Random();

        private static void Simulate(Graph topology)
        {
            int tick = 0;
            while (true)
            {
                Console.ReadLine();
                Console.WriteLine($"Tick {tick}");

                // 10% chance for each node to create a new packet
                foreach (var srcNd in topology.Nodes)
                {
                    if (rnd.NextDouble() < 0.10)
                    {
                        RandomNode rndNode = new RandomNode();
                        Node dstNd = new Node(new RouterData(rndNode.Get(srcNd.Data.ID)));
                        _topology.NodeWithID(srcNd.Data.ID).Data.ReceivePacket(new Packet(srcNd.Data.ID, dstNd.Data.ID, tick));
                    }
                }

                foreach (var node in topology.Nodes.Where(n => n.Data.HasPackets).ToList())
                    node.Flood(tick);

                ++tick;
            };

        }
    }
}
