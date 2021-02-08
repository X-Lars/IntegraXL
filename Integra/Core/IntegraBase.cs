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
        /// Stores all instance fields decorated with the <see cref="Offset"/> attribute.
        /// </summary>
        /// <remarks><i>[<see cref="IntegraBase{T}"/>, [<see cref="ushort"/>, <see cref="FieldInfo"/>]] where <see cref="ushort"/> is the offset address.</i></remarks>
        private static Dictionary<ushort, FieldInfo> _Fields = new Dictionary<ushort, FieldInfo>();

        /// <summary>
        /// Stores all instance properties decorated with the <see cref="Offset"/> attribute.
        /// </summary>
        /// <remarks><i>[<see cref="IntegraBase{T}"/>, [<see cref="ushort"/>, <see cref="PropertyInfo"/>]] where <see cref="ushort"/> is the offset address.</i></remarks>
        private static Dictionary<ushort, PropertyInfo> _Properties = new Dictionary<ushort, PropertyInfo>();

        /// <summary>
        /// Stores all non virtual instance references matching the database associated tables.
        /// </summary>
        /// <remarks><i>[<see cref="IntegraBase{T}"/>, [<see cref="string"/>, <see cref="IIntegraDataClass"/>]] where <see cref="string"/> is the property and table name.</i></remarks>
        private static Dictionary<string, IIntegraDataClass> _References = new Dictionary<string, IIntegraDataClass>();

        /// <summary>
        /// Tracks whether the data structure is initialized.
        /// </summary>
        private bool _IsInitialized = false;
      
        #endregion


        #region Constructor

        /// <summary>
        /// Creates a new <see cref="IntegraBase{T}"/> instance.
        /// </summary>
        /// <remarks><i>Default constructor for dynamic instance creation by reflection.</i></remarks>
        public IntegraBase() 
        {
            if (!_IsCached)
                InitializeCache();
        }

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
            Debug.Print($"[{GetType().Name}] {address}");

            Address = address;
            Requests.AddRange(requests);

            // Name defaults to type name
            Name = GetType().Name;
           
            Initialize();
        }

        #endregion

        #region Properties

        private int _ID;

        /// <summary>
        /// Gets the ID of the instance used for data storage.
        /// </summary>
        /// <remarks><i>Defaults to return the <see cref="Session.ID"/>.</i></remarks>
        public int ID 
        {
            get { return Device.Session.ID; }
            internal set
            {
                _ID = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the name of the instance.
        /// </summary>
        /// <remarks><i>Defaults to the instance type name.</i></remarks>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the base address reference of the data structure to the INTEGRA-7.
        /// </summary>
        internal protected IntegraAddress Address { get; set; }

        /// <summary>
        /// Gets the list of requests required to initialize the data structure.
        /// </summary>
        internal protected List<IntegraRequest> Requests { get; private set; } = new List<IntegraRequest>();

        private static bool _IsCached = false;
        /// <summary>
        /// Gets whether the instance is initialized with data.
        /// </summary>
        public virtual bool IsInitialized
        {
            get { return _IsInitialized; }
            internal protected set
            {
                if (_IsInitialized != value)
                {
                    _IsInitialized = value;

                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods


        /// <summary>
        /// Initializes the instance by hooking up to the device system exclusive event listener and request initialization from the INTEGRA-7.
        /// </summary>
        public virtual void Initialize()
        {
            if(!_IsCached)
                InitializeCache();

            if (Device._IsConnected)
            {
                Device.Instance.MidiInputDevice.SystemExclusiveReceived += SystemExclusiveReceived;

                // Non blocking initialization request
                Task.Factory.StartNew(() => Device.Instance.Initialize(this), TaskCreationOptions.LongRunning);
            }
            else
            {
                // Do initialization in the device connected event handler to prevent long running tasks
                Device.Connected += DeviceConnected;
            }
        }

        /// <summary>
        /// Initalizes the fields of the instance from the data part of a received system exclusive.
        /// </summary>
        /// <param name="data">The system exclusive data part.</param>
        /// <returns>True if the class is completely initialized.</returns>
        internal virtual bool Initialize(byte[] data)
        {
            if (!IsInitialized)
            {
                foreach (var field in GetFields())
                //foreach (var field in IntegraCache.GetFields<T>())
                {
                    if (field.Value.FieldType.IsArray)
                    {
                        // Create an array from the field to be able to loop through the field elements
                        Array array = (Array)field.Value.GetValue(this);

                        // Get the type of elements in the array
                        Type arrayType = array.GetType().GetElementType();

                        if (arrayType == typeof(byte))
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
                                byte[] bytes = new byte[4];

                                int offset = field.Key + (i * 4);

                                bytes[0] = (byte)((data[offset]) & 0x0F);
                                bytes[1] = (byte)((data[offset + 1]) & 0x0F);
                                bytes[2] = (byte)((data[offset + 2]) & 0x0F);
                                bytes[3] = (byte)((data[offset + 3]) & 0x0F);

                                if (BitConverter.IsLittleEndian)
                                    Array.Reverse(bytes);

                                array.SetValue(BitConverter.ToInt32(bytes, 0), i);
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
                        byte[] bytes = new byte[4];

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

                        for (int i = 0; i < bytes.Length; i++)
                        {


                            // data 145 key 257
                            //values[i] = data[field.Key + i];
                            bytes[i] = data[key + i];
                        }

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bytes);

                        field.Value.SetValue(this, BitConverter.ToInt32(bytes, 0));
                    }
                    else
                    {
                        int key = field.Key;
                        key = (((key & 0xFF00) >> 8) * 128) + (key & 0x00FF);
                        field.Value.SetValue(this, data[key]);
                    }
                }

                NotifyPropertyChanged(string.Empty, false);
                //DebugPrint();
            }

            return IsInitialized = true;
        }

        /// <summary>
        /// Reinitializes the data of the instance.
        /// </summary>
        public virtual void Reinitialize()
        {
            Console.WriteLine($"[{GetType().Name}.{nameof(Reinitialize)}]");
            IsInitialized = false;
            Initialize();
        }

        #region Methods: Private

        /// <summary>
        /// Generates the cache for all non public <see cref="Offset"/> decorated fields of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="IntegraBase{T}"/> type specifier.</typeparam>
        /// <remarks><i>Contains only fields associated with the INTEGRA-7.</i></remarks>
        private void InitializeCache()
        {
            if (_IsCached)
                return;

            Debug.Print($"[{GetType().Name}.{nameof(InitializeCache)}] {typeof(T).Name}");

            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                Offset offset = field.GetCustomAttribute<Offset>(false);

                if (offset != null)
                {
                    _Fields.Add(offset.Value, field);
                }
            }

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                Offset offset = property.GetCustomAttribute<Offset>(false);

                if (offset != null)
                {
                    _Properties.Add(offset.Value, property);
                }
            }

            _IsCached = true;
        }

        private List<IIntegraDataClass> GetReferences()
        {
            List<IIntegraDataClass> references = new List<IIntegraDataClass>();

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                // Exclude virtual references
                if (!property.GetMethod.IsVirtual || property.GetMethod.IsFinal)
                {
                    if (property.PropertyType.GetInterfaces().Contains(typeof(IIntegraDataClass)))
                    {
                        references.Add((IIntegraDataClass)property.GetValue(this));
                    }
                }
            }

            return references;
        }


        /// <summary>
        /// Gets the property with the specified offset.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="offset">The offset associated to the property.</param>
        /// <returns>The property at the specified offset.</returns>
        public static PropertyInfo Property(ushort offset)
        {
            return _Properties.ContainsKey(offset) ? _Properties[offset] : null;
        }

        /// <summary>
        /// Gets the offset of the specified property.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="name">The property name.</param>
        /// <returns>The offset of the property.</returns>
        private static ushort PropertyOffset(string name)
        {
            return _Properties.Where(v => v.Value.Name == name).FirstOrDefault().Key;
        }

        private static bool IsIntegraProperty(string name)
        {
            foreach(var property in _Properties.Values)
            {
                if (property.Name == name)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the field with the specified offset.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="offset">The offset associated to the field.</param>
        /// <returns>The field at the specified offset.</returns>
        public static FieldInfo Field(ushort offset)
        {
            return _Fields.ContainsKey(offset) ? _Fields[offset] : null;
        }

        /// <summary>
        /// Gets the field cache of the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <returns>The cache associated to <typeparamref name="T"/>.</returns>
        public static Dictionary<ushort, FieldInfo> GetFields()
        {
            return _Fields;
        }

        public static Dictionary<ushort, PropertyInfo> GetProperties()
        {
            return _Properties;
        }

        /// <summary>
        /// Initializes one or more fields from a received system exclusive message.
        /// </summary>
        /// <param name="syx">The received system exclusive message.</param>
        internal void InitializeField(IntegraSystemExclusive syx)
        {
            // The offset of the property to set
            uint offset = syx.Address - Address;

            // 0x10, 0x20, 0x30, 0x12 [systemExclusive.Address]
            // 0x10, 0x20, 0x30, 0x10 [this.Address]
            // ______________________ -
            // 0x00, 0x00, 0x00, 0x02 [offset]

            for (int syxOffset = 0/*, propertyOffset = 0*/; syxOffset < syx.Data.Length; syxOffset++/*, propertyOffset++*/)
            {
                FieldInfo field = Field((ushort)(offset + syxOffset));
                
                // Field not found, field is INTEGRA-7 reserved or item in an array
                if (field == null)
                {
                    // Get the nearest lower neighbour offset decorated field
                    field = _Fields.Where(v => v.Key < offset).OrderByDescending(k => k.Key).FirstOrDefault().Value;

                    // TODO: Multiple values from halfway the array
                    // TODO: Byte array?
                    if(field != null)
                    {
                        if(field.FieldType.IsArray)
                        {
                            // Nearest lower neighbour field is array
                            Array array = (Array)field.GetValue(this);
                            Type arrayType = array.GetType().GetElementType();

                            int arrayOffset = field.GetCustomAttribute<Offset>().Value;
                            // 00 04
                            if (arrayType == typeof(int))
                            {
                                // is in range of field array offset
                                if ((ushort)(offset) < arrayOffset + (array.Length * 4))
                                {
                                    // off 3
                                    Console.WriteLine((ushort)((offset - arrayOffset) / 4));

                                    byte[] values = new byte[4];

                                    //int p = (syx.Data.Length - fieldOffset) / 4;

                                    //// Working on
                                    //int l = syx.Data.Length - fieldOffset;
                                    Array.Copy(syx.Data, syxOffset, values, 0, 4);
                                        

                                    if (BitConverter.IsLittleEndian)
                                        Array.Reverse(values);

                                    array.SetValue(BitConverter.ToInt32(values, 0), (offset - arrayOffset) / 4);
                                    syxOffset += 4;
                                    NotifyPropertyChanged("Item[]", false);
                                    continue;
                                }
                            }
                        }
                    }
                    
                    // Field is not found, INTEGRA-7 reserved
                    continue;
                }

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
                            array.SetValue(syx.Data[syxOffset], i);

                            // Increment the field offset to the next system exclusive byte
                            syxOffset++;
                        }
                    }
                    else if (arrayType == typeof(int))
                    {
                        byte[] values = new byte[4];

                        int p = (syx.Data.Length - syxOffset) / 4;

                        // Working on
                        int l = syx.Data.Length - syxOffset;

                        for (int i = 0; i < p; i++)
                        {
                            Array.Copy(syx.Data, syxOffset, values, 0, 4);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(values);

                            array.SetValue(BitConverter.ToInt32(values, 0), i);
                            syxOffset += 4;
                        }
                    }
                    else
                    {
                        throw new Exception($"[{GetType().Name}.{nameof(Initialize)}] Unsupported array type {field.Name}");
                    }
                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValue(this, Convert.ToBoolean(syx.Data[syxOffset]));
                }
                else if (field.FieldType == typeof(int))
                {
                    byte[] values = syx.Data;


                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(values);

                    field.SetValue(this, BitConverter.ToInt32(values, 0));
                    syxOffset += 4;
                }
                else
                {
                    field.SetValue(this, syx.Data[syxOffset]);
                }

                // Retrieve the accompanied property with the same offset to raise the NotifyPropertyChanged event
                //PropertyInfo property = Property((ushort)(offset + propertyOffset));
                PropertyInfo property = Property((ushort)offset);

                if (property != null)
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

                // Property offset has to be realigned in case the field is an array
                //propertyOffset = syxOffset;
            }
        }

        /// <summary>
        /// Transmits a property value to the INTEGRA-7.
        /// </summary>
        /// <param name="offset">The offset of the property.</param>
        /// <param name="value">The value of the property.</param>
        private void TransmitProperty(ushort offset, byte[] value)
        {
            if (IsInitialized)
            {
                IntegraSystemExclusive systemExclusive = new IntegraSystemExclusive(this.Address, offset, value);

                Device.Instance.SendSystemExclusive(systemExclusive);
            }
        }

        /// <summary>
        /// Transmits an indexer property value to the INTEGRA-7.
        /// </summary>
        /// <param name="propertyName">The name of the indexer property.</param>
        /// <param name="index">The index of the property value.</param>
        private void TransmitIndexerProperty(string propertyName, int? index)
        {
            //Offset offset = GetPropertyOffset(propertyName);
            ushort offset = PropertyOffset(propertyName);
            //ushort offset = IntegraCache.PropertyOffset<T>(propertyName);

            //if (offset != null)
            //{
                if(Field(offset) != null)
                //if (IntegraCache.Field<T>(offset) != null)
                //if (_FieldOffsetCache.ContainsKey(offset.Value))
                {
                    FieldInfo field = Field(offset);
                    //FieldInfo field = IntegraCache.Field<T>(offset);


                    //FieldInfo field = _FieldOffsetCache[offset.Value];

                    if (field.FieldType.IsArray)
                    {
                        // Create an array from the field to be able to loop through the field values
                        Array fieldArray = (Array)field.GetValue(this);

                        // Get the type of elements in the array
                        Type fieldArrayType = fieldArray.GetType().GetElementType();

                        if(fieldArrayType == typeof(byte))
                        {
                            byte[] array = (byte[])field.GetValue(this);

                            TransmitProperty(offset, array);
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

                            TransmitProperty((ushort)(offset + (index * 4)), values);
                        }
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        TransmitProperty(offset, new byte[] { (bool)field.GetValue(this) ? (byte)1 : (byte)0 });
                    }
                    else if (field.FieldType.IsEnum)
                    {
                        object enumValue = Convert.ChangeType(field.GetValue(this), Enum.GetUnderlyingType(field.FieldType));

                        TransmitProperty(offset, new byte[] { (byte)Convert.ChangeType(enumValue, TypeCode.Byte) });
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        byte[] values = BitConverter.GetBytes((int)field.GetValue(this));

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(values);

                        TransmitProperty(offset, new byte[] { values[0], values[1], values[2], values[3] });
                    }
                    else
                    {
                        TransmitProperty(offset, new byte[] { (byte)field.GetValue(this) });
                    }
                }
            //}
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Initializes the instance on device connection.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void DeviceConnected(object sender, EventArgs e)
        {
            Device.Connected -= DeviceConnected;
            //Initialize();
            Device.Instance.MidiInputDevice.SystemExclusiveReceived += SystemExclusiveReceived;
            Task.Factory.StartNew(() => Device.Instance.Initialize(this), TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Eventhandler that handles the <see cref="Device.SystemExclusiveReceived"/>.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="SystemExclusiveMessageEventArgs"/> containing event data.</param>
        /// <remarks><i>The event handler is attached inside the <see cref="Device.Initialize{T}(IntegraBase{T})"/> method invoked by <see cref="Initialize"/>.</i></remarks>
        protected virtual void SystemExclusiveReceived(object sender, SystemExclusiveMessageEventArgs e)
        {
            IntegraSystemExclusive syx = new IntegraSystemExclusive(e.Message);



            if(!IsInitialized)
            {
                if (syx.Address == Address)
                {
                    // Exact match
                    if (syx.Data.Length == Requests[0].Size)
                    {
                        if (Initialize(syx.Data))
                        {
                            // TODO: Remove from Base to Partial collection class
                            // Set the Part property for IIntegraPartial implementing classes which is only present after initialization
                            //if (typeof(T).GetInterfaces().Contains(typeof(IIntegraPartial)))
                            //{
                            //    ((IIntegraPartial)this).Part = (IntegraParts)((Address & 0x00000F00) >> 8);
                            //}

                            Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
                        }
                    }
                    else
                    {
                        InitializeField(syx);
                    }
                }
            }
            else
            {
                //TODO: Check MFX 0xFFFFFF00 won't catch 00 00 01 11 ? or is it catched because of receiving a complete array
                if ((syx.Address & 0xFFFFFF00) == (Address & 0xFFFFFF00))
                {
                    InitializeField(syx);
                }
            }

            //if(syx.Address == Address)
            //{
            //    // Exact match
            //    if (syx.Data.Length == Requests[0].Size)
            //    {
            //        if (Initialize(syx.Data))
            //        {
            //            // Set the Part property for IIntegraPartial implementing classes which is only present after initialization
            //            if(typeof(T).GetInterfaces().Contains(typeof(IIntegraPartial)))
            //            {
            //                ((IIntegraPartial)this).Part = (IntegraParts)((Address & 0x00000F00) >> 8);
            //            }

            //            Device.Instance.ReportProgress(this, new StatusMessage($"Initializing {Name}", "Initialized", 100, "Done"));
            //        }
            //    }
            //    else
            //    {
            //        InitializeField(syx);
            //    }
            //}
            //// TODO: Check MFX 0xFFFFFF00 won't catch 00 00 01 11 ?
            //else if ((syx.Address & 0xFFFFFF00) == (Address & 0xFFFFFF00))
            //{
            //    InitializeField(syx);
            //}
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
        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "", bool transmit = true)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (!IsIntegraProperty(propertyName))
                return;

            if (transmit)
            {
                if (IsInitialized)
                {
                    if (!string.IsNullOrEmpty(propertyName))
                        TransmitIndexerProperty(propertyName, null);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified indexer property.
        /// </summary>
        /// <param name="index">An <see cref="int"/> specifying the index of the indexer property that is changed.</param>
        /// <param name="transmit">A <see cref="bool"/> to determin if the property has to be transmitted.</param>
        protected void NotifyIndexerPropertyChanged(int index, bool transmit = true)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));

            if (!IsIntegraProperty("Item"))
                return;

            if (transmit)
            {
                if (IsInitialized)
                {
                    TransmitIndexerProperty("Item", index);
                }
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Overrides <see cref="ToString"/> to return the name of the instance.
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



        #region IIntegraDataClass

        // TODO: Remove temporary exclude
        private static Type[] _Types = new Type[]
        {
            typeof(StudioSet),
            typeof(StudioSetCommon),
            typeof(StudioSetMidi),
            typeof(StudioSetPart),
            typeof(StudioSetPartEQ),
            typeof(TemporaryTone),
            typeof(ToneMFX),
            typeof(StudioSetMasterEQ),
            typeof(StudioSetCommonChorus),
            typeof(StudioSetCommonReverb),
            typeof(StudioSetCommonMotionalSurround),
            typeof(SuperNATURALAcousticTone),
            typeof(SuperNATURALAcousticToneCommon),
            typeof(SuperNATURALSynthTone),
            typeof(SuperNATURALSynthToneCommon),
            typeof(SuperNATURALSynthTonePartial),
            typeof(SuperNATURALDrumKit),
            typeof(SuperNATURALDrumKitCommon),
            typeof(SuperNATURALDrumKitNote),
            typeof(PCMSynthTone),
            typeof(PCMSynthToneCommon),
            typeof(PCMSynthTonePMT),
            typeof(PCMSynthTonePartial),
            typeof(PCMSynthToneCommon02),
            typeof(PCMDrumKit),
            typeof(PCMDrumKitCommon),
            typeof(DrumKitCommonCompEQ),
            typeof(PCMDrumKitPartial),
            typeof(PCMDrumKitCommon02)
        };

        public virtual void Update()
        {
            // TODO: Remove temporary exclude
            if (!_Types.Contains(GetType()))
                return;

            foreach (var reference in GetReferences())
            {
                reference.Update();
            }

            DataAccess.Update(this);

        }

        public virtual void Insert()
        {
            // TODO: Remove temporary exclude
            if (!_Types.Contains(GetType()))
                return;

            foreach (var reference in GetReferences())
            {
                reference.Insert();
            }

            DataAccess.Insert(this);
        }

        public virtual void Delete()
        {
            // TODO: Remove temporary exclude
            if (!_Types.Contains(GetType()))
                return;

            foreach (var reference in GetReferences())
            {
                reference.Delete();
            }

            DataAccess.Delete(this);
        }

        public virtual void Truncate()
        {
            // TODO: Remove temporary exclusion
            if (!_Types.Contains(GetType()))
                return;

            foreach (var reference in GetReferences())
            {
                reference.Truncate();
            }

            DataAccess.Truncate(this);
        }

        public virtual void Select(int id)
        {
            // TODO: Remove temporary exclude
            if (!_Types.Contains(GetType()))
                return;


            // Prevent property transmission
            IsInitialized = false;

            int result = DataAccess.Select(this, id);

            if(result == 1)
            {

                byte[] data = GetUpdateData();

                if(data != null)
                    Device.Instance.SendSystemExclusive(new IntegraSystemExclusive(Address, 0, GetUpdateData()));

                IsInitialized = true;
                NotifyPropertyChanged(string.Empty, false);
            }
        }
        #endregion

        public byte[] GetUpdateData()
        {
            if (Requests.Count != 1)
                return null;

            byte[] bytes = new byte[Requests[0]];

            for (int i = 0; i < bytes.Length; i++)
            {
                FieldInfo field = Field((ushort)i);

                if (field != null)
                {
                    if (field.FieldType.IsArray)
                    {
                        Array array = (Array)field.GetValue(this);
                        Type arrayType = array.GetType().GetElementType();

                        if (arrayType == typeof(int))
                        {
                            int[] intArray = (int[])field.GetValue(this);

                            for (int j = 0; j < intArray.Length; j++)
                            {
                                byte[] values = new byte[4];
                                values = BitConverter.GetBytes(intArray[j]);

                                if (BitConverter.IsLittleEndian)
                                    Array.Reverse(values);

                                Array.Copy(values, 0, bytes, i, 4);
                                i += 4;
                            }
                        }
                        else if (arrayType == typeof(byte))
                        {
                            byte[] byteArray = (byte[])field.GetValue(this);

                            for (int j = 0; j < byteArray.Length; j++)
                            {
                                bytes[i] = byteArray[j];
                                i++;
                            }
                        }
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        bytes[i] = ((bool)field.GetValue(this)) == true ? (byte)1 : (byte)0;
                    }
                    else if (field.FieldType.IsEnum)
                    {
                        bytes[i] = (byte)field.GetValue(this);
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        byte[] values = BitConverter.GetBytes((int)field.GetValue(this));

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(values);

                        Array.Copy(values, 0, bytes, i, 4);
                        i += 4;

                    }
                    else
                    {
                        bytes[i] = (byte)field.GetValue(this);
                    }
                }
                else
                {
                    bytes[i] = 0;
                }
            }

            return bytes;
        }
    }

}
