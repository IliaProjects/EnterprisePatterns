using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPatterns
{
    public abstract class GatewayBase
    {
        protected string _connectionString;
        protected string _tableName;

        public GatewayBase(string tableName)
        {
            _tableName = tableName;
            using (StreamReader r = new StreamReader("Config.json"))
            {
                _connectionString = JObject.Parse(r.ReadToEnd())["ConnectionStrings"]["DefaultConnection"].Value<string>();
            }
        }

        protected DataTable GetTable()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand($"SELECT * FROM {_tableName}", connection);

                DataTable table = new DataTable();
                adapter.Fill(table);

                connection.Close();
                return table;
            }
        }
        protected DataRow GetRow(string id)
        {
            return GetTable().Select($"ID = {id}").FirstOrDefault();
        }
        protected DataRow[] FindByColumn<T>(string columnName, T value)
        {
            return GetTable().Select($"{columnName} = {value}");
        }
        protected T GetColumn<T>(string id, string columnName)
        {
            return (T)GetRow(id)[columnName];
        }


        protected void InsertRow(DataRow row)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = insertQueryString(row);
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

        protected void UpdateRow(DataRow row)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = updateQueryString(row);
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        protected void DeleteRow(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = $"DELETE FROM {_tableName} WHERE Id={id}";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        private string insertQueryString(DataRow row)
        {
            string leftPart = $"INSERT INTO {_tableName} (";
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

        private string updateQueryString(DataRow row)
        {
            string queryString = $"UPDATE {_tableName} SET ";

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
    }
}
