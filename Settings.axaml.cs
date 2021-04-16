using System;
using System.Threading.Tasks;
using AntStats.Avalonia.Database;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;


namespace AntStats.Avalonia
{
    public class SettingsW : Window
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

        void SetSetting(SettingsClass settings)
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

            if(settings.MysqlPass!=null)
                GetTextBox("MysqlTpassword").Text = settings.MysqlPass;
            
          
            if(settings.MysqlUser!=null)
                GetTextBox("MysqlTuser").Text=settings.MysqlUser;
              
            if(settings.MysqlIP!=null)
                GetTextBox("MysqlTip").Text = settings.MysqlIP  ;
            
        
            
            
            
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

        SettingsClass GetSetting()
        {
            SettingsClass settings = new SettingsClass();
            settings.IP = GetTextBox("Tip").Text;
            
            
            settings.User = GetTextBox("Tuser").Text;
            
            
            settings.Pass = GetTextBox("Tpassword").Text;
            
            
            settings.Port = GetTextBox("Tport").Text;
            
            settings.DataBaseName = GetTextBox("TDataBase").Text;
            
            settings.NameTable = GetTextBox("TnameTable").Text;

            settings.DataBase = GetCToggleSwitch("ToggleSwitchMySql").IsChecked.Value;
            
            settings.Server = GetCToggleSwitch("ToggleSwitchServer").IsChecked.Value;
            
            settings.MysqlPass = GetTextBox("MysqlTpassword").Text;
            
            settings.MysqlUser = GetTextBox("MysqlTuser").Text;
                    
            settings.MysqlIP = GetTextBox("MysqlTip").Text;
            
            
            return settings;
        }


        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            var settings = GetSetting();

         
            Settings.Save(settings);
           
        }

       
        private async void EnabledProgressBar(int maxValue)
        {
            
            this.FindControl<Button>("ButtonTable").IsEnabled = false;
            
            
            this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = true;
            this.FindControl<Label>("CreatingTableLabel").Content = "Creating Table";
            this.FindControl<Label>("CreatingTableLabel").IsVisible = true;

          
            
            while (ProgressBarCreatingData.CreatingTable<maxValue & ProgressBarCreatingData.CreatingTable>-1)
            {
                await Task.Delay(700);
                this.FindControl<ProgressBar>("CreatingTableProgressBar").Value=(int)(((double)ProgressBarCreatingData.CreatingTable/maxValue)*100);
              
            }

            this.FindControl<Button>("ButtonTable").IsEnabled = true;


            if (ProgressBarCreatingData.CreatingTable==maxValue)
            {
                this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = false;
                this.FindControl<Label>("CreatingTableLabel").IsVisible = false; 
            }
            
          
            ProgressBarCreatingData.CreatingTable = 0;

            
            
        }
        private void ShowError(string errorText)
        {
            
            this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = false;

            this.FindControl<Label>("CreatingTableLabel").IsVisible = true;
            this.FindControl<Label>("CreatingTableLabel").Content = errorText;
            this.FindControl<Button>("ButtonTable").IsEnabled = true;
            
        }



        private async void ButtonTable_OnClick(object? sender, RoutedEventArgs e)
        {

            var settingsClass = GetSetting();
            
            AsicStats asicStats = new AsicStats(settingsClass);


            EnabledProgressBar(17);

        
            bool tableExists = false;
            
            try
            {
                await Task.Run(() =>
                    { tableExists = asicStats.CreateDataBaseTable();});
            }
            catch (Exception exception)
            {
                ProgressBarCreatingData.CreatingTable = 0;
                ShowError("DataBase Error");
            }
           
            
            
            
            //if the table already exists
            if (tableExists==true)
            {
                this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = false;
                this.FindControl<Label>("CreatingTableLabel").Content = "The table already exists";

                ProgressBarCreatingData.CreatingTable = -2;
       
                
                this.FindControl<Button>("ButtonTable").IsEnabled = true;
            }
            
            
       


           
            
            Settings.Save(settingsClass);
        }

        private void ToggleSwitchMySql_OnClick(object? sender, RoutedEventArgs e)
        {   GetCToggleSwitch("ToggleSwitchServer").IsChecked = false;
            
        }

        private void ToggleSwitchServer_OnClick(object? sender, RoutedEventArgs e)
        {
            GetCToggleSwitch("ToggleSwitchMySql").IsChecked = false;
        }
    }
}