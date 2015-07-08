using System;
using System.Collections.Generic;
using System.IO;
using AlexaRanks;
using BingRanks;

namespace SiteRank
{
    public class Program
    {
        bool debug = true;

        public static void Main(string[] args)
        {
            Program prg = new Program();
            Alexa alexa = new Alexa();
            Bing bing = new Bing();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string inputfilename = "urls.txt";
            string outputfilename = "siteranks.csv";
            string inputpath = Path.Combine(folder, inputfilename);
            string outputpath = Path.Combine(folder, outputfilename);
            Dictionary<string, List<int>> siteranks = new Dictionary<string, List<int>>();


            //Reading urls and saving to a dictionary
            if (File.Exists(inputpath))
            {
                string[] urls = File.ReadAllLines(inputpath); //create array and put urls from urls.txt inside it
                foreach (var url in urls)
                {
                    int alexarank = Alexa.GetAlexaRank(url);
                    int bingrank = Bing.GetBingRank(url);
                    List<int> ranklist = new List<int>();
                    ranklist.Add(alexarank);
                    ranklist.Add(bingrank);

                    siteranks.Add(url, ranklist); //add the url and the ranks list to the siteranks dictionary

                    if (prg.debug)
                    {
                        Console.WriteLine(url + "," + ranklist[0] + "," + ranklist[1]);
                    }

                }
                Console.WriteLine("SUCCESS! Check your Documents folder for the output file");
            }
            else
            {
                Console.WriteLine("ERROR! Please provide a list of urls in a file named 'urls.txt' in your Documents folder");
            }

            //Writing dictionary to alexaranks.csv
            using (var file = new StreamWriter(outputpath)) //opens or creates new csv file to write url and corresponding site rank to. add true here if you want to append instead of overwrite
            {
                foreach (var siterank in siteranks) //for each url and corresponding site rank
                {
                    file.WriteLine("{0},{1},{2}", siterank.Key, siterank.Value[0], siterank.Value[1]); //write each url and corresponding site ranks to the file created or opened
                }
            }
            //Not sure what this line does but it works if used in lieu of the function above so leaving it commented in for future reference
            //File.WriteAllLines(outputpath, siteranks.Select(x => x.Key + "," + x.Value).ToArray());
        }
    }
}
