using Avalonia.Media;

namespace AntStats.Avalonia
{
    public static class ColumnTarget
    {
        public static void Add()
        {
            int j = 0;
            while (j<9)
            {
                var ColorMoved = Brushes.Gray;
                var ColorLeave = AppStandartData.StandartColorBrush;
                
                AsicColumnTrigger asicColumnTrigger = new AsicColumnTrigger(j);
               
                
                ColumnList.Chain[j].PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                ColumnList.Chain[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                
                ColumnList.Frequency[j]. PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                ColumnList.Frequency[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                
                ColumnList.Status[j].PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                ColumnList.Status[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                
                ColumnList.Watts[j].PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                ColumnList.Watts[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                
                ColumnList.GHideal[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                ColumnList.GHideal[j].PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                
                ColumnList.HW[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                ColumnList.HW[j].PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                
                ColumnList.TempChip[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                ColumnList.TempChip[j].PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                
                ColumnList.GHRT[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                ColumnList.GHRT[j].PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                
                ColumnList.TempPCB[j].PointerLeave += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorLeave);
                ColumnList.TempPCB[j].PointerMoved += (s, e) => asicColumnTrigger.SetAsicColumnColor(ColorMoved);
                    
                j++;
            }

        }
    }
}