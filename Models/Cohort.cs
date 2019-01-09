using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace StudentExercises.Models {
    public class Cohort {

        #region Properties

        public int Id { get; set; }
        public string CohortName { get; set; }
        public List<Student> Students { get; set; } = new List<Student> ();
        public List<Instructor> Instructors { get; set; } = new List<Instructor> ();

        #endregion


        #region Instance Methods

        public override string ToString() {
            return CohortName;
        }

        #endregion


        #region Static Methods

        public static List<Cohort> Get () {
            List<Cohort> cohorts = new List<Cohort> ();

            DatabaseInterface.Query (
                @"SELECT
                    c.Id,
                    c.CohortName
                FROM Cohort c",
                (SqliteDataReader reader) => {
                    while (reader.Read ()) {
                        cohorts.Add (new Cohort () {
                            Id = reader.GetInt32 (0),
                            CohortName = reader[1].ToString ()
                        });
                    }
                }
            );

            return cohorts;
        }

        public static Cohort Get (int id) {
            Cohort cohort = null;

            DatabaseInterface.Query (
                $@"SELECT
                    c.Id,
                    c.CohortName
                FROM Cohort c
                WHERE c.Id = {id}
                ",
                (SqliteDataReader reader) => {
                    while (reader.Read ()) {
                        cohort = new Cohort () {
                            Id = reader.GetInt32 (0),
                            CohortName = reader[1].ToString ()
                        };
                    }
                }
            );

            return cohort;
        }

        public static void CheckTable () {
            var connection = DatabaseInterface.Connection;

            using (connection) {
                connection.Open ();
                SqliteCommand dbcmd = connection.CreateCommand ();

                // Query the child table to see if table is created
                dbcmd.CommandText = $"SELECT Id FROM Cohort";

                try {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader ()) { }
                    dbcmd.Dispose ();
                } catch (Microsoft.Data.Sqlite.SqliteException ex) {
                    if (ex.Message.Contains ("no such table")) {
                        dbcmd.CommandText = $@"CREATE TABLE `Student` (
                            Id  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            Name  TEXT NOT NULL
                        );";
                        try {
                            dbcmd.ExecuteNonQuery ();
                        } catch (Microsoft.Data.Sqlite.SqliteException) {
                            Console.WriteLine ("Table already exists. Ignoring");
                        }
                        dbcmd.Dispose ();
                    }
                }
                connection.Close ();
            }
        }

        #endregion
    }
}
