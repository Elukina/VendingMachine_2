using System.Collections.Generic;
namespace VendingMachine_2
{
    class CoinsSet : IComparable
    {
        public int aCount;
        public int bCount;
        public int residual;

        public CoinsSet(int a, int b, int r)
        {
            aCount = a;
            bCount = b;
            residual = r;
        }

        public int CompareTo(object? obj)
        {
            if (obj is CoinsSet cs)
                return residual.CompareTo(cs.residual);
            else
                throw new Exception();
        }


    }
    class CoinSolver
    {
        private List<CoinsSet> setlist;

        public CoinSolver()
        {
            setlist = new List<CoinsSet>();
        }


        public bool Combinate(int aNom, int bNom, int sum)
        {

            int maxCoin = Math.Max(aNom, bNom);
            int minCoin = Math.Min(aNom, bNom);

            int maxCoinCount = sum / maxCoin;

            while (maxCoinCount >= 0)
            {
                int residual = sum - maxCoinCount * maxCoin;
                int minCoinCount = residual / minCoin;
                residual -= minCoin * minCoinCount;
                if (maxCoin == aNom)
                    setlist.Add(new CoinsSet(maxCoinCount, minCoinCount, residual));
                else
                    setlist.Add(new CoinsSet(minCoinCount, maxCoinCount, residual));
                maxCoinCount--;
            }
            return true;
        }

        public void print(int percent)
        {
            setlist.Sort();
            int count = setlist.Count(s => s.residual == setlist[0].residual);
            double bestfit = 100;
            int bestfitidx = 0;
            for (int i = 0; i < count; i++)
            {
                double actpercent = (double)setlist[i].aCount / (setlist[i].aCount + setlist[i].bCount) * 100;
                double err = Math.Abs(percent - actpercent);
                if (err < bestfit)
                {
                    bestfit = err;
                    bestfitidx = i;
                }
            }
            Console.WriteLine($"Ac: {setlist[bestfitidx].aCount}, Bc: {setlist[bestfitidx].bCount}, R: {setlist[bestfitidx].residual}");
        }

    }
}



