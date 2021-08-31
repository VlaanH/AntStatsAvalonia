using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using AntStats.Avalonia.Profile;
using AntStatsCore.Database;
using AntStatsCore;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;


namespace AntStats.Avalonia
{
    public class SettingsW : Window,INotifyPropertyChanged
    {
        public SettingsW()
        {
            
            InitializeComponent();
            
         
        }

        private  void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            LoadingSettings();
            
        }
        public string SelectedProfile = default;
        
        async void LoadingSettings()
        {
            ProfilesAvaloniaObjList.ListProfilesuAvalonObjSettings = new List<ProfileAvalonObj>();
            try
            {
                AddingExistingSettingsProfiles();
                var Profiles= await Settings.GetProfiles();
                if (Profiles.Count>0)
                {
                    var settings = await Settings.Get(ProfileManagement.GlobalSelectedProfile,default);
                    
                    ProfileManagement.SelectProfile(ProfilesAvaloniaObjList.ListProfilesuAvalonObjSettings,ProfileManagement.GlobalSelectedProfile);
                    
                    SelectedProfile = ProfileManagement.GlobalSelectedProfile;
                    
                    SetSetting(settings); 
                }
            }
            catch (Exception e)
            {
                
            }
            
            
            
        }

        
        
        
        
         void AddProfile(string profileName,bool isEnabled)
        {
            
            ProfileAvalonObj profilesuAvalonObj = new ProfileAvalonObj();
            profilesuAvalonObj.prof.Content = profileName;
            profilesuAvalonObj.prof.IsEnabled = isEnabled;
            profilesuAvalonObj.prof.Classes = Classes.Parse("Profile_buttons");
            
            
            profilesuAvalonObj.minus.Content = "-";
            profilesuAvalonObj.minus.Classes = Classes.Parse("Profile_buttons");
           
            
           
            
            DockPanel dockPanel = new DockPanel();
            dockPanel.Children.Add(profilesuAvalonObj.prof);
            dockPanel.Children.Add(profilesuAvalonObj.minus);

            profilesuAvalonObj.dockPanel = dockPanel;
            
            ProfilesAvaloniaObjList.ListProfilesuAvalonObjSettings.Add(profilesuAvalonObj);
        
            this.FindControl<StackPanel>("Profiles").Children.Add(dockPanel);
            
            
            
            profilesuAvalonObj.minus.Click += (s, e) =>
            { 
               
                this.FindControl<StackPanel>("Profiles").Children.Remove(dockPanel);
                
                Settings.DeleteSettingsProfile(profileName,default);
                
            };
            
            
            profilesuAvalonObj.prof.Click += async (s, e) =>
            {
                    
                for (int i = 0; i < ProfilesAvaloniaObjList.ListProfilesuAvalonObjSettings.Count; i++)
                {
                    ProfilesAvaloniaObjList.ListProfilesuAvalonObjSettings[i].prof.IsEnabled = true;
                }

                profilesuAvalonObj.prof.IsEnabled = false;
                SelectedProfile = profileName;
                
                var settings = await Settings.Get(profileName,default);
                
                SetSetting(settings); 
                
            };
        }


         private async void AddingExistingSettingsProfiles()
         {

             var profiles = await Settings.GetProfiles();
             for (int i = 0; i < profiles.Count; i++)
             {
                 if (profiles[i].NameProfile == SelectedProfile)
                     AddProfile(profiles[i].NameProfile, false);
                 else
                     AddProfile(profiles[i].NameProfile, true);

             }
         }




         private void ButtonAddProfile_OnClick(object? sender, RoutedEventArgs e)
        {
            SettingsData settingsData = new SettingsData();
            
            SetSetting(settingsData);
           
            GetTextBox("TextBoxProfile").Text = "P"+new Random().Next(0,2000);

        }
        
        
        
        
       private ToggleSwitch GetCToggleSwitch(string name)
       {
           return this.FindControl<ToggleSwitch>(name);
       }
    

        private TextBox GetTextBox(string name)
        {
            return this.FindControl<TextBox>(name);
        }
        
        public static void Show(Window parent,string parentProfile)
        {
            var msgbox = new SettingsW();
            msgbox.ShowDialog(parent);
        }

        void SetSetting(SettingsData settings)
        {
            if (settings.NameProfile != null)
                GetTextBox("TextBoxProfile").Text = settings.NameProfile;
            else
                GetTextBox("TextBoxProfile").Text = default;

            if (settings.IP != null)
                GetTextBox("Tip").Text = settings.IP;
            else
                GetTextBox("Tip").Text = default;

            if (settings.User != null)
                GetTextBox("Tuser").Text = settings.User;
            else
                GetTextBox("Tuser").Text = default;
            
            if(settings.Pass!=null)
             GetTextBox("Tpassword").Text = settings.Pass;
            else
                GetTextBox("Tpassword").Text = default;
            
            if(settings.Port!=null)
                GetTextBox("Tport").Text = settings.Port;
            else
                GetTextBox("Tport").Text = default;
            
            if(settings.DataBaseName!=null)
                GetTextBox("TDataBase").Text = settings.DataBaseName;
            else
                GetTextBox("TDataBase").Text = default;
            
            if(settings.NameTable!=null)
                GetTextBox("TnameTable").Text = settings.NameTable;
            else
                GetTextBox("TnameTable").Text = default;

            if(settings.DatabasePass!=null)
                GetTextBox("MysqlTpassword").Text = settings.DatabasePass;
            else
                GetTextBox("MysqlTpassword").Text = default;
          
            if(settings.DatabaseUser!=null)
                GetTextBox("MysqlTuser").Text=settings.DatabaseUser;
            else
                GetTextBox("MysqlTuser").Text = default;
                    
            if(settings.DatabaseIP!=null)
                GetTextBox("MysqlTip").Text = settings.DatabaseIP  ;
            else
                GetTextBox("MysqlTip").Text = default;
            

            if (settings.AutoUpdate != null & settings.AutoUpdateValue!=null)
            {
                GetCToggleSwitch("AutoUpdate").IsChecked = settings.AutoUpdate;
                if (GetCToggleSwitch("AutoUpdate").IsChecked.Value==true)
                {
                    this.FindControl<DockPanel>("DockPanelSliderAutoUpdate").IsVisible = true;
                    this.FindControl<Slider>("AutoUpdateSlider").IsVisible = true;
                    this.FindControl<Slider>("AutoUpdateSlider").Value = double.Parse(settings.AutoUpdateValue);

                }
                else
                {

                    GetCToggleSwitch("AutoUpdate").IsChecked = false;
                    this.FindControl<DockPanel>("DockPanelSliderAutoUpdate").IsVisible = false;
                    
                }
            }
            else
            {   GetCToggleSwitch("AutoUpdate").IsChecked = false;
                this.FindControl<DockPanel>("DockPanelSliderAutoUpdate").IsVisible = false;
                
            }
                


            if (settings.DataBase != null)
            {
                GetCToggleSwitch("ToggleSwitchMySql").IsChecked = settings.DataBase;

                if (settings.DataBase == true)
                {
                    this.FindControl<Grid>("MysqlSettings0").IsVisible = true;
                    this.FindControl<Grid>("MysqlSettings1").IsVisible = true;
                }

              
            }

            if (settings.Server != null)
            {
                GetCToggleSwitch("ToggleSwitchServer").IsChecked=settings.Server;

                if (settings.Server == true)
                {
                    this.FindControl<Grid>("MysqlSettings0").IsVisible = true;
                    this.FindControl<Grid>("MysqlSettings1").IsVisible = true;
                    
                }
            }

            
            
        }

        SettingsData GetSetting()
        {
            SettingsData  settings = new SettingsData();
            
            settings.IP = GetTextBox("Tip").Text;

            settings.AutoUpdate = GetCToggleSwitch("AutoUpdate").IsChecked.Value;
            
            settings.User = GetTextBox("Tuser").Text;
            
            settings.Pass = GetTextBox("Tpassword").Text;
            
            settings.Port = GetTextBox("Tport").Text;
            
            settings.DataBaseName = GetTextBox("TDataBase").Text;
            
            settings.NameTable = GetTextBox("TnameTable").Text;

            settings.DataBase = GetCToggleSwitch("ToggleSwitchMySql").IsChecked.Value;
            
            settings.Server = GetCToggleSwitch("ToggleSwitchServer").IsChecked.Value;
            
            settings.DatabasePass = GetTextBox("MysqlTpassword").Text;
            
            settings.DatabaseUser = GetTextBox("MysqlTuser").Text;
                    
            settings.DatabaseIP = GetTextBox("MysqlTip").Text;

            settings.AutoUpdateValue = this.FindControl<Slider>("AutoUpdateSlider").Value.ToString();
            
            settings.NameProfile= GetTextBox("TextBoxProfile").Text;
            
            return settings;
        }


        private void RemoveSettingsProfiles()
        {
            for (int i = 0; i <  ProfilesAvaloniaObjList.ListProfilesuAvalonObjSettings.Count; i++)
            {
                this.FindControl<StackPanel>("Profiles").Children.Remove(ProfilesAvaloniaObjList.ListProfilesuAvalonObjSettings[i].dockPanel);
            }

            ProfilesAvaloniaObjList.ListProfilesuAvalonObjSettings = new List<ProfileAvalonObj>();

        }
        
        
        
        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            var settings = GetSetting();
            
            Settings.Save(settings);

            RemoveSettingsProfiles();
            AddingExistingSettingsProfiles();
        }

       

        private void ShowError(string errorText)
        {
            
            this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = false;

            this.FindControl<Label>("CreatingTableLabel").IsVisible = true;
            this.FindControl<Label>("CreatingTableLabel").Content = errorText;
            this.FindControl<Button>("ButtonTable").IsEnabled = true;
            
        }

        private int _progress=0;

        public  int ProgressBar
        {
            get { return this._progress; }
            set 
            {
                this._progress = value;
                NotifyPropertyChange("ProgressBar");
            }
        }

        void EnableProgressBar()
        {
            
            this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = true;
            this.FindControl<Label>("CreatingTableLabel").IsVisible = true;
        }

        void DisableProgressBar()
        {
            
            this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = false;

        }
        
        private async void ButtonTable_OnClick(object? sender, RoutedEventArgs e)
        {
            
            DataContext = this;
            DisableProgressBar();
            EnableProgressBar();
            
            var settingsClass = GetSetting();
            
            AsicStats asicStats = new AsicStats(settingsClass);

            
            try
            {
                Result res = Result.NoError;
                int progress = 0;
                bool update = true;
                new Thread(() =>
                { 
                    do
                    {   Thread.Sleep(100);
                        ProgressBar = progress;
                        
                    } while (update == true);
                }).Start();    
                
                await Task.Run(() =>
                {
                    res = asicStats.CreateDataBaseTable(ref progress);
             
                });
                
                update = false;

                
                
                if (res==Result.ErrorExist)
                {
                    ShowError("Table Exist");
                }
             
                Settings.Save(settingsClass,GetTextBox("TextBoxProfile").Text);
            }
            catch (Exception exception)
            {
                ShowError("DataBase Error");
            }

 
          
                
        }

        private void ToggleSwitchMySql_OnClick(object? sender, RoutedEventArgs e)
        {   
            GetCToggleSwitch("ToggleSwitchServer").IsChecked = false;
        }

        private void ToggleSwitchServer_OnClick(object? sender, RoutedEventArgs e)
        {
            GetCToggleSwitch("ToggleSwitchMySql").IsChecked = false;
        }

        private void AutoUpdate_OnClick(object? sender, RoutedEventArgs e)
        {
            if (GetCToggleSwitch("AutoUpdate").IsChecked.Value == true)
                this.FindControl<DockPanel>("DockPanelSliderAutoUpdate").IsVisible = true;
         
            else
                this.FindControl<DockPanel>("DockPanelSliderAutoUpdate").IsVisible = false;

            
            
        }
        
        
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        
    }
}