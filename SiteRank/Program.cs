using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace ConsoleApplication1
{
    class Program
    {
        //snippet taken from Jac Timms http://www.ichi.co.uk/post/12744611627/getting-an-alexa-rank-programmatically-in-csharp
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
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var outputfilename = "alexaranks.csv";
            var inputfilename = "urls.txt";
            var inputpath = Path.Combine(folder, inputfilename);
            var outputpath = Path.Combine(folder, outputfilename);
            var siteranks = new Dictionary<string, int>();

            //Reading urls and saving to a dictionary
            if (File.Exists(inputpath))
            {
                String[] urls = System.IO.File.ReadAllLines(inputpath);
                foreach (var url in urls)
                {
                    int rank = GetAlexaRank(url);
                    siteranks.Add(url, rank);
                    //Console.WriteLine(url + "," + rank);
                }

                Console.WriteLine("SUCCESS! Check your Documents folder for the output file");

            }
            else
            {
                Console.WriteLine("ERROR! Please provide a list of urls in a file named 'urls.txt' in your Documents folder");
            }
            //Writing dictionary to alexaranks.csv
                using (var file = new StreamWriter(outputpath, true))
            {
                foreach (var siterank in siteranks)
                {
                    file.WriteLine("{0},{1}", siterank.Key, siterank.Value);
                }
            }
            //Not sure what this line does but it works if used in lieu of the function about so leaving it commented in for future reference
            //File.WriteAllLines(outputpath, siteranks.Select(x => x.Key + "," + x.Value).ToArray());
        }
    }
}
