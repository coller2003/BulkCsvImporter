using System.Data;

namespace BulkCsvImporter.Model
{
    public class ColumnInfo
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int OrdinalPosition { get; set; }
        public bool IsNullable { get; set; }
        public string MaxLength { get; set; }
        public string NumericPrecision { get; set; }
        public string NumericScale { get; set; }


        protected string MaxLengthFormatted
        {
            // note that columns with a max length return –1.
            get { return MaxLength.Equals("-1") ? "max" : MaxLength; }
        }


        public ColumnInfo ReadFromReader(IDataReader reader)
        {
            // get the necessary information from the datareader.
            // run the SQL on your database to see all the other information available.
            this.Name = reader["COLUMN_NAME"].ToString();
            this.DataType = reader["DATA_TYPE"].ToString();
            this.OrdinalPosition = (int)reader["ORDINAL_POSITION"];
            this.IsNullable = ((string)reader["IS_NULLABLE"]) == "YES";
            this.MaxLength = reader["CHARACTER_MAXIMUM_LENGTH"].ToString();

            if (this.DataType == "decimal")
            {
                this.NumericPrecision = reader["NUMERIC_PRECISION"].ToString();
                this.NumericScale = reader["NUMERIC_SCALE"].ToString();
            }

            return this;
        }

        public override string ToString()
        {
            var lengthOrScale = string.Empty;
            if (this.DataType == "decimal")
                lengthOrScale = $"({this.NumericPrecision}, {this.NumericScale})";
            else
                lengthOrScale = MaxLength == string.Empty ? "" : "(" + MaxLengthFormatted + ")";

            return string.Format("[{0}] {1}{2} {3}NULL", Name, DataType,
                lengthOrScale,
                IsNullable ? "" : "NOT ");
        }
    }
}
