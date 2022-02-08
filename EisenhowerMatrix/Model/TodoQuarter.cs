using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EisenhowerMatrix
{

    public class TodoQuarter
    {
        public readonly QuarterType Status;

        private List<TodoItem> ToDoItems { get; } = new List<TodoItem>();

        public TodoQuarter(QuarterType quarterType)
        {
            Status = quarterType;
        }

        public void AddItem(TodoItem item)
        {
            ToDoItems.Add(item);
        }

        public TodoItem GetItem(int index) => ToDoItems[index];

        public List<TodoItem> GetItems() => ToDoItems;

        public int HowManyItems() => ToDoItems.Count();

        public bool HaveItemsToRemove() => GetItems().Any();

        public override string ToString()
        {
            string s = "";
            foreach (TodoItem item in ToDoItems)
            {
                s += item.ToString() + "\n";
            }
            return s;
        }

    }

}