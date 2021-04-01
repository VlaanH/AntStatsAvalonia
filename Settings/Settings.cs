using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AntStats.Avalonia
{
    public class SettingsClass
    {
        public string Pass { get; set; }
           
        public string User { get; set; }
            
        public string IP { get; set; }
            
        
        
        
        public string MysqlPass { get; set; }
           
        public string MysqlUser { get; set; }
            
        public string MysqlIP { get; set; }
        
        public string NameTable { get; set; }
        public string DataBaseName { get; set; }
        public string Port { get; set; }

        
        
        public bool DataBase { get; set; }
        public bool Server { get;set; }
    }
    
    
    public static class Settings
    {

        public static async Task<SettingsClass> Get()
        {


                SettingsClass settings=new SettingsClass();
                try
                {
                   
                    using (FileStream fs = new FileStream("settings.json", FileMode.OpenOrCreate))
                    {
                        settings = await JsonSerializer.DeserializeAsync<SettingsClass>(fs);
                    }
                }
                catch (Exception e)
                {
                    
                }
              
                return settings;
                
      

     
        }



        public static async void Save(SettingsClass settingsClass)
        {
            File.WriteAllText("settings.json",default);
            using (FileStream fs = new FileStream("settings.json", FileMode.OpenOrCreate))
            { 
                await JsonSerializer.SerializeAsync<SettingsClass>(fs, settingsClass);
            }
        }
    }
}