using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerMatrix
{
    public class TodoMatrixDao : ITodoMatrixDao
    {
        private readonly string _connectionString;

        public TodoMatrixDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TodoMatrix Add(string matrixName)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string insertTodoMatrixSql =
                    @"
INSERT INTO matrix (title)
VALUES (@Title);
SELECT SCOPE_IDENTITY();
";

                command.CommandText = insertTodoMatrixSql;
                command.Parameters.AddWithValue("@Title", matrixName);

                int todoMatrixId = Convert.ToInt32(command.ExecuteScalar());
                TodoMatrix matrix = new TodoMatrix(matrixName, todoMatrixId);
                return matrix;
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public TodoMatrix Get(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectMatrixSql = @"
                SELECT title
                FROM matrix
                WHERE id = @Id;";

                command.Parameters.AddWithValue("@Id", id);
                command.CommandText = selectMatrixSql;

                using var dataReader = command.ExecuteReader();
                TodoMatrix newMatrix = null;

                if (dataReader.Read())
                {
                    string title = (string)dataReader["title"];
                    newMatrix = new TodoMatrix(title, id);
                }
                return newMatrix;


            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public List<TodoMatrix> GetAllTitles()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectAllMatrixSql = @"
                SELECT id, title
                FROM matrix;
";
                command.CommandText = selectAllMatrixSql;

                using var dataReader = command.ExecuteReader();
                TodoMatrix newMatrix = null;
                List<TodoMatrix> allMatrix = new List<TodoMatrix>();

                while (dataReader.Read())
                {
                    int id = Convert.ToInt32(dataReader["id"]);
                    string title = (string)dataReader["title"];

                    newMatrix = new TodoMatrix(title, id);
                    allMatrix.Add(newMatrix);
                }

                return allMatrix;

            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public void ArchiveDoneItems(int matrixId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string removeTodoItemSql =
                    @"
                    DELETE FROM item
                    WHERE   
                    matrix_id = @MatrixId AND is_done=1;
";



                command.CommandText = removeTodoItemSql;

                command.Parameters.AddWithValue("@MatrixId", matrixId);


                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

    }
}