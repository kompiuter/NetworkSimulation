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
            var cmdOptions = new CmdOptions();
            if (CommandLine.Parser.Default.ParseArguments(args, cmdOptions)) // Valid arguments from cmd
            {
                var simOptions = new SimulationOptions
                {
                    Topology = Graph.GetSampleTopology(),
                    PacketGenerationChance = cmdOptions.PacketGenChance,
                    PacketHopCount = cmdOptions.PacketHopCount,
                    TickInterval = cmdOptions.TickInterval
                };

                ShowSimulationMenu(simOptions);
            }
            else
                Console.WriteLine("Invalid arguments supplied");        
             
            Console.ReadLine();
        }

        static void ShowSimulationMenu(SimulationOptions options)
        {
            Console.WriteLine("Press enter/return to start the simulation");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Simulation is starting in 3s... Press enter/return to stop the simulation at any time");
            Thread.Sleep(2000);
            Simulation sim = new Simulation(options);
            sim.Run();

            Console.ReadLine();
            sim.Stop();
        }
    }
}
