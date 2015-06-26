using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AlexaRanks
{
    public class Alexa
    {
        /// GET ALEXA RANK
        /// ==========================
        /// <summary>
        /// This will return the global rank of the domain
        /// </summary>
        /// <param name="domain" type="string">The domain you want the rank for</param>
        /// <returns type="int"></returns>
        public static int GetAlexaRank(string domain)
        {
            var alexaRank = 0;
            try
            {
                string url = string.Format("http://data.alexa.com/data?cli=10&dat=snbamz&url={0}", domain);

                XDocument doc = XDocument.Load(url);

                var rank = doc.Descendants("POPULARITY")
                              .Select(node => node.Attribute("TEXT").Value)
                              .FirstOrDefault();

                if (!int.TryParse(rank, out alexaRank))
                {
                    alexaRank = -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return alexaRank;
        }
    }
}
