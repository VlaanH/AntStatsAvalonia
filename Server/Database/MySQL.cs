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

       private static void UpdateData(string connector,string ColumnName,int ColumnId,string Data,string nameTable,ref int progress)
        {
            
            new Thread(() =>
            {
           
                    bool error = false;
          
                    try
                    {
                        MySqlConnection mySqlConnection = new MySqlConnection(connector);
                        mySqlConnection.Open();
                        string Commondq = $"UPDATE {nameTable} SET {ColumnName}='{Data}' WHERE id = '{ColumnId}'";
                        MySqlCommand command = new MySqlCommand(Commondq,mySqlConnection);
                        command.ExecuteNonQuery();
                        mySqlConnection.Close();
                        error = false;
                    }
                    catch (Exception e)
                    {
                    
                        error = true;
                      
                            // Console.WriteLine("Error MySQL");
                            ProgressBarCreatingData.DataBaseError = true;

                        
                    }


                    if (error==false)
                    {
                        ProgressBarCreatingData.DataBase++;
                    }
                    
            }).Start();

           
        }


       private bool tablePresenceInDatabase(string nameTable,string connector,string database)
       {
           try
           {
               MySqlConnection mySqlConnection = new MySqlConnection(connector);
               mySqlConnection.Open();
               MySqlCommand command = new MySqlCommand($"SELECT * FROM {database}."+nameTable,mySqlConnection);

               MySqlDataReader reader = command.ExecuteReader();
               mySqlConnection.Close();
           }
           catch (Exception e)
           {
    
               return false;
           }
         

           return true;
       }








       public bool CreateTable(string connector,string nameTable,string database,ref int progress)
       {
          
         
           if (tablePresenceInDatabase(nameTable, connector, database) == false)
           { progress++;
               string table =
                   $"CREATE TABLE `{database}`.`{nameTable}` (" +
                   "`Chain` VARCHAR(45) NULL," +
                   "`Frequency` VARCHAR(45) NULL," +
                   "`Watts` VARCHAR(45) NULL," +
                   "`GHideal` VARCHAR(45) NULL," +
                   "`GHRT` VARCHAR(45) NULL, " +
                   "`HW` VARCHAR(45) NULL, " +
                   "`TempPCB` VARCHAR(45) NULL, " +
                   "`TempChip` VARCHAR(45) NULL, " +
                   "`Status` VARCHAR(45) NULL," +
                   "`id` INTEGER NOT NULL)";
               
               
               MySqlConnection mySqlConnection = new MySqlConnection(connector);
               progress++;
               
               
               mySqlConnection.Open();
               progress++;
               MySqlCommand command = new MySqlCommand(table,mySqlConnection);
               command.ExecuteNonQuery();
               progress++;
               mySqlConnection.Close();
               progress++;

               for (int i = 0; i <= 10; i++)
               {
                   progress++;
                   string addColumn = $"INSERT INTO `{database}`.`{nameTable}` (`id`) VALUES ('{i}')";
                   mySqlConnection.Open();
                   new MySqlCommand(addColumn,mySqlConnection).ExecuteNonQuery();
                   mySqlConnection.Close();
                   
               }
            
               progress++;

               return false;
           }

           return true;
       }



       public AsicStandartStatsObject GetAsicColumnData(string connector,string nameTable)
       {
         
            AsicStandartStatsObject asicsObject=new AsicStandartStatsObject();
            
            MySqlConnection mySqlConnection = new MySqlConnection(connector);
            
            mySqlConnection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM asic."+nameTable,mySqlConnection);

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

        
  
        
        
        public void SetAsicColumnData(string connectionString, AsicStandartStatsObject column,string table,ref int progress)
        {
            for (int i = 0; i <= 8; i++)
            {

                if (ProgressBarCreatingData.DataBaseError != true)
                { 
                    
                    UpdateData(connectionString, "Chain", i, column.LasicAsicColumnStats[i].Chain, table,ref progress);
                    UpdateData(connectionString, "Frequency", i, column.LasicAsicColumnStats[i].Frequency, table,ref progress);
                    UpdateData(connectionString, "Watts", i, column.LasicAsicColumnStats[i].Watts, table,ref progress);
                    Thread.Sleep(400);
                }
                if (ProgressBarCreatingData.DataBaseError != true)
                { 
                    UpdateData(connectionString, "GHideal", i,column.LasicAsicColumnStats[i].GHideal,table,ref progress);
                    UpdateData(connectionString, "GHRT", i,column.LasicAsicColumnStats[i].GHRT,table,ref progress);
                    UpdateData(connectionString, "HW", i,column.LasicAsicColumnStats[i].HW,table,ref progress);
                    Thread.Sleep(400);
                    
                }

                if (ProgressBarCreatingData.DataBaseError != true)
                {
                    UpdateData(connectionString, "TempPCB", i,column.LasicAsicColumnStats[i].TempPCB,table,ref progress);
                    UpdateData(connectionString, "TempChip", i,column.LasicAsicColumnStats[i].TempChip,table,ref progress);
                    UpdateData(connectionString, "Status", i,column.LasicAsicColumnStats[i].Status,table,ref progress);
                    Thread.Sleep(400);
                }

                
                
               
            } 
            
        

            if (ProgressBarCreatingData.DataBaseError != true)
            { 
                UpdateData(connectionString, "Status", 9,column.ElapsedTime,table,ref progress);
                UpdateData(connectionString, "GHRT", 9,column.HashrateAVG,table,ref progress);
                UpdateData(connectionString, "Chain", 9,column.DateTime,table,ref progress);
                
            }
           
            
            
            
        }
    }
}