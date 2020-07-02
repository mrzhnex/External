using System;

namespace External
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello world!");

            ExInt exInt = 0;
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "a")
                {
                    throw new Exception("ad");
                }
                exInt += line;
                Console.WriteLine("Number+: " + exInt.ToString());
                line = Console.ReadLine();
                if (line == "a")
                {
                    throw new Exception("ad");
                }
                exInt -= line;
                Console.WriteLine("Number-: " + exInt.ToString());
            }
            



            Console.WriteLine("Press any button...");
            Console.ReadKey();
        }
    }
}