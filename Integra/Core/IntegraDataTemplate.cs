using Integra.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    public abstract class IntegraDataTemplate<T> : INotifyPropertyChanged where T: IntegraDataTemplate<T>
    {
        private static Dictionary<string, PropertyInfo> _PropertyCache = new Dictionary<string, PropertyInfo>();
        private static bool _IsCached = false;

        public IntegraDataTemplate() 
        {
        }

       
        protected IntegraDataTemplate(uint id, byte[] data) 
        {
        }

        private void InitializeCache()
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var property in properties)
            {
                _PropertyCache.Add(property.Name, property);
            }

            _IsCached = true;
        }

        public Dictionary<string, PropertyInfo> PropertyCache
        {
            get 
            {
                if (!_IsCached)
                    InitializeCache();

                return _PropertyCache; 
            }
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// Event to raise when a property value of an <see cref="INotifyPropertyChanged"/> implementing class is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Method to call on a property of an <see cref="INotifyPropertyChanged"/> implementing class when it's value is changed to raise the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">A <see cref="string"/> equal to the name of the property that is changed.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
