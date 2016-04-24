namespace _04.GUnit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class GUnit
    {
        private const string Pattern = 
            @"^(?<class>[A-Z][a-zA-Z0-9]+)\s\|\s(?<method>[A-Z][a-zA-Z0-9]+)\s\|\s(?<unit>[A-Z][a-zA-Z0-9]+)$";

        private static Dictionary<string, Dictionary<string, HashSet<string>>> hierarchy;
         
        public static void Main()
        {
            hierarchy = new Dictionary<string, Dictionary<string, HashSet<string>>>();
            var regex = new Regex(Pattern);

            var input = Console.ReadLine();
            while (input != "It's testing time!")
            {
                var unit = regex.Match(input);
                if (unit.Success)
                {
                    var className = unit.Groups["class"].Value;
                    var methodName = unit.Groups["method"].Value;
                    var testName = unit.Groups["unit"].Value;

                    if (!hierarchy.ContainsKey(className))
                    {
                        hierarchy[className] = new Dictionary<string, HashSet<string>>();
                    }

                    if (!hierarchy[className].ContainsKey(methodName))
                    {
                        hierarchy[className][methodName] = new HashSet<string>();
                    }

                    hierarchy[className][methodName].Add(testName);
                }

                input = Console.ReadLine();
            }

            foreach (var classItem in hierarchy
                .OrderByDescending(c => c.Value.Sum(m => m.Value.Count))
                .ThenBy(c => c.Value.Count)
                .ThenBy(c => c.Key, StringComparer.Ordinal))
            {
                Console.WriteLine($"{classItem.Key}:");
                foreach (var methodItem in classItem.Value
                    .OrderByDescending(m => m.Value.Count)
                    .ThenBy(m => m.Key, StringComparer.Ordinal))
                {
                    Console.WriteLine($"##{methodItem.Key}");
                    foreach (var unitTest in methodItem.Value
                        .OrderBy(u => u.Length)
                        .ThenBy(u => u, StringComparer.Ordinal))
                    {
                        Console.WriteLine($"####{unitTest}");
                    }
                }
            }
        }
    }
}