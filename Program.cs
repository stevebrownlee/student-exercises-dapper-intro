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
            .ToList();
            // .ForEach(i => Console.WriteLine($"{i.FirstName} {i.LastName} ({i.SlackHandle}) is coaching {i.Cohort.Name}"));


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

            db.Query<Cohort, Instructor, Student, Cohort>(@"
                SELECT
                       c.Id,
                       c.Name,
                       i.Id,
                       i.FirstName,
                       i.LastName,
                       i.CohortId,
                       i.SlackHandle,
                       i.Specialty,
                       s.Id,
                       s.FirstName,
                       s.LastName,
                       s.CohortId,
                       s.SlackHandle
                FROM Cohort c
                LEFT JOIN Instructor i ON c.Id = i.CohortId
                LEFT JOIN Student s ON c.Id = s.CohortId
            ", (cohort, instructor, student) =>
            {
                if (!report.ContainsKey(cohort.Id))
                {
                    report[cohort.Id] = cohort;
                }

                if (
                    instructor != null &&
                    report[cohort.Id].Instructors.Where(i => i.Id == instructor.Id).Count() == 0
                   )
                {
                    report[cohort.Id].Instructors.Add(instructor);
                }
                if (student != null)
                {
                    report[cohort.Id].Students.Add(student);
                }
                return cohort;
            });

            /*
                Iterate the key/value pairs in the dictionary
             */
            foreach (KeyValuePair<int, Cohort> cohort in report)
            {
                Console.WriteLine($"{cohort.Value.Name} has the following instructors and students:");
                cohort.Value.Instructors.Distinct().ToList().ForEach(i => Console.WriteLine($"\t- {i.FirstName} {i.LastName} is an instructor"));
                cohort.Value.Students.ForEach(i => Console.WriteLine($"\t- {i.FirstName} {i.LastName} is a student"));
            }


            Console.WriteLine("\n\n\n");



            Dictionary<int, Exercise> exerciseStudents = new Dictionary<int, Exercise>();

            db.Query<Exercise, Student, Exercise>(@"
                SELECT
                       e.Id,
                       e.Name,
                       e.Language,
                       s.Id,
                       s.FirstName,
                       s.LastName,
                       s.CohortId,
                       s.SlackHandle
                FROM Exercise e
                JOIN StudentExercise se ON se.ExerciseId = e.Id
                JOIN Student s ON s.Id = se.StudentId
            ", (exercise, student) =>
            {
                if (!exerciseStudents.ContainsKey(exercise.Id))
                {
                    exerciseStudents[exercise.Id] = exercise;
                }

                exerciseStudents[exercise.Id].AssignedStudents.Add(student);
                return exercise;
            });

            /*
                Iterate the key/value pairs in the dictionary
             */
            foreach (KeyValuePair<int, Exercise> e in exerciseStudents)
            {
                Console.WriteLine($"{e.Value.Name} has the following students working on it.");
                e.Value.AssignedStudents.ForEach(s => Console.WriteLine($"\t- {s.FirstName} {s.LastName}"));
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
