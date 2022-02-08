using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerMatrix
{
    public interface ITodoMatrixDao
    {
        public TodoMatrix Add(string matrixName);

        public TodoMatrix Get(int id);

        List<TodoMatrix> GetAllTitles();

        public void ArchiveDoneItems(int matrixId);
    }
}