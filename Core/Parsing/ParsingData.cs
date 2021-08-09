using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace AntStatsCore.Parsing
{
    public static class ParsingData
    {
        public static List<string> ParsTable(string html,string nameTable,string parsingTablePattern)
        {

            string pattern = parsingTablePattern;
           
            string value = Regex.Match(html, pattern).Groups[1].Value;

            List<string> list = default;
            HtmlDocument htmlDocument2 = new HtmlDocument();
            htmlDocument2.LoadHtml(value);
            HtmlNode htmlNode = htmlDocument2.DocumentNode.SelectSingleNode(nameTable);
            if (htmlNode != null)
            {
                list = Enumerable.ToList<string>(Enumerable.Where<string>((IEnumerable<string>) htmlNode.InnerText.Split(new char[1]
                {
                    '\n'
                }, StringSplitOptions.RemoveEmptyEntries), (Func<string, bool>) (str => str != "---")));

               
            }
            return list;
        }

        public static JObject ParsAPI(string html)
        {
            
            var pattern = @"}],""([\w \W ]+)}";
            string json = @"{"""+Regex.Match(html, pattern).Groups[1].Value+"}";
          
            JObject obj = JObject.Parse(json);
            AsicStandardStatsObject statsObject = new AsicStandardStatsObject();
            
            if (obj.ContainsKey("STATS")) {
                JArray values = (JArray)obj["STATS"];
             
                // Do we have any values in our array?
                if (values.Count > 0) {
                    JObject firstItem = (JObject)values[1];

                    return firstItem;
                    
                }
            }


            return default;
        }


    }
    
    
    
    
}