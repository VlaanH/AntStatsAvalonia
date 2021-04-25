using System;
using AntStats.Avalonia.Database;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;

namespace AntStats.Avalonia
{
    public class Stats : Window
    {
   
        public Stats()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        
       
        
        private Label GetLabel(string name)
        {
            return this.FindControl<Label>(name);
        }
        

        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            AddBasicElements();
            ColumnTarget.Add();
        }
        
        
        List<Label> ColumnAdd(string columnName,int columnId)
        {
            List<Label> column = new List<Label>();
            for (int i = 1; i < 10; i++)
            {  
                
                var label = CustomItem.AddLabel(columnName+i);
                this.FindControl<Grid>("MainW").Children.Add(label);
                Grid.SetColumn(label, columnId);
                Grid.SetRow(label, i);
                
                column.Add(label);
            }

            return column;
        }
        void AddBasicElements()
        { 
            ColumnList.Status = ColumnAdd("Status", 8);  
            ColumnList.TempChip = ColumnAdd("TempChip", 7);  
            ColumnList.TempPCB = ColumnAdd("TempPCB", 6);  
            ColumnList.HW = ColumnAdd("HW", 5);   
            ColumnList.GHRT = ColumnAdd("GHRT", 4);   
            ColumnList.GHideal = ColumnAdd("GHideal", 3);
            ColumnList.Watts = ColumnAdd("Watts", 2);
            ColumnList.Frequency = ColumnAdd("Frequency", 1);
            ColumnList.Chain = ColumnAdd("Chain", 0);
   
        }


        private async void SetAsicColumnTable(AsicStandartStatsObject asicColumn)
        {
            int maxI = 9;
            for (int i = 0; i < maxI; i++)
            {
                await Task.Delay(10);
                
                ColumnList.Status[i].Content=asicColumn.LasicAsicColumnStats[i].Status;
                ColumnList.TempChip[i].Content=asicColumn.LasicAsicColumnStats[i].TempChip;
                ColumnList.TempPCB[i].Content=asicColumn.LasicAsicColumnStats[i].TempPCB;
                ColumnList.HW[i].Content=asicColumn.LasicAsicColumnStats[i].HW;
                ColumnList.GHRT[i].Content=asicColumn.LasicAsicColumnStats[i].GHRT;
                ColumnList.GHideal[i].Content= asicColumn.LasicAsicColumnStats[i].GHideal;
                ColumnList.Watts[i].Content=asicColumn.LasicAsicColumnStats[i].Watts;
                ColumnList.Frequency[i].Content=asicColumn.LasicAsicColumnStats[i].Frequency;
                ColumnList.Chain[i].Content=asicColumn.LasicAsicColumnStats[i].Chain;
            }

            GetLabel("ElapsedTime").Content = asicColumn.ElapsedTime;
            GetLabel("DateTime").Content = asicColumn.DateTime;
            GetLabel("HashrateAvg").Content = asicColumn.HashrateAVG;
    

        }



        private async void EnabledProgressBar(int maxValue)
        {

            this.FindControl<Button>("ButtonStats").IsEnabled = false;
            
            
            this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = true;
            this.FindControl<Label>("DatabaseProgressBarText").IsVisible = true;
            this.FindControl<Label>("DatabaseProgressBarText").Content = "Database update";


            while (ProgressBarCreatingData.DataBase < maxValue & ProgressBarCreatingData.DataBaseError == false)
            {
              
                this.FindControl<ProgressBar>("DatabaseProgressBar").Value =
                    (int) (((double) ProgressBarCreatingData.DataBase / maxValue) * 100);
                await Task.Delay(700);
            } 
            
            
            this.FindControl<Button>("ButtonStats").IsEnabled = true;
            
            
            
            
            this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = false;
            this.FindControl<Label>("DatabaseProgressBarText").IsVisible = false;
            this.FindControl<ProgressBar>("DatabaseProgressBar").Value = 0;

            if (ProgressBarCreatingData.DataBaseError==true)
            {
                
                ShowError("Server Error");
                _errors = true;
             
            }
           
        }

        private void ShowError(string errorText)
        {
            this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = false;

            this.FindControl<Label>("DatabaseProgressBarText").IsVisible = true;
            this.FindControl<Label>("DatabaseProgressBarText").Content = errorText;
            this.FindControl<Button>("ButtonStats").IsEnabled = true;
            
        }




        private bool _errors = false;
        async void Button_OnClick(object? sender, RoutedEventArgs e)
        {  
            _errors = false;
            ShowError(default);
            ProgressBarCreatingData.DataBase = 0;ProgressBarCreatingData.DataBaseError = false;this.FindControl<ProgressBar>("DatabaseProgressBar").Value = 0;
            AsicStandartStatsObject statsObject = new AsicStandartStatsObject();
            SettingsClass settings = new SettingsClass();
           
            
            
               
            
            
               await Task.Run(() =>
               {  settings = Settings.Get().Result; });

                  
               if(settings.Server==true)
                    EnabledProgressBar(83);

               

               
           
                   
                   
               AsicStats asicStats = new AsicStats(settings);

               if (settings.DataBase==true)
               {
                   try
                   {
                       await Task.Run(() => 
                           { statsObject = asicStats.GetDataBase(); });
                   }
                   catch (Exception exception)
                   {
                       ShowError("DataBase Error");
                       _errors = true;
                   }
                   
               }
               else
               {
                   try
                   {
                       await Task.Run(() =>
                           { statsObject = asicStats.GetLocalhost(); });
                   }
                   catch (Exception exception)
                   {
                       ShowError("Localhost Error");
                       _errors = true;
                   }
                      
                      
                       
                   if (settings.Server == true & _errors == false)
                   {
                       await Task.Run(() => 
                           { asicStats.SetDataBase(statsObject); });
                        
                   }
                       
               }

                 


               if(_errors==false)
                   SetAsicColumnTable(statsObject);
               
        }

      
        private void Settings_OnClick(object? sender, RoutedEventArgs e)
        {
            SettingsW.Show(this);
        }
    }
}