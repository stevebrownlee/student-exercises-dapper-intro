using System.Collections.Generic;
using Dapper;
using Microsoft.Data.Sqlite;

namespace nss.Data
{
    public class Exercise : Model
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public List<Student> AssignedStudents { get; set; } = new List<Student>();

        public static void Create(SqliteConnection db)
        {
            db.Execute(@"CREATE TABLE Exercise (
                `Id`	   INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                `Name`	   TEXT NOT NULL,
                `Language` TEXT NOT NULL
            )");
        }

        public static void Seed(SqliteConnection db)
        {
            db.Execute(@"INSERT INTO Exercise VALUES (null, 'ChickenMonkey', 'JavaScript')");
            db.Execute(@"INSERT INTO Exercise VALUES (null, 'Overly Excited', 'JavaScript')");
            db.Execute(@"INSERT INTO Exercise VALUES (null, 'Boy Bands & Vegetables', 'JavaScript')");
        }
    }
}
