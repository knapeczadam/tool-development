using System.ComponentModel;

namespace W07_Async04
{
    internal class Program
    {
    static void Main(string[] args)
    {
        DoSomething();
    }

    private static void DoSomething()
    {
        BackgroundWorker worker = new BackgroundWorker();
        worker.DoWork += (sender, args) => ExpensiveOperation();
        worker.RunWorkerCompleted += (sender, args) => Console.WriteLine("Operation finished");
        worker.RunWorkerAsync();
    }

    private static void ExpensiveOperation()
    {
        Thread.Sleep(3000);
        Console.WriteLine("finished doing expensive operation");
    }
    }
}
