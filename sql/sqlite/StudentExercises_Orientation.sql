CREATE TABLE `Cohort` (
    Id           INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    CohortName   TEXT NOT NULL
);

CREATE TABLE `Student` (
	Id  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	FirstName  TEXT NOT NULL,
	LastName TEXT NOT NULL,
	CohortId INTEGER NOT NULL,
	FOREIGN KEY(`CohortId`) REFERENCES Cohort(`Id`)
);

CREATE TABLE `Exercise` (
    Id		 INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Name	 TEXT NOT NULL,
    Language TEXT NOT NULL
);

CREATE TABLE `StudentExercise` (
    Id	        INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    ExerciseId	INTEGER NOT NULL,
    StudentId 	INTEGER NOT NULL,
    InstructorId 	INTEGER NOT NULL,
    FOREIGN KEY(`ExerciseId`) REFERENCES Exercise(`Id`),
    FOREIGN KEY(`StudentId`) REFERENCES Student(`Id`)
);
