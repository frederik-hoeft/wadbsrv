using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace wadbsrv.Database
{
    /// <summary>
    /// Provides thread-safe database access
    /// </summary>
    public static class DatabaseManager
    {
        public static string connectionString { get; set; } = null;
        private static SQLiteConnection sqlConnection;
        private static SQLiteCommand sqlCommand;
        private static bool setupComplete = false;

        private static void Setup()
        {
            if (!setupComplete)
            {
                if (connectionString == null)
                {
                    throw new Exception("Call MainServer.LoadConfig first!");
                }
                sqlConnection = new SQLiteConnection(connectionString);
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                setupComplete = true;
            }
        }

        #region Database Access

        /// <summary>
        /// Returns result of SQLite database query as a string array.
        /// </summary>
        /// <param name="query">SQLite query to be executed.</param>
        /// <param name="columns">Number of columns returned by query.</param>
        /// <returns></returns>
        public static async Task<SqlPacket> GetDataArray(string query, int columns)
        {
            Setup();
            string[] data = new string[columns];
            bool readData = false;
            using (DatabaseThreadWatcher _ = new DatabaseThreadWatcher())
            {
                sqlCommand = sqlConnection.CreateCommand();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                // Is secure: only main server instances can access the database server.
                // SQL Injection checks are running on main server.
                sqlCommand.CommandText = query;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                try
                {
                    await sqlCommand.ExecuteNonQueryAsync();
                }
                catch (DbException ex)
                {
                    return SqlPacket.Create(null, false, MainServer.Config.DebuggingEnabled ? "Error: '" + ex.ToString() + "'\nTried to execute the following query: \'" + query + "\'" : string.Empty);
                }
                using SQLiteDataReader reader = (SQLiteDataReader)await sqlCommand.ExecuteReaderAsync();
                while (reader.Read())
                {
                    try
                    {
                        for (int i = 0; i < columns; i++)
                        {
                            if (!readData) readData = true;
                            data[i] = reader[i].ToString();
                        }
                    }
                    catch (IndexOutOfRangeException outOfRange)
                    {
                        return SqlPacket.Create(null, false, MainServer.Config.DebuggingEnabled ? "Error 'Reader out of range' occurred: '" + outOfRange.ToString() + "'\nTried to execute the following query: \'" + query + "\'" : string.Empty);
                    }
                }
            }
            return SqlPacket.Create(readData ? data : Array.Empty<string>());
        }

        /// <summary>
        /// Returns result of SQLite database query as 2d string array.
        /// </summary>
        /// <param name="query">SQLite query to be executed.</param>
        /// <param name="columns">Number of columns returned by query.</param>
        /// <returns></returns>
        public static async Task<SqlPacket> GetDataAs2DArray(string query, int columns)
        {
            Setup();
            List<string[]> outerList = new List<string[]>();
            bool readData = false;
            using (DatabaseThreadWatcher _ = new DatabaseThreadWatcher())
            {
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                // Is secure: only main server instances can access the database server.
                // SQL Injection checks are running on main server.
                sqlCommand.CommandText = query;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                try
                {
                    await sqlCommand.ExecuteNonQueryAsync();
                }
                catch (DbException ex)
                {
                    return SqlPacket.Create(null, false, MainServer.Config.DebuggingEnabled ? "Error: '" + ex.ToString() + "'\nTried to execute the following query: \'" + query + "\'" : string.Empty);
                }
                using SQLiteDataReader reader = (SQLiteDataReader)await sqlCommand.ExecuteReaderAsync();
                while (reader.Read())
                {
                    try
                    {
                        string[] row = new string[columns];
                        for (int i = 0; i < columns; i++)
                        {
                            if (!readData) readData = true;
                            row[i] = reader[i].ToString();
                        }
                        outerList.Add(row);
                    }
                    catch (IndexOutOfRangeException outOfRange)
                    {
                        return SqlPacket.Create(null, false, MainServer.Config.DebuggingEnabled ? "Error 'Reader out of range' occurred: '" + outOfRange.ToString() + "'\nTried to execute the following query: \'" + query + "\'" : string.Empty);
                    }
                }
            }
            return SqlPacket.Create(readData ? outerList.ToArray() : Array.Empty<string[]>());
        }

        /// <summary>
        /// Returns result of SQLite database query as string or empty string if the query returns nothing.
        /// </summary>
        /// <param name="query">SQLite query to be executed.</param>
        /// <returns></returns>
        public static async Task<SqlPacket> GetSingleOrDefault(string query)
        {
            Setup();
            string returnData = string.Empty;
            using (DatabaseThreadWatcher _ = new DatabaseThreadWatcher())
            {
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                // Is secure: only main server instances can access the database server.
                // SQL Injection checks are running on main server.
                sqlCommand.CommandText = query;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                try
                {
                    await sqlCommand.ExecuteNonQueryAsync();
                }
                catch (DbException ex)
                {
                    return SqlPacket.Create(null, false, MainServer.Config.DebuggingEnabled ? "Error: '" + ex.ToString() + "'\nTried to execute the following query: \'" + query + "\'" : string.Empty);
                }
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
                        return SqlPacket.Create(null, false, MainServer.Config.DebuggingEnabled ? "Error 'Reader out of range' occurred: '" + outOfRange.ToString() + "'\nTried to execute the following query: \'" + query + "\'" : string.Empty);
                    }
                }
            }
            return SqlPacket.Create(returnData);
        }

        /// <summary>
        /// Executes a SQLite query to manipulate data.
        /// </summary>
        /// <param name="query">SQLite query to be executed.</param>
        /// <returns></returns>
        public static async Task<SqlPacket> ModifyData(string query)
        {
            Setup();
            using DatabaseThreadWatcher _ = new DatabaseThreadWatcher();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
            // Is secure: only main server instances can access the database server.
            // SQL Injection checks are running on main server.
            sqlCommand.CommandText = query;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
            try
            {
                int i = await sqlCommand.ExecuteNonQueryAsync();
                return SqlPacket.Create(i);
            }
            catch (DbException ex)
            {
                return SqlPacket.Create(null, false, MainServer.Config.DebuggingEnabled ? "Error: '" + ex.ToString() + "'\nTried to execute the following query: \'" + query + "\'" : string.Empty);
            }
        }

        #endregion Database Access
    }
}