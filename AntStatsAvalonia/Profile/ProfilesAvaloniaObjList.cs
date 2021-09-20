using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace AntStats.Avalonia.Profile
{
    
    public class ProfileAvalonObj
    {
        public Button minusButton = new Button();
            
        public Button profButton = new Button();
        
        public DockPanel dockPanel = new DockPanel();
    }
    
    public static class ProfilesAvaloniaObjList
    {
        public static List<ProfileAvalonObj> ListProfilesAvalonObjStats = new List<ProfileAvalonObj>();
        
        public static List<ProfileAvalonObj> ListProfilesAvalonObjSettings = new List<ProfileAvalonObj>();
    }
    
    
    
    
    public static class ProfileManagement
    {
        //variable to transfer the selected profile to the settings page
        public static string GlobalSelectedProfile = default;
        
        public static void SelectProfile(List<ProfileAvalonObj>profileList,string profileName )
        {
           
            for (int i = 0; i < profileList.Count; i++)
                if ((string) profileList[i].profButton.Content==profileName)
                {
                    for (int j = 0; j < profileList.Count; j++)
                    {
                        profileList[j].profButton.IsEnabled = true;
                    }

                    profileList[i].profButton.IsEnabled = false; 
                }

            GlobalSelectedProfile = profileName;

        }

    }

}