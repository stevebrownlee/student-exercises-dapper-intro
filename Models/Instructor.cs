using Dapper;
using Microsoft.Data.Sqlite;

namespace nss.Data
{
    public class Instructor : Model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public string Specialty { get; set; }
        public Cohort Cohort { get; set; }

        public void AssignExercise(Exercise e, Student s)
        {
            SqliteConnection db = DatabaseInterface.Connection;

            db.Execute($@"INSERT INTO StudentExercise
                        (ExerciseId, StudentId, InstructorId)
                        VALUES
                        ({e.Id}, {s.Id}, {this.Id})");
        }

        public static void Create(SqliteConnection db)
        {
            db.Execute($@"CREATE TABLE Instructor (
                `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                `FirstName`	TEXT NOT NULL,
                `LastName`	TEXT NOT NULL,
                `SlackHandle`	TEXT NOT NULL,
                `Specialty` TEXT,
                `CohortId`	INTEGER NOT NULL,
                FOREIGN KEY(`CohortId`) REFERENCES `Cohort`(`Id`)
            )");
        }

        public static void Seed(SqliteConnection db)
        {
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
