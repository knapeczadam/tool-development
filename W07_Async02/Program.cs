namespace W07_Async02
{
    internal class Program
    {
    static void Main(string[] args)
    {
        Program p = new Program();
        p.DoSomething();
    }

    private void DoSomething()
    {
        Thread thread = new Thread(ExpensiveOperation);
        thread.Start();
        
        Console.WriteLine("operation has ended");
    }

    private void ExpensiveOperation()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Finished doing expensive operation");
    }
    }
}
