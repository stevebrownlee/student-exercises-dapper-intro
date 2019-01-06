/* Add students - with a cohort FK - without knowing the integer value */
INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Thomas',
       'Bangs',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Evening Cohort 1'
;

INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Kate',
       'Williams',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Evening Cohort 1'
;


INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Tanner',
       'Terry',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 10'
;


INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Sarah',
       'Story',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 10'
;


INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Jessawynn',
       'Parker',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 11'
;


INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Michael',
       'Conrad',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 11'
;


INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Dylan',
       'Thomas',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 12'
;


INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Jeremy',
       'Swain',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 12'
;


INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Hannah',
       'Hall',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 13'
;


INSERT INTO Student (FirstName, LastName, CohortId)
SELECT 'Sule',
       'Allen',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 13'
;


