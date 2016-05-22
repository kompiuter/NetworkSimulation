using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Flooding
{
    class Program
    {
        static void Main(string[] args)
        {
            var simOptions = new SimulationOptions
            {
                Topology = Graph.GetSampleTopology(),
                PacketGenerationChance = 0.1,
                PacketHopCount = 4,
                TickInterval = 250
            };

            Simulation sim = new Simulation(simOptions);
            sim.Run();

            Console.ReadLine();
        }
    }
}
