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
                string _connectionString = $"Data Source=./StudentExercises.db";
                return new SqliteConnection(_connectionString);
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
                    db.Execute(@"CREATE TABLE Cohort (
                        `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `Name`	TEXT NOT NULL UNIQUE
                    )");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Evening Cohort 1')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 10')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 11')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 12')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 13')");

                    db.Execute(@"INSERT INTO Cohort
                        VALUES (null, 'Day Cohort 21')");

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
                    db.Execute($@"CREATE TABLE Instructor (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `FirstName`	varchar(80) NOT NULL,
                        `LastName`	varchar(80) NOT NULL,
                        `SlackHandle`	varchar(80) NOT NULL,
                        `Specialty` varchar(80),
                        `CohortId`	integer NOT NULL,
                        FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
                    )");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Steve',
                              'Brownlee',
                              '@coach',
                              'Dad jokes',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Evening Cohort 1'
                    ");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Joe',
                              'Shepherd',
                              '@joes',
                              'Analogies',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 13'
                    ");

                    db.Execute($@"INSERT INTO Instructor
                        SELECT null,
                              'Jisie',
                              'David',
                              '@jisie',
                              'Student success',
                              c.Id
                        FROM Cohort c WHERE c.Name = 'Day Cohort 21'
                    ");
                }
            }
        }
    }
}