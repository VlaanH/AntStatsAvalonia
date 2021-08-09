using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AntStatsCore.Parsing;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AntStatsCore
{
    public class Html_In_AsicStandartStatsObject
    {
        static bool IsOld(string ASIC)
        {
            if (ASIC=="ASIC#")
                return false;
            else
                return true;
        }

        public static AsicStandardStatsObject WebConvert(string html,ParsingObject parsingObject)
        {
            AsicStandardStatsObject LasicColumn = new AsicStandardStatsObject();


            List<string> listTableStats  = ParsingData.ParsTable(html, parsingObject.MainNameTable,
                parsingObject.MainTableParsingPattern);

            List<string> summaryTable = ParsingData.ParsTable(html, parsingObject.AdditionalNameTable,
                parsingObject.AdditionalTableParsingPattern);


           
            if (IsOld(listTableStats[1]))
            {
                LasicColumn = AsiCparsingMethods.GetOldData(LasicColumn, listTableStats);
            }
            else
            {
                LasicColumn = AsiCparsingMethods.GetNewData(LasicColumn, listTableStats);
            }
         
            
            
            
            

            LasicColumn.HashrateAVG = listTableStats[listTableStats.Count-1];
            LasicColumn.DateTime = DateTime.Now.ToString();
            LasicColumn.ElapsedTime = summaryTable[8 + 0];
            return LasicColumn;
        }



        public static AsicStandardStatsObject ApiConvert(string html)
        {
            AsicStandardStatsObject asicStandard = new AsicStandardStatsObject();
            
            var Json=ParsingData.ParsAPI(html);

            asicStandard = AsiCparsingMethods.GetApiData(asicStandard,Json);


            return asicStandard;
        }




        public static AsicStandardStatsObject _Convert (ParsingAuthorizationWeb.WebHtmlObject html,ParsingObject parsingObject)
        {
            AsicStandardStatsObject asicStandard = new AsicStandardStatsObject();
            
            AsicStandardStatsObject webStandardStatsObject = default;
            try
            {
                webStandardStatsObject = WebConvert(html.HtmlWebSiteWebsite, parsingObject);
            }
            catch (Exception)
            {
                // ignored
            }

            

            if (webStandardStatsObject == default)
                asicStandard = ApiConvert(html.ApiHtml);
            else
                asicStandard = webStandardStatsObject;

         
            return asicStandard;
        }



    }
}