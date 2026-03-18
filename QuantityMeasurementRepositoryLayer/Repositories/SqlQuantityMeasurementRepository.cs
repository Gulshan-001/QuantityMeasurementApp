using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories
{
    public class SqlQuantityMeasurementRepository : IQuantityMeasurementRepository
    {
        private readonly string _connectionString =
            "Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=True;";

        // ============================
        // SAVE
        // ============================

        public void Save(QuantityMeasurementEntity entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                INSERT INTO MeasurementLogs
                (OperationType, OperationCode, InputType, OutputType,
                 InputData, ResultData, IsSuccess, ErrorMessage)
                VALUES
                (@OperationType, @OperationCode, @InputType, @OutputType,
                 @InputData, @ResultData, @IsSuccess, @ErrorMessage);
                 
                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@OperationType", entity.OperationType);
                cmd.Parameters.AddWithValue("@OperationCode", entity.OperationCode);
                cmd.Parameters.AddWithValue("@InputType", entity.InputType);
                cmd.Parameters.AddWithValue("@OutputType", (object?)entity.OutputType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@InputData", entity.InputData);
                cmd.Parameters.AddWithValue("@ResultData", (object?)entity.ResultData ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsSuccess", entity.IsSuccess);
                cmd.Parameters.AddWithValue("@ErrorMessage", (object?)entity.ErrorMessage ?? DBNull.Value);

                connection.Open();

                // 🔥 get inserted ID
                int insertedId = Convert.ToInt32(cmd.ExecuteScalar());

                // 🔥 optional audit log (CREATE)
                AddAuditLog(insertedId, "CREATE", null, entity.InputData + " => " + entity.ResultData, "SYSTEM");
            }
        }

        // ============================
        // GET ALL
        // ============================

        public List<QuantityMeasurementEntity> GetAll()
        {
            var list = new List<QuantityMeasurementEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM MeasurementLogs ORDER BY CreatedAt DESC";

                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(Map(reader));
                }
            }

            return list;
        }

        // ============================
        // GET BY ID
        // ============================

        public QuantityMeasurementEntity GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM MeasurementLogs WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return Map(reader);
                }
            }
    
            return null;
        }

        // ============================
        // DELETE
        // ============================

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                //  get old data for audit
                var old = GetById(id);

                string query = "DELETE FROM MeasurementLogs WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                cmd.ExecuteNonQuery();

                //  audit delete
                if (old != null)
                {
                    AddAuditLog(id, "DELETE", old.InputData + " => " + old.ResultData, null, "SYSTEM");
                }
            }
        }

        // ============================
        // AUDIT LOG
        // ============================

        public void AddAuditLog(int logId, string actionType, string oldValue, string newValue, string changedBy)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                INSERT INTO MeasurementAuditLogs
(LogId, ActionType, OldValue, NewValue, ChangedBy)
                VALUES
                (@LogId, @ActionType, @OldValue, @NewValue, @ChangedBy)";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@LogId", logId);
                cmd.Parameters.AddWithValue("@ActionType", actionType);
                cmd.Parameters.AddWithValue("@OldValue", (object?)oldValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NewValue", (object?)newValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ChangedBy", changedBy ?? "SYSTEM");

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ============================
        // MAPPER
        // ============================

        private QuantityMeasurementEntity Map(SqlDataReader reader)
        {
            return new QuantityMeasurementEntity
            {
                Id = Convert.ToInt32(reader["Id"]),
                OperationType = reader["OperationType"].ToString(),
                OperationCode = Convert.ToInt32(reader["OperationCode"]),
                InputType = reader["InputType"].ToString(),
                OutputType = reader["OutputType"]?.ToString(),
                InputData = reader["InputData"].ToString(),
                ResultData = reader["ResultData"]?.ToString(),
                IsSuccess = Convert.ToBoolean(reader["IsSuccess"]),
                ErrorMessage = reader["ErrorMessage"]?.ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}