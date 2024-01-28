using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Day20
{
    private static Queue<(Module? source, Module destination, Pulse pulse)>? _queue;
    public static string Part1()
    {
        using var reader = new StreamReader("Day20/input.txt");
        var lines = reader.ReadToEnd().Split("\r\n");
        var modules = new List<Module>();
        foreach (var line in lines)
        {
            var parts = line.Split("->").Select(s => s.Trim()).ToList();
            var name = parts[0];
            var output = parts[1].Contains(',') ? [.. parts[1].Split(',').Select(s => s.Trim())] : new string[] { parts[1] };
            modules.Add(ParseModule(name, output));
        }
        foreach (var module in modules)
        {
            module.Destination = modules.Where(m => module.RawDestination.Contains(m.Name)).ToArray();
            if (module is ConjunctionModule)
                (module as ConjunctionModule).Inputs = modules
                    .Where(m => m.RawDestination.Contains(module.Name))
                    .ToDictionary(m => m.Name, m => Pulse.Low);
            if (module.Destination.Length == 0)
                module.Destination = [new OutputModule { Name = "output" }];
        }
        var broadcaster = modules.FirstOrDefault(m => m is BroadcastModule);
        double lowCount = 0;
        double highCount = 0;
        if (broadcaster != null)
            for (int i = 0; i < 1000; i++)
            {
                _queue = new Queue<(Module? source, Module destination, Pulse pulse)>();
                _queue.Enqueue((null, broadcaster, Pulse.Low));
                while (_queue.Count != 0)
                {
                    var (source, destination, pulse) = _queue.Dequeue();
                    //if (i < 10) Console.WriteLine($"{source?.ToString() ?? "button"} -{pulse}-> {destination}");
                    destination.Input = (source, pulse);
                    if (pulse == Pulse.Low)
                        lowCount++;
                    else
                        highCount++;
                }
                //if (i < 10) Console.WriteLine();
            }
        return $"{lowCount * highCount}";
    }

    public static Module ParseModule(string name, string[] output)
    {
        if (name.StartsWith('%')) //FlipFlop
            return new FlipFlopModule { Name = name[1..], RawDestination = output };
        if (name.StartsWith('&')) //Conjunction
            return new ConjunctionModule { Name = name[1..], RawDestination = output };
        else //broadcaster
            return new BroadcastModule { RawDestination = output };
    }

    public enum Pulse
    {
        Low = 0, High = 1
    }

    public abstract class Module
    {
        public virtual string Name { get; set; }
        public virtual (Module?, Pulse) Input { get; set; }

        public string[] RawDestination { get; set; }
        public Module[] Destination { get; set; }
        public Module()
        {
            Name = string.Empty;
            RawDestination = [];
            Destination = [];
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }

    public class OutputModule : Module
    {
        public OutputModule() : base()
        {
            
        }
    }

    public class BroadcastModule : Module
    {
        public BroadcastModule() : base() { }
        public override string Name => "broadcaster";
        public override (Module?, Pulse) Input
        {
            set
            {
                var (source, pulse) = value;
                if (Destination != null && Destination.Length != 0 && _queue != null)
                    foreach (var module in Destination)
                        _queue.Enqueue((this, module, pulse));
            }
        }
    }

    public class ConjunctionModule : Module
    {
        public Dictionary<string, Pulse> Inputs { get; set; } = [];
        public ConjunctionModule() : base() { }
        public override (Module?, Pulse) Input
        {
            set
            {
                var (source, pulse) = value;
                if (source != null && _queue != null)
                {
                    Inputs[source.Name] = pulse;
                    if (Inputs.All(i => i.Value == Pulse.High))
                        foreach (var module in Destination)
                            _queue.Enqueue((this, module, Pulse.Low));
                    else
                        foreach (var module in Destination)
                            _queue.Enqueue((this, module, Pulse.High));
                }
            }
        }
    }

    public class FlipFlopModule : Module
    {
        public bool State { get; set; } = false;
        public FlipFlopModule() : base() { }
        public override (Module?, Pulse) Input
        {
            set
            {
                var (source, pulse) = value;
                if (source != null && pulse == Pulse.Low && _queue != null)
                {
                    if (State)
                        foreach (var module in Destination)
                            _queue.Enqueue((this, module, Pulse.Low));
                    else
                        foreach (var module in Destination)
                            _queue.Enqueue((this, module, Pulse.High));
                    State = !State;
                }
            }
        }
    }
}
