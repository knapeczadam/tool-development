using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace W01_TaskManager_v2;

public class Task
{
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public string Category { get; set; }
    public bool IsComplete { get; set; }

    public override string ToString()
    {
        string status = IsComplete ? "[x]" : "[ ]";
        string dateInfo = DueDate.HasValue ? $" (Due: {DueDate:yyyy-MM-dd})" : "";
        return $"{status} {Description} - Category: {Category}{dateInfo}";
    }
}

public class TaskManager
{
    private List<Task> _tasks = [];
    private const string FILE_PATH = "tasks.txt";

    public void AddTask(string description, DateTime? dueDate)
    {
        _tasks.Add(new Task { Description = description, DueDate = dueDate });
        Console.WriteLine("Task added successfully");
    }

    public void ListTasks()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine("No tasks to show.");
            return;
        }

        var sorted = _tasks.OrderBy(t => t.DueDate ?? DateTime.MaxValue).ToList();
        Console.WriteLine("Tasks (sorted by due date):");

        for (int i = 0; i < sorted.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {sorted[i]}");
        }
    }

    public void ToggleTaskComplete(int index)
    {
        if (index < 1 || index > _tasks.Count)
        {
            Console.WriteLine("Invalid index.");
            return;
        }

        _tasks[index - 1].IsComplete = !_tasks[index - 1].IsComplete;
        Console.WriteLine("Task completion status updated.");
    }

    public void SearchTasks(string keyword)
    {
        var results = _tasks.Where(t => t.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                                        || t.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        if (results.Count == 0)
        {
            Console.WriteLine("No matching tasks found");
            return;
        }
        
        Console.WriteLine("Search results:");
        foreach (var task in results)
        {
            Console.WriteLine(task);
        }
    }

    public bool RemoveTask(int index)
    {
        if (index < 1 || index > _tasks.Count)
        {
            Console.WriteLine("Invalid index.");
            return false;
        }
        
        _tasks.RemoveAt(index - 1);
        Console.WriteLine("Task removed");
        return true;
    }

    public void SaveTasks()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
            File.WriteAllText("tasks.json", json);
            Console.WriteLine("Tasks saved to JSON");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving tasks: {e.Message}");
        }
        
        // try
        // {
        //     using StreamWriter writer = new StreamWriter(FILE_PATH);
        //     foreach (var task in _tasks)
        //     {
        //         string line = $"{task.Description}|{task.DueDate?.ToString("yyyy-MM-dd")}";
        //         writer.WriteLine(line);
        //     }
        //     Console.WriteLine("Tasks saved to filed.");
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine($"Error saving tasks: {e.Message}");
        // }
    }

    public void LoadTasks()
    {
        try
        {
            if (!File.Exists("tasks.json"))
            {
                Console.WriteLine("No saved tasks found.");
                return;
            }

            string json = File.ReadAllText("tasks.json");
            _tasks = JsonConvert.DeserializeObject<List<Task>>(json) ?? [];
            Console.WriteLine("Tasks loaded from JSON.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading tasks: {e.Message}");
        }
        // try
        // {
        //     if (!File.Exists(FILE_PATH))
        //     {
        //         Console.WriteLine("No saved tasks found.");
        //         return;
        //     }
        //     
        //     _tasks.Clear();
        //     foreach (var line in File.ReadAllLines(FILE_PATH))
        //     {
        //         var parts = line.Split('|');
        //         string description = parts.First();
        //         DateTime? dueDate = null;
        //         if (parts.Length > 1 && DateTime.TryParseExact(parts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture,
        //                 DateTimeStyles.None, out DateTime parsedDate))
        //         {
        //             dueDate = parsedDate;
        //         }
        //         
        //         _tasks.Add(new Task { Description = description, DueDate = dueDate });
        //     }
        //     Console.WriteLine("Tasks loaded successfully.");
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine($"Error loading tasks: {e.Message}");
        // }
    }
}

internal abstract class Program
{
    private static void Main(string[] args)
    {
        TaskManager manager = new();

        while (true)
        {
            Console.WriteLine("""
=== Task Manager ===
1. Add Task
2. List Tasks
3. Remove Task
4. Save Tasks
5. Load Tasks
6. Mark Task Complete/Incomplete
7. Search Tasks
0. Exit
""");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter task description: ");
                    string desc = Console.ReadLine();
                    
                    Console.Write("Enter due date (yyyy-MM-dd or press Enter for no date): ");
                    string dateInput = Console.ReadLine();

                    DateTime? dueDate = null;
                    if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParseExact(dateInput, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        dueDate = parsedDate;
                    }
                    manager.AddTask(desc, dueDate);
                    break;
                case "2":
                    manager.ListTasks();
                    break;
                case "3":
                    Console.Write("Enter task number to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int index))
                        manager.RemoveTask(index);
                    else
                        Console.WriteLine("Invalid number.");
                    break;
                case "4":
                    manager.SaveTasks();
                    break;
                case "5":
                    manager.LoadTasks();
                    break;
                case "6":
                    Console.Write("Enter task number to toggle its status: ");
                    if (int.TryParse(Console.ReadLine(), out int result))
                        manager.ToggleTaskComplete(result);
                    else
                        Console.WriteLine("Invalid number.");
                    break;
                case "7":
                    Console.Write("Enter words to search for tasks: ");
                    manager.SearchTasks(Console.ReadLine());
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.WriteLine();
        }
    }
}
