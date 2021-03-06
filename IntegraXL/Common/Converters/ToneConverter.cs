using Integra;
using Integra.Core;
using Integra.Database;
using Integra.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
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

    public class PCMNoteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;

            return IntegraPCMDrumKitNotes.Values[id];

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class SNDNoteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;

            return IntegraSNDNotes.Values[id];

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
    public class WaveFormConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            // TODO: Data access is called for every note

            int id = (int)values;
            IntegraWaveFormType type = (IntegraWaveFormType)int.Parse((string)parameter);
            

            return DataAccess.SelectWaveForms(type)[id];
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ToneBankEnumerationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //IntegraToneBanks type = (IntegraToneBanks)(int)value;

            if (value != null)
            {
                FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : value.ToString();
            }

            return value;
        }

        /// <summary>
        /// ConvertBack value from binding back to source object. This isn't supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new Exception("Can't convert back");
        }
    }

    public class ToneCategoryEnumerationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IntegraToneCategories type = (IntegraToneCategories)(byte)value;

            if (value != null)
            {
                FieldInfo fieldInfo = type.GetType().GetField(type.ToString());

                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : type.ToString();
            }

            return value;
        }

        /// <summary>
        /// ConvertBack value from binding back to source object. This isn't supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class ExpansionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IntegraExpansions expansion = (IntegraExpansions)value;

            if (expansion == IntegraExpansions.Off)
                return true;

            return Device.Instance.VirtualSlots[expansion];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    //public class LoadedExpansionConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        Console.WriteLine(value.GetType().Name);
    //        IntegraToneBanks type = (IntegraToneBanks)value;

    //        if (type > IntegraToneBanks.Exp19)
    //            return true;

    //        MainWindow handle = Application.Current.MainWindow as MainWindow;

    //        if (handle.VirtualSlots.Contains((IntegraExpansions)type))
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    /// <summary>
    //    /// ConvertBack value from binding back to source object. This isn't supported.
    //    /// </summary>
    //    public object ConvertBack(object value, Type targetType,
    //        object parameter, CultureInfo culture)
    //    {
    //        throw new Exception("Can't convert back");
    //    }
    //}
}
