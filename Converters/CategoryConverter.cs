using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TaskApplication.Converters
{
    public class CategoryConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           int category = (int)value;
            switch (category)
            {
                case 0:
                    return "low";
                case 1:
                    return "normal";
                case 2:
                    return "high";
                default:
                    return -1;
            }
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string category = (string)value;
            switch (category)
            {
                case "low":
                    return 0;
                case "normal":
                    return 1;
                case "high":
                    return 2;
                default:
                    return -1;
            }
        }
    }
}
