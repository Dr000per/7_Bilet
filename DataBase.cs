using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_Bilet
{
    internal class DataBase : IDisposable
    {
        bool _isConnected;
        SqlConnection _connection;
        string _connectionString = @"Data Source = DESKTOP-BBAJL13\SQLEXPRESS; Initial Catalog = bileti; Integrated Security = True;";      // для своего ПК
        // string _connectionString = @"Server = db.edu.cchgeu.ru; DataBase = 193_Shelylin; User = 193_Shelylin; Password = 193_Shelylin;"  // для базы теха

        public DataBase()
        {
            OpenConnection();
        }

        public void OpenConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _isConnected = true;
        }

        public void CloseConnection()
        {
            _connection.Close();
            _isConnected = false;
        }

        public DataTable ExecuteSql(string sql)
        {
            DataTable table = new DataTable();
            SqlCommand command = new SqlCommand(sql, _connection);
            var reader = command.ExecuteReader();
            table.Load(reader);

            return table;
        }

        public void ExecuteSqlNonQuery(string sql)
        {
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
