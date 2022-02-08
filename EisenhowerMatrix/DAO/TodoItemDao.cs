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
    public class TodoItemDao : ITodoItemDao
    {
        private readonly string _connectionString;

        public TodoItemDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(TodoItem item)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string insertTodoItemSql =
                    @"
INSERT INTO item (title, deadline, is_important, matrix_id, is_done)
VALUES (@Title, @Deadline, @IsImportant, @MatrixId, @IsDone);
SELECT SCOPE_IDENTITY();
";

                command.CommandText = insertTodoItemSql;
                command.Parameters.AddWithValue("@Title", item.Title);
                command.Parameters.AddWithValue("@Deadline", item.Deadline);
                command.Parameters.AddWithValue("@IsImportant", Convert.ToByte(item.IsImportant));
                command.Parameters.AddWithValue("@MatrixId", item.MatrixId);
                command.Parameters.AddWithValue("@IsDone", item.IsDone);


                int TodoItemitemId = Convert.ToInt32(command.ExecuteScalar());
                item.Id = TodoItemitemId;
            }
            catch (SqlException e)
            {
                throw;
            }

        }


        public TodoItem Get(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectItemSql = @"
                SELECT title, deadline, is_important, matrix_id, is_done
                FROM item
                WHERE id = @Id;
";
                command.Parameters.AddWithValue("@Id", id);
                command.CommandText = selectItemSql;

                using var dataReader = command.ExecuteReader();
                TodoItem newItem = null;

                if (dataReader.Read())
                {
                    string title = (string)dataReader["title"];
                    DateTime deadline = Convert.ToDateTime(dataReader["deadline"]);
                    bool isImportnat = Convert.ToBoolean((byte)dataReader["is_important"]);
                    int matrixId = Convert.ToInt32(dataReader["matrix_id"]);
                    bool isDone = (bool)dataReader["is_done"];

                    newItem = new TodoItem(id, title, deadline, isImportnat, matrixId, isDone);
                }
                return newItem;


            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public List<TodoItem> GetAll(int matrixId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectAllItemsSql = @"
                SELECT id, title, deadline, is_important, is_done
                FROM item
                WHERE matrix_id = @MatrixId;
";
                command.Parameters.AddWithValue("@MatrixId", matrixId);
                command.CommandText = selectAllItemsSql;

                using var dataReader = command.ExecuteReader();
                TodoItem newItem = null;
                List<TodoItem> allItems = new List<TodoItem>();

                while (dataReader.Read())
                {
                    int id = Convert.ToInt32(dataReader["id"]);
                    string title = (string)dataReader["title"];
                    DateTime deadline = Convert.ToDateTime(dataReader["deadline"]);
                    bool isImportnat = (bool)dataReader["is_important"];
                    bool isDone = (bool)dataReader["is_done"];

                    newItem = new TodoItem(id, title, deadline, isImportnat, matrixId, isDone);
                    allItems.Add(newItem);
                }

                return allItems;

            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public void MarkOrUnmark(int id, bool isDone)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string markOrUnmarkTodoItemSql =
                    @"
                    UPDATE item
                    SET
                    is_done = @IsDone
                    WHERE   
                    id = @Id
";

                byte isDoneByte = Convert.ToByte(isDone);


                command.CommandText = markOrUnmarkTodoItemSql;
                command.Parameters.AddWithValue("@IsDone", isDoneByte);
                command.Parameters.AddWithValue("@Id", id);


                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void RemoveItem(int id)
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
                    id = @Id
";



                command.CommandText = removeTodoItemSql;

                command.Parameters.AddWithValue("@Id", id);


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