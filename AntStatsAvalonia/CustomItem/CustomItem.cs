using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AntStats.Avalonia
{
    public class CustomItem
    {
        public static Label AddLabel(string nameLabel)
        {
            Label labelCustom = new Label();
            
            labelCustom.Content = String.Format(nameLabel);
            labelCustom.Name = String.Format(nameLabel);
            labelCustom.VerticalAlignment = VerticalAlignment.Stretch;
            labelCustom.Content = "-";
            labelCustom.FontSize = 16;
            labelCustom.Margin = new Thickness(1);
            labelCustom.VerticalContentAlignment = VerticalAlignment.Center;
            labelCustom.HorizontalContentAlignment = HorizontalAlignment.Center;


            labelCustom.Background = AppStandartData.StandartColorBrush;
            return labelCustom;
        }




    }
}