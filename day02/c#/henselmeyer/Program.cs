using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    class Program
    {
        static int? Process(IList< int > ram)
        {
            var ip = 0;
            while (true)
            {
                var lowerLimit = 0;
                var upperLimit = ram.Count;
                if (ip < lowerLimit || ip + 3 >= upperLimit)
                {
                    return null;
                }
                var opcode = ram[ip + 0];
                var addr1  = ram[ip + 1];
                var addr2  = ram[ip + 2];
                var addr3  = ram[ip + 3];
                if (addr1 < lowerLimit || addr2 < lowerLimit || addr3 < lowerLimit || addr1 >= upperLimit || addr2 >= upperLimit || addr3 >= upperLimit)
                {
                    return null;
                }
                var op1 = ram[addr1];
                var op2 = ram[addr2];
                int result;
                switch (opcode)
                {
                    case 1:
                        result = op1 + op2;
                        ip += 4;
                        break;
                    case 2:
                        result = op1 * op2;
                        ip += 4;
                        break;
                    case 99:
                        return ram.First();
                    default:
                        return null;
                }
                ram[addr3] = result;
            }
        }
        static T Do<T>(Func< IList< int >, T > func) => 
            func
            (
                File
                .ReadAllText("input.txt")
                .Split(new[] { ','}, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ConvertAll(int.Parse)
                .ToList()
            );
        static int? DoOne() => Do(Process);
        static int DoTwo() => 
            Do
            (
                ram =>
                (
    	            from noun in Enumerable.Range(1, 99)
                    from verb in Enumerable.Range(1, 99)
                    select new { noun = noun, verb = verb }
                )
                .Where(pair => 
                    Process
                    (
                        Enumerable
                        .Empty< int >()
                        .Append(ram.First())
                        .Append(pair.noun)
                        .Append(pair.verb)
                        .Concat(ram.Skip(3))
                        .ToList()
                    ) == 19690720
                )
                .Select(pair => pair.noun * 100 + pair.verb)
                .First()
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
