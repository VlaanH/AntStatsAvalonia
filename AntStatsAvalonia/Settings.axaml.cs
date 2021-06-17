using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
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
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private  void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            LoaudingSettings();
        }

        async void LoaudingSettings ()
        {
            
            var settings =  await Settings.Get();
      
            SetSetting(settings); 
         
            
        }

       private ToggleSwitch GetCToggleSwitch(string name)
       {
           return this.FindControl<ToggleSwitch>(name);
       }
    

        private TextBox GetTextBox(string name)
        {
            return this.FindControl<TextBox>(name);
        }
        
        public static void Show(Window parent)
        {
            var msgbox = new SettingsW();
            msgbox.ShowDialog(parent);
        }

        void SetSetting(SettingsData settings)
        {
            if(settings.IP!=null)
                GetTextBox("Tip").Text = settings.IP;
            if(settings.User!=null)
                GetTextBox("Tuser").Text = settings.User;
            
            if(settings.Pass!=null)
             GetTextBox("Tpassword").Text = settings.Pass;
            
            if(settings.Port!=null)
                GetTextBox("Tport").Text = settings.Port;
            
            if(settings.DataBaseName!=null)
                GetTextBox("TDataBase").Text = settings.DataBaseName;
            
            if(settings.NameTable!=null)
                GetTextBox("TnameTable").Text = settings.NameTable;

            if(settings.DatabasePass!=null)
                GetTextBox("MysqlTpassword").Text = settings.DatabasePass;
            
          
            if(settings.DatabaseUser!=null)
                GetTextBox("MysqlTuser").Text=settings.DatabaseUser;
              
            if(settings.DatabaseIP!=null)
                GetTextBox("MysqlTip").Text = settings.DatabaseIP  ;


            if (settings.AutoUpdate != null & settings.AutoUpdateValue!=null)
            {
                GetCToggleSwitch("AutoUpdate").IsChecked = settings.AutoUpdate;
                if (GetCToggleSwitch("AutoUpdate").IsChecked.Value==true)
                {
                    this.FindControl<DockPanel>("DockPanelSliderAutoUpdate").IsVisible = true;
                    this.FindControl<Slider>("AutoUpdateSlider").Value = double.Parse(settings.AutoUpdateValue);

                }
               
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

        SettingsData  GetSetting()
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
            
            
            return settings;
        }


        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            var settings = GetSetting();

         
            Settings.Save(settings);
           
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
                
            }
            catch (Exception exception)
            {
                ShowError("DataBase Error");
            }

           
            
            Settings.Save(settingsClass);
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