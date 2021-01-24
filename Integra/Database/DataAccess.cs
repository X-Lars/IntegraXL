using Integra.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Database
{
    public class DataAccess
    {
        public static int GetNextID()
        {
            int nextID;

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                string sql = "SELECT IDENT_CURRENT('StudioSet')";

                using (var command = new SqlCommand(sql, connection))
                {

                    nextID = Convert.ToInt32(command.ExecuteScalar());
                }

                CloseConnection(connection);

            }

            return nextID + 1;
        }

        public static void SaveSession(string name)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                string sql = $"INSERT INTO Sessions (Name) VALUES('{name}')";

                using (var command = new SqlCommand(sql, connection))
                {
                    var res = command.ExecuteNonQuery();
                    Console.WriteLine(res);
                }

                CloseConnection(connection);
            }
        }

        public static void Save<T>(IntegraBase<T> integraBase) where T: IntegraBase<T>
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                OpenConnection(connection);

                //string sql = $"INSERT INTO '{integraBase.GetType().Name}' ('ID', 'Name') VALUES('{Device.Session.SessionID}', 'Test')";
                string sql = $"INSERT INTO {integraBase.GetType().Name} (ID, Name) VALUES({Device.ID}, 'Test')";

                using (var command = new SqlCommand(sql, connection))
                {
                    //var res = command.ExecuteNonQuery();

                    //Debug.Print($"[{nameof(DataAccess)}.{nameof(Save)}<{integraBase.GetType().Name}>] Result: {res}");
                }

                CloseConnection(connection);
            }
        }

        public static void Save<T>(IntegraBase<T> integraBase, List<SQLParameter> parameters) where T: IntegraBase<T>
        {
            string columns = string.Empty;
            string values = string.Empty;

            columns = string.Join(",", parameters.Select(x => x.Name));

            for (int i = 0; i < parameters.Count; i++)
            {
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

            if(values.Length > 0)
                values = values.Remove(values.Length - 1);

            Console.WriteLine(columns);
            Console.WriteLine(values);

            string sql = $"INSERT INTO {integraBase.GetType().Name} ({columns}) VALUES({values})";
            Console.WriteLine(sql);
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Integra"].ConnectionString;
        }

        private static void OpenConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        private static void CloseConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }
}
