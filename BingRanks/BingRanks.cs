using System.IO;
using System;
using System.Collections.Generic;

namespace BingRanks
{
    public class Bing
    {
        private Dictionary<string, int> rankInfo;

        public Bing()
        {
            rankInfo = new Dictionary<string, int>();
        }

        public bool LoadRankData(string path)
        {
            char[] delimiters = new char[] { '\t' };
            string line;
            string[] parts;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        line = sr.ReadLine();
                        parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            rankInfo[parts[0]] = Convert.ToInt32(parts[1]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public int GetBingRank(string domain)
        {
            int rank;
            rankInfo.TryGetValue(domain, out rank);
            if (rank == 0)
            {
                rank = Int32.MaxValue;
            }
            return rank;
        }

    }
}
