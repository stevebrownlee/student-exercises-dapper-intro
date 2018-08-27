using System.Collections.Generic;

namespace nss
{
    public class Cohort
    {
        public string Program { get; set; }

        public int Number { get; set; }

        public List<Student> Students { get; } = new List<Student>();
        public List<Instructor> Instructors { get; } = new List<Instructor>();

        public Cohort (string program, int number)
        {
            Program = program;
            Number = number;
        }
    }
}