using System;

namespace External
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Enter the number:");
            string line = Console.ReadLine();
            EInt number = line;
            while (line.ToLower() != "exit")
            {
                Console.WriteLine("Number: " + number.ToString());
                line = Console.ReadLine();
                if (line.Length > 0)
                {
                    string symbol = line[0].ToString();
                    line = line.Substring(1);

                    switch (symbol)
                    {
                        case "+":
                            number += line;
                            break;
                        case "-":
                            number -= line;
                            break;
                        case "*":
                            number *= line;
                            break;
                        case "/":
                            number /= line;
                            break;
                        default:
                            line = symbol + line;
                            number = line;
                            break;
                    }
                }
            }
            
            Console.ReadKey();
        }
    }
}