using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AntStatsCore
{
    public class SettingsData
    {
        public string Pass { get; set; }
           
        public string User { get; set; }
            
        public string IP { get; set; }
            
        
        
        
        public string DatabasePass { get; set; }
           
        public string DatabaseUser { get; set; }
            
        public string DatabaseIP { get; set; }
        
        public string NameTable { get; set; }
        public string DataBaseName { get; set; }
        public string Port { get; set; }



        public string AutoUpdateValue { get; set; }



        public bool AutoUpdate { get; set; }
        public bool DataBase { get; set; }
        public bool Server { get;set; }
    }
    
    
    public static class Settings
    {

        public static async Task<SettingsData> Get()
        {
            SettingsData settings = new SettingsData();
                
                try
                {
                    using (FileStream fs = new FileStream("settings.json", FileMode.OpenOrCreate))
                    {
                        settings = await JsonSerializer.DeserializeAsync<SettingsData>(fs);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

            return settings;
          
        }



        public static async void Save(SettingsData settingsClass)
        {
           
            using (FileStream fs = new FileStream("settings.json", FileMode.OpenOrCreate))
            { 
                //cleaning
                fs.SetLength(default);
                
                await JsonSerializer.SerializeAsync<SettingsData>(fs, settingsClass);
            }
        }
    }
}