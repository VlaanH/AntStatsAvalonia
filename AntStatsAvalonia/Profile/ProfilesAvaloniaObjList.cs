using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace AntStats.Avalonia.Profile
{
    
    public class ProfileAvalonObj
    {
        public Button minus = new Button();
            
        public Button prof = new Button();
        
        public DockPanel dockPanel = new DockPanel();
    }
    
    public static class ProfilesAvaloniaObjList
    {
        public static List<ProfileAvalonObj> ListProfilesuAvalonObjStats = new List<ProfileAvalonObj>();
        
        public static List<ProfileAvalonObj> ListProfilesuAvalonObjSettings = new List<ProfileAvalonObj>();
    }
    
    
    
    
    public static class ProfileManagement
    {

        public static string GlobalSelectedProfile = default;
        
        public static void SelectProfile(List<ProfileAvalonObj>profileList,string profileName )
        {
           
            for (int i = 0; i < profileList.Count; i++)
                if ((string) profileList[i].prof.Content==profileName)
                {
                    for (int j = 0; j < profileList.Count; j++)
                    {
                        profileList[j].prof.IsEnabled = true;
                    }

                    profileList[i].prof.IsEnabled = false; 
                }

            GlobalSelectedProfile = profileName;

        }

    }

}