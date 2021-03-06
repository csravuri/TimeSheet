
IF NOT EXISTS(SELECT * FROM sys.sysdatabases WHERE name='TimeSheet')
BEGIN
	CREATE DATABASE TimeSheet
END
GO

USE TimeSheet
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=N'TimeSheetDetails')
BEGIN	
	CREATE TABLE TimeSheetDetails
	(
		ID INT IDENTITY PRIMARY KEY,
		TaskID VARCHAR(100),
		TaskName VARCHAR(2000),
		ClientName VARCHAR(100),
		TaskDate DATETIME,
		TaskStartTime TIME,
		TaskEndTime TIME,
		IsCoded BIT,
		IsReviewed BIT,
		IsCheckin BIT,
		Comment VARCHAR(5000)
	)		
END
GO


IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=N'ClientDetails')
BEGIN	
	CREATE TABLE ClientDetails
	(
		ID INT IDENTITY PRIMARY KEY,
		ClientName VARCHAR(100),
		ClientDescription VARCHAR(200)
	)		
END
GO



