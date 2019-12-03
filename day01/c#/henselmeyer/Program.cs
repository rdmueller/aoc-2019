using System;
using System.IO;
using System.Linq;

namespace day01
{
    class Program
    {
        static long Do(Converter< long , long > func) => 
            File
                .ReadAllLines("input.txt")
                .ToList()
                .ConvertAll(long.Parse)
                .ConvertAll(func)
                .Sum()
        ;
        static long DoOne() => 
            Do
            (
                i => 
                    (long)((i / 3.0) - 2)
            );
        static long DoTwo() =>
            Do
            (
                i => 
                    Enumerable
                    .Range(0, 1000)
                    .ToList()
                    .ConvertAll(_ => i = (long)((i / 3.0) - 2))
                    .TakeWhile(j => j > 0)
                    .Sum()
            );
        static void Main(string[] args) =>
            Console.WriteLine
            (
                (args.FirstOrDefault() ?? string.Empty) == "one" ? $"{DoOne()}" :
                (args.FirstOrDefault() ?? string.Empty) == "two" ? $"{DoTwo()}" :
                "Usage: day02 [one|two]"
            );
    }
}
