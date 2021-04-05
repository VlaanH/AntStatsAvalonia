using System.Collections.Generic;

namespace AntStats.Avalonia.Database
{
    public interface IDatabase
    {
        void CreateTable(string connector, string nameTable,string database);
        AsicStandartStatsObject GetAsicColumnData(string connectionString,string table);
        
        void SetAsicColumnData(string connectionString,AsicStandartStatsObject column,string table);
    }
}