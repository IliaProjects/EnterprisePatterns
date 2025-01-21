using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public abstract class DataMapperBase
    {
        protected string _connectionString;
        public DataMapperBase()
        {
            using (StreamReader r = new StreamReader("Config.json"))
            {
                _connectionString = JObject.Parse(r.ReadToEnd())["ConnectionStrings"]["DefaultConnection"].Value<string>();
            }
        }


        protected DataTable GetTable(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand($"SELECT * FROM {tableName}", connection);

                DataTable table = new DataTable();
                adapter.Fill(table);

                connection.Close();
                return table;
            }
        }
        protected DataRow GetRow(DataTable table, string key)
        {
            return table.Select($"ID = {key}").FirstOrDefault();
        }
        protected DataRow[] FindRows(DataTable table, string column, string value)
        {
            return table.Select($"{column} = {value}");
        }

        protected void InsertRow(DataRow row, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = insertQueryString(row, tableName);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                foreach (DataColumn column in row.Table.Columns)
                {
                    command.Parameters.AddWithValue(column.ColumnName, row[column.ColumnName]);
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        private string insertQueryString(DataRow row, string tableName)
        {
            string leftPart = $"INSERT INTO {tableName} (";
            string rightPart = " VALUES (";

            int columnsCount = row.Table.Columns.Count;
            for (int i = 0; i < columnsCount; i++)
            {
                string columnName = row.Table.Columns[i].ColumnName;
                leftPart += columnName;
                rightPart += "@" + columnName;

                leftPart += i + 1 == columnsCount ? ")" : ",";
                rightPart += i + 1 == columnsCount ? ")" : ",";
            }
            return leftPart + " OUTPUT Inserted.id " + rightPart;
        }
        protected void UpdateRow(DataRow row, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = updateQueryString(row, tableName);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        private string updateQueryString(DataRow row, string tableName)
        {
            string queryString = $"UPDATE {tableName} SET ";

            int columnsCount = row.Table.Columns.Count;
            for (int i = 0; i < columnsCount; i++)
            {
                string columnName = row.Table.Columns[i].ColumnName;
                if (!"Id".Equals(columnName))
                {
                    queryString += $"{columnName}={row[columnName]}";
                    queryString += i + 1 != columnsCount ? ", " : "";
                }
            }

            queryString += $" WHERE Id='{row["Id"]}';";
            return queryString;
        }
        protected void DeleteRow(string id, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = $"DELETE FROM {tableName} WHERE Id={id}";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
