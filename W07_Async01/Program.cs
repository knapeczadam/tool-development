namespace W07_Async01
{
    internal class Program
    {
    private static void Main(string[] args)
    {
        var p = new Program();
        p.DoSomething();
    }

    private void DoSomething()
    {
        ExpensiveOperation();
        Console.WriteLine("operation has ended");
    }

    private void ExpensiveOperation()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Finished doing expensive operation");
    }
    }
}
