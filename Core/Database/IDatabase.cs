using System.Collections.Generic;

namespace AntStatsCore.Database
{
    public interface IDatabase
    {
        Result CreateTable(string connector, string nameTable,string database,ref int percentageProgress);
        AsicStandardStatsObject GetAsicColumnData(string connectionString,string table,string databaseName);
        
        Result SetAsicColumnData(string connectionString, AsicStandardStatsObject column, string table,ref int percentageProgress);
    }

    public enum Result
    {
        NoError,
        ErrorExist
    }
    
}