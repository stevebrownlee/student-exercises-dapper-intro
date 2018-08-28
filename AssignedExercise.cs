using System;

namespace nss
{
    public class AssignedExercise
    {
        public Exercise Exercise { get; }
        public Instructor Instructor { get; }
        public DateTime DateAssigned { get; }

        public AssignedExercise (Exercise exercise, Instructor coach, DateTime stamp)
        {
            Exercise = exercise;
            Instructor = coach;
            DateAssigned = stamp;
        }

    }
}