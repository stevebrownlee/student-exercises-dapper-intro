using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using StudentExercises.Models;

/*
    To install required packages from NuGet
        1. `dotnet add package Microsoft.Data.Sqlite`
        2. `dotnet add package Dapper`
        3. `dotnet restore`
 */

namespace StudentExercises {
    class Program {
        static void Main (string[] args) {
            // Verify that the tables exist in the database
            Student.CheckTable ();
            Cohort.CheckTable ();

            // Get all students
            Student.Get().ForEach( Console.WriteLine );

            // Choice will hold the number entered by the user
            // after main menu ws displayed
            int choice;

            do {
                // Show the main menu
                choice = MainMenu.Show ();

                switch (choice) {
                    // Menu option 1: Adding cohort
                    case 1:
                        StudentController.Create ();
                        break;

                    // Menu option 2: Adding student
                    case 2:
                        StudentController.Create ();
                        break;
                }
            } while (choice != 5);

            /*
                1. Create Exercises table and seed it
                2. Create Student table and seed it  (use sub-selects)
                3. Create StudentExercise table and seed it (use sub-selects)
                4. List the instructors and students assigned to each cohort
                5. List the students working on each exercise, include the
                   student's cohort and the instructor who assigned the exercise
             */
        }
    }
}