using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using StudentExercises.Models;
using StudentExercises.Views;

namespace StudentExercises {
    public class StudentController {

        public static void Create () {
            // Show the user prompts to create a student
            Student student = CreateStudentView.Show();

            // Save student to database and store the updated student object
            student = Student.Create(student);

            // Attach a cohort instance to the student.Cohort property
            student.Cohort = Cohort.Get(student.CohortId);

            // Display student
            Console.WriteLine(student.ToString());
        }


    }

}