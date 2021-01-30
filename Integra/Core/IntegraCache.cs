using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Integra.Core
{
    /// <summary>
    /// Module wide cache for <see cref="IntegraBase{T}"/> derived classes.
    /// </summary>
    public static class IntegraCache
    {
        #region Fields 

        /// <summary>
        /// Stores all instance fields decorated with the <see cref="Offset"/> attribute.
        /// </summary>
        /// <remarks><i>[<see cref="IntegraBase{T}"/>, [<see cref="ushort"/>, <see cref="FieldInfo"/>]] where <see cref="ushort"/> is the offset address.</i></remarks>
        private static Dictionary<Type, Dictionary<ushort, FieldInfo>> _Fields = new Dictionary<Type, Dictionary<ushort, FieldInfo>>();

        /// <summary>
        /// Stores all instance properties decorated with the <see cref="Offset"/> attribute.
        /// </summary>
        /// <remarks><i>[<see cref="IntegraBase{T}"/>, [<see cref="ushort"/>, <see cref="PropertyInfo"/>]] where <see cref="ushort"/> is the offset address.</i></remarks>
        private static Dictionary<Type, Dictionary<ushort, PropertyInfo>> _Properties = new Dictionary<Type, Dictionary<ushort, PropertyInfo>>();

        /// <summary>
        /// Stores all non virtual instance properties matching the database associated table fields.
        /// </summary>
        /// <remarks><i>[<see cref="IntegraBase{T}"/>, [<see cref="string"/>, <see cref="PropertyInfo"/>]] where <see cref="string"/> is the property and field name.</i></remarks>
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> _DataTemplates = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        /// <summary>
        /// Stores all non virtual instance references matching the database associated tables.
        /// </summary>
        /// <remarks><i>[<see cref="IntegraBase{T}"/>, [<see cref="string"/>, <see cref="IIntegraDataClass"/>]] where <see cref="string"/> is the property and table name.</i></remarks>
        private static Dictionary<Type, Dictionary<string, IIntegraDataClass>> _References = new Dictionary<Type, Dictionary<string, IIntegraDataClass>>();

        #endregion

        #region Methods

        #region Methods: Private

        /// <summary>
        /// Generates the cache for all non public <see cref="Offset"/> decorated fields of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="IntegraBase{T}"/> type specifier.</typeparam>
        /// <remarks><i>Contains only fields associated with the INTEGRA-7.</i></remarks>
        private static void InitializeFieldsCache<T>() where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(InitializeFieldsCache)}] {typeof(T).Name}");

            Type type = typeof(T);

            _Fields.Add(type, new Dictionary<ushort, FieldInfo>());

            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                Offset offset = field.GetCustomAttribute<Offset>(false);

                if (offset != null)
                {
                    Debug.Print($"0x{offset.Value.ToString("X4")} {field.Name}");
                    _Fields[type].Add(offset.Value, field);
                }
            }
        }

        /// <summary>
        /// Generates the cache for all public <see cref="Offset"/> decorated properties of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="IntegraBase{T}"/> type specifier.</typeparam>
        /// <remarks><i>Contains only properties associated with the INTEGRA-7.</i></remarks>
        private static void InitializePropertiesCache<T>() where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(InitializePropertiesCache)}] {typeof(T).Name}");

            Type type = typeof(T);

            _Properties.Add(type, new Dictionary<ushort, PropertyInfo>());

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                Offset offset = property.GetCustomAttribute<Offset>(false);

                if (offset != null)
                {
                    Debug.Print($"0x{offset.Value.ToString("X4")} {property.Name}");
                    _Properties[type].Add(offset.Value, property);
                }
            }
        }

        /// <summary>
        /// Generates the cache for all public non virtual reference properties of the <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance containing the reference(s).</param>
        /// <remarks><i>References are used for recursive data loading by the data access layer simulating foreign key constraints.</i></remarks>
        private static void InitializeReferenceCache<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(InitializeReferenceCache)}] {typeof(T).Name}");

            Type type = instance.GetType();

            _References.Add(type, new Dictionary<string, IIntegraDataClass>());

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                // Exclude virtual references
                if (!property.GetMethod.IsVirtual || property.GetMethod.IsFinal)
                {
                    if (property.PropertyType.GetInterfaces().Contains(typeof(IIntegraDataClass)))
                    {
                        _References[type].Add(property.Name, (IIntegraDataClass)property.GetValue(instance));
                    }
                }
            }
        }

        /// <summary>
        /// Generates the cache of properties matching the fields of the specified <typeparamref name="T"/> associated dtatabase table.
        /// </summary>
        /// <typeparam name="T">The type specifier defining the associated table.</typeparam>
        private static void InitializeDataTemplateCache<T>()
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(InitializeDataTemplateCache)}] {typeof(T).Name}");

            _DataTemplates.Add(typeof(T), new Dictionary<string, PropertyInfo>());

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !p.GetGetMethod().IsVirtual || p.GetGetMethod().IsFinal).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                _DataTemplates[typeof(T)].Add(properties[i].Name, properties[i]);
                Debug.Print($"-{properties[i].Name}");
            }
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// Gets the cache of references for the specified <paramref name="instance"/> of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>The reference cache for the specified <paramref name="instance"/>.</returns>
        public static Dictionary<string, IIntegraDataClass> GetReferences<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetReferences)}] <{instance.GetType().Name}>");

            if (!_References.ContainsKey(instance.GetType()))
                InitializeReferenceCache(instance);

            return _References[instance.GetType()];
        }

        /// <summary>
        /// Gets the data template containing the database fields of <typeparamref name="T"/>'s associated table.
        /// </summary>
        /// <typeparam name="T">The table type specifier </typeparam>
        /// <returns>A collection of fields and matching properties associated with <typeparamref name="T"/>.</returns>
        public static Dictionary<string, PropertyInfo> GetDataTemplate<T>()
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetDataTemplate)}] <{typeof(T).Name}>");
            if (!_DataTemplates.ContainsKey(typeof(T)))
                InitializeDataTemplateCache<T>();

            return _DataTemplates[typeof(T)];
        }

        /// <summary>
        /// Gets the data of the specified <paramref name="collection"/> for batch insert into the database.
        /// </summary>
        /// <typeparam name="T">The collection type specifier.</typeparam>
        /// <typeparam name="U">The collection data type specifier.</typeparam>
        /// <param name="collection">The collection to insert.</param>
        /// <returns>A data table containing the values of the <paramref name="collection"/> ready for batch insert.</returns>
        public static DataTable GetBatchData<T, U>(IntegraBaseCollection<T, U> collection) where T : IntegraBase<T> where U : IntegraDataTemplate<U>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetBatchData)}] {typeof(T).GetType().Name}<{typeof(U).Name}>");

            if (!_DataTemplates.ContainsKey(typeof(U)))
                InitializeDataTemplateCache<U>();

            DataTable dataTable = new DataTable();

            // Generate table columns matching property name and type
            foreach (var property in _DataTemplates[typeof(U)])
            {
                if (property.Value.PropertyType.IsEnum)
                {
                    dataTable.Columns.Add(property.Value.Name, typeof(byte));
                }
                else
                {
                    dataTable.Columns.Add(property.Value.Name, property.Value.PropertyType);
                }

                Debug.Write($"{property.Value.Name,-20}");
            }

            Debug.Write("\n");

            // Generate table rows with values
            foreach (U item in collection)
            {
                DataRow row = dataTable.NewRow();

                foreach (var property in _DataTemplates[typeof(U)])
                {
                    if (property.Value.PropertyType.IsEnum)
                    {
                        row[property.Key] = (byte)property.Value.GetValue(item);
                    }
                    else
                    {
                        row[property.Key] = property.Value.GetValue(item);
                    }

                    Debug.Write($"{row[property.Key],-20}");
                }

                dataTable.Rows.Add(row);

                Debug.Write("\n");
            }

            return dataTable;
        }

        /// <summary>
        /// Gets the SQL parameters to insert or select the specified <paramref name="instance"/> based on the associated data template.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to insert or select.</param>
        /// <returns>A collection of parameter names and values.</returns>
        public static Dictionary<string, string> GetSQLParameters<T>(IntegraBase<T> instance, string id = null) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetSQLParameters)}] {instance.GetType().Name}");

            if (!_DataTemplates.ContainsKey(instance.GetType()))
                InitializeDataTemplateCache<T>();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var property in _DataTemplates[instance.GetType()].Values)
            {
                Type type = property.PropertyType;

                // Reference type, replace null with session ID
                if (type.GetInterfaces().Contains(typeof(IIntegraDataClass)))
                {
                    parameters.Add(property.Name, id);
                    continue;
                }

                if (type.IsArray)
                {
                    Array array = (Array)property.GetValue(instance);
                    Type arrayType = array.GetType().GetElementType();

                    if (arrayType == typeof(byte) || arrayType == typeof(int))
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            // Property name is "Name#" where '#' is the index
                            parameters.Add(property.Name + (i), $"{property.GetValue(instance)}");
                        }
                    }
                }
                else if (type == typeof(byte) || type == typeof(int) || type == typeof(short))
                {
                    if (property.GetIndexParameters().Length > 0)
                    {
                        Offset offset = property.GetCustomAttribute<Offset>(false);

                        FieldInfo field = Field<T>(offset.Value);

                        Array propertyArray = (Array)field.GetValue(instance);

                        for (int i = 0; i < propertyArray.Length; i++)
                        {
                            // Property name is "Item#" where '#' is the index
                            parameters.Add(property.Name + (i), $"{property.GetValue(instance, new object[] { i })}");
                        }
                    }
                    else
                    {
                        parameters.Add(property.Name, $"{property.GetValue(instance)}");
                    }
                }
                else if (type.IsEnum)
                {
                    parameters.Add(property.Name, $"{Convert.ChangeType(property.GetValue(instance), Enum.GetUnderlyingType(type))}");

                }
                else if (type == typeof(bool))
                {
                    parameters.Add(property.Name, $"{(((bool)property.GetValue(instance)) ? 1 : 0)}");
                }
                else if (type == typeof(string))
                {

                    parameters.Add(property.Name, $"'{property.GetValue(instance).ToString().Replace("'", "''")}'");
                }
            }

            foreach (KeyValuePair<string, string> sqlParameter in parameters)
            {
                Debug.Print($"{sqlParameter.Key, -32} {sqlParameter.Value}");
            }

            return parameters;
        }

        /// <summary>
        /// Gets the property with the specified offset.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="offset">The offset associated to the property.</param>
        /// <returns>The property at the specified offset.</returns>
        public static PropertyInfo Property<T>(ushort offset) where T : IntegraBase<T>
        {
            if (_Properties.ContainsKey(typeof(T)))
                return _Properties[typeof(T)][offset];

            InitializePropertiesCache<T>();

            return _Properties[typeof(T)][offset];
        }

        /// <summary>
        /// Gets the offset of the specified property.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="name">The property name.</param>
        /// <returns>The offset of the property.</returns>
        public static ushort PropertyOffset<T>(string name) where T : IntegraBase<T>
        {
            if (_Properties.ContainsKey(typeof(T)))
                return _Properties[typeof(T)].Where(v => v.Value.Name == name).FirstOrDefault().Key;

            InitializePropertiesCache<T>();

            return _Properties[typeof(T)].Where(v => v.Value.Name == name).FirstOrDefault().Key;
        }

        /// <summary>
        /// Gets the field cache of the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <returns>The cache associated to <typeparamref name="T"/>.</returns>
        public static Dictionary<ushort, FieldInfo> GetFields<T>() where T : IntegraBase<T>
        {
            if (_Fields.ContainsKey(typeof(T)))
                return _Fields[typeof(T)];

            InitializeFieldsCache<T>();

            return _Fields[typeof(T)];
        }

        /// <summary>
        /// Gets the field with the specified offset.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="offset">The offset associated to the field.</param>
        /// <returns>The field at the specified offset.</returns>
        public static FieldInfo Field<T>(ushort offset) where T : IntegraBase<T>
        {
            if (_Fields.ContainsKey(typeof(T)))
            {
                if (_Fields[typeof(T)].ContainsKey(offset))
                {
                    return _Fields[typeof(T)][offset];
                }
                else
                {
                    return null;
                }
            }

            InitializeFieldsCache<T>();

            return _Fields[typeof(T)].ContainsKey(offset) ? _Fields[typeof(T)][offset] : null;
        }

        #endregion

        #endregion
    }
}
