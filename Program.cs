using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using nss.Data;
using Dapper;

namespace nss
{
    class Program
    {
        static void Main(string[] args)
        {
            SqliteConnection db = DatabaseInterface.Connection;
            DatabaseInterface.CheckExerciseTable();
            DatabaseInterface.CheckCohortTable();
            DatabaseInterface.CheckInstructorsTable();
            DatabaseInterface.CheckStudentTable();

            db.Query<Exercise>(@"SELECT * FROM Exercise")
              .ToList()
              .ForEach(e => Console.WriteLine(e.Name));

            db.Query<Instructor>(@"SELECT * FROM Instructor")
              .ToList()
              .ForEach(i => Console.WriteLine($"{i.FirstName} {i.LastName}"));

            db.Query<Student>(@"SELECT * FROM Student")
              .ToList()
              .ForEach(i => Console.WriteLine($"{i.FirstName} {i.LastName}"));

            db.Query<Cohort>(@"SELECT * FROM Cohort")
              .ToList()
              .ForEach(i => Console.WriteLine($"{i.Name}"));



        }
    }
}
