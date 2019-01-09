using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace StudentExercises.Models {
    public class Student {

        #region Properties

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public int CohortId { get; set; }
        public Cohort Cohort { get; set; }
        public List<Exercise> AssignedExercises { get; set; } = new List<Exercise> ();

        #endregion


        #region Instance Methods

        public override string ToString () {
            return $"{FirstName} {LastName} has an Id of {Id} and was in {Cohort}";
        }

        #endregion


        #region Static Methods
        public static Student Get (int id) {
            Student student = null;

            DatabaseInterface.Query (
                $@"SELECT
                    s.Id,
                    s.FirstName,
                    s.LastName,
                    s.CohortId
                FROM Student s
                WHERE s.Id = {id}",
                (SqliteDataReader reader) => {
                    while (reader.Read ()) {
                        student = new Student () {
                            Id = reader.GetInt32 (0),
                            FirstName = reader[1].ToString (),
                            LastName = reader[2].ToString (),
                            CohortId = reader.GetInt32 (3)
                        };
                    }
                }
            );

            return student;
        }

        public static List<Student> Get () {
            List<Student> students = new List<Student> ();

            DatabaseInterface.Query (
                @"SELECT
                    s.Id,
                    s.FirstName,
                    s.LastName,
                    s.CohortId
                FROM Student s
                ",
                (SqliteDataReader reader) => {
                    while (reader.Read ()) {
                        students.Add (new Student () {
                            Id = reader.GetInt32 (0),
                            FirstName = reader[1].ToString (),
                            LastName = reader[2].ToString (),
                            CohortId = reader.GetInt32 (3)
                        });
                    }
                }
            );

            return students;
        }

        public static Student Create (Student student) {
            student.Id = DatabaseInterface.Insert ($@"
                INSERT INTO Student
                    (FirstName, LastName, SlackHandle, CohortId)
                VALUES
                    ('{student.FirstName}', '{student.LastName}', '{student.SlackHandle}', {student.CohortId})
                ");

            return student;
        }

        public static void CheckTable () {
            var connection = DatabaseInterface.Connection;

            using (connection) {
                connection.Open ();
                SqliteCommand dbcmd = connection.CreateCommand ();

                // Query the child table to see if table is created
                dbcmd.CommandText = $"SELECT Id FROM Student";

                try {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader ()) { }
                    dbcmd.Dispose ();
                } catch (Microsoft.Data.Sqlite.SqliteException ex) {
                    if (ex.Message.Contains ("no such table")) {
                        dbcmd.CommandText = $@"CREATE TABLE `Student` (
                            Id  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            FirstName  TEXT NOT NULL,
                            LastName TEXT NOT NULL,
                            CohortId INTEGER NOT NULL,
                            FOREIGN KEY(`CohortId`) REFERENCES Cohort(`Id`)
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
