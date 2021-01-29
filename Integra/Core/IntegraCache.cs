using Integra.Core.Interfaces;
using Integra.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Core
{
    
    public class Test : IntegraBaseBase<Test>
    {
        public Test()
        {
            //IntegraCache.Field(this, 0x1800).Initialize(new IntegraSystemExclusive(new MidiXL.SystemExclusiveMessage(new byte[] { })));
                
        }
    }

    public class IntegraCache
    {
        // [IntegraBase<T> [0x0000, fieldinfo]]
        private static Dictionary<Type, Dictionary<ushort, FieldInfo>> _Fields = new Dictionary<Type, Dictionary<ushort, FieldInfo>>();
        // [IntegraBase<T> [0x0000, PropertyInfo]]
        private static Dictionary<Type, Dictionary<ushort, PropertyInfo>> _Properties = new Dictionary<Type, Dictionary<ushort, PropertyInfo>>();
        // [IntegraDataTemplate<T> [Property.Name, Property.PropertyType]]
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> _DataTemplates = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        // All offset decorated fields
        private static void InitializeFieldsCache<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(InitializeFieldsCache)}] {instance.GetType().Name}");
            Type type = instance.GetType();

            _Fields.Add(type, new Dictionary<ushort, FieldInfo>());

            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                Offset offset = field.GetCustomAttribute<Offset>(false);

                if (offset != null)
                {
                    Debug.Print($"{offset.Value.ToString("X4")} {field.Name}");
                    _Fields[type].Add(offset.Value, field);
                }
            }
        }

        // All offset decorated properties
        private static void InitializePropertiesCache<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(InitializePropertiesCache)}] {instance.GetType().Name}");
            Type type = instance.GetType();

            _Properties.Add(type, new Dictionary<ushort, PropertyInfo>());

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                Offset offset = property.GetCustomAttribute<Offset>(false);

                if (offset != null)
                {
                    Debug.Print($"{offset.Value.ToString("X4")} {property.Name}");
                    _Properties[type].Add(offset.Value, property);
                }
            }
        }

        // All public non virtual properties of the data template
        private static void InitializeDataTemplateCache<T>() where T: IntegraDataTemplate<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(InitializeDataTemplateCache)}] {typeof(T).Name}");

            _DataTemplates.Add(typeof(T), new Dictionary<string, PropertyInfo>());

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(p => !p.GetGetMethod().IsVirtual || p.GetGetMethod().IsFinal).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                _DataTemplates[typeof(T)].Add(properties[i].Name, properties[i]);
            }
        }

        //public static Dictionary<string, PropertyInfo> GetTemplateProperties<T>() where T: IntegraDataTemplate<T>
        //{
        //    Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetTemplateProperties)}] <{typeof(T).Name}>");

        //    if (!_DataTemplates.ContainsKey(typeof(T)))
        //        InitializeDataTemplateCache<T>();

        //    Dictionary<string, PropertyInfo> templateProperties = new Dictionary<string, PropertyInfo>();

        //    foreach(var item in _DataTemplates[typeof(T)])
        //    {
        //        templateProperties.Add(item.Value.Name, item.Value);
        //    }

        //    return templateProperties;
        //}

        //public static PropertyInfo GetTemplateProperty<T>(string name) where T : IntegraDataTemplate<T>
        //{
        //    Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetTemplateProperty)}] <{typeof(T).Name}> {name}");
            
        //    if (!_DataTemplates.ContainsKey(typeof(T)))
        //        InitializeDataTemplateCache<T>();

        //    return _DataTemplates[typeof(T)].Values.Where(n => n.Name == name).FirstOrDefault();
        //}


        public static Dictionary<string, PropertyInfo> GetTemplate<T>() where T: IntegraDataTemplate<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetTemplate)}] <{typeof(T).Name}>");
            if (!_DataTemplates.ContainsKey(typeof(T)))
                InitializeDataTemplateCache<T>();

            return _DataTemplates[typeof(T)];
        }

        // Datatable filled from collection 
        public static DataTable GetBatchData<T, U>(IntegraBaseCollection<T, U> collection) where T : IntegraBase<T> where U : IntegraDataTemplate<U>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetSQLParameters)}] {typeof(T).GetType().Name}<{typeof(U).Name}>");

            if (!_DataTemplates.ContainsKey(typeof(U)))
                InitializeDataTemplateCache<U>();

            DataTable dataTable = new DataTable();

            // Generate table columns matching property name and type
            foreach(var property in _DataTemplates[typeof(U)])
            {
                if(property.Value.PropertyType.IsEnum)
                {
                    dataTable.Columns.Add(property.Value.Name, typeof(byte));
                }
                else
                {
                    dataTable.Columns.Add(property.Value.Name, property.Value.PropertyType);
                }

                Debug.Write($"{property.Value.Name, -20}");
            }

            Debug.Write("\n");

            foreach(U item in collection)
            {
                DataRow row = dataTable.NewRow();

                foreach(var property in _DataTemplates[typeof(U)])
                {
                    if(property.Value.PropertyType.IsEnum)
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

        // SQL parameters for all offset decorated properties
        public static Dictionary<string, string> GetSQLParameters<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(IntegraCache)}.{nameof(GetSQLParameters)}] {instance.GetType().Name}");

            if (!_Properties.ContainsKey(instance.GetType()))
                InitializePropertiesCache(instance);

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var property in _Properties[instance.GetType()].Values)
            {
                Type type = property.PropertyType;

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
                        FieldInfo field = Field(instance, offset.Value);

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
                    parameters.Add(property.Name, $"'{property.GetValue(instance)}'");
                }
            }

            foreach(KeyValuePair<string, string> sqlParameter in parameters)
            {
                Console.WriteLine($"{sqlParameter.Key, -32} {sqlParameter.Value}");
            }

            return parameters;
        }

        // Get property with specified offset
        public static PropertyInfo Property<T>(IntegraBase<T> instance, ushort offset) where T : IntegraBase<T>
        {
            if (_Properties.ContainsKey(typeof(T)))
                return _Properties[typeof(T)][offset];

            InitializePropertiesCache(instance);

            return _Properties[typeof(T)][offset];
        }

        // Get field with specified offset
        public static FieldInfo Field<T>(IntegraBase<T> instance, ushort offset) where T : IntegraBase<T>
        {
            if (_Fields.ContainsKey(typeof(T)))
                return _Fields[typeof(T)][offset];

            InitializeFieldsCache(instance);

            return _Fields[typeof(T)][offset];
        }
    }
}
