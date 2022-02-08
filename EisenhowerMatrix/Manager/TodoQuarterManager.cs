using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerMatrix
{
    public class TodoQuarterManager
    {
        private Display _display = new Display();
        private bool _mark = true;
        private bool _unmark = false;


        private Dictionary<QuarterType, string> headers = new Dictionary<QuarterType, string>() {
            { QuarterType.IU, "Urgent & Important" },
            { QuarterType.NU, "Urgent & Not Important" },
            { QuarterType.IN, "Not Urgent & Important" },
            { QuarterType.NN, "Not Urgent & Not Important" },

        };

        public void DisplayQuarter(QuarterType quarterType, TodoQuarter quarter, TodoItemDao itemDao, TodoMatrix matrix)
        {
            string header = headers[quarterType];
            _display.ClearScreen();
            _display.DisplaQuarter(quarter, header);
            Run(itemDao, matrix, quarter);
        }

        public void AddItem(TodoItemDao itemDao, TodoMatrix matrix)
        {
            Dictionary<string, string> newItemInfo = _display.NewItemInfo();
            string title = newItemInfo["title"];
            DateTime deadline = Convert.ToDateTime(newItemInfo["deadline"]);
            bool isImportant = false;
            if (newItemInfo["isImportant"] == "y")
            {
                isImportant = true;
            }
            TodoItem newItem = new TodoItem(title, deadline, isImportant, matrix.Id);
            itemDao.Add(newItem);


        }

        private void RemoveItem(TodoItemDao itemDao, TodoQuarter quarter)
        {
            bool haveItemsToRemove = quarter.HaveItemsToRemove();
            if (haveItemsToRemove)
            {
                int indexOfPickedItem = ItemPicker(quarter);
                itemDao.RemoveItem(indexOfPickedItem);
            }
            else
            {
                _display.PrintMessage(_display.noItemsToRemove);
                _display.PressAnyKey();
            }
        }

        private int ItemPicker(TodoQuarter quarter)
        {
            int howManyItems = quarter.HowManyItems();
            int itemIndex = 0;
            if (howManyItems > 1)
            {
                _display.PickItem();
                string userInput = _display.UserInput();
                itemIndex = Convert.ToInt32(userInput);

            }
            else if (howManyItems == 1)
            {

                itemIndex = quarter.GetItems()[0].Id;
            }
            return itemIndex;
        }

        private void MarkOrUnmark(bool shouldMark, TodoItemDao itemDao, TodoQuarter quarter)
        {
            int howManyItems = quarter.HowManyItems();
            int itemIndex = 0;
            bool mark = true;
            bool unmark = false;
            if (shouldMark && howManyItems >= 1)
            {
                int indexOfPickedItem = ItemPicker(quarter);
                itemDao.MarkOrUnmark(indexOfPickedItem, mark);
            }
            else if (!shouldMark && howManyItems >= 1)
            {
                int indexOfPickedItem = ItemPicker(quarter);
                itemDao.MarkOrUnmark(indexOfPickedItem, unmark);
            }
            else
            {
                _display.PrintMessage(_display.noItemsToPick);
                _display.PressAnyKey();
            }
        }

        private void Run(TodoItemDao itemDao, TodoMatrix matrix, TodoQuarter quarter)
        {

            _display.ClearScreen();
            _display.DisplaQuarter(quarter, headers[quarter.Status]);
            _display.PrintOption("1", "Add item");
            _display.PrintOption("2", "Remove item");
            _display.PrintOption("3", "Mark item as done");
            _display.PrintOption("4", "Mark item as not done ");
            _display.PrintOption("5", "Back");
            _display.PrintMessage(_display.ChooseOption);
            string userChoice = _display.UserInput();

            switch (userChoice)
            {
                case "1":
                    AddItem(itemDao, matrix);
                    break;
                case "2":
                    RemoveItem(itemDao, quarter);
                    break;
                case "3":
                    MarkOrUnmark(_mark, itemDao, quarter);
                    break;
                case "4":
                    MarkOrUnmark(_unmark, itemDao, quarter);
                    break;
                case "5":
                    break;

            }

        }

    }
}