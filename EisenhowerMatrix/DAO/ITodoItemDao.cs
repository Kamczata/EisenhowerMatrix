using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerMatrix
{ 
    public interface ITodoItemDao
    {
        public void Add(TodoItem item);

        public void MarkOrUnmark(int id, bool isDone);

        public TodoItem Get(int id);

        List<TodoItem> GetAll(int matrixId);

        public void RemoveItem(int id);

    }
}