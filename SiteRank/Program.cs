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
            var siteranks = new Dictionary<string, string>();
            
            //Reading urls and saving to a dictionary
            if(File.Exists(inputpath))
            { 
                String [] urls = System.IO.File.ReadAllLines(inputpath);
                foreach (var url in urls)
                {
                    int rank = GetAlexaRank(url);
                    siteranks.Add(url, rank.ToString());
                   // Console.WriteLine(url + "," + rank);
                }
            }

            //Writing dictionary to file
            if (!File.Exists(outputpath))
            {
                File.Create(outputpath);
            }

           File.WriteAllLines(outputpath, siteranks.Select(x => x.Key + "," + x.Value).ToArray());

        }
    }
}
