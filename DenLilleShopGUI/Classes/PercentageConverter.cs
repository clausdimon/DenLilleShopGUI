﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace DenLilleShopGUI.Classes
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToDouble(value) *
                System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}