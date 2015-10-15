using System.IO;
using System;

namespace BingRanks
{
		public class Bing
		{
		public static int GetBingRank(string path, string domain)
		{
            

			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					while (sr.Peek() >= 0)
					{
						var bingrankline = (sr.ReadLine());

						char[] delimiters = new char[] { '\t' };
						string[] parts = bingrankline.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

						if (parts[0].Contains(domain))
						{
							return int.Parse(parts[1]);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return -1;
		}
	}

}
