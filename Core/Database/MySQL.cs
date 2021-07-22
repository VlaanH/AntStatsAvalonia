using System;
using System.Threading;
using MySqlConnector;


namespace AntStatsCore.Database
{
    public class MySQL : IDatabase
    {

        private static void addprogress(ref int progress,ref int maxProgress,ref int percentageProgress)
        {
            progress++; 
            percentageProgress = (int) ( (float) progress / maxProgress * 100);
       
        }


        private static Result TestConnection(string connector)
        {
            Result response=Result.NoError;
            
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(connector); 
                mySqlConnection.Open(); 
                mySqlConnection.Close(); 
            }
            catch (Exception )
            { 
                response = Result.ErrorExist;
            }


            return response;
        }

        
        
        
        

        private static void UpdateData(string connector,string columnName,int columnId,string data,string nameTable, ref int percentageProgress,ref int maxProgress,ref int progress)
       {
          
            new Thread(() =>
            {
           
                 
          
                    try
                    {
                        MySqlConnection mySqlConnection = new MySqlConnection(connector);
                        mySqlConnection.Open();
                        string Commondq = $"UPDATE {nameTable} SET {columnName}='{data}' WHERE id = '{columnId}'";
                        MySqlCommand command = new MySqlCommand(Commondq,mySqlConnection);
                        command.ExecuteNonQuery();
                        mySqlConnection.Close();
                       
                    }
                    catch (Exception)
                    {
                       
                      
                    }


                    
                    
            }).Start();

            
            addprogress(ref progress, ref maxProgress, ref percentageProgress);
                
           
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
           catch (Exception)
           {
    
               return false;
           }
         

           return true;
       }








       public Result CreateTable(string connector,string nameTable,string database,ref int percentageProgress)
       {
         
           int maxProgress = 16;
           int progress=0;
         
           if (tablePresenceInDatabase(nameTable, connector, database) == false)
           {  
               addprogress(ref progress, ref maxProgress, ref percentageProgress);
               
               
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
               
               addprogress(ref progress, ref maxProgress, ref percentageProgress);
               
               
               mySqlConnection.Open();
               
               
               addprogress(ref progress, ref maxProgress, ref percentageProgress);
               
               
               MySqlCommand command = new MySqlCommand(table,mySqlConnection);
               command.ExecuteNonQuery();
             
               addprogress(ref progress, ref maxProgress, ref percentageProgress);
               
               
               mySqlConnection.Close();
           
               addprogress(ref progress, ref maxProgress, ref percentageProgress);
               

               for (int i = 0; i <= 9; i++)
               {
                   progress++;
                   string addColumn = $"INSERT INTO `{database}`.`{nameTable}` (`id`) VALUES ('{i}')";
                   mySqlConnection.Open();
                   new MySqlCommand(addColumn,mySqlConnection).ExecuteNonQuery();
                   mySqlConnection.Close();
                   
               }
            
               addprogress(ref progress, ref maxProgress, ref percentageProgress);

               return Result.NoError;
           }

           return Result.ErrorExist;
       }



       public AsicStandardStatsObject GetAsicColumnData(string connector,string nameTable,string  databaseName)
       {
         
           AsicStandardStatsObject asicsObject=new AsicStandardStatsObject();
            
           MySqlConnection mySqlConnection = new MySqlConnection(connector);
            
           mySqlConnection.Open();
           MySqlCommand command = new MySqlCommand("SELECT * FROM "+databaseName+"."+nameTable,mySqlConnection);

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




               if (i<9)
               {
                   asicsObject.LasicAsicColumnStats.Add(asicColumnClass); 
               }
            
           }
            
            
           mySqlConnection.Close();

            
            
           return asicsObject;
       }

        
  
        
        
        public  Result SetAsicColumnData(string connectionString, AsicStandardStatsObject column,string table,ref int percentageProgress)
       {
           Result dataBaseErrorExists=Result.NoError;
           int maxProgress = 84;
           int progress=0;
           
           dataBaseErrorExists = TestConnection(connectionString); 
           for (int i = 0; i <= 9-1; i++)
           {

              
               
               if (dataBaseErrorExists == Result.NoError)
               {   
                    UpdateData(connectionString, "Chain", i, column.LasicAsicColumnStats[i].Chain, table,ref percentageProgress,ref maxProgress,ref progress);
                   UpdateData(connectionString, "Frequency", i, column.LasicAsicColumnStats[i].Frequency, table,ref percentageProgress,ref maxProgress,ref progress);
                   UpdateData(connectionString, "Watts", i, column.LasicAsicColumnStats[i].Watts, table,ref percentageProgress,ref maxProgress,ref progress);
               
                   Thread.Sleep(200);
                   UpdateData(connectionString, "GHideal", i,column.LasicAsicColumnStats[i].GHideal,table,ref percentageProgress,ref maxProgress,ref progress);
                  UpdateData(connectionString, "GHRT", i,column.LasicAsicColumnStats[i].GHRT,table,ref percentageProgress,ref maxProgress,ref progress);
                  UpdateData(connectionString, "HW", i,column.LasicAsicColumnStats[i].HW,table,ref percentageProgress,ref maxProgress,ref progress);
               
             
                 
                  Thread.Sleep(200);
              
                   UpdateData(connectionString, "TempPCB", i,column.LasicAsicColumnStats[i].TempPCB,table,ref percentageProgress,ref maxProgress,ref progress);
                   UpdateData(connectionString, "TempChip", i,column.LasicAsicColumnStats[i].TempChip,table,ref percentageProgress,ref maxProgress,ref progress);
                   UpdateData(connectionString, "Status", i,column.LasicAsicColumnStats[i].Status,table,ref percentageProgress,ref maxProgress,ref progress);
               }

        
                
               
           } 
            
       
           
           if (dataBaseErrorExists==Result.NoError)
           { 
               UpdateData(connectionString, "Status", 9,column.ElapsedTime,table,ref percentageProgress,ref maxProgress,ref progress);
               UpdateData(connectionString, "GHRT", 9,column.HashrateAVG,table,ref percentageProgress,ref maxProgress,ref progress);
               UpdateData(connectionString, "Chain", 9,column.DateTime,table,ref percentageProgress,ref maxProgress,ref progress);
                
           }



           return dataBaseErrorExists;
       }
    }
}