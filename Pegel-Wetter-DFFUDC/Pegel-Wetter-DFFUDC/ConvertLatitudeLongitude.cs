using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pegel_Wetter_DFFUDC
{
    class ConvertLatitudeLongitude
    {
        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value is double latitude && parameter is double longitude)
            {
                return new Adress(latitude, longitude);
            }
            return new Adress(0, 0);
        }

        public object BackConvert(object value, Type type, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) //automatisch generiert
        {
            throw new NotImplementedException();
        }
    }
}
