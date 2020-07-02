namespace External
{
    public static class Kudash
    {
        public static bool IsMagicSquare(int[,] mass)
        {
            if (mass.GetLength(0) != mass.GetLength(1))
                return false;
            int magicNumber = 0;
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                int sumColumn = 0;
                for (int k = 0; k < mass.GetLength(1); k++)
                {
                    sumColumn += mass[i, k];
                    if (k == mass.GetLength(1) - 1)
                    {
                        if (magicNumber == 0)
                        {
                            magicNumber = sumColumn;
                        }
                        else
                        {
                            if (magicNumber != sumColumn)
                                return false;
                        }
                    }
                }
            }

            for (int i = 0; i < mass.GetLength(0); i++)
            {
                int sumLine = 0;
                for (int k = 0; k < mass.GetLength(1); k++)
                {
                    sumLine += mass[k, i];
                    if (k == mass.GetLength(1) - 1)
                    {
                        if (magicNumber == 0)
                        {
                            magicNumber = sumLine;
                        }
                        else
                        {
                            if (magicNumber != sumLine)
                                return false;
                        }
                    }
                }
            }

            int sumDiagonal = 0;
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                sumDiagonal += mass[i, i];
                if (i == mass.GetLength(1) - 1)
                {
                    if (magicNumber == 0)
                    {
                        magicNumber = sumDiagonal;
                    }
                    else
                    {
                        if (magicNumber != sumDiagonal)
                            return false;
                    }
                }
            }

            //1 2 3
            //4 5 6
            //7 8 9
            sumDiagonal = 0;
            int count = 0;
            for (int i = mass.GetLength(0) -1; i >= 0; i--)
            {

                sumDiagonal += mass[i, count];
                if (i == 0)
                {
                    if (magicNumber == 0)
                    {
                        magicNumber = sumDiagonal;
                    }
                    else
                    {
                        if (magicNumber != sumDiagonal)
                            return false;
                    }
                }
                count++;
            }

            return true;
        }
    }
}