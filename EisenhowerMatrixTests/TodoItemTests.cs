using NUnit.Framework;
using System;
using EisenhowerMatrix;


namespace EisenhowerMatrix
{
    [TestFixture]
    public class TodoItemTests
    {
        [Test]
        public void TodoItemConstructorWithId_ReturnProperties()
        {
            int id = 100;
            string title = "Cool item";
            DateTime deadline = DateTime.Today;
            bool isImportant = true;
            int matrixId = 3;
            bool isDone = true;

            TodoItem item = new TodoItem(id, title, deadline, isImportant, matrixId, isDone);
            Assert.AreEqual((id, title, deadline, isImportant, matrixId, isDone), (item.Id, item.Title, item.Deadline, item.IsImportant, item.MatrixId, item.IsDone));
        }

        [Test]
        public void TodoItemConstructorWithoutId_ReturnProperties()
        {
            string title = "Cool item";
            DateTime deadline = DateTime.Today;
            bool isImportant = true;
            int matrixId = 3;
            bool isDone = false;

            TodoItem item = new TodoItem(title, deadline, isImportant, matrixId);
            Assert.AreEqual((title, deadline, isImportant, matrixId, isDone), (item.Title, item.Deadline, item.IsImportant, item.MatrixId, item.IsDone));
        }

        [Test]
        public void TodoItemNotDone_ToString_Formated_Correctly()
        {
            int id = 3;
            string title = "Cool item";
            DateTime deadline = DateTime.Today;
            bool isImportant = true;
            int matrixId = 3;
            bool isDone = false;

            TodoItem item = new TodoItem(id, title, deadline, isImportant, matrixId, isDone);

            string expectedString = $"[ ] {id}. {deadline.Day}-{deadline.Month} {title}";
            Assert.AreEqual(expectedString, item.ToString());
        }

        [Test]
        public void TodoItemDone_ToString_Formated_Correctly()
        {
            int id = 3;
            string title = "Cool item";
            DateTime deadline = DateTime.Today;
            bool isImportant = true;
            int matrixId = 3;
            bool isDone = true;

            TodoItem item = new TodoItem(id, title, deadline, isImportant, matrixId, isDone);

            string expectedString = $"[x] {id}. {deadline.Day}-{deadline.Month} {title}";
            Assert.AreEqual(expectedString, item.ToString());
        }

        [Test]
        public void TodoItem_GetTitleLength_Returns_Correct_Value()
        {
            int id = 3;
            string title = "Cool item";
            DateTime deadline = DateTime.Today;
            bool isImportant = true;
            int matrixId = 3;
            bool isDone = false;
            int expectedLength = title.Length;

            TodoItem item = new TodoItem(id, title, deadline, isImportant, matrixId, isDone);
            Assert.AreEqual(expectedLength, item.GetTitleLength());
        }
    }
}
