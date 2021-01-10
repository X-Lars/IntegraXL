using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    /// <summary>
    /// Base class for all INTEGRA-7 data structures
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IntegraBase<T> : INotifyPropertyChanged where T : IntegraBase<T>
    {
        #region Constructor

        public IntegraBase()
        {
            Name = GetType().Name;
        }

        #endregion

        #region Properties

        public string Name { get; }

        #endregion

        #region INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="propertyName">A <see cref="string"/> containing the name of the property that is changed.</param>
        /// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
