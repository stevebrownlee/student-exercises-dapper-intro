using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Collections;
using Dapper;

namespace nss.Data
{
    public class DatabaseInterface
    {
        public static SqliteConnection Connection
        {
            get
            {
                string env = $"{Environment.GetEnvironmentVariable("NSS_DB")}";
                string _connectionString = $"Data Source={env}";
                return new SqliteConnection(_connectionString);
            }
        }

        public static void CheckTable<T>(
            string table,
            Action<SqliteConnection> create,
            Action<SqliteConnection> seed
        )
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                IEnumerable<T> resources = db.Query<T>($"SELECT Id FROM {table}");
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    create(db);
                    seed(db);
                }
            }
        }
    }
}