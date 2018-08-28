using System;
using System.Collections.Generic;
using System.Linq;

namespace nss
{
    class Program
    {
        static void Main(string[] args)
        {
            Cohort cohort11 = new Cohort("Day", 11);
            Student micah = new Student("Micah", "Wells");
            cohort11.Students.Add(micah);

            Instructor luke = new Instructor("Luke", "Lancaster");
            cohort11.Instructors.Add(luke);


            List<Exercise> exercises = new List<Exercise>() {
                new Exercise() { Id = 1, Name = "Overly Excited", LearningObjective = "Iteration"},
                new Exercise() { Id = 2, Name = "Boy Bands", LearningObjective = "Array methods"},
                new Exercise() { Id = 3, Name = "Car Lot", LearningObjective = "DOM Manipulation"},
                new Exercise() { Id = 4, Name = "Hulkify", LearningObjective = "Event Listeners"},
            };

            luke.AssignExercise(micah, exercises.Single(e => e.Id == 4));
            luke.AssignExercise(micah, exercises.Single(e => e.Id == 2));

            foreach (AssignedExercise ex in micah.AssignedExercises)
            {
                Console.WriteLine($"{micah.Name} has been assigned {ex.Exercise.Name} by {ex.Instructor.Name}");
            }

        }
    }
}
