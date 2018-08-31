using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using nss.Data;
using Dapper;

/*
    To install required packages from NuGet
        1. `dotnet add package Microsoft.Data.Sqlite`
        2. `dotnet add package Dapper`
        3. `dotnet restore`
 */

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
            DatabaseInterface.CheckStudentExerciseTable();

            db.Query<Instructor>(@"SELECT * FROM Instructor")
              .ToList()
              .ForEach(i => Console.WriteLine($"{i.FirstName} {i.LastName}"));



            db.Query<Cohort>(@"SELECT * FROM Cohort")
              .ToList()
              .ForEach(i => Console.WriteLine($"{i.Name}"));


            /*
                Query the database for each instructor, and join in the instructor's cohort.
                Since an instructor is only assigned to one cohort at a time, you can simply
                assign the corresponding cohort as a property on the instance of the
                Instructor class that is created by Dapper.
             */
            db.Query<Instructor, Cohort, Instructor>(@"
                SELECT i.CohortId,
                       i.FirstName,
                       i.LastName,
                       i.SlackHandle,
                       i.Specialty,
                       i.Id,
                       c.Id,
                       c.Name
                FROM Instructor i
                JOIN Cohort c ON c.Id = i.CohortId
            ", (instructor, cohort) =>
            {
                instructor.Cohort = cohort;
                return instructor;
            })
            .ToList()
            .ForEach(i => Console.WriteLine($"{i.FirstName} {i.LastName} ({i.SlackHandle}) is coaching {i.Cohort.Name}"));


            /*
                Querying the database in the opposite direction is noticeably more
                complex and abstract. In the query below, you start with the Cohort
                table, and join the Instructor table. Since more than one instructor
                can be assigned to a Cohort, then you get multiple rows in the result.

                Example:
                    1,"Evening Cohort 1",1,"Steve","Brownlee",1,"@coach","Dad jokes"
                    5,"Day Cohort 13",2,"Joe","Shepherd",5,"@joes","Analogies"
                    6,"Day Cohort 21",3,"Jisie","David",6,"@jisie","Student success"
                    6,"Day Cohort 21",4,"Emily","Lemmon",6,"@emlem","Latin"

                If you want to consolidate both Jisie and Emily into a single
                collection of Instructors assigned to Cohort 21, you will need to
                create a Dictionary and build it up yourself from the result set.

                - The unique keys in the Dictionary will be Id of each Cohort
                - The value will be an instance of the Cohort class, which has an
                        Instructors property.
             */
            Dictionary<int, Cohort> report = new Dictionary<int, Cohort>();

            db.Query<Cohort, Instructor, Cohort>(@"
                SELECT
                       c.Id,
                       c.Name,
                       i.Id,
                       i.FirstName,
                       i.LastName,
                       i.CohortId,
                       i.SlackHandle,
                       i.Specialty
                FROM Cohort c
                JOIN Instructor i ON c.Id = i.CohortId
            ", (cohort, instructor) =>
            {
                // Does the Dictionary already have the key of the cohort Id?
                if (!report.ContainsKey(cohort.Id))
                {
                    // Create the entry in the dictionary
                    report[cohort.Id] = cohort;
                }

                // Add the instructor to the current cohort entry in Dictionary
                report[cohort.Id].Instructors.Add(instructor);
                return cohort;
            });

            /*
                Iterate the key/value pairs in the dictionary
             */
            foreach (KeyValuePair<int, Cohort> cohort in report)
            {
                Console.WriteLine($"{cohort.Value.Name} has {cohort.Value.Instructors.Count} instructors.");
            }


            Dictionary<int, Student> studentExercises = new Dictionary<int, Student>();

            db.Query<Student, Exercise, Student>(@"
                SELECT
                       s.Id,
                       s.FirstName,
                       s.LastName,
                       s.SlackHandle,
                       e.Id,
                       e.Name,
                       e.Language
                FROM Student s
                JOIN StudentExercise se ON s.Id = se.StudentId
                JOIN Exercise e ON se.ExerciseId = e.Id
            ", (student, exercise) =>
            {
                if (!studentExercises.ContainsKey(student.Id))
                {
                    studentExercises[student.Id] = student;
                }
                studentExercises[student.Id].AssignedExercises.Add(exercise);
                return student;
            });

            foreach (KeyValuePair<int, Student> student in studentExercises)
            {
                List<string> assignedExercises = new List<string>();
                student.Value.AssignedExercises.ForEach(e => assignedExercises.Add(e.Name));
                Console.WriteLine($@"{student.Value.FirstName} {student.Value.LastName} is working on {String.Join(',', assignedExercises)}.");
            }




            /*
                1. Create Exercises table and seed it
                2. Create Student table and seed it  (use sub-selects)
                3. Create StudentExercise table and seed it (use sub-selects)
                4. List the instructors and students assigned to each cohort
                5. List the students working on each exercise
             */
        }
    }
}
