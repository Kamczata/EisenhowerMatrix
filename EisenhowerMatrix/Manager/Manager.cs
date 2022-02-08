using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisenhowerMatrix
{
    public class Manager
    {
        private string ConnectionString => ConfigurationManager.AppSettings["connectionString"];
        private TodoMatrixDao _matrixDao;
        private TodoItemDao _itemDao;
        private TodoMatrixManager matrixManager = new TodoMatrixManager();
        private Display _display = new Display();

        public readonly string Name = "MAIN MENU";

        public void Run()
        {

            bool running = true;

            while (running)
            {

                try
                {
                    Setup();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Could not connect to the database.");
                    _display.PressAnyKey();
                    return;
                }
                _display.ClearScreen();
                _display.PrintMenuName(Name);
                _display.PrintOption("1", "Create new Matrix");
                _display.PrintOption("2", "Display saved Matrix");
                _display.PrintOption("0", "Quit");
                _display.PrintMessage(_display.ChooseOption);
                string userChoice = _display.UserInput();

                switch (userChoice)
                {
                    case "1":
                        matrixManager.CreateNewMatrix(_matrixDao, _itemDao);
                        break;
                    case "2":
                        matrixManager.DisplayAllMatrixes(_matrixDao, _itemDao);
                        break;
                    case "0":
                        System.Environment.Exit(1);
                        break;
                }
            }

            void Setup()
            {
                string connectionString = ConnectionString;
                _matrixDao = new TodoMatrixDao(connectionString);
                _itemDao = new TodoItemDao(connectionString);
            }
        }
    }
}