using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace StudentExercises {
    public class DatabaseInterface {
        public static SqliteConnection Connection {
            get {
                string connectionString = $"Data Source=./sql/sqlite/StudentExercises_Orientation.db";
                return new SqliteConnection (connectionString);
            }
        }

        public static void Query (string command, Action<SqliteDataReader> handler) {
            var connection = Connection;

            using (connection) {
                connection.Open ();
                SqliteCommand dbcmd = connection.CreateCommand ();
                dbcmd.CommandText = command;

                using (SqliteDataReader dataReader = dbcmd.ExecuteReader ()) {
                    handler (dataReader);
                }

                dbcmd.Dispose ();
                connection.Close ();
            }
        }

        public static void Delete (string command) {
            var connection = Connection;

            using (connection) {
                connection.Open ();
                SqliteCommand dbcmd = connection.CreateCommand ();
                dbcmd.CommandText = command;

                dbcmd.ExecuteNonQuery ();

                dbcmd.Dispose ();
                connection.Close ();
            }
        }

        public static int Insert (string command) {
            // var connection = Connection;
            int insertedItemId = 0;

            Query($"{command}; select last_insert_rowid();",
                (SqliteDataReader reader) => {
                    while (reader.Read ()) {
                        insertedItemId = reader.GetInt32 (0);
                    }
                }
            );

            return insertedItemId;
        }

    }
}
