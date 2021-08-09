using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AntStatsCore.Parsing
{
    public static class AsiCparsingMethods
    {
        public static AsicStandardStatsObject GetApiData(AsicStandardStatsObject asicStandard, JObject jObject)
        {
            int amountHashbord = 0;
            List<AsicColumnClass> asicColumn = new List<AsicColumnClass>();
            
            if (jObject.ContainsKey("Elapsed")) 
            {
               
                var t = TimeSpan.FromSeconds(double.Parse((string) jObject["Elapsed"]));
                      
                asicStandard.ElapsedTime = string.Format("{0:D2}d{1:D2}h{2:D2}m{3:D2}s", t.Days, t.Hours, t.Minutes, t.Seconds);

            }
            if (jObject.ContainsKey("total_rate")) 
            {
                asicStandard.HashrateAVG = (string)jObject["total_rate"];
            }

            asicStandard.DateTime = DateTime.Now.ToString();

          
            
            for (int i = 0; i < 9; i++)
            {
                if (jObject.ContainsKey("temp3_" + i))
                {
                    
                    if ((string) jObject["temp3_"+i]!="0")
                    {
                        AsicColumnClass Column = new AsicColumnClass();
                        Column.TempChip= (string) jObject["temp3_"+i];
                    
                    
                        asicColumn.Add(Column);
                        amountHashbord = i;   
                    }
                   
                }

             
            }
            
            for (int i = 0; i < amountHashbord; i++)
            {
                asicColumn[i].Chain = (i+1).ToString();

            } 
            
            for (int i = 0; i < amountHashbord+1; i++)
            {
                string apiValue = "freq";
                
                if (jObject.ContainsKey(apiValue + i))
                {
                    
                    asicColumn[i-1].Frequency= (string) jObject[apiValue+i];
                    
                }
                
            } 
            
            for (int i = 0; i < amountHashbord+1; i++)
            {
                string apiValue = "chain_power";
                
                if (jObject.ContainsKey(apiValue + i))
                {
                    
                    asicColumn[i-1].Watts= (string) jObject[apiValue+i];
                    
                }
                
            } 
            
            
            for (int i = 0; i < amountHashbord+1; i++)
            {
                string apiValue = "chain_hw";
                
                if (jObject.ContainsKey(apiValue + i))
                {
                    
                    asicColumn[i-1].HW= (string) jObject[apiValue+i];
                    
                }
                
            } 
            for (int i = 0; i < amountHashbord+1; i++)
            {
                string apiValue = "temp_pcb";
                
                if (jObject.ContainsKey(apiValue + i))
                {
                    
                    asicColumn[i-1].TempPCB= (string) jObject[apiValue+i];
                    
                }
                
            } 
            
            for (int i = 0; i < amountHashbord+1; i++)
            {
                string apiValue = "chain_rate";
                
                if (jObject.ContainsKey(apiValue + i))
                {
                    
                    asicColumn[i-1].GHRT= (string) jObject[apiValue+i];
                    
                }
                
            } 
            
            for (int i = 0; i < amountHashbord+1; i++)
            {
                string apiValue = "chain_rateideal";
                
                if (jObject.ContainsKey(apiValue + i))
                {
                    
                    asicColumn[i-1].GHideal= (string) jObject[apiValue+i];
                    
                }
                
            } 
            
            for (int i = 0; i < amountHashbord+1; i++)
            {
                string apiValue = "chain_acs";
                
                if (jObject.ContainsKey(apiValue + i))
                {
                    if (" oooooooo oooooooo oo"==(string) jObject[apiValue+i] |
                        "oooooooo oooooooo oo"==(string) jObject[apiValue+i] | 
                        " oooooooooo oooooooooo oooooooooo oooooooooo oooooooooo oooooooooo"==(string) jObject[apiValue+i])
                    {
                        asicColumn[i - 1].Status = "OK(o)";
                    }
                    else
                    {
                        asicColumn[i - 1].Status = "X";
                    }
                 
                    
                }
                
            }
            
            

            asicStandard.LasicAsicColumnStats = asicColumn;
            return asicStandard;
        }




        public static AsicStandardStatsObject GetOldData(AsicStandardStatsObject LasicColumn, List<string> listTableStats)
        {
        
            for (int i = 1; i < (int)listTableStats.Count/10; i++)
            {
                  
                try
                {
                    AsicColumnClass asicColumn = new AsicColumnClass();
        
                    asicColumn.Chain = listTableStats[i*10+1];
                    asicColumn.Frequency = listTableStats[i*10 + 3];
                    asicColumn.Watts = listTableStats[i*10 + 4];
                    asicColumn.GHideal = listTableStats[i*10 + 5];
                    asicColumn.GHRT = listTableStats[i*10 + 6];
                    asicColumn.HW = listTableStats[i*10 + 7];
                    asicColumn.TempPCB = listTableStats[i*10 + 8];
                          
                    asicColumn.TempChip = listTableStats[i*10 + 9];
                            
              
                    if (listTableStats[i*10 + 10]==" oooooooo oooooooo oo"&listTableStats[i*10 + 10]=="oooooooo oooooooo oo")
                    {
                        asicColumn.Status = listTableStats[i * 10 + 10] = "OK(o)";
                    }
                    else
                    {
                        asicColumn.Status = listTableStats[i * 10 + 10] = "X";
                    }
        
                    LasicColumn.LasicAsicColumnStats.Add(asicColumn);
                            
                }
                catch (Exception)
                {
                    // ignored
                }
                        
                        
            }
        
            return LasicColumn; 
        }
        
        public  static AsicStandardStatsObject GetNewData(AsicStandardStatsObject LasicColumn, List<string> listTableStats)
        {
        
            for (int i = 1; i < (int)listTableStats.Count/10; i++)
            {
                  
                try
                {
                    AsicColumnClass asicColumn = new AsicColumnClass();
        
                    asicColumn.Chain = listTableStats[i*10+3];
                    asicColumn.Frequency = listTableStats[i*10 + 5];
                    asicColumn.Watts = listTableStats[i*10 + 6];
                    asicColumn.GHideal = listTableStats[i*10 + 7];
                    asicColumn.GHRT = listTableStats[i*10 + 8];
                    asicColumn.HW = listTableStats[i*10 + 9];
                    asicColumn.TempPCB = listTableStats[20];
                            
                    asicColumn.TempChip = listTableStats[i*10 + 11];
                            
              
                    if (listTableStats[i*10 + 12]==" oooooooo oooooooo oo" || listTableStats[i*10 + 12]=="oooooooo oooooooo oo")
                    {
                        asicColumn.Status = listTableStats[i * 10 + 12] = "OK(o)";
                    }
                    else
                    {
                        asicColumn.Status = listTableStats[i * 10 + 12] = "X";
                    }
        
                    LasicColumn.LasicAsicColumnStats.Add(asicColumn);
                            
                }
                catch (Exception)
                {
                    // ignored
                }
                        
                        
            }
                    
            return LasicColumn;
        }
    }
}