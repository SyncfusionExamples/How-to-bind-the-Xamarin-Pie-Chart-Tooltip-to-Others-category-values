using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SimpleSample
{
    public class ChartValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if(value != null)
            {
                IList<DataModel> dataList = new List<DataModel>();
                if (value is DataModel)
                {
                    dataList.Add(value as DataModel);
                    return dataList;
                }
                else
                {
                    return value;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ChartAvgValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                //Return string value.
                if (parameter != null && parameter.ToString() == "Label")
                {
                    return value is List<object> ? "Others" : (value as DataModel).XData;
                }
                else
                {
                    //Return average value.
                    if (value is List<object>)
                    {
                        var collection = value as List<object>;
                        var sum = collection.Sum(item => (item as DataModel).YData);
                        return Math.Round(sum / collection.Count, 2);
                    }
                    else
                    {
                        return (value as DataModel).YData;
                    }
                }
            }
         
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}
