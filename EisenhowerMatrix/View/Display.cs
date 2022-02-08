using System;
using System.Collections.Generic;
using System.Text;

namespace EisenhowerMatrix
{
    public class Display
    {


        public readonly string askForTitle = "Provide new item title";
        public readonly string askForDeadline = "Provide the deadline YYYY-MM-DD";
        public readonly string isItImportant = "Is it important? y - for yes; n - for no";
        public readonly string noItemsToPick = "No items to pick!";
        public readonly string noItemsToRemove = "No items to remove!";

        public readonly Dictionary<QuarterType, string> headers = new Dictionary<QuarterType, string>() {
            { QuarterType.IU, "Urgent & Important" },
            { QuarterType.NU, "Urgent & Not Important" },
            { QuarterType.IN, "Not Urgent & Important" },
            { QuarterType.NN, "Not Urgent & Not Important" },

        };

        public readonly string ProvideMatrixTitle = "Provide Matrix Title";
        public readonly string Back = "Back";
        public readonly string ChooseOption = "Choose option: ";


        public void PrintMenuName(string name) => Console.WriteLine($"{name} \n");

        public void PrintOption(string index, string option) => Console.WriteLine($"{index}. {option}");

        public void PrintMatrixName(TodoMatrix matrix) => Console.WriteLine($"{matrix.Title} \n");

        public void PrintMatrixTitleWithId(TodoMatrix matrix) => Console.WriteLine($"{matrix.Id}. {matrix.Title} \n");

        public void PickItem() => PrintMessage("Choose item id");

        public void DisplayMatrix(TodoMatrix matrix)
        {
            Console.WriteLine(matrix.ToString());
        }

        public void ClearScreen() => Console.Clear();

        public void PrintMessage(string message) => Console.WriteLine(message);

        public void DisplaQuarter(TodoQuarter quarter, string header)
        {
            Console.WriteLine(header);
            Console.WriteLine(quarter.ToString());
        }

        public string UserInput() => Console.ReadLine();

        public ConsoleKeyInfo PressAnyKey() => Console.ReadKey();

        public Dictionary<string, string> NewItemInfo()
        {
            Dictionary<string, string> newItemInfo = new Dictionary<string, string>();
            PrintMessage(askForTitle);
            newItemInfo["title"] = UserInput();
            PrintMessage(askForDeadline);
            newItemInfo["deadline"] = UserInput();
            PrintMessage(isItImportant);
            newItemInfo["isImportant"] = UserInput();
            return newItemInfo;
        }


        public void DisplayInfoAboutWrongInput()
        {
            Console.WriteLine("Wrong input, try again.");
        }
    }
}