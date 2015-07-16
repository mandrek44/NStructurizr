using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var examples = new Dictionary<string, Action>
            {
                {"FinancialRiskSystem", FinancialRiskSystem.Run}, 
                {"TechTribesContainers", TechTribesContainers.Run}
            };

            if (!args.Any())
            {
                Console.WriteLine("Which example to run? (you can pass the name of the example as argument)");
                Console.WriteLine("Available examples: " + string.Join(", ", examples.Keys));
                args = new[] {Console.ReadLine()};
            }

            foreach (var example in examples.Where(example => args.Contains(example.Key)))
            {
                example.Value();
            }
        }
    }
}
