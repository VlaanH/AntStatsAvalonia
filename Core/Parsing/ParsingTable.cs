using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace AntStatsCore.Parsing
{
    public class ParsingTable
    {
        public static List<string> Pars(string html,string nameTable,string parsingTablePattern)
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
    }
}