using System;

namespace EisenhowerMatrix
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; private set; }
        public DateTime Deadline { get; private set; }
        public bool IsDone { get; private set; }
        public bool IsImportant { get; private set; }
        public int MatrixId { get; set; }

        // when I create new item retrived from database
        public TodoItem(int id, string title, DateTime deadline, bool isImportant, int matrixId, bool isDone)
        {
            Id = id;
            Title = title;
            Deadline = deadline;
            this.IsImportant = isImportant;
            MatrixId = matrixId;
            IsDone = isDone;
        }

        // when I add new item to database
        public TodoItem(string title, DateTime deadline, bool isImportant, int matrixId)
        {
            Title = title;
            Deadline = deadline;
            this.IsImportant = isImportant;
            MatrixId = matrixId;
            IsDone = false;
        }


        public override string ToString()
        {
            // [x] 12-6 submit assignment
            // [ ] 28-6 submit assignment
            string dateAndTitle = $"{this.Id}. {this.Deadline.Day}-{this.Deadline.Month} {this.Title}";
            return ((this.IsDone ? "[x]" : "[ ]") + $" {dateAndTitle}");
        }

        public int GetTitleLength() => Title.Length;


    }

}