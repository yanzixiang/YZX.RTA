namespace mathview.View
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// A better <see cref="BooleanToVisibilityConverter"/> implementation.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// The True visibility state.
        /// </summary>
        public Visibility True { get; set; }

        /// <summary>
        /// The False visibility state.
        /// </summary>
        public Visibility False { get; set; }

        /// <summary>
        /// Creates a new <see cref="BooleanToVisibilityConverter"/> instance.
        /// </summary>
        public BooleanToVisibilityConverter()
        {
            this.True = Visibility.Visible;
            this.False = Visibility.Collapsed;
        }

        /// <summary>
        /// Converts the value.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">An optional parameter.</param>
        /// <param name="culture">The conversion culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is bool) && (targetType == typeof(Visibility)))
                return (bool)value ? this.True : this.False;

            if ((value is bool?) && (targetType == typeof(Visibility)))
            {
                var v = (bool?)value;
                return (v.HasValue && v.Value) ? this.True : this.False;
            }

#if SILVERLIGHT
            return null;
#else
            return Binding.DoNothing;
#endif
        }

        /// <summary>
        /// Converts the value back.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">An optional parameter.</param>
        /// <param name="culture">The conversion culture.</param>
        /// <returns>The converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
