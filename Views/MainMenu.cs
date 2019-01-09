using System;

namespace StudentExercises {
    public class MainMenu {
        public static int Show () {
            Console.Clear ();
            Console.WriteLine ("WELCOME TO STUDENT EXERCISES SYSTEM");
            Console.WriteLine ("***********************************");
            Console.WriteLine ("1. Add a cohort");
            Console.WriteLine ("2. Add a student");
            Console.WriteLine ("3. Add an exercise");
            Console.WriteLine ("4. Assign an exercise to a student");
            Console.WriteLine ("5. Exit");
            Console.Write ("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey ();
            Console.WriteLine ("");
            return int.Parse (enteredKey.KeyChar.ToString ());
        }
    }
}