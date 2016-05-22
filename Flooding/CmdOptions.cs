using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooding
{
    class CmdOptions
    {
        [Option('t', Required = true, HelpText = "(int) Tick interval in ms, must be greater than 0")]
        public int TickInterval { get; set; }

        [Option('h', Required = true, HelpText = "(byte) Packet max hop count, must be between 1 (inclusive) and 255 (inclusive)")]
        public byte PacketHopCount { get; set; }

        [Option('c', Required = true, HelpText = "(double) Chance for a node to generate a new packet each tick, must be between 0 (exclusive) and 1 (inclusive)")]
        public double PacketGenChance { get; set; }

        [HelpOption]
        public string GetUsage() => HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
    }
}
