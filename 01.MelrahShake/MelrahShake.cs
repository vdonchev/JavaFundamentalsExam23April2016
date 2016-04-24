namespace _01.MelrahShake
{
    using System;

    public static class MelrahShake
    {
        public static void Main()
        {
            var word = Console.ReadLine();
            var needle = Console.ReadLine();
            while (true)
            {
                var firstMatchStart = word.IndexOf(needle);
                var firstMatchEnd = word.IndexOf(needle) + needle.Length;
                var lastMatchStart = word.LastIndexOf(needle);

                if (needle.Length == 0 ||
                    firstMatchStart == lastMatchStart ||
                    firstMatchEnd > lastMatchStart)
                {
                    break;
                }
                
                word = word.Remove(firstMatchStart, needle.Length);
                word = word.Remove(lastMatchStart - needle.Length, needle.Length);

                needle = needle.Remove(needle.Length / 2, 1);
                Console.WriteLine("Shaked it.");
            }

            Console.WriteLine("No shake.");
            Console.WriteLine(word);
        }
    }
}