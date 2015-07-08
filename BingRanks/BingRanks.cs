using System.IO;
using System;

namespace BingRanks
{
		public class Bing
		{
		public static int GetBingRank(string domain)
		{
			string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string bingranksfile = "bingranks.txt";
			string bingrankspath = Path.Combine(folder, bingranksfile);

			try
			{
				using (StreamReader sr = new StreamReader(bingrankspath))
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
