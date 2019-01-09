using System;
using System.Collections.Generic;
using StudentExercises.Models;

namespace StudentExercises.Views {
    public class CreateStudentView {
        public static Student Show () {
            Student student = new Student ();

            Console.Clear ();

            Console.WriteLine ("First name");
            Console.Write ("> ");
            student.FirstName = Console.ReadLine ();

            Console.WriteLine ("Last name");
            Console.Write ("> ");
            student.LastName = Console.ReadLine ();

            Console.WriteLine ("Slack handle");
            Console.Write ("> ");
            student.SlackHandle = Console.ReadLine ();

            Console.WriteLine ("Choose cohort");
            foreach (Cohort cohort in Cohort.Get())
            {
                Console.WriteLine($"{cohort.Id}. {cohort.CohortName}");
            }
            student.CohortId = Int32.Parse(Console.ReadLine ());

            return student;
        }
    }
}