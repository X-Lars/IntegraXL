using Integra.Core.Interfaces;
using Integra.Database;
using Integra.Models;
using MidiXL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Integra.Core
{
    
    /// <summary>
    /// Base class for all INTEGRA-7 data structures.
    /// </summary>
    /// <typeparam name="T">A class <typeparamref name="T"/> defining the INTEGRA-7 data structure.</typeparam>
    public abstract class IntegraBase<T> : IIntegraDataClass, INotifyPropertyChanged where T : IntegraBase<T>
    {
        #region Fields

        /// <summary>
        /// Cache for storing fields decorated with the <see cref="Offset"/> attribute.
        /// </summary>
        private static Dictionary<ushort, FieldInfo> _FieldCache = new Dictionary<ushort, FieldInfo>();

        /// <summary>
        /// Cache for storing properties decorated with the <see cref="Offset"/> attribute.
        /// </summary>
        private static Dictionary<ushort, PropertyInfo> _PropertyCache = new Dictionary<ushort, PropertyInfo>();

        /// <summary>
        /// Tracks whether the data structure is initialized.
        /// </summary>
        private bool _IsInitialized = false;

        /// <summary>
        /// Tracks whether the cache has been initialized.
        /// </summary>
        private static bool _IsCached = false;

        #endregion


        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraBase{T}"/> instance.
        /// </summary>
        /// <remarks><i>Default constructor for dynamic instance creation by reflection.</i></remarks>
        public IntegraBase() { }

        /// <summary>
        /// Creates and initializes new unconnected <see cref="IntegraBase{T}"/> instance.
        /// </summary>
        /// <param name="address">The INTEGRA-7 base address the data structure refers to.</param>
        /// <remarks><i>Use for creation of single or collection instances that need manual configuration.</i></remarks>
        public IntegraBase(IntegraAddress address)
        {
            Address = address;

            // Name defaults to type name
            Name = GetType().Name;
        }

        /// <summary>
        /// Creates and initializes a new connected <see cref="IntegraBase{T}"/> instance.
        /// </summary>
        /// <param name="address">The INTEGRA-7 base address the data structure refers to.</param>
        /// <param name="request">The request specifying the size of the data structure.</param>
        /// <remarks><i>Use for creation of a single instance.</i></remarks>
        public IntegraBase(IntegraAddress address, IntegraRequest request) : this(address, new IntegraRequest[] { request }) { }

        /// <summary>
        /// Creates and initializes a new connected <see cref="IntegraBase{T}"/> instance.
        /// </summary>
        /// <param name="address">The INTEGRA-7 base address the data structure refers to.</param>
        /// <param name="requests">The requests specifying the number of items to</param>
        /// <remarks><i>Use for creation of a collection of instances.</i></remarks>
        public IntegraBase(IntegraAddress address, IntegraRequest[] requests)
        {
            Debug.Print($"[{GetType().Name}]");

            Address = address;
            Requests.AddRange(requests);

            // Name defaults to type name
            Name = GetType().Name;

           
            Initialize();
        }

        

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name defaulted to the inheriting data structure type name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the base address of the data structure in the INTEGRA-7.
        /// </summary>
        internal protected IntegraAddress Address { get; set; }

        /// <summary>
        /// Gets the list of request addresses required to initialize the data structure.
        /// </summary>
        internal protected List<IntegraRequest> Requests { get; private set; } = new List<IntegraRequest>();

        public virtual bool IsInitialized
        {
            get { return _IsInitialized; }
            set
            {
                if (_IsInitialized == value)
                    return;

                _IsInitialized = value;

                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void DeviceConnected(object sender, EventArgs e)
        {
            Device.Connected -= DeviceConnected;

            Device.Instance.MidiInputDevice.SystemExclusiveReceived += SystemExclusiveReceived;

            Task.Factory.StartNew(() => Device.Instance.Initialize(this), TaskCreationOptions.LongRunning);
        }

        public virtual void Initialize()
        {
            InitializeCache();

            if (Device._IsConnected)
            {
                Device.Instance.MidiInputDevice.SystemExclusiveReceived += SystemExclusiveReceived;

                // [NON BLOCKING]
                Task.Factory.StartNew(() => Device.Instance.Initialize(this), TaskCreationOptions.LongRunning);
            }
            else
            {
                Device.Connected += DeviceConnected;
            }
        }

        /// <summary>
        /// Initalizes the fields of the <see cref="IntegraBase{T}"/> inherited class.
        /// </summary>
        /// <param name="data">The <see cref="IntegraSystemExclusive.Data"/> part to initialize the class.</param>
        /// <returns>A <see cref="bool"/> containing true if the class is initialized.</returns>
        protected virtual bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                int count = _FieldCache.Count;

                foreach(KeyValuePair<ushort, FieldInfo> field in _FieldCache)
                {
                    if(field.Value.FieldType.IsArray)
                    {
                        // Create an array from the field to be able to loop through the field elements
                        Array array = (Array)field.Value.GetValue(this);

                        // Get the type of elements in the array
                        Type arrayType = array.GetType().GetElementType();

                        if(arrayType == typeof(byte))
                        {
                            for (int i = 0; i < array.Length; i++)
                            {
                                // Set the value of the selected index from the data
                                array.SetValue(data[field.Key + i], i);
                            }
                        }
                        else if (arrayType == typeof(int))
                        {
                            for (int i = 0; i < array.Length; i++)
                            {
                                byte[] values = new byte[4];

                                int offset = field.Key + (i * 4);

                                //values[0] = (byte)((data[offset] >> 12) & 0x0F);
                                //values[1] = (byte)((data[offset + 1] >> 8) & 0x0F);
                                //values[2] = (byte)((data[offset + 2] >> 4) & 0x0F);
                                //values[3] = (byte)((data[offset + 3]) & 0x0F);
                                values[0] = (byte)((data[offset]) & 0x0F);
                                values[1] = (byte)((data[offset + 1]) & 0x0F);
                                values[2] = (byte)((data[offset + 2]) & 0x0F);
                                values[3] = (byte)((data[offset + 3]) & 0x0F);

                                if (BitConverter.IsLittleEndian)
                                    Array.Reverse(values);

                                array.SetValue(BitConverter.ToInt32(values, 0), i);
                            }
                        }
                        else
                        {
                            throw new Exception($"[{GetType().Name}.{nameof(Initialize)}] Unsupported array type {field.Value.Name}");
                        }
                    }
                    else if (field.Value.FieldType == typeof(bool))
                    {
                        field.Value.SetValue(this, Convert.ToBoolean(data[field.Key]));
                    }
                    else if (field.Value.FieldType == typeof(int))
                    {
                        byte[] values = new byte[4];

                        int key = field.Key;

                        //key = (((key & 0x00000F00) >> 16) * 128) + (key & 0x000000FF);
                        //key = (key & 0x00000FF);
                        // Ensure MIDI range 0x7F
                        key = (((key & 0xFF00) >> 8) * 128) + (key & 0x00FF);
                        //Console.WriteLine(key);
                        //Console.WriteLine(key);
                        //if (key > 128)
                        //{
                        //    key %= 128;
                        //    key += 128;
                        //}

                        for (int i = 0; i < values.Length; i++)
                        {
                            
                            
                            // data 145 key 257
                            //values[i] = data[field.Key + i];
                            values[i] = data[key + i];
                        }

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(values);

                        field.Value.SetValue(this, BitConverter.ToInt32(values, 0));
                    }
                    else
                    {
                        field.Value.SetValue(this, data[field.Key]);
                    }
                }

                NotifyPropertyChanged(string.Empty, false);
                //DebugPrint();
            }

            return IsInitialized = true;
        }

        /// <summary>
        /// Reinitializes the fields of the <see cref="IntegraBase{T}"/> derived class.
        /// </summary>
        internal virtual void Reinitialize()
        {
            Console.WriteLine($"[{GetType().Name}.{nameof(Reinitialize)}]");
            IsInitialized = false;
            Initialize();
        }

        #region Methods: Private

        /// <summary>
        /// Initalizes the field and property cache.
        /// </summary>
        private void InitializeCache()
        {
            if (_IsCached)
                return;

            // Get all private instance fields
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            // If the field has an offset attribute, add it to the field cache
            foreach (FieldInfo field in fields)
            {
                Offset attribute = field.GetCustomAttribute<Offset>(false);

                if (attribute != null)
                {
                    if (_FieldCache.ContainsKey(attribute.Value))
                        throw new Exception($"[{GetType().Name}.{nameof(InitializeCache)}] Duplicate field offset attribute 0x{attribute.Value.ToString("X4")}!");

                    _FieldCache.Add(attribute.Value, field);
                }
            }

            // Get all public instance properties
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // If the property has an offset attribute, add it to the property cache
            foreach (PropertyInfo property in properties)
            {
                Offset attribute = property.GetCustomAttribute<Offset>(false);

                if (attribute != null)
                {
                    if (_PropertyCache.ContainsKey(attribute.Value))
                        throw new Exception($"[{GetType().Name}.{nameof(InitializeCache)}] Duplicate property offset attribute 0x{attribute.Value.ToString("X4")}!");

                    _PropertyCache.Add(attribute.Value, property);
                }
            }

            _IsCached = true;
        }

        private FieldInfo GetCachedField(ushort offset)
        {
            if (!_FieldCache.ContainsKey(offset))
                return null;

            return _FieldCache[offset];
        }

        private PropertyInfo GetCachedProperty(ushort offset)
        {
            if (!_PropertyCache.ContainsKey(offset))
                return null;

            return _PropertyCache[offset];
        }

        private Offset GetPropertyOffset(string propertyName)
        {
            foreach(var property in _PropertyCache)
            {
                if(property.Value.Name == propertyName)
                {
                    return new Offset(property.Key);
                }
            }

            return null;
        }

        protected void InitializeField(IntegraSystemExclusive syx)
        {
            // The offset of the property to set
            uint offset = syx.Address - Address;

            // 0x10, 0x20, 0x30, 0x12 [systemExclusive.Address]
            // 0x10, 0x20, 0x30, 0x10 [this.Address]
            // ______________________ -
            // 0x00, 0x00, 0x00, 0x02 [offset]

            for (int fieldOffset = 0, propertyOffset = 0; fieldOffset < syx.Data.Length; fieldOffset++, propertyOffset++)
            {
                FieldInfo field = GetCachedField((ushort)(offset + fieldOffset));

                // The field is not found, skip current iteration
                if (field == null)
                    continue;

                // The field is an array of bytes
                if (field.FieldType.IsArray)
                {
                    // Create an array from the field to be able to loop through the field values
                    Array array = (Array)field.GetValue(this);

                    // Get the type of elements in the array
                    Type arrayType = array.GetType().GetElementType();

                    if (arrayType == typeof(byte))
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            // Set the value of the selected index
                            array.SetValue(syx.Data[fieldOffset], i);

                            // Increment the field offset to the next system exclusive byte
                            fieldOffset++;
                        }
                    }
                    else if (arrayType == typeof(int))
                    {
                        byte[] values = new byte[4];

                        int p = (syx.Data.Length - fieldOffset) / 4;

                        // Working on
                        int l = syx.Data.Length - fieldOffset;

                        for (int i = 0; i < p; i++)
                        {
                            Array.Copy(syx.Data, fieldOffset, values, 0, 4);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(values);

                            array.SetValue(BitConverter.ToInt32(values, 0), i);
                            fieldOffset += 4;
                        }
                        //Array.Copy(syx.Data, fieldOffset, array, 0, l);
                        //fieldOffset += p * 4;


                        //Array.Copy(syx.Data, syx.Data.Length - 4, values, 0, 4);

                        //if (BitConverter.IsLittleEndian)
                        //    Array.Reverse(values);
                        
                        //array.SetValue(BitConverter.ToInt32(values, 0), p - 1);
                        //fieldOffset += p * 4;
                        ////propertyOffset += 4;
                    }
                    else
                    {
                        throw new Exception($"[{GetType().Name}.{nameof(Initialize)}] Unsupported array type {field.Name}");
                    }
                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValue(this, Convert.ToBoolean(syx.Data[fieldOffset]));
                }
                else if (field.FieldType == typeof(int))
                {
                    byte[] values = syx.Data;


                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(values);

                    field.SetValue(this, BitConverter.ToInt32(values, 0));
                    fieldOffset += 4;
                }
                else
                {
                    field.SetValue(this, syx.Data[fieldOffset]);
                }

                // Retrieve the accompanied property with the same offset to raise the NotifyPropertyChanged event
                PropertyInfo property = GetCachedProperty((ushort)(offset + propertyOffset));

                if(property != null)
                {
                    // Check for indexer property
                    if (property.GetIndexParameters().Length == 0)
                    {
                        NotifyPropertyChanged(property.Name, false);
                        //Debug.Print($"[{Name}] {property.Name}: {property.GetValue(this)}");
                    }
                    else
                    {
                        NotifyPropertyChanged("Item[]", false);
                        //Debug.Print($"[{Name}] {property.Name}");
                    }
                }

                // Property offset has to be realligned in case the field is an array
                propertyOffset = fieldOffset;
            }
        }

        private void TransmitProperty(ushort offset, byte[] value)
        {
            if (IsInitialized)
            {
                IntegraSystemExclusive systemExclusive = new IntegraSystemExclusive(this.Address, offset, value);

                Device.Instance.SendSystemExclusive(systemExclusive);
                
            }
        }

        private void TransmitProperty(string propertyName, int? index)
        {
            Offset offset = GetPropertyOffset(propertyName);

            if(offset != null)
            {
                if (_FieldCache.ContainsKey(offset.Value))
                {
                    FieldInfo field = _FieldCache[offset.Value];

                    if (field.FieldType.IsArray)
                    {
                        // Create an array from the field to be able to loop through the field values
                        Array fieldArray = (Array)field.GetValue(this);

                        // Get the type of elements in the array
                        Type fieldArrayType = fieldArray.GetType().GetElementType();

                        if(fieldArrayType == typeof(byte))
                        {
                            byte[] array = (byte[])field.GetValue(this);

                            TransmitProperty(offset.Value, array);
                        }
                        else if (fieldArrayType == typeof(int))
                        {
                            if (index == null)
                                throw new ArgumentNullException(nameof(index));

                            int[] array = (int[])field.GetValue(this);

                            byte[] values = new byte[4];

                            values = BitConverter.GetBytes(array[(int)index]);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(values);

                            TransmitProperty((ushort)(offset.Value + (index * 4)), values);
                        }
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        TransmitProperty(offset.Value, new byte[] { (bool)field.GetValue(this) ? (byte)1 : (byte)0 });
                    }
                    else if (field.FieldType.IsEnum)
                    {
                        object enumValue = Convert.ChangeType(field.GetValue(this), Enum.GetUnderlyingType(field.FieldType));

                        TransmitProperty(offset.Value, new byte[] { (byte)Convert.ChangeType(enumValue, TypeCode.Byte) });
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        byte[] values = BitConverter.GetBytes((int)field.GetValue(this));

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(values);

                        TransmitProperty(offset.Value, new byte[] { values[0], values[1], values[2], values[3] });
                    }
                    else
                    {
                        TransmitProperty(offset.Value, new byte[] { (byte)field.GetValue(this) });
                    }
                }
            }
        }

       
        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Eventhandler that handles the <see cref="Device.SystemExclusiveReceived"/>.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="SystemExclusiveMessageEventArgs"/> containing event data.</param>
        /// <remarks><i>The event handler is attached inside the <see cref="Device.Initialize{T}(IntegraBase{T})"/> method invoked by <see cref="Initialize"/>.</i></remarks>
        internal virtual void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);

            if(syx.Address == Address)
            {
                // Exact match
                if (syx.Data.Length == Requests[0].Size)
                {
                    if (Initialize(syx.Data))
                        Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                }
                else
                {
                    InitializeField(syx);
                }
            }
            else if ((syx.Address & 0xFFFFFF00) == (Address & 0xFFFFFF00))
            {
                InitializeField(syx);
            }
            //else if ((syx.Address & 0xF000FF00) == (Address &0xF000FF00))
            //{
            //    if(syx.Data.Length == 4)
            //    InitializeField(syx);
            //}
            else
            {
                //Debug.Print($"[{GetType().Name}.{nameof(SystemExclusiveReceived)}] Unhandled addres: [{syx.Address}]");
            }
        }

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
        /// <param name="transmit">A <see cref="bool"/> to determin if the property has to be transmitted.</param>
        /// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "", bool transmit = true)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if(transmit)
            {
                if(IsInitialized)
                {
                    if(!string.IsNullOrEmpty(propertyName))
                        TransmitProperty(propertyName, null);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified indexer property.
        /// </summary>
        /// <param name="index">An <see cref="int"/> specifying the index of the indexer property that is changed.</param>
        /// <param name="transmit">A <see cref="bool"/> to determin if the property has to be transmitted.</param>
        /// <remarks><i>If no property name is specified, the actual name of the property in code is used.</i></remarks>
        protected void NotifyIndexerPropertyChanged(int index, bool transmit = true)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));

            if (transmit)
            {
                if (IsInitialized)
                {
                    TransmitProperty("Item", index);
                }
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides <see cref="ToString"/> to return the name of the <see cref="IntegraBase{T}"/> inheriting class.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Debug

        /// <summary>
        /// Prints the property values of the <see cref="IntegraBase{T}"/> derived class.
        /// </summary>
        [Conditional("DEBUG")]
        public void DebugPrint()
        {
            string caption = $"{GetType().Name.ToUpper()} PROPERTY VALUES";

            Debug.WriteLine(new string('-', caption.Length));
            Debug.WriteLine(caption);
            Debug.WriteLine(new string('-', caption.Length));

            PropertyInfo[] propertyInfo = GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);

            foreach (var propertyName in propertyInfo)
            {
                // TODO: Raises exception on indexer property
                var propertyValue = propertyName.GetValue(this);

                if (propertyValue != null)
                {
                    if (propertyName.PropertyType.IsEnum)
                    {
                        Debug.WriteLine($"- {propertyName.Name} = {propertyValue.GetType().GetMember(propertyValue.ToString()).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? propertyValue.ToString()}");
                    }
                    else
                    {
                        Debug.WriteLine($"- {propertyName.Name} = {propertyValue.ToString()}");
                    }
                }
                else
                {
                    Debug.WriteLine($"- {propertyName.Name} = NULL");
                }
            }

            Debug.WriteLine(new string('-', caption.Length));
            Debug.WriteLine("");
        }

        #endregion

        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }

        #region Database
        //TODO: Remove to data access layer
        public virtual void Save()
        {
            //DataAccess.Save(this);

            if (GetType() != typeof(StudioSetMidi) && GetType() != typeof(StudioSet) && GetType() != typeof(StudioSetCommon))
                return;

            List<SQLParameter> parameters = new List<SQLParameter>();

            

            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            
            foreach(var property in properties)
            {
                Type type = property.PropertyType;

                if (type == typeof(Tone))
                    continue;

                if(type.GetInterfaces().Contains(typeof(IIntegraDataClass)))
                {
                    // The property is of type IntegraBase<T>
                    Debug.Print($"SAVE --> [{GetType().Name}] --> SAVE --> {property.Name}");

                    // Prevent property null values in case of StudioSetPart where not all properties are initialized
                    // Only one of five tone properties are initialized
                    if (property.GetValue(this) == null)
                        continue;

                    // Invoke the save method on the property
                    ((IIntegraDataClass)property.GetValue(this)).Save();
                }
                else if(property.GetCustomAttribute(typeof(Offset)) != null)
                {
                    // The property is part of this class and has an offset attribute
                    // Handle saving of the property
                    Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <{type}>");

                    Offset index = (Offset)property.GetCustomAttribute(typeof(Offset));

                    if(type == typeof(string))
                    {
                        // The property is of type string
                        Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <string>");

                        parameters.Add(new SQLParameter(type, property.Name, property.GetValue(this)));
                    }
                    else if(type.IsArray)
                    {
                        // The property is an array
                        Array propertyArray = (Array)property.GetValue(this);

                        // Get the type of elements in the array
                        Type propertyArrayType = propertyArray.GetType().GetElementType();

                        if(propertyArrayType == typeof(byte))
                        {
                            // The array is of type byte
                            for (int i = 0; i < propertyArray.Length; i++)
                            {
                                Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <byte[{i}]>");

                                // Property name is "<Property Name>#" where '#' is the index
                                parameters.Add(new SQLParameter(propertyArrayType, property.Name + (i), property.GetValue(this)));

                            }

                        }
                        else if (propertyArrayType == typeof(int))
                        {
                            // The array is of type int
                            for (int i = 0; i < propertyArray.Length; i++)
                            {
                                Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <int[{i}]>");

                                // Property name is "<Property Name>#" where '#' is the index
                                parameters.Add(new SQLParameter(propertyArrayType, property.Name + (i), property.GetValue(this)));
                            }
                        }
                    }
                    else if(type == typeof(bool))
                    {
                        // The property is of type bool
                        Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <bool>");
                        parameters.Add(new SQLParameter(type, property.Name, property.GetValue(this)));
                    }
                    else if (type == typeof(short))
                    {
                        Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <short>");
                        parameters.Add(new SQLParameter(type, property.Name, property.GetValue(this)));
                    }
                    else if (type == typeof(int))
                    {
                        if (property.GetIndexParameters().Length > 0)
                        {
                            // The property is an indexer property of type int
                            // Get the values from the backing field
                            Offset offset = (Offset)property.GetCustomAttribute(typeof(Offset));
                            FieldInfo field = GetCachedField(offset.Value);

                            // Create an array of the backing field to get the data length
                            Array propertyArray = (Array)field.GetValue(this);

                            for (int i = 0; i < propertyArray.Length; i++)
                            {
                                Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <int[{i}]>");

                                // Property name is "Item#" where '#' is the index
                                parameters.Add(new SQLParameter(type, property.Name + (i), property.GetValue(this, new object[] { i})));
                            }
                        }
                        else
                        {
                            // The property is of type int
                            Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <int>");
                            if (type.IsEnum)
                            {
                                Type enumType = Enum.GetUnderlyingType(type);

                                //object underlyingValue = Convert.ChangeType(property.GetValue(this), Enum.GetUnderlyingType(property.GetValue(this).GetType()));
                                parameters.Add(new SQLParameter(type, property.Name, Convert.ChangeType(property.GetValue(this), enumType)));

                            }
                            else
                            {
                                parameters.Add(new SQLParameter(type, property.Name, property.GetValue(this)));
                            }
                        }
                    }
                    else
                    {
                        if (property.GetIndexParameters().Length > 0)
                        {
                            // The property is an indexer property of type byte
                            // Get the values from the backing field
                            Offset offset = (Offset)property.GetCustomAttribute(typeof(Offset));
                            FieldInfo field = GetCachedField(offset.Value);

                            // Create an array of the backing field to get the data length
                            Array propertyArray = (Array)field.GetValue(this);

                            for (int i = 0; i < propertyArray.Length; i++)
                            {
                                Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <byte[{i}]>");

                                // Property name is "Item#" where '#' is the index
                                parameters.Add(new SQLParameter(type, property.Name + (i), property.GetValue(this, new object[] { i })));
                            }
                        }
                        else
                        {
                            // The property is of type byte
                            Debug.Print($"SAVE --> [{GetType().Name}] {property.Name} <byte>");
                            if (type.IsEnum)
                            {
                                Type enumType = Enum.GetUnderlyingType(type);
                                
                                //object underlyingValue = Convert.ChangeType(property.GetValue(this), Enum.GetUnderlyingType(property.GetValue(this).GetType()));
                                parameters.Add(new SQLParameter(type, property.Name, Convert.ChangeType(property.GetValue(this), enumType)));
                            }
                            else
                            {
                                parameters.Add(new SQLParameter(type, property.Name, property.GetValue(this)));
                            }
                        }
                    }
                    

                }

            }


            if (GetType() == typeof(StudioSet))
                return;

            DataAccess.Save(this, parameters, GetType().GetInterfaces().Contains(typeof(IIntegraPartial)));
        }

        protected bool _TypeCacheInitialized = false;
        protected List<SQLData> _TypeCache = new List<SQLData>();

        protected virtual void InitializeParameterCache()
        {
            if (_TypeCacheInitialized)
            {
                return;
            }

            Debug.Print($"[{GetType().Name}.{nameof(InitializeParameterCache)}]");

            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            //FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                Type type = property.PropertyType;

                // TODO: Ignore attribute?
                if (type == typeof(Tone))
                    continue;

                if (type.GetInterfaces().Contains(typeof(IIntegraDataClass)))
                {
                    // The property is a IntegraBase<T>
                    _TypeCache.Add(new SQLData(0, typeof(IIntegraDataClass), 0, property.Name));
                }
                else if (property.GetCustomAttribute(typeof(Offset)) != null)
                {
                    // The property is part of this class and has an offset attribute
                    // Handle saving of the property
                    Offset index = (Offset)property.GetCustomAttribute(typeof(Offset));

                    if (type == typeof(string))
                    {
                        // The property is of type string
                        // Get the length of the backing field byte array
                        var fieldInfo = GetCachedField(index.Value);
                        Array fieldArray = (Array)fieldInfo.GetValue(this);
                        
                        _TypeCache.Add(new SQLData(index.Value, typeof(string), fieldArray.Length, property.Name));
                    }
                    else if (type.IsArray)
                    {
                        // The property is an array
                        Array propertyArray = (Array)property.GetValue(this);

                        // Get the type of elements in the array
                        Type propertyArrayType = propertyArray.GetType().GetElementType();


                        if (propertyArrayType == typeof(byte))
                        {
                            _TypeCache.Add(new SQLData(index.Value, typeof(byte[]), propertyArray.Length, property.Name));

                        }
                        else if (propertyArrayType == typeof(int))
                        {
                            _TypeCache.Add(new SQLData(index.Value, typeof(int[]), propertyArray.Length, property.Name));
                        }
                    }
                    else if (type == typeof(short))
                    {
                        _TypeCache.Add(new SQLData(index.Value, typeof(short), 2, property.Name));
                    }
                    else if (type == typeof(bool))
                    {
                        // The property is of type bool
                        _TypeCache.Add(new SQLData(index.Value, typeof(bool), 1, property.Name));
                    }
                    else if (type == typeof(int))
                    {
                        if (property.GetIndexParameters().Length > 0)
                        {
                            // The property is an indexer property of type int
                            // Get the values from the backing field
                            Offset offset = (Offset)property.GetCustomAttribute(typeof(Offset));
                            FieldInfo field = GetCachedField(offset.Value);

                            // Create an array of the backing field to get the data length
                            Array propertyArray = (Array)field.GetValue(this);

                            _TypeCache.Add(new SQLData(index.Value, typeof(int[]), propertyArray.Length, property.Name));
                        }
                        else
                        {
                            //// The property is of type int
                            //if (type.IsEnum)
                            //{
                            //    Type enumType = Enum.GetUnderlyingType(type);

                            //    //object underlyingValue = Convert.ChangeType(property.GetValue(this), Enum.GetUnderlyingType(property.GetValue(this).GetType()));
                            //    _TypeCache.Add(new SQLParameter(index.Value, type, property.Name, Convert.ChangeType(property.GetValue(this), enumType)));

                            //}
                            //else
                            //{
                            _TypeCache.Add(new SQLData(index.Value, typeof(int), 1, property.Name));
                            //}
                        }
                    }
                    else
                    {
                        if (property.GetIndexParameters().Length > 0)
                        {
                            // The property is an indexer property of type byte
                            // Get the values from the backing field
                            Offset offset = (Offset)property.GetCustomAttribute(typeof(Offset));
                            FieldInfo field = GetCachedField(offset.Value);

                            // Create an array of the backing field to get the data length
                            Array propertyArray = (Array)field.GetValue(this);

                            _TypeCache.Add(new SQLData(index.Value, typeof(byte), propertyArray.Length, property.Name));
                        }
                        else
                        {
                            //// The property is of type byte
                            //if (type.IsEnum)
                            //{
                            //    Type enumType = Enum.GetUnderlyingType(type);

                            //    //object underlyingValue = Convert.ChangeType(property.GetValue(this), Enum.GetUnderlyingType(property.GetValue(this).GetType()));
                            //    _TypeCache.Add(new SQLParameter(index.Value, type, property.Name, Convert.ChangeType(property.GetValue(this), enumType)));
                            //}
                            //else
                            //{
                            _TypeCache.Add(new SQLData(index.Value, typeof(byte), 1, property.Name));
                            //}
                        }
                    }
                }
            }

            _TypeCacheInitialized = true;
        }

        public List<SQLData> Parameters
        {
            get 
            {
                if (!_TypeCacheInitialized)
                    InitializeParameterCache();

                return _TypeCache; 
            }
        }

        public virtual void Truncate()
        {
            // TODO: Remove temporary exclusion
            if (GetType() != typeof(StudioSetMidi) && GetType() != typeof(StudioSet) && GetType() != typeof(StudioSetCommon) && GetType() != typeof(Session))
                return;

            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (property.PropertyType.GetInterfaces().Contains(typeof(IIntegraDataClass)))
                {

                    ((IIntegraDataClass)GetType().GetProperty(property.Name).GetValue(this)).Truncate();
                }
            }

            DataAccess.Truncate(this);
        }

        public virtual void Load(int id)
        {
            // TODO: Remove temporary exclude
            if (GetType() != typeof(StudioSetMidi) && GetType() != typeof(StudioSet) && GetType() != typeof(StudioSetCommon))
                return;

            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);

            foreach (var property in Parameters)
            {
                if (property.Type == typeof(IIntegraDataClass))
                {
                    
                    ((IIntegraDataClass)GetType().GetProperty(property.Name).GetValue(this)).Load(id);
                }
            }


            int rows = DataAccess.Load(this, id);

            if (rows > 0)
            {
                foreach (var item in Parameters)
                {
                    if (item.Type != typeof(IIntegraDataClass))
                    {
                        GetType().GetProperty(item.Name).SetValue(this, item.Value);
                    }
                }
            }
        }
        #endregion
    }

}
