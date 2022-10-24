using System;
namespace Commercial_Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test");
            int scenarioNumber = Int32.Parse(args[0]);
            Console.WriteLine(scenarioNumber);
            Scenarios scenarios = new Scenarios();
            scenarios.run(scenarioNumber);
        }
    }
}
