using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevenueRecognitionTableModule.TableModules
{
    public abstract class ModuleBase
    {
        protected string _connectionString;
        protected string _tableName;

        protected ModuleBase(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        protected T GetColumn<T>(int id, string columnName)
        {
            return (T)GetRow(id)[columnName];
        }

        protected DataRow GetRow(int id)
        {
            return GetTable().Select($"ID = {id}").FirstOrDefault();
        }

        protected int InsertRow(DataRow row)
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
                int id = (int)adapter.InsertCommand.ExecuteScalar();
                return id;
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

        protected DataTable GetTable()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand($"SELECT * FROM {_tableName}", connection);
                
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
            }
        }
    }
}
