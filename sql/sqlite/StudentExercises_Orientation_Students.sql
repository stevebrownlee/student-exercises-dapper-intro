/* Add students - with a cohort FK - without knowing the integer value */
INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Thomas',
       'Bangs',
       '@thomas',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Evening Cohort 1'
;

INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Kate',
       'Williams',
       '@katerebekah',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Evening Cohort 1'
;


INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Tanner',
       'Terry',
       '@tannert44',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 10'
;


INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Sarah',
       'Story',
       '@sarah.story',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 10'
;


INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Jessawynne',
       'Parker',
       '@jessawynne',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 11'
;


INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Michael',
       'Conrad',
       '@threepears',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 11'
;


INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Dylan',
       'Thomas',
       '@kdylanthomas',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 12'
;


INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Jeremy',
       'Swain',
       '@jeremycswain',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 12'
;


INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Hannah',
       'Hall',
       '@hannahhall',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 13'
;


INSERT INTO Student (FirstName, LastName, SlackHandle, CohortId)
SELECT 'Sule',
       'Allen',
       '@sule.allen',
       c.Id
FROM Cohort c WHERE c.CohortName = 'Day Cohort 13'
;


