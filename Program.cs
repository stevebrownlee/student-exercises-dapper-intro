using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using nss.Data;
using Dapper;

namespace nss
{
    class Program
    {
        static void Main(string[] args)
        {
            SqliteConnection db = DatabaseInterface.Connection;
            DatabaseInterface.CheckExerciseTable();

            db.Execute($@"INSERT INTO Exercise
                VALUES (null, 'ChickenMonkey', 'JavaScript')");
            db.Execute($@"INSERT INTO Exercise
                VALUES (null, 'Overly Excited', 'JavaScript')");
            db.Execute($@"INSERT INTO Exercise
                VALUES (null, 'Boy Bands & Vegetables', 'JavaScript')");


        }
    }
}
