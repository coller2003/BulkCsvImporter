# BulkCsvImporter
This library let you import bulk csv file to the DB in a high efficiency. 

Realized with the batch importing feature provided by DB.(Only support SQL Server)

The columns mapping between DB table columns and csv columns are based on the configuration.

This library is depend on .Net Framework 4.6.2

Dependent library:

- [LumenWorksX.Framework.IO.CsvReader](https://www.nuget.org/packages/LumenWorksCsvReader/)
- [System.Data.SqlClient.SqlBulkCopy](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopy?view=netframework-4.7.2)

Dependent of database feature:

- [MERGE](https://docs.microsoft.com/en-us/sql/t-sql/statements/merge-transact-sql?view=sql-server-2017)
- [Temp table](https://docs.microsoft.com/en-us/azure/sql-data-warehouse/sql-data-warehouse-tables-temporary)

Supported data type of database:

- uniqueidentifier
- bit
- datetime
- decimal
- int
- varchar
- nvarchar

Here is how to use it:

```c#

//Define the column names mapping from csv to database table as a string array, the first column must be the PK column and should use guid as data type.
var columns = "Id,EmployeeNo,Name,Age,Column3".Split(',').ToList();
//Define the business PK as a string array, these columns should be included in the columns defined in above.
var keys = new List<string>() { "EmployeeNo" };
//Define the connection string.
var connectionString = "data source=.;initial catalog=sa;persist security info=True;user id=sa;password=123456;MultipleActiveResultSets=True;";

//Then here you can start the import.
var singleFileImportOption = new SingleFileImportOption()
                            .BuildDatabaseConnect(DatabaseType.SQLServer, connectionString)
                            //The second arg is the table name that you would like the csv import to.
                            .BuildImportTarget(true, "OverdueInfo", columns, keys)
                            .BuildLocalFileSource(@"E:\OverdueInfo.csv");
var importer = ImporterFactory.CreateInstance(singleFileImportOption);
importer.Import();
