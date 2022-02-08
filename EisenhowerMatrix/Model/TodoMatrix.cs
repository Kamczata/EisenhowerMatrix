using System;
using System.Collections.Generic;
using System.Linq;

namespace EisenhowerMatrix
{
        public class TodoMatrix
        {

            private Dictionary<QuarterType, TodoQuarter> TodoQuarters = new Dictionary<QuarterType, TodoQuarter>();
            private Display display = new Display();
            public string Title;
            public int Id;
            private int maxDays = 3;

            public TodoMatrix(string title, int id)
            {
                Title = title;
                Id = id;
                TodoQuarters[QuarterType.IU] = new TodoQuarter(QuarterType.IU);
                TodoQuarters[QuarterType.IN] = new TodoQuarter(QuarterType.IN);
                TodoQuarters[QuarterType.NU] = new TodoQuarter(QuarterType.NU);
                TodoQuarters[QuarterType.NN] = new TodoQuarter(QuarterType.NN);
            }

            public Dictionary<QuarterType, TodoQuarter> GetQuarters() => TodoQuarters;

            public TodoQuarter GetQuarter(QuarterType status)
            {
                return TodoQuarters[status];
            }


            public void PlaceItems(List<TodoItem> items)
            {
                foreach (TodoItem item in items)
                {
                    bool isUrgent = IsUrgent(item.Deadline);
                    bool isImportant = item.IsImportant;
                    if (isUrgent && isImportant)
                    {
                        TodoQuarters[QuarterType.IU].AddItem(item);
                    }
                    else if (isUrgent && !isImportant)
                    {
                        TodoQuarters[QuarterType.NU].AddItem(item);
                    }
                    else if (!isUrgent && isImportant)
                    {
                        TodoQuarters[QuarterType.IN].AddItem(item);
                    }
                    else if (!isUrgent && !isImportant)
                    {
                        TodoQuarters[QuarterType.NN].AddItem(item);
                    }
                }

            }

            private bool IsUrgent(DateTime deadline)
            {
                DateTime today = DateTime.Today;
                if ((deadline - today).TotalDays >= maxDays)
                {
                    return false;
                }

                return true;
            }

            private string GenerateHalfMatrix(QuarterType quaterType1, QuarterType quaterType2)
            {
                string halfMatrix = $"";
                int quarterWidth = 45;
                int lines = 8;
                string wall = "|";
                string dash = "-";
                string space = " ";
                string emptyHalfLine = multiplySign(space, quarterWidth);

                int Amount1 = TodoQuarters[quaterType1].HowManyItems();
                int Amount2 = TodoQuarters[quaterType2].HowManyItems();
                int max = Math.Max(Amount1, Amount2);
                if (max > lines)
                {
                    lines = max;
                }

                int refilHeader1 = quarterWidth - display.headers[quaterType1].Length;
                int refilHeader2 = quarterWidth - display.headers[quaterType2].Length;

                halfMatrix += display.headers[quaterType1] + multiplySign(space, refilHeader1) + wall + display.headers[quaterType2] + multiplySign(space, refilHeader2) + "\n";
                halfMatrix += multiplySign(dash, quarterWidth * 2 + 1) + "\n";
                for (int i = 0; i < lines; i++)
                {
                    if (Amount1 == 0 || i + 1 > Amount1)
                    {
                        if (Amount2 == 0 || i + 1 > Amount2)
                        {
                            halfMatrix += emptyHalfLine + wall + emptyHalfLine + "\n";
                        }
                        else
                        {
                            int refill = quarterWidth - TodoQuarters[quaterType2].GetItem(i).ToString().Length;
                            halfMatrix += emptyHalfLine + wall + TodoQuarters[quaterType2].GetItem(i) + multiplySign(space, refill) + "\n";
                        }
                    }
                    else
                    {
                        if (Amount2 == 0 || i + 1 > Amount2)
                        {
                            int refill = quarterWidth - TodoQuarters[quaterType1].GetItem(i).ToString().Length;
                            halfMatrix += TodoQuarters[quaterType1].GetItem(i) + multiplySign(space, refill) + wall + emptyHalfLine + "\n";
                        }
                        else
                        {
                            int refill = quarterWidth - TodoQuarters[quaterType1].GetItem(i).ToString().Length;
                            int refill2 = quarterWidth - TodoQuarters[quaterType2].GetItem(i).ToString().Length;
                            halfMatrix += TodoQuarters[quaterType1].GetItem(i) + multiplySign(space, refill) + wall + TodoQuarters[quaterType2].GetItem(i) + multiplySign(space, refill2) + "\n";
                        }
                    }
                }
                halfMatrix += multiplySign(dash, quarterWidth * 2 + 1) + "\n";

                return halfMatrix;


            }

            public override string ToString()
            {
                string matrix = $"";
                matrix += GenerateHalfMatrix(QuarterType.IU, QuarterType.IN);
                matrix += GenerateHalfMatrix(QuarterType.NU, QuarterType.NN);

                return matrix;
            }

            public string multiplySign(string sign, int multiplier)
            {
                if (multiplier < 0)
                {
                    multiplier = 1;
                }
                return String.Concat(Enumerable.Repeat(sign, multiplier));
            }
        }
    }

