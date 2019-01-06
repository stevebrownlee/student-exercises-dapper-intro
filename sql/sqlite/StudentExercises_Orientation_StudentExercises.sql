/*
    Assign exercises to students
*/
INSERT INTO StudentExercise (`StudentId`, `ExerciseId`)
SELECT
    s.Id, e.Id
FROM
    Exercise e, Student s
WHERE
    e.`Name` = 'Junkyard'
    AND s.FirstName = 'Tanner';

/*
INSERT INTO StudentExercise (`StudentId`, `ExerciseId`)
VALUES
(ABS(RANDOM() / 1000000000000000000) + 1, ABS(RANDOM() / 1000000000000000000) + 1)
;
*/