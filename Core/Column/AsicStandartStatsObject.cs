using System.Collections.Generic;

namespace AntStatsCore
{
    public class AsicColumnClass
    {
        public string Watts {get; set;}
        
        public string Frequency {get; set;}
        
        public string Chain {get; set;}
        
        public string GHideal {get; set;}
        
        public string GHRT {get; set;}
        
        public string HW {get; set;}
        
        public string TempPCB {get; set;}
        
        public string TempChip {get; set;}
        
        public string Status {get; set;}

        
    }
    
    
    
    public class AsicStandardStatsObject
    {
        public List<AsicColumnClass> LasicAsicColumnStats = new List<AsicColumnClass>();

        public string ElapsedTime{ get; set; }

        public  string HashrateAVG{ get; set; }
        
        public  string DateTime{ get; set; }

    }
}