using System;
using System.Collections.Generic;
using AntStatsCore.Parsing;

namespace AntStatsCore
{
    public class Html_In_AsicStandartStatsObject
    {
        static bool IsOld(string ASIC)
        {
            if (ASIC=="ASIC#")
                return false;
            else
                return true;
        }
        
        
        
        static AsicStandardStatsObject GetOldData(AsicStandardStatsObject LasicColumn, List<string> listTableStats)
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

        static AsicStandardStatsObject GetNewData(AsicStandardStatsObject LasicColumn, List<string> listTableStats)
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
        
        


       


        public static AsicStandardStatsObject _Convert (string html,ParsingObject parsingObject)
        {
            AsicStandardStatsObject LasicColumn = new AsicStandardStatsObject();

      
     

            

            List<string> listTableStats  = ParsingTable.Pars(html, parsingObject.MainNameTable,
                parsingObject.MainTableParsingPattern);

            List<string> summaryTable = ParsingTable.Pars(html, parsingObject.AdditionalNameTable,
                parsingObject.AdditionalTableParsingPattern);




            if (IsOld(listTableStats[1]))
            {
                LasicColumn = GetOldData(LasicColumn, listTableStats);
            }
            else
            {
                LasicColumn = GetNewData(LasicColumn, listTableStats);
            }
         
            
            
            
            

            LasicColumn.HashrateAVG = listTableStats[listTableStats.Count-1];
            LasicColumn.DateTime = DateTime.Now.ToString();
           LasicColumn.ElapsedTime = summaryTable[8 + 0];

            return LasicColumn;
        }



    }
}