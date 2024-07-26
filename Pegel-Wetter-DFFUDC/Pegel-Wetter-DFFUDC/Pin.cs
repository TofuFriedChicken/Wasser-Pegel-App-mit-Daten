using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Compatibility.Platform;
using Microsoft.Maui.Maps.Handlers;
using Microsoft.Maui.Maps;


namespace Pegel_Wetter_DFFUDC
{
    public class CustomMapHandler : MapHandler
    {
        //public CustomMapHandler() : base()
        //{
        //    MapLoaded += OnMapLoaded;
        //}
        //private void OnMapLoaded(object sender, MapLoadedEventArgs e)
        //{
        //    var map = sender as MapControl;

        //    if (map != null)
        //    {
        //        foreach (var pin in VirtualView.Pins)
        //        {
        //            if (pin is CustomPin customPin)
        //            {
        //                var mapIcon = new MapIcon
        //                {
        //                    Location = new Geopoint(new BasicGeoposition
        //                    {
        //                        Latitude = pin.Location.Latitude,
        //                        Longitude = pin.Location.Longitude
        //                    }),
        //                    Title = pin.Label,
        //                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Resources/Images/waterlevel.png")),
        //                    ZIndex = 0
        //                };
        //            }
        //        }
        //    }
        //}
     
    }
}

