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
                List<T> cohorts = db.Query<T>
                    ($"SELECT Id FROM {table}").ToList();
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

        public static void CheckCohortTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Cohort> cohorts = db.Query<Cohort>
                    ("SELECT Id FROM Cohort").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    Cohort.Create(db);
                    Cohort.Seed(db);
                }
            }
        }

        public static void CheckExerciseTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                // Select the ids from the table to see if it exists
                List<Exercise> ex = db.Query<Exercise>
                    ("SELECT Id FROM Exercise").ToList();
            }
            catch (System.Exception ex)
            {
                /*
                    If an exception was thrown with the text "no such table"
                    then the table doesn't exist. Execute a CREATE TABLE
                    statement to create it.
                */
                if (ex.Message.Contains("no such table"))
                {
                    Exercise.Create(db);
                    Exercise.Seed(db);
                }
            }
        }

        public static void CheckInstructorsTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Instructor> toys = db.Query<Instructor>
                    ("SELECT Id FROM Instructor").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    Instructor.Create(db);
                    Instructor.Seed(db);
                }
            }
        }

        public static void CheckStudentTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Student> toys = db.Query<Student>
                    ("SELECT Id FROM Student").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    Student.Create(db);
                    Student.Seed(db);
                }
            }
        }

        public static void CheckStudentExerciseTable()
        {
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<StudentExercise> se = db.Query<StudentExercise>
                    ("SELECT Id FROM StudentExercise").ToList();
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    StudentExercise.Create(db);
                    StudentExercise.Seed(db);
                }
            }
        }
    }
}