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

            loaudingSettings();
        }

       async void loaudingSettings ()
        {
            
                var settings =  await Settings.Get();
      
                SetSetting(settings); 
         
            
        }

       private CheckBox GetCheckBox(string name)
       {
           return this.FindControl<CheckBox>(name);
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
                GetCheckBox("CheckBoxMySql").IsChecked = settings.DataBase;

                if (settings.DataBase == true)
                {
                    this.FindControl<Grid>("MysqlSettings0").IsVisible = true;
                    this.FindControl<Grid>("MysqlSettings1").IsVisible = true;
                }

              
            }

            if (settings.Server != null)
            {
                GetCheckBox("CheckBoxServer").IsChecked=settings.Server;

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

            settings.DataBase = GetCheckBox("CheckBoxMySql").IsChecked.Value;
            
            settings.Server = GetCheckBox("CheckBoxServer").IsChecked.Value;
            
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

        private async void EnabledProgressBar()
        {
            
            this.FindControl<Button>("ButtonTable").IsEnabled = false;
            
            
            this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = true;
            this.FindControl<Label>("CreatingTableLabel").Content = "Creating Table";
            this.FindControl<Label>("CreatingTableLabel").IsVisible = true;

          
            
            while (ProgressBarCreatingData.CreatingTable<17)
            {
                await Task.Delay(700);
               this.FindControl<ProgressBar>("BarTable").Value=(int)(((double)ProgressBarCreatingData.CreatingTable/17)*100);
              
            }

            this.FindControl<Button>("ButtonTable").IsEnabled = true;
            
            
            
            
            this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = false;
            this.FindControl<Label>("CreatingTableLabel").IsVisible = false;
            ProgressBarCreatingData.CreatingTable = 0;

            
            
        }



        private void CheckBoxMySql_OnClick(object? sender, RoutedEventArgs e)
        {
            
            GetCheckBox("CheckBoxServer").IsChecked = false;
            

        }

        private void CheckBoxServer_OnClick(object? sender, RoutedEventArgs e)
        {
            
            GetCheckBox("CheckBoxMySql").IsChecked = false;
        }

        private async void ButtonTable_OnClick(object? sender, RoutedEventArgs e)
        {

           
            
          

            var settingsClass = GetSetting();
            
            GetAsicStats asicStats = new GetAsicStats(settingsClass);
            
            
            EnabledProgressBar();
            
            bool createTableRes=false;
            await Task.Run(() =>
            {
                createTableRes = asicStats.CreateMySqlTable();
                
                
            });
            
   
            if (createTableRes==false)
            {
                this.FindControl<ProgressBar>("CreatingTableProgressBar").IsVisible = false;
                this.FindControl<Label>("CreatingTableLabel").Content = "The table already exists";
                
                
       
                
                this.FindControl<Button>("ButtonTable").IsEnabled = true;
            }
            
            
       


           
            
            Settings.Save(settingsClass);
        }
    }
}