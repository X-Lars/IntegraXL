using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Integra.Core
{
    /// <summary>
    /// Base class specifying a data template for items to use by the INTEGRA-7 collection classes.
    /// </summary>
    /// <typeparam name="T">The type of item data template.</typeparam>
    public abstract class IntegraDataTemplate<T> : INotifyPropertyChanged where T: IntegraDataTemplate<T>
    {
        #region Fields

        /// <summary>
        /// Stores a property cache per inherited type.
        /// </summary>
        private static Dictionary<string, PropertyInfo> _PropertyCache = new Dictionary<string, PropertyInfo>();

        /// <summary>
        /// Stores whether the properties of the inherited class are cached.
        /// </summary>
        private static bool _IsCached = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraDataTemplate{T}"/> instance.
        /// </summary>
        public IntegraDataTemplate() { }

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraDataTemplate{T}"/> instance.
        /// </summary>
        /// <param name="id">The ID associated with the data.</param>
        /// <param name="data">The data to initialize the template.</param>
        protected IntegraDataTemplate(int id, byte[] data) { }

        #endregion

        /// <summary>
        /// Initializes the property cache.
        /// </summary>
        /// <remarks><i>All public instance properties are cached.</i></remarks>
        protected virtual void InitializeCache()
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var property in properties)
            {
                _PropertyCache.Add(property.Name, property);
            }

            _IsCached = true;
        }

        /// <summary>
        /// Gets a reference to the property cache of the data template.
        /// </summary>
        internal Dictionary<string, PropertyInfo> PropertyCache
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
        /// Event to raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Raises the property changed event for the specified property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
