using System;
using System.Runtime.InteropServices.JavaScript;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis;

namespace W01_ControlFlow
{
    // bash: dotnet add package BenchmarkDotNet
    [MemoryDiagnoser]
    public class ClassificationBenchmark
    {
        private readonly string[] _testInputs =
        [
            "-100", "-0.5", "0", "0.0001", "5", "10", "999", "1000", "100000", "not-a-number", "", null
        ];

        [Benchmark]
        public void TraditionalClassification()
        {
            foreach (var input in _testInputs)
            {
                Program.ClassifyInputTraditional(input);
            }
        }

        [Benchmark]
        public void ModernClassification()
        {
            foreach (var input in _testInputs)
            {
                Program.ClassifyInputModern(input);
            }
        }
    }

    public class BenchmarkRunnerMain
    {
        private static void Main2(string[] args)
        {
            BenchmarkRunner.Run<ClassificationBenchmark>();
        }
    }
    
    public enum NumberClassification
    {
        Positive,
        Negative,
        Zero,
        Small,
        Medium,
        Large,
        Invalid,
        Past,
        Today,
        Future
    }
    
    class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter a value (or 'exit' to quit): ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;
                
                // Try both classification methods
                // NumberClassification resultTraditional = ClassifyInputTraditional(input);
                // NumberClassification resultModern = ClassifyInputModern(input);
                // NumberClassification resultModern = ClassifyInputModerWithTryCatch(input);
                NumberClassification resultModern = ClassifyInputModernWithDate(input);
                
                // Console.WriteLine($"Traditional: {resultTraditional}");
                Console.WriteLine($"Modern: {resultModern}");
            }
        }

        // traditional approach using if/else and switch
        public static NumberClassification ClassifyInputTraditional(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return NumberClassification.Invalid;
            }

            try
            {
                decimal result = decimal.Parse(input);
                {
                    switch (result)
                    {
                        case < 0:
                            return NumberClassification.Negative;
                        case 0:
                            return NumberClassification.Zero;
                        case > 0 and < 10:
                            return NumberClassification.Small;
                        case >= 10 and < 1000:
                            return NumberClassification.Medium;
                        case >= 1000:
                            return NumberClassification.Large;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Traditional] Error parsing input: {e.Message}");
            }
            return NumberClassification.Invalid;
        }

        // modern approach using pattern matching and switch expression
        public static NumberClassification ClassifyInputModern(string input) => input switch
        {
            null or "" => NumberClassification.Invalid,
            { Length: > 0 } when decimal.TryParse(input, out var number) => number switch
            {
                < 0 => NumberClassification.Negative,
                0 => NumberClassification.Zero,
                > 0 and < 10 => NumberClassification.Small,
                >= 10 and < 1000 => NumberClassification.Medium,
                >= 1000 => NumberClassification.Large
            },
            _ => NumberClassification.Invalid
        };

        public static NumberClassification ClassifyInputModerWithTryCatch(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                    return NumberClassification.Invalid;
                
                decimal number = decimal.Parse(input);
                
                return number switch
                {
                    < 0 => NumberClassification.Negative,
                    0 => NumberClassification.Zero,
                    > 0 and < 10 => NumberClassification.Small,
                    >= 10 and < 1000 => NumberClassification.Medium,
                    >= 1000 => NumberClassification.Large
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Traditional] Error parsing input: {e.Message}");
                return NumberClassification.Invalid;
            }
        }

        // private static NumberClassification ClassifyInputModernWithDate(string input) => input switch
        // {
        //     null or "" => NumberClassification.Invalid,
        //     { Length: > 0 } when decimal.TryParse(input, out var number) => number switch
        //     {
        //         < 0 => NumberClassification.Negative,
        //         0 => NumberClassification.Zero,
        //         > 0 and < 10 => NumberClassification.Small,
        //         >= 10 and < 1000 => NumberClassification.Medium,
        //         >= 1000 => NumberClassification.Large
        //     },
        //     {Length: > 0 } when DateTime.TryParse(input, out var dateTime) => dateTime.Date switch
        //     {
        //         var d when d < DateTime.Today => NumberClassification.Past,
        //         var d when d == DateTime.Today => NumberClassification.Today,
        //         var d when d > DateTime.Today => NumberClassification.Future,
        //         _ => NumberClassification.Invalid
        //     },
        // };
        
        
        private static NumberClassification ClassifyInputModernWithDate(string input)
        {
            try
            {
                // Handle empty or whitespace input
                if (string.IsNullOrWhiteSpace(input))
                    return NumberClassification.Invalid;

                // First, try parsing as a number
                if (decimal.TryParse(input, out var number))
                {
                    return number switch
                    {
                        < 0 => NumberClassification.Negative,
                        0 => NumberClassification.Zero,
                        > 0 and < 10 => NumberClassification.Small,
                        >= 10 and < 1000 => NumberClassification.Medium,
                        >= 1000 => NumberClassification.Large
                    };
                }

                // Try parsing as a date
                if (DateTime.TryParse(input, out var dateTime))
                {
                    DateTime today = DateTime.Today;

                    return dateTime.Date switch
                    {
                        var d when d < today => NumberClassification.Past,
                        var d when d == today => NumberClassification.Today,
                        var d when d > today => NumberClassification.Future,
                        _ => NumberClassification.Invalid
                    };
                }

                // If neither parsing attempt succeeds, return Invalid
                return NumberClassification.Invalid;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Modern] Error parsing input: {e.Message}");
                return NumberClassification.Invalid;
            }
        }
    }
}
