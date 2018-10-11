CREATE TABLE Cohort (
    Id           INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    CohortName   TEXT NOT NULL
);


CREATE TABLE Student (
	Id  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	FirstName  TEXT NOT NULL,
	LastName TEXT NOT NULL,
	CohortId INTEGER NOT NULL,
	FOREIGN KEY(CohortId) REFERENCES Cohort(Id)
);

INSERT INTO Cohort (CohortName) VALUES ('Day Cohort 25');
INSERT INTO Cohort (CohortName) VALUES ('Day Cohort 26');
INSERT INTO Cohort (CohortName) VALUES ('Day Cohort 27');
INSERT INTO Cohort (CohortName) VALUES ('Day Cohort 28');

INSERT INTO Student (FirstName, LastName, CohortId)
SELECT "Xavier",
       "Dimitriev",
       c.CohortId
FROM Cohort c WHERE c.CohortName = "Day Cohort 25"
;
