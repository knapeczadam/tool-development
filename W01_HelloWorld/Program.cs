using System;
using System.Linq;
using System.Text.RegularExpressions;

/*
 Extra challenge:
 Add string padding to align output nicely
 Create a name formatter that capitalizes first letters
 Extract and display middle names if provided
 Count and display vowels and consonants in the name
 Implement a simple name reverser
 */
namespace W01_HelloWorld
{
    internal class Program
    {
        internal struct Name
        {
            public string FirstName;
            public string LastName;
            public string FullName;

            public Name(string firstName, string lastName, string fullName)
            {
                FirstName = firstName;
                LastName = lastName;
                FullName = fullName;
            }

            public string GetName()
            {
                return !string.IsNullOrWhiteSpace(FullName) ? FullName : string.Concat(FirstName, " ", LastName);
            }
        }

        private static void Main(string[] args)
        {
            Name name = default;
            name.FirstName = GetValidName("first");
            name.LastName = GetValidName("last");
            DisplayGreetings(name);
            DisplayCount(name);
            DisplayInitials(name);
            DisplayInUpperCase(name);
            DisplayVowelCount(name);
            DisplayConsonantCount(name);
            DisplayInReverse(name);
            DisplayCapitalized(name);
            DisplayMiddleName(name);
            DisplayPaddedName(name);

            name.FullName = GetValidName("full");
            DisplayGreetings(name);
            DisplayCount(name);
            DisplayInitials(name);
            DisplayInUpperCase(name);
            DisplayVowelCount(name);
            DisplayConsonantCount(name);
            DisplayInReverse(name);
            DisplayCapitalized(name);
            DisplayMiddleName(name);
            DisplayPaddedName(name);
        }

        private static string GetValidName(string nameType)
        {
            while (true)
            {
                Console.WriteLine($"Enter your {nameType} name: ");
                string input = Console.ReadLine() ?? "";

                if (IsValidName(ref input, out string message))
                {
                    return input;
                }

                Console.WriteLine($"Error: {message} Please try again!");
            }
        }

        private static bool IsValidName(ref string name, out string message)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                message = "Name cannot be empty.";
                return false;
            }

            bool isLetters = Regex.IsMatch(name.Trim(), @"^[a-zA-Z\s]+$");
            if (!isLetters)
            {
                message = "Name can only contain letters and spaces.";
                return false;
            }

            message = "";
            name = Regex.Replace(name, @"\s+", " ").Trim();
            return true;
        }

        private static void DisplayGreetings(Name name)
        {
            Console.WriteLine($"Hello, {name.GetName()}!");
        }

        private static void DisplayCount(Name name)
        {
            int totalCount = CountLetters(name);
            Console.WriteLine($"Your name is {totalCount} characters long (excluding spaces)");
        }

        private static int CountLetters(Name name)
        {
            return name.GetName().Replace(" ", "").Length;
        }

        private static void DisplayInUpperCase(Name name)
        {
            Console.WriteLine($"Your name in uppercase: {name.GetName().ToUpper()}");
        }

        private static void DisplayInitials(Name name)
        {
            var initials = string.Join(".",
                name.GetName().Split(" ")
                    .Where(n => n.Length > 0)
                    .Select(n => char.ToUpper(n[0]))) + ".";
            Console.WriteLine($"Your initials are: {initials}");
        }

        private static void DisplayVowelCount(Name name)
        {
            int numOfVowels = Regex.Matches(name.GetName(), @"[aeiouAEIOU]").Count;
            Console.WriteLine($"Number of vowels: {numOfVowels}");
        }

        private static void DisplayConsonantCount(Name name)
        {
            int numOfConsonants = Regex.Matches(name.GetName(), @"(?i)[b-df-hj-np-t-v-z]").Count;
            Console.WriteLine($"Number of consonants: {numOfConsonants}");
        }

        private static void DisplayInReverse(Name name)
        {
            string reversed = new string(name.GetName().Reverse().ToArray());
            Console.WriteLine($"Your name in reversed: {reversed}");
        }

        private static void DisplayCapitalized(Name name)
        {
            var parts = name.GetName().Split(" ");
            var capitalized = parts
                .Where(p => p.Length > 0)
                .Select(p => char.ToUpper(p[0]) + (p.Length > 1 ? p.Substring(1).ToLower() : ""));

            string fullName = string.Join(" ", capitalized);
            Console.WriteLine($"Your capitalized name: {fullName}");
        }

        private static void DisplayMiddleName(Name name)
        {
            var parts = name.GetName().Split(" ");
            if (parts.Length == 3)
            {
                Console.WriteLine($"Your middle name is: {parts[1]}");
            }
        }

        private static void DisplayPaddedName(Name name)
        {
            const int padding = 5;
            var padded = name.GetName().PadLeft(name.GetName().Length + padding, '-');
            padded = padded.PadRight(padded.Length + padding, '-');
            Console.WriteLine($"Padded name: {padded}");
        }
    }
}