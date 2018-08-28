using System.Collections.Generic;

namespace nss
{
    public class Student : Person, IPerson, IStudent
    {
        public List<AssignedExercise> AssignedExercises { get; } = new List<AssignedExercise>();
        public Computer Computer { get; set; }
        public double PreworkCompleted { get; set; }

        public Student (string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }
    }
}