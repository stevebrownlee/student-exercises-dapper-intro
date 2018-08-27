using System.Collections.Generic;

namespace nss
{
    public class Instructor : Person
    {
        public Instructor (string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public void AssignExercise (Student student, Exercise exercise)
        {
            /*
                Refactor this method and the `AssignedExercises` property
                on Student to keep track of which instructor assigned the
                exercise, and when.
            */
            student.AssignedExercises[exercise] = this;
        }

    }
}