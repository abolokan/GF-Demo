# GF-Demo
Linq example, clear arhitecture, DDD

# Project Setup and Data Generation Guide

## Overview

This guide outlines the steps to set up the project, apply database migrations, and generate test data. It also includes SQL scripts to verify the data in the database.

## Steps

### Step 1: Modify Connection Strings for Migrations

1. Navigate to `Persistence.MigrationsHelper`.
2. Open `appsettings.json`.
3. Update the `ConnectionStrings` section with your database connection details.

### Step 2: Apply Migrations

1. Run the `Persistence.MigrationsHelper` project.
2. Ensure that all pending migrations are applied to the database.

### Step 3: Modify Connection Strings for the Web API

1. Navigate to `Web.API`.
2. Open `appsettings.json`.
3. Update the `ConnectionStrings` section with your database connection details.

### Step 4: Run the Web API

1. Run the `Web.API` project.
2. Open a browser and navigate to [https://localhost:7075](https://localhost:7075) to ensure the API is running.

### Step 5: Generate Test Data

1. Use the following POST request to generate test data:
    ```
    POST https://localhost:7075/test-data/generate/{fromYear}/{toYear}
    ```
    Replace `{fromYear}` and `{toYear}` with the desired range of years for which to generate data.

### Step 6: Retrieve Data

1. Use the following GET request to retrieve the generated data:
    ```
    GET https://localhost:7075/contracts/{year}
    ```
    Replace `{year}` with any year between `{fromYear}` and `{toYear}` specified in Step 5.
	
### Step 7: Run company-contracts ReactJs 'npm start' and open http://localhost:3000/ 

## SQL Scripts to Verify Data

### Script 1: Basic Data Check

```sql
DECLARE @company nvarchar(max) = '8A7D803E-A13A-430D-912A-406CFEFC831C';

SELECT * FROM Companies WHERE Id = @company;
SELECT * FROM Contracts WHERE ProviderId = @company;
SELECT * FROM Agreements WHERE ContractId IN (SELECT Id FROM Contracts WHERE ProviderId = @company) ORDER BY StartDate;
SELECT [Month], Hours, AgreementId 
FROM WorkHours 
WHERE AgreementId IN (SELECT Id FROM Agreements WHERE ContractId IN (SELECT Id FROM Contracts WHERE ProviderId = @company)) 
ORDER BY [Month];
```

### Script 2: Detailed Data Check

```sql
DECLARE @company nvarchar(max) = '8A7D803E-A13A-430D-912A-406CFEFC831C';

SELECT 
    c.Id AS CompanyId, 
    c.Name AS CompanyName, 
    cnt.Id AS ContractId, 
    cnt.ActiveFrom AS ContractActiveFrom, 
    cnt.ActiveTo AS ContractActiveTo, 
    agr.Id AS AgreementId, 
    agr.StartDate AS AgreementStartDate, 
    agr.Amount AS HourlyPrice, 
    wh.[Year], 
    wh.[Month], 
    wh.[Hours] 
FROM [fgdemo].[dbo].[Companies] AS c
INNER JOIN Contracts AS cnt ON c.Id = cnt.ProviderId
INNER JOIN Agreements AS agr ON cnt.Id = agr.ContractId
INNER JOIN WorkHours AS wh ON agr.Id = wh.AgreementId
WHERE c.Id = @company
ORDER BY agr.StartDate;
```

## Conclusion

By following these steps, you will successfully set up the project, apply necessary migrations, and generate test data. The provided SQL scripts can be used to verify that the data has been correctly inserted into the database.
