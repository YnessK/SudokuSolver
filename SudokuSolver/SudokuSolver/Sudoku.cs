using System;
using System.Collections.Generic;
using System.Linq;


namespace SudokuSolver
{
    public class Sudoku
    {
        public const int BoardSize = 9;

        public int[,] Board;


        public Sudoku(int[,] board)
        {
            this.Board = board;
        }

        public Sudoku(int difficulty)
        {
            Board = new int[9, 9];
            Solve();
            Random r1 = new Random();
            Random r2 = new Random();
            int count = BoardSize * BoardSize - difficulty;
            while ( count != 0)
            {
                int i = r1.Next(0,9);
                int j = r1.Next(0,9);
                if (Board[i,j] != 0)
                {
                    Board[i, j] = 0;
                    count--;
                }
            }
        }
        
        
        public bool __IsLignValid()
        {
            for (int lign = 0; lign < BoardSize; lign++)
            {
                List<int> numbers = new List<int>();
                for (int column = 0; column < BoardSize; column++)
                {
                    if (numbers.Contains(Board[lign, column]))
                        return false;
                    if (Board[lign, column] == 0)
                        continue;
                    numbers.Add(Board[lign, column]);
                }
            }

            return true;
        }
        
        public bool __IsColumnValid()
        {
            for (int column = 0; column < BoardSize; column++)
            {
                List<int> numbers = new List<int>();
                for (int lign = 0; lign < BoardSize; lign++)
                {
                    if (numbers.Contains(Board[lign, column]))
                        return false;
                    if (Board[lign, column] == 0)
                        continue;
                    numbers.Add(Board[lign, column]);
                }
            }

            return true;
        }

        public bool __IsMiniBoardValid(int r, int c)
        {
            List<int> numbers = new List<int>();
            bool valid = true;
            
            for (int i = r; i < r + 3; i++)
            {
                for (int j = c; j < c + 3; j++)
                {
                    if (numbers.Contains(Board[i, j]))
                        return false;
                    if (Board[i, j] == 0)
                        continue;
                    numbers.Add(Board[i, j]);
                }
            }

            return valid;
        }
        
        public bool IsBoardValid()
        {
            bool valid = true;

            if (!__IsColumnValid() || !__IsLignValid())
                return false;

            //test into a square 3x3  
            for (int lign = 0; lign <= 6; lign += 3)
            {
                for (int column = 0; column <= 6; column += 3)
                {
                    if (!__IsMiniBoardValid(lign, column))
                        return false;
                }
            }

            return valid;
        }
        
        public bool IsSolved()
        {
            bool solved = true;
            if (IsBoardValid()) {
                for (int i = 0; i < BoardSize; i++) {
                    for (int j = 0; j < BoardSize; j++) {
                        if (Board[i, j] == 0)
                        {
                            solved = false;
                        }
                    }
                }
            }
            else {
                solved = false;
            }

            return solved;
        }

        
        public bool Solve()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Board[i, j] == 0)
                    {
                        List<int> tested = new List<int>() {1,2,3,4,5,6,7,8,9};
                        Random random = new Random();
                        while (tested.Count != 0)
                        {
                            int number = random.Next(0,tested.Count);
                            Board[i, j] = tested[number];
                            tested.RemoveAt(number);
                            if (IsBoardValid())
                            {
                                if (Solve())
                                    return true;
                            }
                            Board[i, j] = 0;
                        }

                        return false;
                    }
                }
            }
            return true;
        }
        

        public static void Play()
        {
            Console.WriteLine("Choose a level of diﬀiculty (Easy, Medium, Hard) : ");
            
            Console.WriteLine("Easy : 45 cases filled");
            Console.WriteLine("Medium : 30 cases filled");
            Console.WriteLine("Hard : 20 cases filled");
            bool p = true;
            int d = 0;
            while (p)
            {
                string diff = Console.ReadLine();
                switch (diff)
                {
                    case "Easy":
                        d = 45;
                        p = false;
                        break;
                    case "Medium":
                        d = 30;
                        p = false;
                        break;
                    case "Hard":
                        d = 20;
                        p = false;
                        break;
                    default:
                        Console.Error.WriteLine("this level is not available");
                        Console.WriteLine("Easy : 45 cases filled");
                        Console.WriteLine("Medium : 30 cases filled");
                        Console.WriteLine("Hard : 20 cases filled");
                        break;
                }
            }
            Sudoku sudoku = new Sudoku(d);

            while (!sudoku.IsSolved())
            {
                sudoku.Print();
                
                Console.WriteLine("Coordinates and value : ");
                string input = Console.ReadLine();
                string[] _input = input.Split(' ');
                List<int> numbers = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
                List<int> pos = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8};

                if (_input.Length == 3 && pos.Contains(Int32.Parse(_input[0])) &&
                    pos.Contains(Int32.Parse(_input[1])) && numbers.Contains(Int32.Parse(_input[2])))
                {
                    sudoku.Board[Int32.Parse(_input[1]), Int32.Parse(_input[0])] = Int32.Parse(_input[2]);
                }
                else
                {
                    Console.Error.WriteLine("Invalid input");
                }            
            }
            Console.WriteLine("You won");
        }

        #region Provided

        public const int HorizontalMargin = 2;

        public void Print()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                if (i == 0)
                    PrintLine('┌', '┬', '┐');
                else
                    PrintLine('├', '┼', '┤');
                
                for (int j = 0; j < BoardSize; j++)
                {
                    Console.Write('│');
                    PrintWithMargins(Board[i, j] == 0 ? " " : Board[i, j].ToString());
                }
                
                Console.WriteLine('│'); 
            }

            PrintLine('└', '┴', '┘');
        }

        public void PrintLine(char start, char middle, char end)
        {
            int caseSize = (2 + HorizontalMargin * 2);
            
            Console.Write(start);
            for (int i = 0; i < caseSize * BoardSize - 1; i++)
            {
                Console.Write((i + 1) % caseSize == 0 ? middle : '─');
            }
            Console.WriteLine(end);
        }

        public void PrintWithMargins(string value)
        {
            for (int i = 0; i < HorizontalMargin * 2 + 1; i++)
            {
                Console.Write(i == HorizontalMargin ? value : ' ');
            }
        }

        #endregion
    }
}