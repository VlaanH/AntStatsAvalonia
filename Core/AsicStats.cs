using System;
using AntStatsCore.Database;
using AntStatsCore.Parsing;

namespace AntStatsCore
{
    public class AsicStats
    {
        private SettingsData Settings { get; set; }
        ParsingObject ParsingObject{ get; set; }
  
        
        public AsicStats(SettingsData settings,ParsingObject parsingObject = default)
        {
            if (parsingObject==default)
                ParsingObject = StandardData.StandardParsingObject;
            else
                ParsingObject = parsingObject;
            
            
            
            Settings = settings;
        }

        
        public Result CreateDataBaseTable(ref int percentageProgress)
        {
            string connector=$"Server={Settings.DatabaseIP};port={Settings.Port};Database={Settings.DataBaseName};Uid={Settings.DatabaseUser};pwd={Settings.DatabasePass};charset=utf8";
            
            MySQL mySql = new MySQL();
                
            return mySql.CreateTable(connector,Settings.NameTable,Settings.DataBaseName,ref percentageProgress);
            
        }


        
        public AsicStandardStatsObject GetLocalhost()
        {
            var webHtmlObject = ParsingAuthorizationWeb.DownloadHtmlWebsiteOrApi(Settings.IP, Settings.User,
                Settings.Pass);

                
             return Html_In_AsicStandartStatsObject._Convert(webHtmlObject,ParsingObject);
            
        }
        
        public AsicStandardStatsObject GetDataBase()
        {
          
            MySQL mySql = new MySQL();

            string connector=$"Server={Settings.DatabaseIP};port={Settings.Port};Database={Settings.DataBaseName};Uid={Settings.DatabaseUser};pwd={Settings.DatabasePass};charset=utf8";
            
                
            return mySql.GetAsicColumnData(connector,Settings.NameTable,Settings.DataBaseName);
            
        }

      
        
        public Result SetDataBase(AsicStandardStatsObject statsObject,ref int percentageProgress)
        {
          
            MySQL mySql = new MySQL();

            string connector=$"Server={Settings.DatabaseIP};port={Settings.Port};Database={Settings.DataBaseName};Uid={Settings.DatabaseUser};pwd={Settings.DatabasePass};charset=utf8";
            
            
          
           return mySql.SetAsicColumnData(connector,statsObject,Settings.NameTable,ref percentageProgress);
          
        }
    }
}
