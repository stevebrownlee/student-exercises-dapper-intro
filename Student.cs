using System.Collections.Generic;

namespace nss
{
    public class Student : Person
    {
        public List<AssignedExercise> AssignedExercises { get; } = new List<AssignedExercise>();

        public Student (string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }
    }
}