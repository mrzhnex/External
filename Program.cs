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
                    line = line.Substring(1);

                    switch (symbol)
                    {
                        case "+":
                            exInt += line;
                            break;
                        case "-":
                            exInt -= line;
                            break;
                        case "*":
                            exInt *= line;
                            break;
                        default:
                            line = symbol + line;
                            exInt = line;
                            break;
                    }
                }
                Console.WriteLine("Number: " + exInt.ToString());
            }
            
            Console.ReadKey();
        }
    }
}