﻿using IntegraXL.Extensions;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    public abstract class IntegraModel<TModel> : IntegraModel, INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Tracks wheter <see cref="TModel"/> type is cached.
        /// </summary>
        private static bool _IsCached = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraModel{TModel}"/> instance.<br/>
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        /// <remarks><i>
        /// - All derived classes require an <see cref="IntegraAttribute"/>.<br/>
        /// - Sets the address, request and size from the <see cref="IntegraAttribute"/>.<br/>
        /// - Collections are responsible for there own requests.<br/>
        /// - Initializes the name with the type name.<br/>
        /// - Connects the model to the device.<br/>
        /// - Caches the model's <see cref="OffsetAttribute"/> decorated fields and properties.<br/>
        /// </i></remarks>
        internal IntegraModel(Integra device, bool connect = true) : base(device, connect) 
        {
            if (!IsCached)
                IsCached = this.Cache();
        }

        internal IntegraModel(IntegraModel<TModel> instance) : base(instance) { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets wheter the <see cref="TModel"/> type is cached.
        /// </summary>
        public static bool IsCached
        {
            get { return _IsCached; }
            set
            {
                if (_IsCached != value)
                    _IsCached = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the model's data to an <see cref="byte"/> array.
        /// </summary>
        /// <returns>An ordered byte array containing the model's data.</returns>
        /// <exception cref="IntegraException"></exception>
        /// <remarks><i>The array is ordered by field offset and can be used as system exclusive data part.</i></remarks>
        public override byte[] Serialize()
        {
            if (!IsInitialized)
                throw new IntegraException($"[{nameof(IntegraModel)}<{typeof(TModel).Name}>.{nameof(Serialize)}()]\n" +
                                           $"Serialization of uninitialized model.");

            var fields = this.CachedFields().OrderBy(x => x.Key).ToArray() ??
                throw new IntegraException($"[{nameof(IntegraModel)}<{typeof(TModel)}>.{nameof(Serialize)}()]\n" +
                                           $"Uncached models cannot be serialized.");

            List<byte> values = new();

            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo field = fields[i].Value;

                if (field.FieldType == typeof(byte))
                {
                    object? value = field.GetValue(this);
                    Debug.Assert(value != null);

                    values.Add((byte)value);
                }
                else if (field.FieldType == typeof(bool))
                {
                    object? value = field.GetValue(this);
                    Debug.Assert(value != null);

                    values.Add((bool)value ? (byte)1 : (byte)0);
                }
                else if (field.FieldType == typeof(int))
                {
                    object? value = field.GetValue(this);
                    Debug.Assert(value != null);

                    byte[] bytes = BitConverter.GetBytes((int)value);

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(bytes);

                    for (int b = 0; b < 4; b++)
                    {
                        values.Add(bytes[b]);
                    }
                }
                else if (field.FieldType.IsEnum)
                {
                    Type type = field.FieldType.GetEnumUnderlyingType();

                    if (type == typeof(byte))
                    {
                        object? value = field.GetValue(this);
                        Debug.Assert(value != null);

                        values.Add((byte)value);
                    }
                    else if (type == typeof(int))
                    {
                        object? value = field.GetValue(this);
                        Debug.Assert(value != null);

                        byte[] bytes = BitConverter.GetBytes((int)value);

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bytes);

                        for (int b = 0; b < 4; b++)
                        {
                            values.Add(bytes[b]);
                        }
                    }
                    else if (type == typeof(short))
                    {
                        object? value = field.GetValue(this);
                        Debug.Assert(value != null);

                        byte[] bytes = BitConverter.GetBytes((short)value);

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bytes);

                        for (int b = 0; b < 2; b++)
                        {
                            values.Add(bytes[b]);
                        }

                    }
                    else
                    {
                        throw new NotImplementedException($"{GetType().Name}.{nameof(TransmitProperty)}] {field.FieldType.Name} {field.Name}");
                    }
                }
                else if (field.FieldType.IsArray)
                {
                    Array? array = field.GetValue(this) as Array;

                    Debug.Assert(array != null);

                    Type? type = array.GetType().GetElementType();

                    Debug.Assert(type != null);

                    if (type == typeof(byte))
                    {
                        for (int a = 0; a < array.Length; a++)
                        {
                            object? value = array.GetValue(a);
                            Debug.Assert(value != null);
                            values.Add((byte)value);
                        }
                    }
                    else if (type == typeof(int))
                    {
                        for (int a = 0; a < array.Length; a++)
                        {
                            int[]? value = field.GetValue(this) as int[];

                            Debug.Assert(value != null);

                            byte[] bytes = BitConverter.GetBytes(value[a]);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(bytes);

                            for (int b = 0; b < 4; b++)
                            {
                                values.Add(bytes[b]);
                            }

                        }
                    }
                    else
                    {
                        throw new IntegraException($"[{nameof(Integra)}.{nameof(Serialize)}()]\n" +
                                                   $"{GetType().Name}: Serialization of unimplemented type {field.FieldType.Name} {field.Name}");
                    }
                }
            }

            Debug.Print($"[{nameof(IntegraModel)}.{nameof(Serialize)}<{GetType().Name}>] {string.Join(" ", (values.ToArray()).Select(x => string.Format("{0:X2}", x)))}");

            return values.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="index"></param>
        /// <exception cref="NotImplementedException"/>
        /// <remarks><i>
        /// - Transmits the property's backing field.<br/>
        /// - The property is responsible for MIDI serialization of the backing field.<br/>
        /// </i></remarks>
        internal void TransmitProperty(string propertyName, int? index = null)
        {
            //if (string.IsNullOrEmpty(propertyName))
            //    return;
            Debug.Assert(!string.IsNullOrEmpty(propertyName));
            Debug.Assert(this.CachedProperties() != null);
            //if (this.CachedProperties() == null)
            //    return;

            if (this.CachedProperties().TryGetValue(propertyName, out int offset))
            {
                FieldInfo field = this.CachedFields()[offset];

                if (field != null)
                {
                    byte[] data = Array.Empty<byte>();

                    if (field.FieldType == typeof(byte))
                    {
                        object? value = field.GetValue(this);
                        Debug.Assert(value != null);
                        data = new byte[] { (byte)value };
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        object? value = field.GetValue(this);
                        Debug.Assert(value != null);

                        data = new byte[] { (bool)value ? (byte)1 : (byte)0 };
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        object? value = field.GetValue(this);
                        Debug.Assert(value != null);

                        byte[] bytes = BitConverter.GetBytes((int)value);

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bytes);

                        data = bytes;
                    }
                    else if (field.FieldType.IsEnum)
                    {
                        Type type = field.FieldType.GetEnumUnderlyingType();

                        if (type == typeof(byte))
                        {
                            object? value = field.GetValue(this);
                            Debug.Assert(value != null);
                            data = new byte[] { (byte)value };
                        }
                        else if(type == typeof(int))
                        {
                            object? value = field.GetValue(this);
                            Debug.Assert(value != null);
                            byte[] bytes = BitConverter.GetBytes((int)value);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(bytes);

                            data = bytes;
                        }
                        else if (type == typeof(short))
                        {
                            object? value = field.GetValue(this);
                            Debug.Assert(value != null);

                            byte[] bytes = BitConverter.GetBytes((short)value);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(bytes);

                            data = bytes;

                        }
                        else
                        {
                            throw new NotImplementedException($"{GetType().Name}.{nameof(TransmitProperty)}] {field.FieldType.Name} {field.Name}");
                        }
                    }
                    else if (field.FieldType.IsArray)
                    {
                        Array? array = field.GetValue(this) as Array;

                        Debug.Assert(array != null);

                        Type? type = array.GetType().GetElementType();

                        Debug.Assert(type != null);

                        if (type == typeof(byte))
                        {
                            data = new byte[array.Length];

                            Array.Copy(array, data, array.Length);
                        }
                        else if (type == typeof(int))
                        {
                            Debug.Assert(index != null);
                            int[]? value = field.GetValue(this) as int[];

                            Debug.Assert(value != null);

                            byte[] bytes = BitConverter.GetBytes(value[(int)index]);
                             
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(bytes);

                            data = bytes;
                            offset += (int)(index * 4);
                        }
                        else
                        {
                            throw new NotImplementedException($"{GetType().Name}.{nameof(TransmitProperty)}] {field.FieldType.Name} {field.Name}");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException($"{GetType().Name}.{nameof(TransmitProperty)}] {field.FieldType.Name} {field.Name}");
                    }

                    Device.TransmitSystemExclusive(new IntegraSystemExclusive(Address, offset, data));
                }
            }
        }

        internal void ReceivedProperty(IntegraSystemExclusive systemExclusive)
        {
            //if (!IsCached)
            //    return;

            int offset = systemExclusive.Address - Address;

            // Offset 0x0100
            if (offset > 127)
                offset -= 128;

            var fields = this.CachedFields();
            int indexer = 0;
            for (int dataOffset = 0; dataOffset < systemExclusive.Data.Length; dataOffset++)
            {
                int fieldOffset = offset + dataOffset;

                fields.TryGetValue(fieldOffset, out FieldInfo? field);

                // TODO: should never be null?
                // TODO: Create cached property entry for each indexed property
                if(field == null)
                {
                    for (int i = fieldOffset; i > 0; i--)
                    {
                        if (!fields.Keys.Contains(i))
                        {
                            indexer++;
                        }
                        else
                        {
                            field = fields[i];
                            break;
                        }

                    }
                }


                if (field == null)
                    continue;

                if (field.FieldType == typeof(byte))
                {
                    field.SetValue(this, systemExclusive.Data[dataOffset]);

                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValue(this, Convert.ToBoolean(systemExclusive.Data[dataOffset]));

                }
                else if (field.FieldType == typeof(int))
                {
                    // TODO: Extension method
                    byte[] bytes = new byte[4];

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        bytes[i] = systemExclusive.Data[dataOffset + i];
                    }

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(bytes);

                    field.SetValue(this, BitConverter.ToInt32(bytes, 0));
                    //pro += 4;
                    dataOffset += 3;
                }
                else if (field.FieldType.IsEnum)
                {
                    Type type = field.FieldType.GetEnumUnderlyingType();

                    if (type == typeof(byte))
                    {
                        field.SetValue(this, systemExclusive.Data[dataOffset]);
                    }
                    else if (type == typeof(int))
                    {
                        byte[] bytes = new byte[4];

                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bytes[i] = systemExclusive.Data[dataOffset + i];
                        }

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bytes);

                        field.SetValue(this, BitConverter.ToInt32(bytes, 0));
                        //pro += 4;
                        dataOffset += 3;


                    }
                    else if (type == typeof(short))
                    {
                        byte[] bytes = new byte[2];

                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bytes[i] = systemExclusive.Data[dataOffset + i];
                        }

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bytes);

                        field.SetValue(this, BitConverter.ToInt16(bytes, 0));

                        dataOffset += 1;
                    }
                    else
                    {
                        throw new NotImplementedException($"{GetType().Name}.{nameof(ReceivedProperty)}] {field.FieldType.Name} {field.Name}");
                    }
                }
                else if (field.FieldType.IsArray)
                {
                    Array? array = field.GetValue(this) as Array;

                    Debug.Assert(array != null);

                    Type? arrayType = array.GetType().GetElementType();

                    Debug.Assert(arrayType != null);

                    if (arrayType == typeof(byte))
                    {
                        for (int a = 0; a < array.Length; a++)
                        {
                            array.SetValue(systemExclusive.Data[dataOffset], a);
                            dataOffset++;

                            if (dataOffset + 1 > systemExclusive.Data.Length)
                                break;
                        }
                    }
                    else if (arrayType == typeof(int))
                    {
                        // TODO: Check if MFX parameter byte and if possible to do conversion here so the backing paramater array will contain actual integers without the parameter prefix
                        byte[] bytes = new byte[4];

                        // Divide the remaining system exclusive data length by four bytes to get the number of parameters
                        int parameterCount = (int)((systemExclusive.Data.Length - dataOffset) / 4);

                        int index = 0;

                        if (array.Length > parameterCount)
                        {
                            // The remaining system exclusive data length is smaller than the array, use the number of remaining parameters as indexer
                            index = parameterCount;
                        }
                        else
                        {
                            // The remaining system exclusive data length is greather than the array, use the length of the array as indexer
                            index = array.Length;
                        }

                        for (int a = 0; a < index; a++)
                        {
                            Array.Copy(systemExclusive.Data, dataOffset, bytes, 0, 4);

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(bytes);

                            array.SetValue(BitConverter.ToInt32(bytes, 0), a);

                            // Increment the data offset by four bytes
                            dataOffset += 4;
                        }

                    }
                    else
                    {
                        throw new NotImplementedException($"{GetType().Name}.{nameof(ReceivedProperty)}] {field.FieldType.Name} {field.Name}");
                    }
                }
                else
                {
                    throw new NotImplementedException($"{GetType().Name}.{nameof(ReceivedProperty)}] {field.FieldType.Name} {field.Name}");
                }

                string property = this.CachedProperties().Where(x => x.Value == fieldOffset).FirstOrDefault().Key;

                if (!string.IsNullOrEmpty(property))
                {
                    Debug.Print($"RX [{GetType().Name}] {property} = {field.GetValue(this)} {GetUID():X4}");

                    base.NotifyPropertyChanged(property);
                    //base.NotifyPropertyChanged(string.Empty);
                }
            }

            base.NotifyPropertyChanged(string.Empty);
        }

        /// <summary>
        /// Initializes the model with the specified data.
        /// </summary>
        /// <param name="data">The data used to initialize the model.</param>
        /// <remarks><i>
        /// - Transmits the data to the physical device.<br/>
        /// - Initializes or overwrites the model's data.<br/>
        /// </i></remarks>
        internal virtual void Load(byte[] data)
        {
            IsInitialized = false;

            Debug.Print($"[{nameof(IntegraModel)}.{nameof(Load)}<{GetType().Name}>] {string.Join(" ", (data.Select(x => string.Format("{0:X2}", x))))}");
            Device.TransmitSystemExclusive(new IntegraSystemExclusive(Address, 0, data));

            Initialize(data);
        }

        #endregion

        #region Overrides: Model

        /// <summary>
        /// Sends the system exclusive request to initialize of the model.
        /// </summary>
        internal override void RequestInitialization()
        {
            foreach(var request in Requests)
            {
                Device.TransmitSystemExclusive(new IntegraSystemExclusive(Address, request));
            }
        }

        protected override void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
            if (!IsCached)
                return;

            if (e.SystemExclusive.Address == Address)
            {
                if (e.SystemExclusive.Data.Length == Size)
                {
                    // Model data received
                    if (Initialize(e.SystemExclusive.Data))
                    {
                        // Model is initialized
                    }
                }
                else
                {
                    // TODO: 
                    ReceivedProperty(e.SystemExclusive);
                }

            }
            else if (e.SystemExclusive.Address.InRange(Address, Address + Size))
            {

                // Parameter data received
                ReceivedProperty(e.SystemExclusive);
            }
        }

        
        /// <summary>
        /// Initializes the model with data.
        /// </summary>
        /// <param name="data">The data to initialize the model.</param>
        /// <returns>True if the model is initialized.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        internal override bool Initialize(byte[] data)
        {
            // TODO: Combine received property into the initialize method
            //if (!IsInitialized)
            //{
            double progress = 0;

            foreach (var field in this.CachedFields())
            {
                var fieldType = field.Value.FieldType;

                if (fieldType == typeof(byte))
                {
                    field.Value.SetValue(this, data[field.Key]);
                }
                else if (fieldType == typeof(bool))
                {
                    field.Value.SetValue(this, Convert.ToBoolean(data[field.Key]));
                }
                else if (fieldType == typeof(int))
                {
                    byte[] bytes = new byte[4];

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        bytes[i] = data[field.Key + i];
                    }

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(bytes);

                    field.Value.SetValue(this, BitConverter.ToInt32(bytes, 0));
                }
                else if (fieldType.IsEnum)
                {
                    Type type = field.Value.FieldType.GetEnumUnderlyingType();

                    if (type == typeof(byte))
                    {
                        field.Value.SetValue(this, data[field.Key]);
                    }
                    
                    else if(type == typeof(int))
                    {
                        byte[] bytes = new byte[4];

                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bytes[i] = data[field.Key + i];
                        }

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bytes);

                        field.Value.SetValue(this, BitConverter.ToInt32(bytes, 0));
                    }
                    else if (type == typeof(short))
                    {
                        byte[] bytes = new byte[2];

                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bytes[i] = data[field.Key + i];
                        }

                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(bytes);

                        field.Value.SetValue(this, BitConverter.ToInt16(bytes, 0));
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else if (fieldType.IsArray)
                {
                    // Get the field array

                    if (field.Value.GetValue(this) is not Array array)
                        throw new NullReferenceException();

                    // Get the type of array elements
                    Type? type = array.GetType().GetElementType();

                    if (type == null)
                        throw new NullReferenceException();

                    if (type == typeof(byte))
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            array.SetValue(data[field.Key + i], i);
                        }
                    }
                    else if (type == typeof(int))
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            // Create byte array to extract the system exclusive data by four bytes
                            byte[] bytes = new byte[4];

                            // Increment the field offset by four bytes
                            uint offset = (uint)(field.Key + (i * 4));

                            // Some structures are varable length depending on the type
                            if (offset + 4 > data.Length)
                                break;

                            // Read four bytes from the system exclusive data
                            for (int j = 0; j < bytes.Length; j++)
                            {
                                bytes[j] = (byte)((data[offset + j]) & 0x0F);
                            }

                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(bytes);

                            array.SetValue(BitConverter.ToInt32(bytes, 0), i);
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }

                progress++;

                //Device.ReportProgress(this, progress, fieldCount);
                // Update progress
            }
            //}

            return IsInitialized = true;
        }

        /// <summary>
        /// Method to invoke when a property is changed.
        /// </summary>
        /// <param name="propertyName">The name of the property, defaults to the caller member name.</param>
        /// <param name="index">The index of the property, if applicable.</param>
        /// <remarks>
        /// <b>IMPORTANT:</b><br/>
        /// <i>Properties decorated with the <see cref="OffsetAttribute"/> are transmitted to the device.</i><br/>
        /// <i>Transmission can be prevented by calling the method with <see cref="string.Empty"/> explicitly.</i><br/>
        /// <i>Uncached or uninitialized models do not transmit properties.</i><br/>
        /// </remarks>
        protected override void NotifyPropertyChanged([CallerMemberName] string propertyName = "", int? index = null)
        {
            base.NotifyPropertyChanged(propertyName, index);

            if (!IsCached)
                return;

            if (string.IsNullOrEmpty(propertyName))
                return;

            // Only transmit properties if the model is initialized
            if (IsInitialized)
            {
                //IsDirty = true;
                if (index != null)
                {
                    //TODO: Transmit indexed property for MFX parameters
                    TransmitProperty(propertyName, index);
                }
                else
                {
                    TransmitProperty(propertyName);
                }
            }
        }

        #endregion
    }


    /// <summary>
    /// Base class for all MIDI enabled INTEGRA-7 models.
    /// </summary>
    public abstract class IntegraModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        /// <summary>
        /// Tracks whether the model is initialized.
        /// </summary>
        private bool _IsInitialized;

        /// <summary>
        /// Tracks wheter the model has unsaved changes.
        /// </summary>
        private bool _IsDirty;

        /// <summary>
        /// Tracks whether the model is disposed.
        /// </summary>
        private bool _IsDisposed;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraModel"/> instance.<br/>
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        /// <remarks><i>
        /// - All derived classes require an <see cref="IntegraAttribute"/>.<br/>
        /// - Sets the address, request and size from the <see cref="IntegraAttribute"/>.<br/>
        /// - Collections are responsible for there own requests.<br/>
        /// - Initializes the name with the type name.<br/>
        /// - Connects the model to the device.<br/>
        /// </i></remarks>
        /// <exception cref="IntegraException"></exception>
        internal IntegraModel(Integra device, bool connect = true)
        {
            Device = device ?? 
                throw new IntegraException($"[{nameof(IntegraModel)}]\n" +
                                           $"{GetType().Name}: Device is null.");

            Attribute = GetType().GetCustomAttribute<IntegraAttribute>() ??
                throw new IntegraException($"[{nameof(IntegraModel)}]\n" +
                                           $"{GetType().Name}: Attribute missing.\n" +
                                           $"All {nameof(IntegraModel)} derived classes require an {nameof(IntegraAttribute)}.");

            Name    = GetType().Name;
            Size    = Attribute.Size;
            Address = Attribute.Address;

            if(connect)
                Connect();

            // Collections are responsible for generating the request(s)
            if (GetType().IsSubclassOf(typeof(IntegraCollection)))
            {
                IsCollection = true;
                return;
            }

            Requests.Add(new IntegraRequest(Attribute.Request));
        }

        internal IntegraModel(IntegraModel instance)
        {
            instance.Initialize(Serialize());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the physical INTEGRA-7 address of the model.
        /// </summary>
        public IntegraAddress Address { get; internal protected set; }

        /// <summary>
        /// Gets the list of requests to initialize the model.
        /// </summary>
        internal List<IntegraRequest> Requests { get; } = new();

        /// <summary>
        /// Gets the device associated with the model.
        /// </summary>
        internal protected Integra Device { get; private set; }

        /// <summary>
        /// Gets the model's <see cref="IntegraAttribute"/>.
        /// </summary>
        internal protected IntegraAttribute Attribute { get; }

        /// <summary>
        /// Gets wheter the model is connected to the device.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets the name of the model.
        /// </summary>
        /// <remarks><i>Defaults to the type name.</i></remarks>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Gets the fixed model size in bytes or the fixed item count for collection types.<br/>
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b><br/>
        /// <i>The size is not serialized to the MIDI range.</i>
        /// </remarks>
        public int Size { get; internal protected set; }

        /// <summary>
        /// Gets wheter the model represents a collection.
        /// </summary>
        internal bool IsCollection { get; }

        /// <summary>
        /// Gets wheter the model has unsaved changes.
        /// </summary>
        public virtual bool IsDirty 
        {
            get => _IsDirty;
            protected set 
            {
                if (_IsDirty != value)
                {
                    _IsDirty = value;
                }
            } 
        }

        /// <summary>
        /// Gets wheter the model is initialized.
        /// </summary>
        /// <remarks><i>When set to true, property changed is raised for all properties.</i></remarks>
        public virtual bool IsInitialized
        {
            get => _IsInitialized;

            internal protected set
            {
                if (_IsInitialized != value)
                {
                    //Debug.Print($"[{GetType().Name}] {nameof(IsInitialized)} = {value}");

                    _IsInitialized = value;

                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects the model to the device to (re)enable receiving system exclusive messages.
        /// </summary>
        /// <remarks><i>Models are connected by default on instantiation.</i></remarks>
        /// <exception cref="IntegraException"/>
        internal protected void Connect()
        {
            if (Device == null)
                throw new IntegraException($"[{nameof(IntegraModel)}.{nameof(Connect)}()]\nThe device is null.");

            if (IsConnected)
                return;

            Device.SystemExclusiveReceived += SystemExclusiveReceived;
            IsConnected = true;
        }

        /// <summary>
        /// Disconnects the model from the device to disable receiving system exclusive messages.
        /// </summary>
        /// <exception cref="IntegraException"/>
        internal void Disconnect()
        {
            if (Device == null)
                throw new IntegraException($"[{nameof(IntegraModel)}.{nameof(Disconnect)}()]\nThe device is null.");

            if (!IsConnected)
                return;

            Device.SystemExclusiveReceived -= SystemExclusiveReceived;
            IsConnected = false;
        }

        /// <summary>
        /// Method to send the system exclusive request(s) to initialize the model. 
        /// </summary>
        /// <remarks><i>
        /// - The method is invoked by the <see cref="Integra"/> task queue.<br/>
        /// - The method <b>requires</b> to transmit the system exclusive(s) nescessary to fully initialize the model.<br/>
        /// </i></remarks>
        internal abstract void RequestInitialization();

        /// <summary>
        /// Method to serialize the model data to an <see cref="byte"/> array.
        /// </summary>
        /// <returns>Must return the model's data as an byte array.</returns>
        /// <remarks><i>The byte array is <b>required</b> to be ordered by field offset so it can be transmitted as system exclusive data.</i></remarks>
        public abstract byte[] Serialize();

        /// <summary>
        /// Event handler for received system exclusive messages.
        /// </summary>
        /// <param name="sender">The device that raised the event.</param>
        /// <param name="e">The event's associated data.</param>
        protected abstract void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e);

        /// <summary>
        /// Method to initialize the model with data.
        /// </summary>
        /// <param name="data">The data to initialize the model.</param>
        /// <returns>Must return true if the model is initialized.</returns>
        internal abstract bool Initialize(byte[] data);

        /// <summary>
        /// Gets the unique identifier based on the model's address.
        /// </summary>
        /// <returns>A unique identifier for the current model.</returns>
        internal protected virtual int GetUID()
        {
            return Address;
        }

        #endregion

        #region Overrides: Object

        /// <summary>
        /// Returns a string that represents the current model.
        /// </summary>
        /// <returns>A string representation of the current model.</returns>
        public override string ToString()
        {
            return $"{Name} 0x{GetUID():X4}";
        }

        #endregion

        #region Interfaces: INotifyPropertyChanged

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Method to invoke when a property is changed.
        /// </summary>
        /// <param name="propertyName">The name of the property, defaults to the caller member name.</param>
        /// <param name="index">Unimplemented optional parameter for indexed properties.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "", int? index = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (!IsDirty)
                {
                    IsDirty = true;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDirty)));
                }
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Interfaces: IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed)
            {
                if (disposing)
                {
                    // REMOVE DEVICE EVENT HANDLER
                    if (Device != null)
                    {
                        Device.SystemExclusiveReceived -= SystemExclusiveReceived;
                    }
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _IsDisposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~IntegraModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        #endregion
    }
}
