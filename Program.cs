using System;

namespace External
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello world!");



            while (true)
            {
                int[,] mass = new int[4, 4];
                Random random = new Random();
                for (int i = 0; i < mass.GetLength(0); i++)
                {
                    for (int k = 0; k < mass.GetLength(1); k++)
                    {
                        mass[i, k] = random.Next(0, 4);
                        //Console.Write(mass[i, k] + " ");
                    }
                    //Console.WriteLine();
                }

                if (Kudash.IsMagicSquare(mass))
                {
                    Console.WriteLine("Ура. Кудаш сдал ЕГЭ");
                    for (int i = 0; i < mass.GetLength(0); i++)
                    {
                        for (int k = 0; k < mass.GetLength(1); k++)
                        {
                            mass[i, k] = random.Next(0, 4);
                            Console.Write(mass[i, k] + " ");
                        }
                        Console.WriteLine();
                    }
                    Console.ReadKey();
                }
            }


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