using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AntStats.Avalonia.Database
{
    public class MySQL : IDatabase
    {

       private static void updateData(string connector,string ColumnName,int ColumnId,string Data,string table)
        {
            new Thread(() =>
            {
                
                
                //Sometimes errors occur when trying to update data. This code is needed to minimize this.
                bool error = false;
                for (int i =0;error||i<10;i++)
                {
                    try
                    {
                        MySqlConnection mySqlConnection = new MySqlConnection(connector);
                        mySqlConnection.Open();
                        string Commondq = $"UPDATE {table} SET {ColumnName}='{Data}' WHERE id = '{ColumnId}'";
                        MySqlCommand command = new MySqlCommand(Commondq,mySqlConnection);
                        command.ExecuteNonQuery();
                        mySqlConnection.Close();
                        error = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error MySQL");
                        error = true;
                    }
                    
                   
                }
                
               
 
            }).Start();
          
        }

        public AsicStandartStatsObject GetAsicColumnData(string connector,string table)
        {
            AsicStandartStatsObject asicsObject=new AsicStandartStatsObject();
            
            MySqlConnection mySqlConnection = new MySqlConnection(connector);
            
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM asic."+table,mySqlConnection);

            MySqlDataReader reader = command.ExecuteReader();

            for (int i=0;reader.Read();i++)
            {   
                AsicColumnClass asicColumnClass = new AsicColumnClass();  
               
                       
                    
                    asicColumnClass.Chain=reader[0].ToString();
                    asicColumnClass.Frequency=reader[1].ToString();
                    asicColumnClass.Watts=reader[2].ToString();
                    asicColumnClass.GHideal=reader[3].ToString();
                    asicColumnClass.GHRT=reader[4].ToString();
                    asicColumnClass.HW= reader[5].ToString();
                    asicColumnClass.TempPCB=reader[6].ToString();
                    asicColumnClass.TempChip=reader[7].ToString();
                    asicColumnClass.Status=reader[8].ToString();

                    if (i == 9)
                    {
                        asicsObject.HashrateAVG = reader[4].ToString();
                        asicsObject.DateTime=reader[0].ToString();
                        asicsObject.ElapsedTime=reader[8].ToString();
                    }

                    
                


                asicsObject.LasicAsicColumnStats.Add(asicColumnClass);
            }
            
            
            mySqlConnection.Close();

            
            
            return asicsObject;
        }

        public void SetAsicColumnData(string connectionString, AsicStandartStatsObject column,string table)
        {
            for (int i = 0; i <= 8; i++)
            {
             
                updateData(connectionString, "Chain", i+1,column.LasicAsicColumnStats[i].Chain,table);
                updateData(connectionString, "Frequency", i+1,column.LasicAsicColumnStats[i].Frequency,table);
                updateData(connectionString, "Watts", i+1,column.LasicAsicColumnStats[i].Watts,table);
                updateData(connectionString, "GHideal", i+1,column.LasicAsicColumnStats[i].GHideal,table);
                updateData(connectionString, "GHRT", i+1,column.LasicAsicColumnStats[i].GHRT,table);
                updateData(connectionString, "HW", i+1,column.LasicAsicColumnStats[i].HW,table);
                updateData(connectionString, "TempPCB", i+1,column.LasicAsicColumnStats[i].TempPCB,table);
                updateData(connectionString, "TempChip", i+1,column.LasicAsicColumnStats[i].TempChip,table);
                updateData(connectionString, "Status", i+1,column.LasicAsicColumnStats[i].Status,table);
              
            } 
            
        

            //updateData(connectionString, "Watts", 9,column.);
            updateData(connectionString, "Status", 9+1,column.ElapsedTime,table);
            updateData(connectionString, "GHRT", 9+1,column.HashrateAVG,table);
            updateData(connectionString, "Chain", 9+1,column.DateTime,table);
            
            
            
        }
    }
}