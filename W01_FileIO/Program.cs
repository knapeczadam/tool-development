using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace W01_FileIO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string path = "test.txt";
            Encoding encoding = Encoding.UTF8;
            
            // writing
            try
            {
                Console.WriteLine("Enter text (type 'done' to finish):");
                List<string> lines = [];
                while (true)
                {
                    string input = Console.ReadLine() ?? "";
                    if (input == "done")
                    {
                        // WriteToFile(lines.ToArray(), "test.txt");
                        Console.WriteLine("Writing to file...");
                        WriteToFile(lines.ToArray(), path, Encoding.UTF8, true);
                        Console.WriteLine("File written successfully!");
                        break;
                    }

                    lines.Add(input);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Access denied: {e.Message}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"IO error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }

            // reading
            try
            {
                Console.WriteLine("\nReading file contents:");
                string?[] lines = ReadFromFile(path, encoding);
                lines.ToList().ForEach(Console.WriteLine);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            // reading error
            Console.Write("Would you like to read a different file? (y/n)");
            string line = Console.ReadLine() ?? string.Empty;
            if (line == "y")
            {
                Console.Write("Enter file path:");
                string newPath = Console.ReadLine() ?? string.Empty;
                ReadFromFile(newPath, encoding);
            }
        }

        private static void WriteToFile(string[] lines, string path)
        {
            // File.WriteAllLines(path, lines);
            using StreamWriter writer = new StreamWriter(path);
            lines.ToList().ForEach(writer.WriteLine);
        }

        private static string?[] ReadFromFile(string path)
        {
            // return File.ReadAllLines(path);
            using StreamReader reader = new StreamReader(path);
            List<string?> lines = [];
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }

            return lines.ToArray();
        }

        private static void WriteToFile(string[] lines, string path, Encoding encoding, bool append = false)
        {
            try
            {
                using StreamWriter write = new StreamWriter(path, append, encoding);
                foreach (var line in lines)
                {
                    write.WriteLine(line);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw;
            }
            catch (IOException e)
            {
                throw;
            }
        }

        private static string[] ReadFromFile(string path, Encoding encoding)
        {
            try
            {
                var lines = new List<string>();
                using StreamReader reader = new StreamReader(path, encoding);
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }

                return lines.ToArray();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e.FileName}");
                return [];
            }
            catch (IOException e)
            {
                throw;
            }
        }
    }
}
