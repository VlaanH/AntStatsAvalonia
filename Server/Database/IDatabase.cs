using System.Collections.Generic;

namespace AntStats.Avalonia.Database
{
    public interface IDatabase
    {
        AsicStandartStatsObject GetAsicColumnData(string connectionString,string table);
        
        void SetAsicColumnData(string connectionString,AsicStandartStatsObject column,string table);
    }
}