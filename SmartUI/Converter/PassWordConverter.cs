using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DiningReservation.UI.Converter
{
    public class PassWordConverter : IValueConverter
    {
        private string realWord = "";

        private char replaceChar = '*';

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                string temp = parameter.ToString();
                if (!string.IsNullOrEmpty(temp))
                {
                    replaceChar = temp.First();
                }
            }

            if (value != null)
            {
                realWord = value.ToString();
            }


            string replaceWord = "";
            for (int index = 0; index < realWord.Length; ++index)
            {
                replaceWord += replaceChar;
            }

            return replaceWord;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string backValue = "";
            if (value != null)
            {
                string strValue = value.ToString();
                for (int index = 0; index < strValue.Length; ++index)
                {
                    if (strValue.ElementAt(index) == replaceChar)
                    {
                        backValue += realWord.ElementAt(index);
                    }
                    else
                    {
                        backValue += strValue.ElementAt(index);
                    }
                }
            }
            return backValue;
        }

    }
}
