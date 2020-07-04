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
                if (line.Length > 0)
                {
                    string symbol = line[0].ToString();
                    
                    switch (symbol)
                    {
                        case "+":
                            line = line.Substring(1);
                            exInt += line;
                            break;
                        case "-":
                            line = line.Substring(1);
                            exInt -= line;
                            break;
                        case "*":
                            line = line.Substring(1);
                            exInt *= line;
                            break;
                        default:
                            exInt = line;
                            break;
                    }
                }
                Console.WriteLine("Number: " + exInt.ToString());
            }
            



            Console.WriteLine("Press any button...");
            Console.ReadKey();
        }
    }
}