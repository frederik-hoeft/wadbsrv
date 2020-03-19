using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace wadbsrv.Database
{
    /// <summary>
    /// Provides thread-safe database access
    /// </summary>
    public static class DatabaseManager
    {
        // TODO: return error messages as API response
        // TODO: outsource
        private const string connectionString = "Data Source=" + @"Resources\localdata_windows.db";

        #region Database Access
        /// <summary>
        /// Returns result of SQLite database query as a string array.
        /// </summary>
        /// <param name="query">SQLite query to be executed.</param>
        /// <param name="columns">Number of columns returned by query.</param>
        /// <returns></returns>
        public static async Task<string[]> GetDataArray(string query, int columns)
        {
            string[] data = new string[columns];
            using (DatabaseThreadWatcher _ = new DatabaseThreadWatcher())
            using (SQLiteConnection sqlConnection = new SQLiteConnection(connectionString))
            {
                sqlConnection.Open();
                using SQLiteCommand sqlCommand = sqlConnection.CreateCommand();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                // Is secure: only main server instances can access the database server.
                // SQL Injection checks are running on main server.
                sqlCommand.CommandText = query;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                await sqlCommand.ExecuteNonQueryAsync();
                using SQLiteDataReader reader = (SQLiteDataReader)await sqlCommand.ExecuteReaderAsync();
                while (reader.Read())
                {
                    try
                    {
                        for (int i = 0; i < columns; i++)
                        {
                            data[i] = reader[i].ToString();
                        }
                    }
                    catch (IndexOutOfRangeException outOfRange)
                    {
                        Console.WriteLine("Error 'Reader out of range' occured: '{0}'\nTried to execute the following query: \"" + query + "\"", outOfRange);
                        return Array.Empty<string>();
                    }
                }
                sqlConnection.Close();
            }
            return data;
        }
        /// <summary>
        /// Returns result of SQLite database query as 2d string array.
        /// </summary>
        /// <param name="query">SQLite query to be executed.</param>
        /// <param name="columns">Number of columns returned by query.</param>
        /// <returns></returns>
        public static async Task<string[][]> GetDataAs2DArray(string query, int columns)
        {
            List<string[]> outerList = new List<string[]>();
            using (DatabaseThreadWatcher _ = new DatabaseThreadWatcher())
            using (SQLiteConnection sqlConnection = new SQLiteConnection(connectionString))
            {
                sqlConnection.Open();
                using SQLiteCommand sqlCommand = sqlConnection.CreateCommand();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                // Is secure: only main server instances can access the database server.
                // SQL Injection checks are running on main server.
                sqlCommand.CommandText = query;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                await sqlCommand.ExecuteNonQueryAsync();
                using SQLiteDataReader reader = (SQLiteDataReader)await sqlCommand.ExecuteReaderAsync();
                while (reader.Read())
                {
                    try
                    {
                        string[] row = new string[columns];
                        for (int i = 0; i < columns; i++)
                        {
                            row[i] = reader[i].ToString();
                        }
                        outerList.Add(row);
                    }
                    catch (IndexOutOfRangeException outOfRange)
                    {
                        Console.WriteLine("Error 'Reader out of range' occured: '{0}'\nTried to execute the following query: \"" + query + "\"", outOfRange);
                        return Array.Empty<string[]>();
                    }
                }
            }
            return outerList.ToArray();
        }
        /// <summary>
        /// Returns result of SQLite database query as string or empty string if the query returns nothing.
        /// </summary>
        /// <param name="query">SQLite query to be executed.</param>
        /// <returns></returns>
        public static async Task<string> GetSingleOrDefault(string query)
        {
            string returnData = string.Empty;
            using (DatabaseThreadWatcher _ = new DatabaseThreadWatcher())
            using (SQLiteConnection sqlConnection = new SQLiteConnection(connectionString))
            {
                sqlConnection.Open();
                using SQLiteCommand sqlCommand = sqlConnection.CreateCommand();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                // Is secure: only main server instances can access the database server.
                // SQL Injection checks are running on main server.
                sqlCommand.CommandText = query;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                await sqlCommand.ExecuteNonQueryAsync();
                using SQLiteDataReader reader = (SQLiteDataReader)await sqlCommand.ExecuteReaderAsync();
                while (reader.Read())
                {
                    try
                    {
                        returnData = reader[0].ToString();
                        break;
                    }
                    catch (IndexOutOfRangeException outOfRange)
                    {
                        Console.WriteLine("Error 'Reader out of range' occured: '{0}'\nTried to execute the following query: \"" + query + "\"", outOfRange);
                        return string.Empty;
                    }
                }
            }
            return returnData;
        }
        /// <summary>
        /// Executes a SQLite query to manipulate data.
        /// </summary>
        /// <param name="query">SQLite query to be executed.</param>
        /// <returns></returns>
        public static async Task<int> ModifyData(string query)
        {
            using DatabaseThreadWatcher _ = new DatabaseThreadWatcher();
            using SQLiteConnection sqlConnection = new SQLiteConnection(connectionString);
            sqlConnection.Open();
            using SQLiteCommand sqlCommand = sqlConnection.CreateCommand();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
            // Is secure: only main server instances can access the database server.
            // SQL Injection checks are running on main server.
            sqlCommand.CommandText = query;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
            return await sqlCommand.ExecuteNonQueryAsync();
        }
        #endregion
    }
}
