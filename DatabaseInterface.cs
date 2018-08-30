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

        public static void CheckExerciseTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Exercise> toys = db.Query<Exercise>
                    ($"SELECT e.Id FROM Exercise e").ToList();

            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    db.Execute($@"CREATE TABLE Exercise (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `Name`	varchar(80) NOT NULL,
                        `Language` varchar(80) NOT NULL
                    )");

                }
            }
        }
    }
}