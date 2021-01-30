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
                            batch.WriteToServer(IntegraCache.GetBatchData(collection));
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
        public static List<U> SelectAll<T, U>(IntegraBase<T> instance, IntegraDataTemplate<U> template) where T : IntegraBase<T> where U : IntegraDataTemplate<U>
        {
            List<U> result = new List<U>();

            string sql = $"SELECT * FROM {GetTableName(instance)}";

            Dictionary<string, PropertyInfo> templateCache = IntegraCache.GetDataTemplate<U>();

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
                                
                                Dictionary<string, PropertyInfo> parameters = IntegraCache.GetDataTemplate<T>();

                                foreach (var parameter in parameters)
                                {
                                    if(parameter.Value.PropertyType.GetInterfaces().Contains(typeof(IIntegraDataClass)))
                                    {
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
                                        
                                        //parameter.Value.SetValue(instance, reader.GetValue(offset + columnOffset));
                                        parameter.Value.SetValue(instance, reader.GetValue(reader.GetOrdinal(parameter.Key)));
                                        offset++;

                                        Debug.Print($"-{parameter.Value.Name, -20} = {parameter.Value.GetValue(instance)}");
                                    }

                                    //instance.NotifyPropertyChanged(parameter.Key);
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

       
        private const string SQL_SCHEMA_INDEXES = "IndexColumns";
        private const string SQL_SCHEMA_COLUMN_NAME = "column_name";
        private const string SQL_SCHEMA_COLUMN_ORDINAL = "ordinal_position";

        /// <summary>
        /// Gets the index columns for the specified <paramref name="instance"/> associated table.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance specifying the database table.</param>
        /// <returns>A list of key value pairs where the key is the <b><i>zero based</i></b> column ordinal and the value the column name of the table index.</returns>
        /// <remarks><i>SQL Column oridinals are not zero based.</i></remarks>
        private static Dictionary<int, string> GetIndex<T>(IntegraBase<T> instance) where T: IntegraBase<T>
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
                    keyColumns.Add((int)indexes.Rows[i][SQL_SCHEMA_COLUMN_ORDINAL]-1, indexes.Rows[i][SQL_SCHEMA_COLUMN_NAME].ToString());
                }
                
                CloseConnection(connection);
            }

            return keyColumns;
        }

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

            Dictionary<string, PropertyInfo> template = IntegraCache.GetDataTemplate<T>();

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

        /// <summary>
        /// Stores the specified <paramref name="instance"/> in the database.
        /// </summary>
        /// <typeparam name="T">The instance type specifier.</typeparam>
        /// <param name="instance">The instance to store.</param>
        /// <param name="parameters"></param>
        public static void Insert<T>(IntegraBase<T> instance) where T: IntegraBase<T>
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

            Dictionary<string, string> sqlp = IntegraCache.GetSQLParameters(instance, Device.Session.ID.ToString());

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

            // Insert all referenced data structures
            Dictionary<string, IIntegraDataClass> references = IntegraCache.GetReferences(instance);

            foreach (var reference in references)
            {
                reference.Value.Insert();
            }
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

        public static int Update<T>(IntegraBase<T> instance) where T: IntegraBase<T>
        {
            int result = 0;

            Debug.Print($"[{nameof(DataAccess)}.{nameof(Insert)}] {instance.GetType().Name}");

            if (!Exists(instance))
            {
                Debug.Print($"[{nameof(DataAccess)}.{nameof(Insert)}] {instance.GetType().Name} No record found to update.");
                //TODO: Insert?
                return 0;
            }

            Dictionary<string, string> sqlp = IntegraCache.GetSQLParameters(instance, Device.Session.ID.ToString());

            if (sqlp.Count == 0)
                return 0;

            string update = string.Join(",", sqlp.Select(x => x.Key + "=" + x.Value));

            // Where clause
            Dictionary<int, string> keyColumns = GetIndex(instance);

            string[] value = new string[keyColumns.Count];

            Dictionary<string, PropertyInfo> template = IntegraCache.GetDataTemplate<T>();

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

            // Insert all referenced data structures
            Dictionary<string, IIntegraDataClass> references = IntegraCache.GetReferences(instance);

            foreach (var reference in references)
            {
                reference.Value.Insert();
            }

            return result;
        }

        public static void Delete<T>(IntegraBase<T> instance) where T: IntegraBase<T>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(Delete)}<{instance.GetType().Name}>]");

            Dictionary<int, string> keyColumns = GetIndex(instance);

            string[] value = new string[keyColumns.Count];

            Dictionary<string, PropertyInfo> template = IntegraCache.GetDataTemplate<T>();

            foreach (var item in keyColumns.OrderBy(i => i.Key))
            {
                //PropertyInfo property = template[item.Value];
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

        public static void Truncate<T>(IntegraBase<T> integraBase) where T: IntegraBase<T>
        {
            Debug.Print($"[{nameof(DataAccess)}.{nameof(Truncate)}<{integraBase.GetType().Name}>]");

            string sql = $"TRUNCATE TABLE {integraBase.GetType().Name}";

            Debug.Print(sql);

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                using (var command = new SqlCommand(sql, connection))
                {
                    foreach(var reference in IntegraCache.GetReferences(integraBase))
                    {
                        reference.Value.Truncate();
                    }

                    var res = command.ExecuteNonQuery();

                    
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
