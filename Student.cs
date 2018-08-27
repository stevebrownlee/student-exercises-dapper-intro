using System.Collections.Generic;

namespace nss
{
    public class Student
    {
        private string _firstName;
        private string _lastName;

        public string Name
        {
            get => $"{_firstName} {_lastName}";
        }

        public List<Exercise> AssignedExercises { get; } = new List<Exercise>();

        public Student (string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }
    }
}