# BulkCsvImporter
This library let you import bulk csv file to the DB in a high efficiency. 

Realized with the batch importing feature provided by DB.(Only support SQL Server)

The columns mapping between DB table columns and csv columns are based on the configuration.

Dependent library:
- LumenWorksX.Framework.IO.CsvReader

Supported data type of database:

- uniqueidentifier
- bit
- datetime
- decimal
- int
- varchar
- nvarchar
