namespace W07_Async03
{
    internal class Program
    {
    static void Main(string[] args)
    {
        DoSomething();
    }

    private static void DoSomething()
    {
        Thread thread = new Thread(ExpensiveOperation);
        thread.Start();
        if (thread.ThreadState == ThreadState.Running)
        {
            Console.WriteLine("operation is still running. Waiting for it to end");
            thread.Join();
            
            Console.WriteLine("operation has ended");
        }
    }

    private static void ExpensiveOperation()
    {
        Thread.Sleep(3000);
        Console.WriteLine("finished doing expensive operation");
    }
    }
}
