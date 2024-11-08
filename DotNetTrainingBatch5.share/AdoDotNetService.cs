﻿using System.Data;
using System.Data.SqlClient;

namespace DotNetTrainingBatch5.share
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable Query(string query, params SqlParameterModel[] sqlParameters ) 
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            if (sqlParameters is not null) 
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    command.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }
            

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            return dt;
        }
        public int  Execute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    command.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }


            var result = command.ExecuteNonQuery();

            connection.Close();

            return result;
        }
    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public SqlParameterModel() { }
        public SqlParameterModel(string name, object value) 
        {
            Name = name;
            Value = value;
        }
    }
   

}
