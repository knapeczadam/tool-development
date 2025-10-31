namespace W07_Async05
{
    internal class Program
    {
    static void Main(string[] args)
    {
        DoSomething();
    }

    private static void DoSomething()
    {
        
        Task runningTask = Task.Run(() =>
        {
            ExpensiveOperation();
            Console.WriteLine("operation finished");
        });

        while (!runningTask.IsCompleted)
        {
            
        }
    }

    private static void ExpensiveOperation()
    {
        Thread.Sleep(3000);
        Console.WriteLine("finished doing expensive operation");
    }
    }
}
