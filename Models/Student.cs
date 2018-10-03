using System.Collections.Generic;
using Dapper;
using Microsoft.Data.Sqlite;

namespace nss.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort Cohort { get; set; }
        public List<Exercise> AssignedExercises { get; set; } = new List<Exercise>();

        public static void Create(SqliteConnection db)
        {
            db.Execute($@"CREATE TABLE Student (
                `Id`	       INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                `FirstName`	   TEXT NOT NULL,
                `LastName`	   TEXT NOT NULL,
                `SlackHandle`  TEXT NOT NULL,
                `CohortId`	   INTEGER NOT NULL,
                FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
            )");
        }

        public static void Seed(SqliteConnection db)
        {
            db.Execute($@"INSERT INTO Student
                SELECT null,
                        'Kate',
                        'Williams',
                        '@katerebekah',
                        c.Id
                FROM Cohort c WHERE c.Name = 'Evening Cohort 1'
            ");

            db.Execute($@"INSERT INTO Student
                SELECT null,
                        'Ryan',
                        'Tanay',
                        '@ryan.tanay',
                        c.Id
                FROM Cohort c WHERE c.Name = 'Day Cohort 10'
            ");

            db.Execute($@"INSERT INTO Student
                SELECT null,
                        'Juan',
                        'Rodriguez',
                        '@juanrod',
                        c.Id
                FROM Cohort c WHERE c.Name = 'Day Cohort 11'
            ");
        }
    }
}
