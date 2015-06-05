using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//this is a comment
namespace ConsoleApplication1
{
    class Program
    {
        static List<string> siteRanks = new List<string>();
        static List<string> urls = new List<string>() { "facebook.com", "bing.com", "google.com" };
        static int GetAlexaRank(string domain)
        {
            var alexaRank = 0;
            try
            {
                var url = string.Format("http://data.alexa.com/data?cli=10&dat=snbamz&url={0}", domain);

                var doc = XDocument.Load(url);

                var rank = doc.Descendants("POPULARITY")
                .Select(node => node.Attribute("TEXT").Value)
                .FirstOrDefault();

                if (!int.TryParse(rank, out alexaRank))
                    alexaRank = -1;

            }
            catch (Exception e)
            {
                return -1;
            }

            return alexaRank;

        }

        static void Main(string[] args)
        {
            foreach (var url in urls)
            {
                int rank = GetAlexaRank(url);
                var siteRank = url + "," + rank.ToString();
                siteRanks.Add(siteRank);
            }
            //foreach (var siteRank in siteRanks)
            //{
            //Console.WriteLine("{0}, {1}", siteRank.Key, siteRank.Value);
            //File.WriteAllLines("C:\\Users\rachelni\\Documents\\alexaranks.csv", siteRank.Key, siteRank.Value);
            //}
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = "alexaranks.csv";

            var path = Path.Combine(folder, filename);

            //if (File.Exists(path))
            //{
            //}

            File.WriteAllLines(path, siteRanks);
        }
    }
}
