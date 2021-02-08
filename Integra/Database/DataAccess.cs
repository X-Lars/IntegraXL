using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Integra.Database
{
    public class DataAccess
    {

        public static void InsertWaveForms()
        {
            int result;
            string sql = $"SELECT COUNT(ID) FROM WaveForms";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    var count = command.ExecuteScalar();

                    // Check for DBNull when the table is empty
                    if (count.GetType() == typeof(DBNull))
                    {
                        result = 0;
                    }
                    else
                    {
                        result = Convert.ToInt32(count);
                    }
                }

                CloseConnection(connection);

            }

            if (result == 7500)
            {
                Console.WriteLine("Waveforms already inserted!");
                return;
            }
            DataTable data = new DataTable();

            data.Columns.Add();
            data.Columns.Add();
            data.Columns.Add();

            using (StreamReader reader = new StreamReader(@"Resources\WaveForms.csv"))
            {
                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine().Split(new char[] { ';' });
                    data.Rows.Add(values);
                    Console.WriteLine(values);
                }
            }

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (SqlBulkCopy batch = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {

                        batch.DestinationTableName = "WaveForms";
                        batch.BatchSize = 4000;

                        try
                        {
                            batch.WriteToServer(data);
                            transaction.Commit();

                            Debug.Print($"[{nameof(DataAccess)}.{nameof(BatchInsert)}] Successful");
                        }
                        catch (Exception exception)
                        {
                            transaction.Rollback();
                            connection.Close();

                            Debug.Print($"[{nameof(DataAccess)}.{nameof(BatchInsert)}] {exception.Message}");
                        }
                    }
                }

                connection.Close();
            }
        }

        #region Constants

        private const string SQL_SCHEMA_INDEXES = "IndexColumns";
        private const string SQL_SCHEMA_COLUMN_NAME = "column_name";
        private const string SQL_SCHEMA_COLUMN_ORDINAL = "ordinal_position";

        #endregion

        #region Fields

        /// <summary>
        /// Stores all non virtual instance properties matching the database associated table fields.
        /// </summary>
        /// <remarks><i>[<see cref="IntegraBase{T}"/>, [<see cref="string"/>, <see cref="PropertyInfo"/>]] where <see cref="string"/> is the property and field name.</i></remarks>
        private static Dictionary<Type, Dictionary<string, PropertyInfo>> _DataTemplates = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        #endregion

        #region Methods: Private

        /// <summary>
        /// Retreives the specified connection string from the App.config.
        /// </summary>
        /// <param name="database">The name of the connection string to retreive.</param>
        /// <returns>The specified connection string.</returns>
        public static string GetConnectionString(string database = "Integra")
        {
            return ConfigurationManager.ConnectionStrings[database].ConnectionString;
        }

        /// <summary>
        /// Opens the specified connection.
        /// </summary>
        /// <param name="connection">The connection to open.</param>
        private static void OpenConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        /// <summary>
        /// Closes the specified connection.
        /// </summary>
        /// <param name="connection">The connection to close.</param>
        private static void CloseConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        /// <summary>
        /// Gets the table name associated to the <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to retreive the table name.</param>
        /// <returns>The name of the table from the <see cref="Table"/> attribute or it's default type name.</returns>
        private static string GetTableName<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Table table = (Table)instance.GetType().GetCustomAttribute(typeof(Table));

            return table == null ? instance.GetType().Name : table.Name;
        }

        /// <summary>
        /// Generates the cache of properties matching the fields of the specified <typeparamref name="T"/> associated dtatabase table.
        /// </summary>
        /// <typeparam name="T">The type specifier defining the associated table.</typeparam>
        private static void InitializeDataTemplateCache<T>()
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(InitializeDataTemplateCache)}] {typeof(T).Name}");

            _DataTemplates.Add(typeof(T), new Dictionary<string, PropertyInfo>());

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !p.GetGetMethod().IsVirtual || p.GetGetMethod().IsFinal).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                _DataTemplates[typeof(T)].Add(properties[i].Name, properties[i]);
                Debug.Print($"-{properties[i].Name}");
            }
        }

        /// <summary>
        /// Gets the data template containing the database fields of <typeparamref name="T"/>'s associated table.
        /// </summary>
        /// <typeparam name="T">The table type specifier </typeparam>
        /// <returns>A collection of fields and matching properties associated with <typeparamref name="T"/>.</returns>
        private static Dictionary<string, PropertyInfo> GetDataTemplate<T>()
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(GetDataTemplate)}] <{typeof(T).Name}>");
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
        private static DataTable GetBatchData<T, U>(IntegraBaseCollection<T, U> collection) where T : IntegraBase<T> where U : IntegraDataTemplate<U>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(GetBatchData)}] {typeof(T).GetType().Name}<{typeof(U).Name}>");

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
        private static Dictionary<string, string> GetSQLParameters<T>(IntegraBase<T> instance, string id = null) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(GetSQLParameters)}] {instance.GetType().Name}");

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

                        FieldInfo field = IntegraBase<T>.Field(offset.Value);
                        //FieldInfo field = Field<T>(offset.Value);

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
                Debug.Print($"{sqlParameter.Key,-32} {sqlParameter.Value}");
            }

            return parameters;
        }

        /// <summary>
        /// Gets the index columns for the specified <paramref name="instance"/> associated table.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance specifying the database table.</param>
        /// <returns>A list of key value pairs where the key is the <b><i>zero based</i></b> column ordinal and the value the column name of the table index.</returns>
        /// <remarks><i>SQL Column oridinals are not zero based.</i></remarks>
        private static Dictionary<int, string> GetIndex<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Dictionary<int, string> keyColumns = new Dictionary<int, string>();
            string[] result;

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                string[] indexRestrictions = new string[5];
                indexRestrictions[2] = GetTableName(instance);

                DataTable indexes = connection.GetSchema(SQL_SCHEMA_INDEXES, indexRestrictions);

                result = new string[indexes.Rows.Count];

                for (int i = 0; i < indexes.Rows.Count; i++)
                {
                    keyColumns.Add((int)indexes.Rows[i][SQL_SCHEMA_COLUMN_ORDINAL] - 1, indexes.Rows[i][SQL_SCHEMA_COLUMN_NAME].ToString());
                }

                CloseConnection(connection);
            }

            return keyColumns;
        }

        #endregion

        public static void Test<T>(T instance)
        {
            if(typeof(T).GetInterface(nameof(IEnumerable<T>)) != null)
            {
                if(typeof(T).GetGenericArguments().Length != 1)
                    throw new NotSupportedException("Nested collection");

                // Generic collection type
                Console.WriteLine(typeof(T).GenericTypeArguments[0].Name);
            }
            else
            {
                Console.WriteLine(typeof(T).Name);
            }
        }

        #region Methods: Internal

        /// <summary>
        /// Inserts a collection into the database.
        /// </summary>
        /// <typeparam name="T">The collection type specifier.</typeparam>
        /// <typeparam name="U">The item type specifier.</typeparam>
        /// <param name="collection">The collection to insert.</param>
        /// <param name="template">The item template used by the collection.</param>
        internal static void BatchInsert<T, U>(IntegraBaseCollection<T, U> collection) where T: IntegraBase<T> where U: IntegraDataTemplate<U>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(BatchInsert)}] {GetTableName(collection)}");

            // Execute the batch
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (SqlBulkCopy batch = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {

                        batch.DestinationTableName = collection.GetType().Name;
                        batch.BatchSize = 2000;

                        try
                        {
                            batch.WriteToServer(GetBatchData(collection));
                            transaction.Commit();

                            Debug.Print($"[{nameof(DataAccess)}.{nameof(BatchInsert)}] Successful");
                        }
                        catch (Exception exception)
                        {
                            transaction.Rollback();
                            connection.Close();

                            Debug.Print($"[{nameof(DataAccess)}.{nameof(BatchInsert)}] {exception.Message}");
                        }
                    }
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Gets the next free ID from the database table associated with the specified <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance specifying the associated table.</param>
        /// <returns>The next free ID to store an <paramref name="instance"/> of <typeparamref name="T"/>.</returns>
        internal static int GetNextID<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            int result;

            string sql = $"SELECT MAX(ID) FROM {GetTableName(instance)}";

            using (var connection = new SqlConnection(GetConnectionString()))
            {

                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    var maxID = command.ExecuteScalar();

                    // Check for DBNull when the table is empty
                    if (maxID.GetType() == typeof(DBNull))
                    {
                        result = 0;
                    }
                    else
                    {
                        result = Convert.ToInt32(maxID);
                    }
                }

                CloseConnection(connection);

            }

            return result + 1;
        }

        /// <summary>
        /// Gets the number of specified <paramref name="instance"/>s from the associated database table.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to to count.</param>
        /// <returns>The count of <paramref name="instance"/>s of type <typeparamref name="T"/> in the associated database table.</returns>
        internal static int GetCount<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            int result;
            string sql = $"SELECT COUNT(ID) FROM {GetTableName(instance)}";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    var count = command.ExecuteScalar();

                    // Check for DBNull when the table is empty
                    if (count.GetType() == typeof(DBNull))
                    {
                        result = 0;
                    }
                    else
                    {
                        result = Convert.ToInt32(count);
                    }
                }

                CloseConnection(connection);

            }

            return result;
        }

        #endregion

        #region Methods: Public

        public static ObservableCollection<T> SelectAll<T>()
        {
            if (typeof(T).GetInterface(nameof(IEnumerable<T>)) != null)
            {
                if (typeof(T).GetGenericArguments().Length > 1)
                    throw new NotSupportedException("Nested collection");

                // Generic collection type

                foreach (var item in typeof(T).GenericTypeArguments)
                {
                    Console.WriteLine(item.Name);
                }

            }
            else
            {
                Console.WriteLine(typeof(T).Name);
            }

            ObservableCollection<T> result = new ObservableCollection<T>();

            string sql = $"SELECT * FROM {typeof(T).Name}";

            Dictionary<string, PropertyInfo> templateCache = GetDataTemplate<T>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var item = Activator.CreateInstance<T>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    //PropertyInfo property = template.PropertyCache[reader.GetName(i)];
                                    PropertyInfo property = templateCache[reader.GetName(i)];
                                    if (property != null)
                                    {
                                        property.SetValue(item, reader.GetValue(i));
                                    }
                                }

                                result.Add(item);
                            }
                        }
                    }
                }

                CloseConnection(connection);
            }

            return result;
        }

        /// <summary>
        /// Gets a list of all specified <paramref name="instance"/>s from the database.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <typeparam name="U">The item type specifier.</typeparam>
        /// <param name="instance">The instance specifying the associated table.</param>
        /// <param name="template">The item template to generate the list.</param>
        /// <returns>A list of <paramref name="instance"/>s.</returns>
        public static List<U> SelectAll<T, U>(IntegraBase<T> instance, IntegraDataTemplate<U> template) where T : IntegraBase<T> where U : IntegraDataTemplate<U>
        {
            if (typeof(T).GetInterface(nameof(IEnumerable<T>)) != null)
            {
                if (typeof(T).GetGenericArguments().Length > 1)
                    throw new NotSupportedException("Nested collection");

                // Generic collection type

                foreach (var item in typeof(T).GenericTypeArguments)
                {
                    Console.WriteLine(item.Name);
                }
                
            }
            else
            {
                Console.WriteLine(typeof(T).Name);
            }

            List<U> result = new List<U>();

            string sql = $"SELECT * FROM {GetTableName(instance)}";

            Dictionary<string, PropertyInfo> templateCache = GetDataTemplate<U>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var item = Activator.CreateInstance<U>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    //PropertyInfo property = template.PropertyCache[reader.GetName(i)];
                                    PropertyInfo property = templateCache[reader.GetName(i)];
                                    if (property != null)
                                    {
                                        property.SetValue(item, reader.GetValue(i));
                                    }
                                }

                                result.Add(item);
                            }
                        }
                    }
                }

                CloseConnection(connection);
            }

            return result;
        }

        /// <summary>
        /// Gets the <paramref name="instance"/> with the specified <paramref name="id"/> from the database.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to load.</param>
        /// <param name="id">The ID of the instance in the database.</param>
        public static int Select<T>(IntegraBase<T> instance, int id) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(Select)}] {instance.GetType().Name}");

            int result = 0;

            string sql = $"SELECT * FROM {GetTableName(instance)} WHERE ID = {id}";

            // Skip the ID column
            int columnOffset = 0;

            // Skip the Part column
            // Add the AND clause
            if (instance.GetType().GetInterfaces().Contains(typeof(IIntegraPartial)))
            {
                sql += $" AND Part = {(byte)((IIntegraPartial)instance).Part}";
                columnOffset += 1;
            }

            if(instance.GetType().GetInterfaces().Contains(typeof(IIntegraSynthTonePartial)))
            {
                sql += $" AND Partial = {(byte)((IIntegraSynthTonePartial)instance).Partial}";
                columnOffset += 1;
            }

            if (instance.GetType().GetInterfaces().Contains(typeof(IIntegraDrumKitPartial)))
            {
                sql += $" AND Note = {(byte)((IIntegraDrumKitPartial)instance).Note}";
                columnOffset += 1;
            }

            Console.WriteLine(sql);

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int count = reader.FieldCount - columnOffset;
                        int offset = 0;

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (result == 1)
                                    throw new Exception("");
                                
                                Dictionary<string, PropertyInfo> parameters = GetDataTemplate<T>();

                                foreach (var parameter in parameters)
                                {
                                    if(parameter.Value.PropertyType.GetInterfaces().Contains(typeof(IIntegraDataClass)))
                                    {
                                        // Reference parameters can be null
                                        if(parameter.Value.GetValue(instance)!= null)
                                            ((IIntegraDataClass)parameter.Value.GetValue(instance)).Select(id);
                                        
                                        offset++;
                                    }
                                    else if(parameter.Value.PropertyType.IsArray)
                                    {
                                        Array array = (Array)parameter.Value.GetValue(instance);

                                        for (int i = 0; i < array.Length; i++)
                                        {
                                            parameter.Value.SetValue(instance, reader.GetValue(offset + columnOffset), new object[] { i });
                                            offset++;
                                        }
                                    }
                                    else if(parameter.Value.PropertyType.IsEnum)
                                    {
                                        parameter.Value.SetValue(instance, Enum.Parse(parameter.Value.PropertyType, reader.GetValue(reader.GetOrdinal(parameter.Key)).ToString()));
                                    }
                                    else
                                    {
                                        if (parameter.Value.GetIndexParameters().Length != 0)
                                        {
                                            Offset attribute = parameter.Value.GetCustomAttribute<Offset>(false);

                                            FieldInfo field = IntegraBase<T>.Field(attribute.Value);

                                            Array propertyArray = (Array)field.GetValue(instance);

                                            for (int i = 0; i < propertyArray.Length; i++)
                                            {
                                                // Property name is "Item#" where '#' is the index

                                                if (reader.GetValue(reader.GetOrdinal(parameter.Key + (i))).GetType() == typeof(DBNull))
                                                {
                                                    parameter.Value.SetValue(instance, 0, new object[] { i });
                                                }
                                                else
                                                {
                                                    parameter.Value.SetValue(instance, reader.GetValue(reader.GetOrdinal(parameter.Key + (i))), new object[] { i });
                                                }

                                                offset++;
                                            }
                                        }
                                        else
                                        {
                                            parameter.Value.SetValue(instance, reader.GetValue(reader.GetOrdinal(parameter.Key)));
                                            offset++;
                                        }
                                    }
                                }

                                result += 1;
                            }
                        }
                    }
                }

                CloseConnection(connection);
            }

            return result;
        }

        // TODO: Remove instance to be able to truncate table based on type only
        public static void Truncate<T>(IntegraBase<T> integraBase) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(Truncate)}<{integraBase.GetType().Name}>]");

            string sql = $"TRUNCATE TABLE {typeof(T).Name}";

            Debug.Print(sql);

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    var res = command.ExecuteNonQuery();
                }

                CloseConnection(connection);
            }
        }

        public static void Delete<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(Delete)}<{instance.GetType().Name}>]");

            Dictionary<int, string> keyColumns = GetIndex(instance);

            string[] value = new string[keyColumns.Count];

            Dictionary<string, PropertyInfo> template = GetDataTemplate<T>();

            foreach (var item in keyColumns.OrderBy(i => i.Key))
            {
                PropertyInfo property = template[item.Value];

                if (property.PropertyType.IsEnum)
                {
                    value[item.Key] = Convert.ToByte(property.GetValue(instance)).ToString();
                }
                else
                    value[item.Key] = instance.GetType().GetProperty(item.Value).GetValue(instance).ToString();
            }



            string where = string.Empty;

            foreach (var item in keyColumns.OrderBy(i => i.Key))
            {
                where += item.Value;
                where += "=";
                where += value[item.Key];

                if (item.Key != keyColumns.Count - 1)
                    where += " AND ";
            }

            //string sql = $"SELECT * FROM {GetTableName(instance)} WHERE {where}";
            string sql = $"DELETE FROM {GetTableName(instance)} WHERE {where}";
            Console.WriteLine(sql);

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    var res = command.ExecuteNonQuery();
                }

                CloseConnection(connection);
            }
        }

        /// <summary>
        /// Stores the specified <paramref name="instance"/> in the database.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to store.</param>
        /// <param name="parameters"></param>
        public static void Insert<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(Insert)}] {instance.GetType().Name}");

            if (Exists(instance))
            {
                Debug.Print($"[{nameof(DataAccess)}.{nameof(Insert)}] {instance.GetType().Name} Already exists.");
                //TODO: Update
                return;
            }


            //if(IntegraCache.GetSQLParameters(instance).Keys.Count == 0)
            //    return;

            Dictionary<string, string> sqlp = GetSQLParameters(instance, Device.Session.ID.ToString());

            if (sqlp.Count == 0)
                return;

            string columns = string.Join(",", sqlp.Keys);
            string values = string.Join(",", sqlp.Values);


            string sql = $"INSERT INTO {GetTableName(instance)} ({columns}) VALUES({values})";
            Console.WriteLine(sql);
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    var res = command.ExecuteNonQuery();

                    Debug.Print($"[{nameof(DataAccess)}.{nameof(Insert)}<{instance.GetType().Name}>] Result: {res}");
                }

                CloseConnection(connection);
            }
        }

        public static int Update<T>(IntegraBase<T> instance) where T : IntegraBase<T>
        {
            int result = 0;

            Debug.Print($"[{nameof(DataAccess)}.{nameof(Insert)}] {instance.GetType().Name}");

            if (!Exists(instance))
            {
                Debug.Print($"[{nameof(DataAccess)}.{nameof(Insert)}] {instance.GetType().Name} No record found to update.");
                //TODO: Insert?
                return 0;
            }

            Dictionary<string, string> sqlp = GetSQLParameters(instance, Device.Session.ID.ToString());

            if (sqlp.Count == 0)
                return 0;

            string update = string.Join(",", sqlp.Select(x => x.Key + "=" + x.Value));

            // Where clause
            Dictionary<int, string> keyColumns = GetIndex(instance);

            string[] value = new string[keyColumns.Count];

            Dictionary<string, PropertyInfo> template = GetDataTemplate<T>();

            foreach (var item in keyColumns.OrderBy(i => i.Key))
            {
                PropertyInfo property = template[item.Value];

                if (property.PropertyType.IsEnum)
                {
                    value[item.Key] = Convert.ToByte(property.GetValue(instance)).ToString();
                }
                else
                    value[item.Key] = instance.GetType().GetProperty(item.Value).GetValue(instance).ToString();

            }

            string where = string.Empty;

            foreach (var item in keyColumns.OrderBy(i => i.Key))
            {
                where += item.Value;
                where += "=";
                where += value[item.Key];

                if (item.Key != keyColumns.Count - 1)
                    where += " AND ";
            }



            string sql = $"UPDATE {GetTableName(instance)} SET {update} WHERE {where}";
            Console.WriteLine(sql);
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    result = command.ExecuteNonQuery();

                    Debug.Print($"[{nameof(DataAccess)}.{nameof(Update)}<{instance.GetType().Name}>] Result: {result}");
                }

                CloseConnection(connection);
            }

            return result;
        }

        #endregion



        /// <summary>
        /// Checks if the specified <paramref name="instance"/> exists in the database.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance specifying the database table.</param>
        /// <returns>True if the current instance exists in the associated database table.</returns>
        /// <remarks><i>The check is performed on the index with the current property values of the <paramref name="instance"/>.</i></remarks>
        public static bool Exists<T>(IntegraBase<T> instance) where T: IntegraBase<T>
        {
            Dictionary<int, string> keyColumns = GetIndex(instance);

            string[] value = new string[keyColumns.Count];

            Dictionary<string, PropertyInfo> template = GetDataTemplate<T>();

            foreach (var item in keyColumns.OrderBy(i => i.Key))
            {
                PropertyInfo property = template[item.Value];
                
                if(property.PropertyType.IsEnum)
                {
                    value[item.Key] = Convert.ToByte(property.GetValue(instance)).ToString();
                }
                else
                    value[item.Key] = instance.GetType().GetProperty(item.Value).GetValue(instance).ToString();
               
            }
           

            bool result = false;

            string where = string.Empty;

            foreach(var item in keyColumns.OrderBy(i => i.Key))
            {
                where += item.Value;
                where += "=";
                where += value[item.Key];

                if (item.Key != keyColumns.Count - 1 )
                    where += " AND ";
            }

            string sql = $"SELECT * FROM {GetTableName(instance)} WHERE {where}";
            Console.WriteLine(sql);

            using (var connection = new SqlConnection(GetConnectionString()))
            {

                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result = reader.HasRows;
                    }
                }

                CloseConnection(connection);

            }

            return result;
        }
    }
}
