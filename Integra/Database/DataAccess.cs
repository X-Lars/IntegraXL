using Integra.Core;
using Integra.Core.Interfaces;
using Integra.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Integra.Database
{
    public class DataAccess
    {
        /// <summary>
        /// Creates a list of SQL parameters based upon the instance type.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to create the parameters.</param>
        /// <returns>A list of SQL parameters to store the instance into the database.</returns>
        private static List<SQLParameter> GetSQLParameters<T>(IntegraBase<T> instance) where T: IntegraBase<T>
        {
            List<SQLParameter> result = new List<SQLParameter>();

            PropertyInfo[] properties = instance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var property in properties)
            {
                // Skip virtual properties
                if (property.GetGetMethod().IsVirtual)
                    continue;

                Type type = property.PropertyType;
                
                // Only cache property decorated with the offset attribute
                if (property.GetCustomAttribute(typeof(Offset)) != null)
                {
                    Offset index = (Offset)property.GetCustomAttribute(typeof(Offset));

                    if (type == typeof(string))
                    {
                        // The property is of type string
                        result.Add(new SQLParameter(type, property.Name, property.GetValue(instance)));
                    }
                    else if (type.IsArray)
                    {
                        // The property is an array
                        Array propertyArray = (Array)property.GetValue(instance);

                        // Get the type of elements in the array
                        Type propertyArrayType = propertyArray.GetType().GetElementType();

                        if (propertyArrayType == typeof(byte))
                        {
                            // The array is of type byte
                            for (int i = 0; i < propertyArray.Length; i++)
                            {
                                // Property name is "<Property Name>#" where '#' is the index
                                result.Add(new SQLParameter(propertyArrayType, property.Name + (i), property.GetValue(instance)));
                            }
                        }
                        else if (propertyArrayType == typeof(int))
                        {
                            // The array is of type int
                            for (int i = 0; i < propertyArray.Length; i++)
                            {
                                // Property name is "<Property Name>#" where '#' is the index
                                result.Add(new SQLParameter(propertyArrayType, property.Name + (i), property.GetValue(instance)));
                            }
                        }
                    }
                    else if (type == typeof(bool))
                    {
                        // The property is of type bool
                        result.Add(new SQLParameter(type, property.Name, property.GetValue(instance)));
                    }
                    else if (type == typeof(short))
                    {
                        result.Add(new SQLParameter(type, property.Name, property.GetValue(instance)));
                    }
                    else if (type == typeof(int))
                    {
                        if (property.GetIndexParameters().Length > 0)
                        {
                            // The property is an indexer property of type int
                            // Get the values from the backing field
                            Offset offset = (Offset)property.GetCustomAttribute(typeof(Offset));

                            FieldInfo field = instance.GetCachedField(offset.Value);

                            // Create an array of the backing field to get the data length
                            Array propertyArray = (Array)field.GetValue(instance);

                            for (int i = 0; i < propertyArray.Length; i++)
                            {
                                // Property name is "Item#" where '#' is the index
                                result.Add(new SQLParameter(type, property.Name + (i), property.GetValue(instance, new object[] { i })));
                            }
                        }
                        else
                        {
                            // The property is of type int
                            if (type.IsEnum)
                            {
                                Type enumType = Enum.GetUnderlyingType(type);

                                //object underlyingValue = Convert.ChangeType(property.GetValue(this), Enum.GetUnderlyingType(property.GetValue(this).GetType()));
                                result.Add(new SQLParameter(type, property.Name, Convert.ChangeType(property.GetValue(instance), enumType)));

                            }
                            else
                            {
                                result.Add(new SQLParameter(type, property.Name, property.GetValue(instance)));
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
                            FieldInfo field = instance.GetCachedField(offset.Value);

                            // Create an array of the backing field to get the data length
                            Array propertyArray = (Array)field.GetValue(instance);

                            for (int i = 0; i < propertyArray.Length; i++)
                            {
                                // Property name is "Item#" where '#' is the index
                                result.Add(new SQLParameter(type, property.Name + (i), property.GetValue(instance, new object[] { i })));
                            }
                        }
                        else
                        {
                            // The property is of type byte
                            if (type.IsEnum)
                            {
                                Type enumType = Enum.GetUnderlyingType(type);

                                result.Add(new SQLParameter(type, property.Name, Convert.ChangeType(property.GetValue(instance), enumType)));
                            }
                            else
                            {
                                result.Add(new SQLParameter(type, property.Name, property.GetValue(instance)));
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Inserts a collection into the database.
        /// </summary>
        /// <typeparam name="T">The collection type specifier.</typeparam>
        /// <typeparam name="U">The item type specifier.</typeparam>
        /// <param name="collection">The collection to insert.</param>
        /// <param name="template">The item template used by the collection.</param>
        internal static void BatchInsert<T, U>(IntegraBaseCollection<T, U> collection, IntegraDataTemplate<U> template) where T: IntegraBase<T> where U: IntegraDataTemplate<U>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(BatchInsert)}] {GetTableName(collection)}");

            DataTable table = new DataTable();


            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(U));
            // TODO: exclude virtual

            // Create the table columns based on the properties of the data template
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];

                
                if(property.PropertyType.IsEnum)
                {
                    table.Columns.Add(property.Name, typeof(byte));
                }
                else
                {
                    table.Columns.Add(property.Name, property.PropertyType);
                }
                
            }

            // Array to assign the row data
            object[] dataRow = new object[properties.Count];

            // Create the table rows based on the items of the collection
            foreach (U item in collection.Collection)
            {
                for (int i = 0; i < dataRow.Length; i++)
                {
                    if (properties[i].PropertyType.IsEnum)
                    {
                        dataRow[i] = (byte)properties[i].GetValue(item);
                    }
                    else
                    {
                        dataRow[i] = properties[i].GetValue(item);
                    }
                }

                table.Rows.Add(dataRow);
            }

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
                            batch.WriteToServer(table);
                            transaction.Commit();
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
        internal static int GetNextID<T>(IntegraBase<T> instance) where T: IntegraBase<T>
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
        internal static int GetCount<T>(IntegraBase<T> instance) where T: IntegraBase<T>
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

        /// <summary>
        /// Gets a list of all specified <paramref name="instance"/>s from the database.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <typeparam name="U">The item type specifier.</typeparam>
        /// <param name="instance">The instance specifying the associated table.</param>
        /// <param name="template">The item template to generate the list.</param>
        /// <returns>A list of <paramref name="instance"/>s.</returns>
        public static List<U> Select<T, U>(IntegraBase<T> instance, IntegraDataTemplate<U> template) where T: IntegraBase<T> where U: IntegraDataTemplate<U>
        {
            List<U> result = new List<U>();

            string sql = $"SELECT * FROM {GetTableName(instance)}";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var item = Activator.CreateInstance<U>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    PropertyInfo property = template.PropertyCache[reader.GetName(i)];

                                    if(property != null)
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
        public static int Load<T>(IntegraBase<T> instance, int id) where T: IntegraBase<T>
        {
            int result = 0;

            string sql = $"SELECT * FROM {GetTableName(instance)} WHERE ID = {id}";

            // Skip the ID column
            int columnOffset = 1;

            // Skip the Part column
            // Add the AND clause
            if (instance.GetType().GetInterfaces().Contains(typeof(IIntegraPartial)))
            {
                sql += $" AND Part = {(byte)((IIntegraPartial)instance).Part}";
                columnOffset += 1;
            }

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

                                for (int i = 0; i < instance.Parameters.Count; i++)
                                {
                                    SQLData data = instance.Parameters[i];

                                    if (data.Type == typeof(string))
                                    {
                                        data.Value = reader.GetString(offset + columnOffset);

                                        if (data.Value == null)
                                            data.Value = string.Empty;

                                        offset++;
                                    }
                                    else if (data.Type == typeof(byte[]))
                                    {
                                        byte[] values = new byte[data.Size];

                                        for (int v = 0; v < data.Size; v++)
                                        {
                                            values[v] = reader.GetByte(offset + columnOffset);
                                            offset++;
                                        }

                                        data.Value = values;
                                    }
                                    else if (data.Type == typeof(int[]))
                                    {
                                        int[] values = new int[data.Size];

                                        for (int v = 0; v < data.Size; v++)
                                        {
                                            values[v] = reader.GetInt32(offset + columnOffset);
                                            offset++;
                                        }

                                        data.Value = values;
                                    }
                                    else if (data.Type == typeof(short))
                                    {
                                        data.Value = reader.GetInt16(offset + columnOffset);
                                        offset++;
                                    }
                                    else if (data.Type == typeof(bool))
                                    {
                                        data.Value = reader.GetBoolean(offset + columnOffset);
                                        offset++;
                                    }
                                    else if (data.Type == typeof(int))
                                    {
                                        data.Value = reader.GetInt32(offset + columnOffset);
                                        offset++;
                                    }
                                    else if (data.Type == typeof(byte))
                                    {
                                        data.Value = reader.GetByte(offset + columnOffset);
                                        offset++;
                                    }
                                }

                                result += 1;
                            }
                            
                        }
                        else
                        {
                            result = 0;
                        }

                    }
                }

                CloseConnection(connection);

            }

            return result;
        }

        public static bool Exists<T>(IntegraBase<T> instance, List<SQLParameter> parameters) where T: IntegraBase<T>
        {
            bool result = false;

            string where = string.Empty;

            for (int i = 0; i < parameters.Count; i++)
            {
                where += parameters[i].Name;
                where += "=";
                where += parameters[i].Value.ToString();

                if(i != parameters.Count - 1)
                    where += " AND ";

            }

            string sql = $"SELECT * FROM {GetTableName(instance)} WHERE {where}";


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

        /// <summary>
        /// Stores the specified <paramref name="instance"/> in the database.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to store.</param>
        /// <param name="parameters"></param>
        public static void Save<T>(IntegraBase<T> instance, List<SQLParameter> parameters = null, bool useSessionID = true) where T: IntegraBase<T>
        {
            if (parameters == null)
                parameters = GetSQLParameters(instance);

            if (parameters.Count == 0)
                return;

            string columns = useSessionID == true ?  "ID," : string.Empty;
            string values = useSessionID == true ?  $"{Device.Session.SessionID}," : string.Empty;

            // Add the part parameter if the instance implements the IIntegraPartial interface
            if(instance.GetType().GetInterfaces().Contains(typeof(IIntegraPartial)))
            {
                columns += "Part,";
                values += $"{(byte)((IIntegraPartial)instance).Part},";
            }

            for (int i = 0; i < parameters.Count; i++)
            {
                columns += parameters[i].Name;
                columns += ",";

                if(parameters[i].Type == typeof(bool))
                {
                    values += (bool)parameters[i].Value == true ? 1 : 0;
                }
                else if (parameters[i].Type == typeof(string))
                {
                    values += $"'{parameters[i].Value}'";
                }
                else
                {
                    values += parameters[i].Value.ToString();
                }

                values += ",";
            }

            // Remove trailing separators
            if (columns.Length > 0)
                columns = columns.Remove(columns.Length - 1);

            if(values.Length > 0)
                values = values.Remove(values.Length - 1);


            string sql = $"INSERT INTO {GetTableName(instance)} ({columns}) VALUES({values})";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    var res = command.ExecuteNonQuery();

                    Debug.Print($"[{nameof(DataAccess)}.{nameof(Save)}<{instance.GetType().Name}>] Result: {res}");
                }

                CloseConnection(connection);
            }

            Console.WriteLine(sql);
        }

        /// <summary>
        /// Gets the table name associated to the <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to retreive the table name.</param>
        /// <returns>The name of the table from the <see cref="Table"/> attribute or it's default type name.</returns>
        private static string GetTableName<T>(IntegraBase<T> instance) where T: IntegraBase<T>
        {
            Table table = (Table)instance.GetType().GetCustomAttribute(typeof(Table));

            return table == null ? instance.GetType().Name : table.Name;
        }

        public static void Update<T>(IntegraBase<T> integraBase) where T: IntegraBase<T>
        {

        }

        public static void Delete<T>(IntegraBase<T> integraBase) where T: IntegraBase<T>
        {

        }

        public static void Truncate<T>(IntegraBase<T> integraBase) where T: IntegraBase<T>
        {
            string sql = $"TRUNCATE TABLE {integraBase.GetType().Name}";

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    var res = command.ExecuteNonQuery();

                    Debug.Print($"[{nameof(DataAccess)}.{nameof(Save)}<{integraBase.GetType().Name}>] Result: {res}");
                }

                CloseConnection(connection);
            }
        }

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
    }
}
