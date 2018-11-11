using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ImageConversionUtility
{
    public class BaseDataAccess
    {
        #region Protected Properties

        protected string ConnectionString { get; set; }

        #endregion Protected Properties

        #region Private Methods

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();

            return connection;
        }

        #endregion Private Methods

        #region Public Constructors

        public BaseDataAccess()
        {
        }

        public BaseDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected int ExecuteNonQuery(string procedureName, List<DbParameter> parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            var returnValue = -1;

            using (var connection = GetConnection())
            {
                var cmd = GetCommand(connection, procedureName, commandType);

                if (parameters != null && parameters.Count > 0) cmd.Parameters.AddRange(parameters.ToArray());

                returnValue = cmd.ExecuteNonQuery();
            }

            return returnValue;
        }

        protected object ExecuteScalar(string procedureName, List<SqlParameter> parameters)
        {
            object returnValue = null;

            using (DbConnection connection = GetConnection())
            {
                var cmd = GetCommand(connection, procedureName, CommandType.StoredProcedure);

                if (parameters != null && parameters.Count > 0) cmd.Parameters.AddRange(parameters.ToArray());

                returnValue = cmd.ExecuteScalar();
            }

            return returnValue;
        }

        protected DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            var command = new SqlCommand(commandText, connection as SqlConnection)
            {
                CommandType = commandType
            };
            return command;
        }

        protected DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            DbDataReader ds;

            DbConnection connection = GetConnection();
            {
                var cmd = GetCommand(connection, procedureName, commandType);
                if (parameters != null && parameters.Count > 0) cmd.Parameters.AddRange(parameters.ToArray());

                ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            return ds;
        }

        protected SqlParameter GetParameter(string parameter, object value)
        {
            var parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value)
            {
                Direction = ParameterDirection.Input
            };
            return parameterObject;
        }

        protected SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null,
            ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            var parameterObject = new SqlParameter(parameter, type);
            ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText ||
                type == SqlDbType.Text) parameterObject.Size = -1;

            parameterObject.Direction = parameterDirection;

            if (value != null)
                parameterObject.Value = value;
            else
                parameterObject.Value = DBNull.Value;

            return parameterObject;
        }

        #endregion Protected Methods
    }
}