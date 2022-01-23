using IntegraXL.Extensions;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace IntegraXL.Core
{
    /// <summary>
    /// Base class for all MIDI enabled INTEGRA-7 data models.
    /// </summary>
    public abstract class IntegraModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Tracks if the model is initialized.
        /// </summary>
        private bool _IsInitialized = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraModel"/> instance.<br/>
        /// </summary>
        /// <param name="device">The device to connect the model.</param>
        internal IntegraModel(Integra device) : this()
        {
            Device = device;
            Connect();
        }

        /// <summary>
        /// Creates and initializes a new <see cref="IntegraModel"/> instance.<br/>
        /// </summary>
        /// <remarks>
        /// <i>All derived classes require an <see cref="IntegraAttribute"/>.</i><br/>
        /// </remarks>
        private IntegraModel()
        {
            Debug.Print($"[{nameof(IntegraModel)}] Contructor<{GetType().Name}>()]");

            IntegraAttribute? attribute = GetType().GetCustomAttribute<IntegraAttribute>();

            Debug.Assert(attribute != null);

            Name = GetType().Name;
            Size = attribute.Size;
            Address = attribute.Address;

            if (GetType().IsSubclassOf(typeof(IntegraCollection)))
            {
                return;
            }

            Requests.Add(new IntegraRequest(attribute.Request));

            this.Cache();
        }

        /// <summary>
        /// Requests the device to initialize the model.
        /// </summary>
        /// <returns>An awaitable task that returns true if the model is initialized.</returns>
        internal virtual async Task<bool> Initialize()
        {
            Debug.Print($"[{nameof(IntegraModel)}] {nameof(Initialize)}<{GetType().Name}>()");

            return await Device.InitializeModel(this);
        }

        /// <summary>
        /// Connects the model to the device to (re)enable receiving system exclusive messages.
        /// </summary>
        /// <remarks><i>Models are connected by default on instantiation.</i></remarks>
        internal void Connect()
        {
            Debug.Print($"[{nameof(IntegraModel)}] {nameof(Connect)}<{GetType().Name}>()");
            Device.SystemExclusiveReceived += SystemExclusiveReceived;
            IsConnected = true;
        }

        /// <summary>
        /// Disconnects the model from the device to disable receiving system exclusive messages.
        /// </summary>
        internal void Disconnect()
        {
            Debug.Print($"[{nameof(IntegraModel)}] {nameof(Disconnect)}<{GetType().Name}>()");
            Device.SystemExclusiveReceived -= SystemExclusiveReceived;
            IsConnected = false;
        }

        /// <summary>
        /// Handles system exclusive messages filtered by exact adress and size matching.
        /// </summary>
        /// <param name="sender">The device that raised the event.</param>
        /// <param name="e">The system exclusive data.</param>
        protected virtual void SystemExclusiveReceived(object? sender, IntegraSystemExclusiveEventArgs e)
        {
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
            }
            else if (e.SystemExclusive.Address.InRange(Address, (int)(Address + Size)))
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
        protected virtual bool Initialize(byte[] data)
        {
            // TODO: Combine received property into the initialize method
            if (!IsInitialized)
            {
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

                    // Update progress
                }
            }

            return IsInitialized = true;
        }

        #endregion

        // Initialize data
        // Receive parameter
        // Send parameter
        // Save
        // Load
        // State

        #region Properties

        /// <summary>
        /// Gets the physical INTEGRA-7 address of the model.
        /// </summary>
        public IntegraAddress Address { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        internal List<IntegraAddress> Requests { get; } = new();

        /// <summary>
        /// Gets the device associated with the model.
        /// </summary>
        internal Integra Device { get; }

        /// <summary>
        /// Gets wheter the model is connected to the device.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets the user friendly name of the model.
        /// </summary>
        public string Name { get; internal set; }
        
        /// <summary>
        /// Gets the fixed model size in bytes or the fixed item count for collection types.<br/>
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets wheter the model is initialized.
        /// </summary>
        public virtual bool IsInitialized
        {
            get => _IsInitialized;

            internal protected set
            {
                if(_IsInitialized != value)
                {
                    //if(value == true)
                    //    Debug.Print($"[{GetType().Name}.{nameof(IsInitialized)}] {value}");

                    _IsInitialized = value;

                    // Raise the property changed event for all properties without transmission
                    NotifyPropertyChanged(string.Empty);
                }
            }
        }

        #endregion

        #region Method


        internal void TransmitProperty(string propertyName, int? index = null)
        {
            //if (string.IsNullOrEmpty(propertyName))
            //    return;

            if (this.CachedProperties() == null)
                return;

            if (this.CachedProperties().TryGetValue(propertyName, out int offset))
            {
                FieldInfo field = this.CachedFields()[offset];

                if (field != null)
                {
                    byte[] data = Array.Empty<byte>();

                    if (field.FieldType == typeof(byte))
                    {
                        data = new byte[] { (byte)field.GetValue(this) };
                    }
                    else if (field.FieldType == typeof(bool))
                    {
                        data = new byte[] { (bool)field.GetValue(this) ? (byte)1 : (byte)0 };
                    }
                    else if (field.FieldType.IsEnum)
                    {
                        Type type = field.FieldType.GetEnumUnderlyingType();

                        if (type == typeof(byte))
                        {
                            data = new byte[] { (byte)field.GetValue(this) };
                        }
                        else
                        {
                            throw new NotImplementedException($"{GetType().Name}.{nameof(TransmitProperty)}] {field.FieldType.Name} {field.Name}");
                        }
                    }
                    else if (field.FieldType.IsArray)
                    {
                        Array array = field.GetValue(this) as Array;
                        Type type = array.GetType().GetElementType();

                        if (type == typeof(byte))
                        {
                            data = new byte[array.Length];

                            Array.Copy(array, data, array.Length);
                        }
                        else if (type == typeof(int))
                        {
                            if (index != null)
                            {
                                int[] ints = (int[])(field.GetValue(this));
                                byte[] bytes = new byte[4];

                                // Get the bytes from the int array value at the specified index
                                bytes = BitConverter.GetBytes(ints[(int)index]);

                                if (BitConverter.IsLittleEndian)
                                    Array.Reverse(bytes);

                                data = bytes;
                                offset += (int)(index * 4);

                            }
                            else
                            {
                                throw new IndexOutOfRangeException($"{GetType().Name}.{nameof(TransmitProperty)}] {field.FieldType.Name} {field.Name} No index provided");
                            }

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

                    Device.TransmitSystemExclusive(new IntegraSystemExclusive(Device.DeviceID, Address, offset, data));
                }
            }
        }

        internal void ReceivedProperty(IntegraSystemExclusive systemExclusive)
        {
            int offset = systemExclusive.Address - Address;

            var fields = this.CachedFields();

            for (int dataOffset = 0; dataOffset < systemExclusive.Data.Length; dataOffset++)
            {
                int fieldOffset = offset + dataOffset;

                fields.TryGetValue(fieldOffset, out FieldInfo field);

                // TODO: should never be null?
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
                else if (field.FieldType.IsEnum)
                {
                    Type type = field.FieldType.GetEnumUnderlyingType();

                    if (type == typeof(byte))
                    {
                        field.SetValue(this, systemExclusive.Data[dataOffset]);
                    }
                    else
                    {
                        throw new NotImplementedException($"{GetType().Name}.{nameof(ReceivedProperty)}] {field.FieldType.Name} {field.Name}");
                    }
                }
                else if (field.FieldType.IsArray)
                {
                    Array array = (Array)field.GetValue(this);
                    Type arrayType = array.GetType().GetElementType();

                    if (arrayType == typeof(byte))
                    {
                        for (int a = 0; a < array.Length; a++)
                        {
                            array.SetValue(systemExclusive.Data[dataOffset], a);
                            dataOffset++;
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
                    Debug.Print($"RX [{GetType().Name}] {property} = {field.GetValue(this)} {GetModelHash():X4}");

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
                }
            }
        }

        /// <summary>
        /// Gets a hash code based on the model's address.
        /// </summary>
        /// <returns>A hash code for the model.</returns>
        internal protected virtual int GetModelHash()
        {
            return Address;
        }

        /// <summary>
        /// Gets a hash code based on the model's type.
        /// </summary>
        /// <returns>A hash code based on the model's type.</returns>
        /// <remarks><i>Used for caching <see cref="IntegraAttribute"/></i> decorated fields and properties.</remarks>
        public int GetTypeHash()
        {
            return GetType().GetHashCode();
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
        /// <param name="index">The index of the property, if applicable.</param>
        /// <remarks>
        /// <b>IMPORTANT:</b><br/>
        /// <i>Properties decorated with the <see cref="IntegraAttribute"/> are transmitted to the device.</i><br/>
        /// <i>Prevent transmission by calling the method with a <see cref="string.Empty"/> explicitly.</i><br/>
        /// <i>Uninitialized models do not transmit properties.</i><br/>
        /// </remarks>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "", int? index = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (string.IsNullOrEmpty(propertyName))
                return;

            // Only transmit properties if the model is initialized
            if (IsInitialized)
            {
                if (index != null)
                {
                    //TODO: Transmit indexed property for MFX parameters
                }
                else
                {

                    TransmitProperty(propertyName);
                }
            }
        }

        #endregion
    }
}
