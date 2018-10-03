using System.Collections.Generic;
using Dapper;
using Microsoft.Data.Sqlite;

namespace nss.Data
{
    public class Cohort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();

        public static void Create(SqliteConnection db)
        {
            db.Execute(@"CREATE TABLE Cohort (
                `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                `Name`	TEXT NOT NULL UNIQUE
            )");
        }

        public static void Seed(SqliteConnection db)
        {
            db.Execute(@"INSERT INTO Cohort VALUES (null, 'Evening Cohort 1')");
            db.Execute(@"INSERT INTO Cohort VALUES (null, 'Day Cohort 10')");
            db.Execute(@"INSERT INTO Cohort VALUES (null, 'Day Cohort 11')");
            db.Execute(@"INSERT INTO Cohort VALUES (null, 'Day Cohort 12')");
            db.Execute(@"INSERT INTO Cohort VALUES (null, 'Day Cohort 13')");
            db.Execute(@"INSERT INTO Cohort VALUES (null, 'Day Cohort 21')");
        }
    }
}
