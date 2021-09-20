using System;
using AntStatsCore.Database;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using AntStatsCore;
using System.Threading.Tasks;
using AntStats.Avalonia.Profile;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using HorizontalAlignment = Avalonia.Layout.HorizontalAlignment;


namespace AntStats.Avalonia
{
  
    public class Stats : Window,INotifyPropertyChanged
    {
       
        
        public Stats()
        {
            this.InitializeComponent();

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
            AddingExistingSettingsProfiles();
           

        }

        public string SelectedProfile = default;
        void profilesAdd(string name,bool IsEnabled)
        {
            
            ProfileAvalonObj profilesuAvalonObj = new ProfileAvalonObj();
            profilesuAvalonObj.profButton.Content = name;
            profilesuAvalonObj.profButton.IsEnabled = IsEnabled;
            profilesuAvalonObj.profButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            profilesuAvalonObj.profButton.HorizontalContentAlignment=HorizontalAlignment.Center;
            
            
            
            ProfilesAvaloniaObjList.ListProfilesAvalonObjStats.Add(profilesuAvalonObj);
           
            this.FindControl<StackPanel>("Profiles").Children.Add(profilesuAvalonObj.profButton);
            
            
            
            profilesuAvalonObj.profButton.Click += (s, e) =>
            {
                ProfileManagement.SelectProfile(ProfilesAvaloniaObjList.ListProfilesAvalonObjStats,name);
                
                SelectedProfile = name;
                ProfileManagement.GlobalSelectedProfile = name;
            };
        }

        private void RemoveSettingsProfiles()
        {
            for (int i = 0; i <  ProfilesAvaloniaObjList.ListProfilesAvalonObjStats.Count; i++)
            {
                this.FindControl<StackPanel>("Profiles").Children.Remove(ProfilesAvaloniaObjList.ListProfilesAvalonObjStats[i].profButton);
            }

            ProfilesAvaloniaObjList.ListProfilesAvalonObjStats = new List<ProfileAvalonObj>();

        }


        private async void AddingExistingSettingsProfiles()
        {
            var profiles = await Settings.GetProfiles();
            if (profiles!=default)
            {
                for (int i = 0; i < profiles.Count; i++)
                {
                    bool enable = profiles[i].NameProfile!=SelectedProfile;


                    profilesAdd(profiles[i].NameProfile,enable);
                
                }
                //selection of the first profile in the list if no profile is selected
                if (ProfilesAvaloniaObjList.ListProfilesAvalonObjStats.Count > 0 & SelectedProfile == default)
                {
                
                    ProfileManagement.SelectProfile(ProfilesAvaloniaObjList.ListProfilesAvalonObjStats,(string)ProfilesAvaloniaObjList.ListProfilesAvalonObjStats[0].profButton.Content);
                    SelectedProfile = (string) ProfilesAvaloniaObjList.ListProfilesAvalonObjStats[0].profButton.Content;
                }
            }
            

        }
     



        private void ButtonUpdatingTheListOfExistingProfiles_OnClick(object? sender, RoutedEventArgs e)
        {
            RemoveSettingsProfiles();
            AddingExistingSettingsProfiles();
         
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
            PlotAddPoint(50);
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


        private async void SetAsicColumnTable(AsicStandardStatsObject asicColumn)
        {
            string contentDefault = "-";
            int maxI = 9;
            for (int i = 0; i < maxI; i++)
            {
                await Task.Delay(10);

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].Status!=null)
                        ColumnList.Status[i].Content=asicColumn.LasicAsicColumnStats[i].Status;
                }
                catch (Exception e)
                {
                    ColumnList.Status[i].Content = contentDefault;
                }

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].TempChip!=null) 
                        ColumnList.TempChip[i].Content=asicColumn.LasicAsicColumnStats[i].TempChip;
                }
                catch (Exception e)
                {
                    ColumnList.TempChip[i].Content = contentDefault;
                }

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].TempPCB!=null) 
                        ColumnList.TempPCB[i].Content=asicColumn.LasicAsicColumnStats[i].TempPCB;
                }
                catch (Exception e)
                {
                    ColumnList.TempPCB[i].Content = contentDefault;
                }

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].HW!=null) 
                        ColumnList.HW[i].Content=asicColumn.LasicAsicColumnStats[i].HW;
                }
                catch (Exception e)
                {
                    ColumnList.HW[i].Content = contentDefault;
                }

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].GHRT!=null) 
                        ColumnList.GHRT[i].Content=asicColumn.LasicAsicColumnStats[i].GHRT;
                }
                catch (Exception e)
                {
                    ColumnList.GHRT[i].Content = contentDefault;
                }

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].GHideal!=null) 
                        ColumnList.GHideal[i].Content= asicColumn.LasicAsicColumnStats[i].GHideal;
                }
                catch (Exception e)
                {
                    ColumnList.GHideal[i].Content = contentDefault;
                }

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].Watts!=null) 
                        ColumnList.Watts[i].Content=asicColumn.LasicAsicColumnStats[i].Watts;
                }
                catch (Exception e)
                {
                    ColumnList.Watts[i].Content = contentDefault;
                }

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].Frequency!=null) 
                        ColumnList.Frequency[i].Content=asicColumn.LasicAsicColumnStats[i].Frequency;
                }
                catch (Exception e)
                {
                    ColumnList.Frequency[i].Content = contentDefault;
                }

                try
                {
                    if (asicColumn.LasicAsicColumnStats[i].Chain!=null) 
                        ColumnList.Chain[i].Content=asicColumn.LasicAsicColumnStats[i].Chain;
                }
                catch (Exception e)
                {
                    ColumnList.Chain[i].Content = contentDefault;
                }
               
                
            }

            GetLabel("ElapsedTime").Content = asicColumn.ElapsedTime;
            GetLabel("DateTime").Content = asicColumn.DateTime;
            GetLabel("HashrateAvg").Content = asicColumn.HashrateAVG;
    

        }



        

        private void ShowError(string errorText)
        {
            this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = false;

            this.FindControl<Label>("DatabaseProgressBarText").IsVisible = true;
            this.FindControl<Label>("DatabaseProgressBarText").Content = errorText;
            this.FindControl<Button>("ButtonStats").IsEnabled = true;
            
        }

        private void BlockButtons(string messages=default)
        {
            this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = false;

            this.FindControl<Label>("DatabaseProgressBarText").IsVisible = true;
            this.FindControl<Label>("DatabaseProgressBarText").Content = messages;
            if((string) this.FindControl<Button>("ButtonStats").Content!="Stop")
                this.FindControl<Button>("ButtonStats").IsEnabled = false;
            this.FindControl<Button>("Settings").IsEnabled = false;
            
        }

        private void UnlockButtons(bool error)
        {
            if (error==false)
                this.FindControl<Label>("DatabaseProgressBarText").Content = default;
            
            this.FindControl<Button>("ButtonStats").IsEnabled = true;
            this.FindControl<Button>("Settings").IsEnabled = true;  
        }


        private bool _errors = false;
        async void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            SettingsData settings = new SettingsData();
            await Task.Run(() =>
                {  settings = Settings.Get(SelectedProfile,default).Result; });

            if (settings.AutoUpdate == true | this.FindControl<Button>("Settings").IsEnabled==false)
            {
                if (this.FindControl<Button>("Settings").IsEnabled==true)
                {
                    this.FindControl<Button>("Settings").IsEnabled = false;
                    this.FindControl<Button>("ButtonStats").Content = "Stop";


                }
                else
                {
                    this.FindControl<Button>("Settings").IsEnabled = true;
                    this.FindControl<Button>("ButtonStats").Content = "Get or Start auto update";
                }

                while (this.FindControl<Button>("Settings").IsEnabled==false)
                {
                  
                    GetStats(true);
                    var delayTime = int.Parse(settings.AutoUpdateValue) * 60;
                    
                    for (int i = 0; i <  delayTime & this.FindControl<Button>("Settings").IsEnabled==false; i++)
                    {
                        
                        this.FindControl<Label>("AutoUpdateProgress").Content = (delayTime)-i+"s";
                        
                        await Task.Delay(1000);
                    }
                   
                }

                this.FindControl<Label>("AutoUpdateProgress").Content = default;
            }
            else
                GetStats(false);
            
        }

        
        
        List<int> dataPoint = new List<int>();

        int getMaxTemp(AsicStandardStatsObject listTemp)
        {
            int maxTemp = 0;
            try
            {
                for (int i = 0; i < listTemp.LasicAsicColumnStats.Count; i++)
                {
                    try
                    {
                        if (maxTemp<int.Parse(listTemp.LasicAsicColumnStats[i].TempChip))
                        {
                            maxTemp = int.Parse(listTemp.LasicAsicColumnStats[i].TempChip);
                        }
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }


            return maxTemp;
        }



        void PlotAddPoint(int point)
        {
            if (point!=0)
            {
                DataContext = this;
            
                dataPoint.Add(point);
            
                Model = DrawPlot(dataPoint,150); 
            }
           
        }
        private PlotModel DrawPlot(List<int> DP,int MaxDrowPoint)
        {
            var plot = new PlotModel 
            { 
                
                LegendPosition = LegendPosition.RightBottom,
                Background = OxyColors.Black,
                TextColor = OxyColors.White,
                PlotType = PlotType.Cartesian,
                
                PlotAreaBorderColor = OxyColors.White
            };
          
            
            plot.Axes.Add(new LinearAxis(){  Title = "Circle", Position = AxisPosition.Bottom});
            plot.Axes.Add(new LinearAxis(){  Title = "Temp", Position = AxisPosition.Left });
            
            
            LineSeries MaxTemp = new LineSeries
            {
                Title = "MAX TEMP", 
                MarkerType = MarkerType.Diamond,   
                Color = OxyColors.Red,
               
            };


            if (MaxDrowPoint<DP.Count)
            {
                DP.RemoveAt(0);
            }

            for (int j = 0; j < DP.Count; j++)
            {
                //the first point is added when the program starts, it is equal to 50 so it is hidden
                if (j>0)
                {
                    MaxTemp.Points.Add(new DataPoint(j,DP[j])); 
                }
                
            }
            
         
            plot.Series.Add(MaxTemp);
            return plot;
        }
        
        
        private PlotModel _Model;

        public PlotModel Model
        {
            get { return this._Model; }
            set 
            {
                this._Model = value;
                NotifyPropertyChange("Model");
            }
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
        

        async void GetStats(bool autoUpdate)
        { 
            this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = false;
            DataContext = this;
            _errors = false;
            ShowError(default);
            this.FindControl<ProgressBar>("DatabaseProgressBar").Value = 0;
            
            
           
           
            
            SettingsData settings = new SettingsData();
            AsicStandardStatsObject statsObject = new AntStatsCore.AsicStandardStatsObject();
            
            
      

            
            
               await Task.Run(() =>
               {  settings = Settings.Get(SelectedProfile,default).Result; });

      
               
               
                   
               AsicStats asicStats = new AsicStats(settings);

               if (settings.DataBase==true)
               {
                   try
                   { 
                       BlockButtons("Getting from DataBase");
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
                       
                        BlockButtons("Getting from Localhost");
                         
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
                     
                        BlockButtons("Update Database");
                       
                       this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = true;
                       Result res = Result.ErrorExist;
                       int progress = 0;
                       bool update = true;
                       new Thread(() =>
                       { 
                           do
                           {   Thread.Sleep(500);
                               ProgressBar = progress;
                           } while (update == true);
                       }).Start();    
                       
                       await Task.Run(() => { res=asicStats.SetDataBase(statsObject, ref progress); });
                       
                       if (res == Result.ErrorExist)
                       {
                           ShowError("DataBase Error"); 
                           _errors = true;
                           this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = false;
                       }
                       this.FindControl<ProgressBar>("DatabaseProgressBar").IsVisible = false;
             
                           

                       
                       update = false;
                   }

               }



               if (autoUpdate == false|(string) this.FindControl<Button>("ButtonStats").Content == "Get or Start auto update")
                   UnlockButtons(_errors);
               else if(_errors==false)
                   this.FindControl<Label>("DatabaseProgressBarText").Content = default;


               if (_errors == false)
               {
                   int maxTemp = 0;
                   await Task.Run(() =>
                   {
                       maxTemp = getMaxTemp(statsObject);
                   });
                   PlotAddPoint(maxTemp);
                   SetAsicColumnTable(statsObject);
               }

               
            
        }



        private void Settings_OnClick(object? sender, RoutedEventArgs e)
        {
            SettingsW.Show(this,SelectedProfile);
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