USE master;

IF DB_ID('Test') IS NOT NULL
BEGIN
    DROP DATABASE [Test];
END

CREATE DATABASE [Test];

GO