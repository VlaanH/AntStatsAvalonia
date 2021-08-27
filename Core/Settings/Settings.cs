using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AntStatsCore
{
    public class SettingsData
    {
        public string NameProfile { get; set; }
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

        public static async Task<List<SettingsData>> GetProfiles()
        {
            
            string profilesDirectory="ProfilesAntStats";
            
            List<SettingsData> settingsDatas=new List<SettingsData>();
            
            List<string> directorys= new List<string>();
            
            await Task.Run(() =>
            {
                if (Directory.Exists(profilesDirectory))
                {
                    var fdirectorys= new DirectoryInfo(profilesDirectory).GetFiles();   
                    for (int i = 0; i < fdirectorys.Length; i++)
                    {
                        var pattern = @"([\w \W ]+).json";
                        string noExtension = Regex.Match(fdirectorys[i].Name, pattern).Groups[1].Value;
                        if (noExtension!="")
                            directorys.Add(noExtension);  
                   
                    }
                }
             

               
               
            });
            
            
            for (int i = 0; i < directorys.Count; i++)
            {
               var settings = await Get(default, profilesDirectory + "/" + directorys[i]);
               
               settings.NameProfile = directorys[i];
               
               settingsDatas.Add(settings);
               
            }
           
            
            
            
            return settingsDatas;
        }

        


        public static async Task<SettingsData> Get(string profilename,string path)
        {
            
            string profilesDirectory="ProfilesAntStats";


            SettingsData settings = new SettingsData();
            
            if (File.Exists(path+profilesDirectory+"/"+profilename+".json"))
            {
                try
                {
                    using (FileStream fs = new FileStream(path+profilesDirectory+"/"+profilename+".json", FileMode.OpenOrCreate))
                    {
                        settings = await JsonSerializer.DeserializeAsync<SettingsData>(fs);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
                

            return settings;
          
        }

        public static async void DeleteSettingsProfile(string profilename,string path)
        {
            string profilesDirectory="ProfilesAntStats";
            await Task.Run(() =>
            {
                if (File.Exists(path+profilesDirectory+"/"+profilename+".json"))
                    File.Delete(path+profilesDirectory+"/"+profilename+".json");
                
            });
        }

        public static async void Save(SettingsData settingsClass,string path=default)
        {
            string profilesDirectory="ProfilesAntStats";
            if (Directory.Exists(profilesDirectory) == false)
                Directory.CreateDirectory(profilesDirectory);
            
            
            using (FileStream fs = new FileStream(path+profilesDirectory+"/"+settingsClass.NameProfile+".json", FileMode.OpenOrCreate))
            { 
                //cleaning
                fs.SetLength(default);
                
                await JsonSerializer.SerializeAsync<SettingsData>(fs, settingsClass);
            }
        }
    }
}