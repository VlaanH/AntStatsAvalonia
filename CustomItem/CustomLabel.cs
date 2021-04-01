using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace AntStats.Avalonia
{
    public class CustomLabel
    {
        public static Label AddLabel(string namelabel)
        {
            Label labelCustom = new Label();
            
            labelCustom.Content = String.Format(namelabel);
            labelCustom.Name = String.Format(namelabel);
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