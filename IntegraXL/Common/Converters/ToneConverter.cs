using Integra.Core;
using Integra.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IntegraXL.Common.Converters
{
    public class ToneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Tone tone = value as Tone;

            if (tone != null)
                return new IntegraTone(tone.MSB, tone.LSB, tone.PC);

            return Binding.DoNothing;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            IntegraTone tone = value as IntegraTone;

            if (tone != null)
                return new Tone(tone);

            return Binding.DoNothing;
        }
    }
}
