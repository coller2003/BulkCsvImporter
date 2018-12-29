using BulkCsvImporter.Abstract;
using BulkCsvImporter.Model;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace BulkCsvImporter.DatabaseImporter
{
    internal class SqlServerImporter : Importer
    {
        public SqlServerImporter(SingleFileImportOption singleFileImportOption) : base(singleFileImportOption)
        { }

        private void CreateTempTable(SqlConnection connection, CsvReader csvReader)
        {
            using (var command = connection.CreateCommand())
            {
                var sql = new StringBuilder();

                sql.AppendLine("CREATE TABLE ##columnTmp(id int, columnName VARCHAR(100));");
                sql.AppendLine("INSERT INTO ##columnTmp(id, columnName)");

                var columns = _singleFileImportOption.ImportTargetOption.Columns;
                for (int i = 0; i < columns.Count; i++)
                {
                    csvReader.Columns.Add(new Column() { Name = columns[i] });
                    sql.AppendLine("SELECT " + i + ",'" + columns[i] + "' UNION ");
                }
                sql = sql.Remove(sql.Length - 9, 9);
                sql.AppendLine(";");
                sql.AppendLine($"SELECT c.* FROM information_schema.columns c INNER JOIN ##columnTmp n ON n.columnName = c.COLUMN_NAME WHERE c.[TABLE_NAME] = @tableName ORDER BY n.id;");
                sql.AppendLine("DROP TABLE ##columnTmp;");
                command.CommandText = sql.ToString();
                command.Parameters.Add("@tableName", SqlDbType.VarChar).Value = _singleFileImportOption.ImportTargetOption.TargetTableName;

                var columnList = new List<ColumnInfo>();
                // Loop over the results and create a ColumnInfo object for each Column in the schema.
                using (IDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (reader.Read())
                    {
                        var column = new ColumnInfo().ReadFromReader(reader);
                        columnList.Add(column);

                        if (reader["DATA_TYPE"].ToString() == "uniqueidentifier")
                        {
                            csvReader.Columns.Single(c => c.Name == reader["COLUMN_NAME"].ToString()).Type = typeof(Guid);
                        }
                        else if (reader["DATA_TYPE"].ToString() == "bit")
                        {
                            csvReader.Columns.Single(c => c.Name == reader["COLUMN_NAME"].ToString()).Type = typeof(bool);
                        }
                        else if (reader["DATA_TYPE"].ToString() == "datetime")
                        {
                            csvReader.Columns.Single(c => c.Name == reader["COLUMN_NAME"].ToString()).Type = typeof(DateTime);
                        }
                        else if (reader["DATA_TYPE"].ToString() == "decimal")
                        {
                            csvReader.Columns.Single(c => c.Name == reader["COLUMN_NAME"].ToString()).Type = typeof(decimal);
                        }
                        else if (reader["DATA_TYPE"].ToString() == "int")
                        {
                            csvReader.Columns.Single(c => c.Name == reader["COLUMN_NAME"].ToString()).Type = typeof(int);
                        }
                        else
                        {
                            csvReader.Columns.Single(c => c.Name == reader["COLUMN_NAME"].ToString()).Type = typeof(string);
                        }
                    }
                }

                string createTempCommand = "CREATE TABLE {0} ({1})";
                StringBuilder sb = new StringBuilder();
                // Loop over each column info object and construct the string needed for the SQL script.
                foreach (var column in columnList)
                {
                    sb.Append(column.ToString());
                }

                // create temp table
                command.CommandText = string.Format(createTempCommand, "##" + _singleFileImportOption.ImportTargetOption.TargetTableName,
                                      string.Join(",", columnList.Select(c => c.ToString()).ToArray()));
                command.ExecuteNonQuery();
            }
        }

        private string GenerateOnlyInsertSql()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"BEGIN TRANSACTION T1;");
            builder.AppendLine($"INSERT INTO [dbo].[{_singleFileImportOption.ImportTargetOption.TargetTableName}] (");

            var columns = _singleFileImportOption.ImportTargetOption.Columns;
            foreach (var column in columns)
            {
                builder.AppendLine($"[{column}],");
            }
            builder = builder.Remove(builder.Length - 3, 3);
            builder.AppendLine($")");
            builder.AppendLine($"SELECT newid(),");

            foreach (var column in columns)
            {
                builder.AppendLine($"{column},");
            }

            builder = builder.Remove(builder.Length - 3, 3);
            builder.AppendLine($" FROM ##{_singleFileImportOption.ImportTargetOption.TargetTableName};");
            builder.AppendLine($" DROP TABLE ##{_singleFileImportOption.ImportTargetOption.TargetTableName}");
            builder.AppendLine("COMMIT TRANSACTION T1;");

            return builder.ToString();
        }

        private string GenerateMergeSql()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"BEGIN TRANSACTION T1;");
            builder.AppendLine($"MERGE [dbo].[{_singleFileImportOption.ImportTargetOption.TargetTableName}] AS T");
            builder.AppendLine($"USING ##{_singleFileImportOption.ImportTargetOption.TargetTableName} AS S");
            builder.AppendLine($"ON (1 = 1");

            foreach (var key in _singleFileImportOption.ImportTargetOption.KeyColumns)
            {
                builder.AppendLine($" AND T.[{key}] = S.[{key}]");
            }

            builder.AppendLine(")");

            builder.AppendLine($"WHEN NOT MATCHED BY TARGET");
            builder.AppendLine($"THEN INSERT (");

            var columns = _singleFileImportOption.ImportTargetOption.Columns;
            foreach (var column in columns)
            {
                builder.AppendLine($"[{column}],");
            }
            builder = builder.Remove(builder.Length - 3, 3);
            builder.AppendLine($")");
            builder.AppendLine($"VALUES (");

            foreach (var column in columns)
            {
                builder.AppendLine($"S.{column},");
            }

            builder = builder.Remove(builder.Length - 3, 3);
            builder.AppendLine(")");
            builder.AppendLine($"WHEN MATCHED");
            builder.AppendLine($"THEN UPDATE SET");
            foreach (var column in columns)
            {
                builder.AppendLine($"T.{column} = S.{column},");
            }
            builder = builder.Remove(builder.Length - 3, 3);
            builder.AppendLine(";");
            builder.AppendLine($" DROP TABLE ##{_singleFileImportOption.ImportTargetOption.TargetTableName};");

            builder.AppendLine("COMMIT TRANSACTION T1;");
            return builder.ToString();
        }

        public override void Import()
        {
            using (var connection = new SqlConnection(_singleFileImportOption.DatabaseConnectOption.ConnectionString))
            {
                connection.Open();
                using (var stream = _streamer.GetFileStream(_singleFileImportOption.FileSourceOption))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var csvReader = new CsvReader(streamReader, true))
                        {
                            this.CreateTempTable(connection, csvReader);
                            using (SqlBulkCopy sqlBulk = new SqlBulkCopy(_singleFileImportOption.DatabaseConnectOption.ConnectionString))
                            {
                                sqlBulk.BulkCopyTimeout = int.MaxValue;
                                sqlBulk.DestinationTableName = "##" + _singleFileImportOption.ImportTargetOption.TargetTableName;

                                sqlBulk.WriteToServer(csvReader);
                            }
                        }
                    }
                }

                using (var command = connection.CreateCommand())
                {
                    var sql = _singleFileImportOption.ImportTargetOption.NeedUpdate ? this.GenerateMergeSql() : this.GenerateOnlyInsertSql();
                    command.CommandText = sql;
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = int.MaxValue;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
