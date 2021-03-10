using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using Model;

namespace UI.Converters
{
    public class SexConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<int> enums)
            {
                return GetEnums(enums);
            }
            if (value is Sex sex)
            {
               return sex == Sex.Male ? "Мужской" : "Женский";
            }
            return "Не определён";

            IEnumerable<string> GetEnums(IEnumerable<int> values)
            {
                foreach (var i in values)
                {
                    switch (i)
                    {
                        case 0:
                            yield return "Мужской";
                            break;
                        case 1:
                            yield return "Женский";
                            break;
                    }
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string sex)
            {
                return sex == "Мужской" ? Sex.Male : Sex.Female;
            }
            return -1;
        }
    }
}
