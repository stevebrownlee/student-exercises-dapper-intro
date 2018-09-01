using System.Collections.Generic;
using Dapper;
using Microsoft.Data.Sqlite;

namespace nss.Data
{
    public class Student : Model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort Cohort { get; set; }
        public List<Exercise> AssignedExercises { get; set; } = new List<Exercise>();

        public static void Create(SqliteConnection db)
        {
            db.Execute($@"CREATE TABLE Student (
                        `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                        `FirstName`	varchar(80) NOT NULL,
                        `LastName`	varchar(80) NOT NULL,
                        `SlackHandle`	varchar(80) NOT NULL,
                        `CohortId`	integer NOT NULL,
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