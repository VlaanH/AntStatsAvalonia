using System.Collections.Generic;

namespace AntStats.Avalonia.Database
{
    public interface IDatabase
    {
        public bool  CreateTable(string connector, string nameTable,string database,ref int progress);
        AsicStandartStatsObject GetAsicColumnData(string connectionString,string table,string database);
        
        void SetAsicColumnData(string connectionString, AsicStandartStatsObject column, string tableref,
            ref int progress);
    }
}