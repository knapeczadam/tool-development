using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace W01_TaskManager_v1;

internal class Task
{
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }

    public override string ToString()
    {
        return DueDate.HasValue
            ? $"{Description} (Due: {DueDate:yyyy-MM-dd})"
            : Description;
    }
}

public class TaskManager
{
    private List<Task> _tasks = [];
    private const string FILE_PATH = "tasks.txt";

    public void AddTask(string description, DateTime? dueDate)
    {
        _tasks.Add(new Task { Description = description, DueDate = dueDate });
    }

    public void ListTasks()
    {
        _tasks.ForEach(Console.WriteLine);
    }

    public bool RemoveTask(int index)
    {
        if (index >= _tasks.Count || index < 0) return false;
        _tasks.RemoveAt(index);
        return true;
    }

    public void SaveTasks()
    {
        var lines = new List<string>();
        _tasks.ForEach(t => lines.Add(t.Description + "|" + t.DueDate));
        File.WriteAllLines(FILE_PATH, lines);
    }

    public void LoadTasks()
    {
        var lines = File.ReadAllLines(FILE_PATH);
        _tasks.Clear();
        foreach (var line in lines)
        {
            string[] task = line.Split("|");
            string description = task.First();
            DateTime? dueDate = null;
            if (task.Length > 1)
            {
                if (DateTime.TryParseExact(task.Last(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var result))
                {
                    dueDate = result;
                }
            }
            _tasks.Add(new Task { Description = description, DueDate = dueDate});
        }

    }
}

internal abstract class Program
{
    private static void Main(string[] args)
    {
        const string menu = """
=== Task Manager === 
1. Add Task
2. List Tasks
3. Remove Task
4. Save Tasks
5. Load Tasks
0. Exit
""";
        Console.WriteLine(menu);

        bool running = true;
        var taskManager = new TaskManager();
        while (running)
        {
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out var option))
            {
                switch (option)
                {
                    case 0:
                        running = false;
                        break;
                    case 1:
                        AddTask(taskManager);
                        break;
                    case 2:
                        ListTasks(taskManager);
                        break;
                    case 3:
                        RemoveTask(taskManager);
                        break;
                    case 4:
                        SaveTasks(taskManager);
                        break;
                    case 5:
                        LoadTasks(taskManager);
                        break;
                }
            }
            else
            {
                
            }
        }
    }

    private static void AddTask(TaskManager taskManager)
    {
        Console.Write("Enter task description: ");
        string description = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Description cannot be empty");
            return;
        }
        
        Console.Write("Enter due date (yyyy-MM-dd or press Enter for no date): ");
        string dueDate = Console.ReadLine();
        if (DateTime.TryParseExact(dueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
        {
            taskManager.AddTask(description, result);
            Console.WriteLine("Task added successfully!");
            return;
        }
        
        taskManager.AddTask(description, null);
        Console.WriteLine("Task added successfully!");
    }

    private static void RemoveTask(TaskManager taskManager)
    {
        Console.Write("Remove task: ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out var result))
        {
            if (taskManager.RemoveTask(result))
                Console.WriteLine("Task has been removed successfully!");
            else
                Console.WriteLine("Unsuccessful, try again!");
        }
    }

    private static void ListTasks(TaskManager taskManager)
    {
        taskManager.ListTasks();
    }

    private static void SaveTasks(TaskManager taskManager)
    {
        taskManager.SaveTasks();
    }

    private static void LoadTasks(TaskManager taskManager)
    {
        taskManager.LoadTasks();
    }
}
