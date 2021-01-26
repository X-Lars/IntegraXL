using Integra.Core;
using Integra.Core.Interfaces;
using System;
using System.Collections.Generic;
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
        private static Dictionary<Type, List<SQLParameter>> _DataTemplateCache = new Dictionary<Type, List<SQLParameter>>();

        /// <summary>
        /// Gets the next highest ID to use for data storage.
        /// </summary>
        /// <typeparam name="T">The type of class specifying the table name to retreive the highest ID from.</typeparam>
        /// <param name="integraBase">The data structure specifying the table name.</param>
        /// <returns>The next free ID for data storage.</returns>
        internal static int GetNextID<T>(IntegraBase<T> integraBase) where T: IntegraBase<T>
        {
            int result;

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                string sql = $"SELECT MAX(ID) FROM {typeof(T).Name}";

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

        public static List<U> Select<T, U>(IntegraBase<T> integraBase, IntegraDataTemplate<U> items) where T: IntegraBase<T> where U: IntegraDataTemplate<U>
        {
            List<U> result = new List<U>();

            string sql = $"SELECT * FROM {typeof(T).Name}";

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
                                    PropertyInfo property = items.PropertyCache[reader.GetName(i)];

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
        /// Gets the data structure with the specified <paramref name="id"/> from the database.
        /// </summary>
        /// <typeparam name="T">The type of class specifying the table name to retreive the data structure from.</typeparam>
        /// <param name="integraBase">The data structure to load.</param>
        /// <param name="id">The ID of the data structure to load.</param>
        public static int Load<T>(IntegraBase<T> integraBase, int id) where T: IntegraBase<T>
        {
            int result = 0;

            string sql = $"SELECT * FROM {typeof(T).Name} WHERE ID = {id}";

            // Skip the ID column
            int columnOffset = 1;

            // Skip the Part column
            // Add the AND clause
            if (integraBase.GetType().GetInterfaces().Contains(typeof(IIntegraPartial)))
            {
                sql += $" AND Part = {(byte)((IIntegraPartial)integraBase).Part}";
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

                                for (int i = 0; i < integraBase.Parameters.Count; i++)
                                {
                                    SQLData data = integraBase.Parameters[i];

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


        public static void Save<T>(IntegraBase<T> integraBase, List<SQLParameter> parameters, bool IsPartial = false, bool IsIdentity = false) where T: IntegraBase<T>
        {
            string columns = string.Empty;
            columns += IsIdentity == true ? string.Empty : "ID,";
            columns += IsPartial == true ? "Part," : string.Empty;

            string values = string.Empty;
            values += IsIdentity == true ? string.Empty : $"{Device.Session.SessionID},";
            values += IsPartial == true ? $"{(byte)((IIntegraPartial)integraBase).Part}," : string.Empty;

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

            if (columns.Length > 0)
                columns = columns.Remove(columns.Length - 1);

            if(values.Length > 0)
                values = values.Remove(values.Length - 1);

            // Use column names
            //string sql = $"INSERT INTO {integraBase.GetType().Name} ({columns}) VALUES({values})";

            // Use column index
            string sql = $"INSERT INTO {integraBase.GetType().Name} ({columns}) VALUES({values})";

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

            Console.WriteLine(sql);
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
