using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchly
{
    class DatabaseHandler
    {

        public const string connectionString = "Data Source=DESKTOP-CBUACSI\\SQLEXPRESS;Initial Catalog=Searchly;Integrated Security=True";

        public static void Insert(string tableName, string[] values)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string cmdS = "Insert into " + tableName + " values (";
            for (int i = 0; i < values.Length; i++)
            {
                cmdS += "'" + values[i] + "'" + ((i == values.Length - 1) ? "" : ", ");
            }
            cmdS += ");";

            SqlCommand cmd = new SqlCommand(cmdS, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void Insert(string tableName, string[] columns, string[] values)
        {
            if (columns.Length != values.Length)
                return;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string cmdS = "Insert into " + tableName + " values (";

            string columnsS = "(";
            string valuesS = "values (";
            for (int i = 0; i < values.Length; i++)
            {
                columnsS += columns[i] + ((i == values.Length - 1) ? "" : ", ");
                valuesS += values[i] + ((i == values.Length - 1) ? "" : ", ");
            }
            columnsS += ") ";
            valuesS += ");";

            SqlCommand cmd = new SqlCommand(cmdS + columnsS + valuesS, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public static DataTable Select(string tableName, string clause = "")
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM " + tableName + " " + clause, connection);

            SqlDataAdapter data = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            data.Fill(table);

            connection.Close();
            data.Dispose();

            return table;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public static DataTable Select(string tableName, string[] columns, string clause = "")
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string columnclause = "";
            for (int i = 0; i < columns.Length; i++)
            {
                columnclause += columns[i] + ((i == columns.Length - 1) ? "" : ", ");
            }

            SqlCommand cmd = new SqlCommand("SELECT " + columnclause + " FROM " + tableName + " " + clause, connection);

            SqlDataAdapter data = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            data.Fill(table);

            connection.Close();
            data.Dispose();

            return table;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void Update(string tableName, string[] columns, string[] values, string clause)
        {
            if (columns.Length != values.Length)
                return;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string valueQ = "";
            for (int i = 0; i < values.Length; i++)
            {
                valueQ += columns[i] + "='" + values[i] + ((i == values.Length - 1) ? "'" : "', ");
            }

            SqlCommand cmd = new SqlCommand("UPDATE " + tableName + " SET " + valueQ + " " + clause, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void Delete(string table, string columnName, string ID)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM {table} WHERE {columnName} = {ID}", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void Execute(string command)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(command, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        //public static void Insert(int itemID,string name, string category, byte[] picture, string description, string date, string location,int userID)
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    connection.Open();
        //    string cmdS = $"Insert into Item values ('{itemID}','{name}','{category}','{Convert.ToBase64String(picture)}','{description}','{date}','{location}','{userID}');";
        //    // Insert Byte [] Value into Sql Table by SqlParameter
        //    //SqlCommand insertCommand = new SqlCommand(cmdS, connection);
        //    //SqlParameter sqlParam = insertCommand.Parameters.AddWithValue("@pic", picture);
        //    //sqlParam.DbType = DbType.Binary;
        //    SqlCommand cmd = new SqlCommand(cmdS, connection);

        //    cmd.ExecuteNonQuery();

        //    connection.Close();
        //}
        //-------------------------------------------------------------------------------------------------------------------------------
    }
}
