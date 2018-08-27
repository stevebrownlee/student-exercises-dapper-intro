using System.Collections.Generic;

namespace nss
{
    public class Student : Person
    {
        public Dictionary<Exercise, Instructor> AssignedExercises { get; }
            = new Dictionary<Exercise, Instructor>();

        public Student (string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }
    }
}