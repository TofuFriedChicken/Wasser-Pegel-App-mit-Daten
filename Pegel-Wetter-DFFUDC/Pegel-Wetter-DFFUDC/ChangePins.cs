using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    // Quelle: vladislavantonyuk.github.io/articles/Customize-map-pins-in-.NET-MAUI/
    // Quelle Pin: www.flaticon.com/free-icon/location_7945007?term=pin+marker&page=1&position=18&origin=tag&related_id=7945007
    public class CustomPin : Pin
    {
        public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CustomPin));

        public ImageSource? ImageSource
        {
            get => (ImageSource?)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
    }
}

    
