using System.Threading;
using System.Threading.Tasks;
using AntStats.Avalonia.Database;

namespace AntStats.Avalonia
{
    public class GetAsicStats
    {
        private SettingsClass Settings { get; set; }

        public GetAsicStats(SettingsClass settings)
        {
            Settings = settings;
        }


        public async void CreateMySqlTable()
        {
          
        
            string connector=$"Server={Settings.MysqlIP};port={Settings.Port};Database={Settings.DataBaseName};Uid={Settings.MysqlUser};pwd={Settings.MysqlPass};charset=utf8";

            
            await Task.Run(() =>
            {
                Database.MySQL mySql = new MySQL();
                
                mySql.CreateTable(connector,Settings.NameTable,Settings.DataBaseName);

            });

      
            
        }
        
        
        
        
        
        public AsicStandartStatsObject GetLocalhost()
        {
            var html = ParsingAuthorizationWeb.DownloadString($"http://{Settings.IP}/cgi-bin/minerStatus.cgi", Settings.User,
                Settings.Pass);
            
                
             return Html_In_AsicStandartStatsObject._Convert(html);
            
        }
        public AsicStandartStatsObject GetMySql()
        {
          
            MySQL mySql = new MySQL();

            string connector=$"Server={Settings.MysqlIP};port={Settings.Port};Database={Settings.DataBaseName};Uid={Settings.MysqlUser};pwd={Settings.MysqlPass};charset=utf8";
            
                
            return mySql.GetAsicColumnData(connector,Settings.NameTable);
            
        }

      
        
        public void SetMySql(AsicStandartStatsObject statsObject)
        {
          
            MySQL mySql = new MySQL();

            string connector=$"Server={Settings.MysqlIP};port={Settings.Port};Database={Settings.DataBaseName};Uid={Settings.MysqlUser};pwd={Settings.MysqlPass};charset=utf8";
            
            
            new Thread(() =>
            {
               mySql.SetAsicColumnData(connector,statsObject,Settings.NameTable);

            }).Start();

      
            
        }
        
        
        
    }
}