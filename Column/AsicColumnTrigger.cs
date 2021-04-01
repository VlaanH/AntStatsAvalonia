using Avalonia.Media;

namespace AntStats.Avalonia
{
    public class AsicColumnTrigger
    { 
        public int j { get; set; }

        public AsicColumnTrigger(int i)
        {
             j=i;
        }


        public void SetAsicColumnColor(ISolidColorBrush colorBrush)
        {

            ColumnList.Chain[j].Background=colorBrush;
            ColumnList.Frequency[j].Background=colorBrush;
            ColumnList.Status[j].Background=colorBrush;
            ColumnList.Watts[j].Background=colorBrush;
            ColumnList.GHideal[j].Background=colorBrush;
            ColumnList.HW[j].Background=colorBrush;
            ColumnList.TempChip[j].Background=colorBrush;
            ColumnList.GHRT[j].Background=colorBrush;
            ColumnList.TempPCB[j].Background=colorBrush;

            
            
        }
    }
}