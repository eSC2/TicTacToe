using System;

namespace TicTacToe
{
    internal class Program
    {

        static string[,] board =
            {
                { " 1 ", " | " , " 2 " , " | " , " 3 " },
                { "---", " | " , "---" , " | " , "---" },
                { " 4 ", " | " , " 5 " , " | " , " 6 " },
                { "---", " | " , "---" , " | " , "---" },
                { " 7 ", " | " , " 8 " , " | " , " 9 " }
            };

        static string[] boardNum = new string[9];

        static bool winner = false;
        static bool draw = false;
        static bool markingSet = false;
        static int currentPlayer = 1;

        static void Main()
        {
            while (!draw || !winner)
            {
                
                PrintBoard();
                AssignBoardNum();

                int playerInput = GetInput(currentPlayer);

                if (playerInput != 0)
                {
                    MarkPlayerBoard(playerInput, currentPlayer);
                    Console.Clear();
                }

                WinningCondition();
                DrawCondition();

                if (markingSet && !winner)
                {
                    ChangePlayer();
                }

                if (winner)
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("Winner: Player " + currentPlayer);
                    break;
                }
                else if (draw)
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("Draw.. Game Over");
                    break;
                }
            }
        }

        static void AssignBoardNum()
        {
            int k = 0;

            for (int i = 0; i < board.GetLength(0); i += 2)
            {
                for (int j = 0; j < board.GetLength(0); j += 2)
                {
                    boardNum[k] = $"board[{i},{j}]";
                    k++;
                }
            }
        }

        static void PrintBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.WriteLine();

                for (int j = 0; j < board.GetLength(1); j++)
                    Console.Write(board[i, j]);
            }
            Console.WriteLine();
        }

        static int GetInput(int currentPlayer)
        {
            string playerSymbol = currentPlayer == 1 ? "X" : "O";

            Console.WriteLine($"Player {currentPlayer} ({playerSymbol}):");
            Console.WriteLine("Please select a number from the board");

            bool checkValidInput = int.TryParse(Console.ReadLine(), out int parsedInput);

            if (!checkValidInput || ( parsedInput > 9 || parsedInput == 0))
            {
                Console.Clear();
                Console.WriteLine("Invalid input.. Please select a number form the board");
                parsedInput = 0;
            }

            return parsedInput;
        }

        static void ChangePlayer()
        {
            if (currentPlayer == 1)
                currentPlayer = 2;
            else
                currentPlayer = 1;
        }

        static void DrawCondition()
        {
            int markedCounter = 0;

            for (int i = 0; i < board.GetLength(0); i += 2)
            {
                for (int j = 0; j < board.GetLength(0); j += 2)
                {
                    if (!int.TryParse(board[i, j].Trim(), out int k))
                    {
                        markedCounter++;
                    }
                }
            }

            if (markedCounter == 9)
                draw = true;
        }

        static bool CheckSelection(int i, int j)
        {
            return int.TryParse(board[i, j].Trim(), out int k);
        }

        static void MarkPlayerBoard(int playerInput, int currentPlayer)
        {

            string playerMark = currentPlayer == 1 ? "X" : "O";
            markingSet = true;

            switch (playerInput)
            {
                case 1:
                    if (CheckSelection(0, 0))
                        board[0, 0] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                case 2:
                    if (CheckSelection(0, 2))
                        board[0, 2] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                case 3:
                    if (CheckSelection(0, 4))
                        board[0, 4] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                case 4:
                    if (CheckSelection(2, 0))
                        board[2, 0] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                case 5:
                    if (CheckSelection(2, 2))
                        board[2, 2] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                case 6:
                    if (CheckSelection(2, 4))
                        board[2, 4] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                case 7:
                    if (CheckSelection(4, 0))
                        board[4, 0] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                case 8:
                    if (CheckSelection(4, 2))
                        board[4, 2] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                case 9:
                    if (CheckSelection(4, 4))
                        board[4, 4] = $" {playerMark} ";
                    else
                        markingSet = false;

                    break;

                default:
                    Console.WriteLine("Invalid input, please try again..");
                    break;
            }

            if (!markingSet)
            {
                Console.WriteLine("Selection taken.. Please try another one..");
            }
        }

        static bool DiagonalCheck()
        {
            string[] WinningDiagonal = new string[3];

            for (int i = 0, k = 0; i < board.GetLength(0); i += 2, k++)
            {
                WinningDiagonal[k] = board[i, i].Trim();
            }

            string combinedSelections = string.Join("", WinningDiagonal);

            return combinedSelections == "XXX" || combinedSelections == "OOO";
        }

        static bool ReverseDiagonalCheck()
        {

            string[] WinningDiagonal = new string[3];

            for (int i = 0, j = board.GetLength(0) - 1, k = 0; i < board.GetLength(0); i += 2, j -= 2, k++)
            {
                WinningDiagonal[k] = board[i, j].Trim();
            }

            string combinedSelections = string.Join("", WinningDiagonal);

            return combinedSelections == "XXX" || combinedSelections == "OOO";
        }

        static bool HorizontalCheck()
        {
            bool chkHorizontal = false;
            string[] horizontalArr = new string[3];

            for (int i = 0; i < board.GetLength(0); i += 2)
            {
                for (int j = 0, k = 0; j < board.GetLength(1); j += 2, k++)
                {
                    horizontalArr[k] = board[i, j].Trim();
                }

                string combinedSelection = string.Join("", horizontalArr);

                if (combinedSelection == "XXX" || combinedSelection == "OOO")
                    chkHorizontal = true;
            }
            return chkHorizontal;
        }

        static bool VerticalCheck()
        {
            bool chkVertical = false;
            string[] verticalArr = new string[3];

            for (int i = 0; i < board.GetLength(0); i += 2)
            {
                for (int j = 0, k = 0; j < board.GetLength(1); j += 2, k++)
                {
                    verticalArr[k] = board[j, i].Trim();
                }

                string combinedSelection = string.Join("", verticalArr);

                if (combinedSelection == "XXX" || combinedSelection == "OOO")
                    chkVertical = true;
            }
            return chkVertical;
        }

        static void WinningCondition()
        {
            bool verticalCheck = VerticalCheck();
            bool horizontalCheck = HorizontalCheck();
            bool diagCheck = DiagonalCheck();
            bool reverseDiagCheck = ReverseDiagonalCheck();

            if (verticalCheck || horizontalCheck || diagCheck || reverseDiagCheck)
                winner = true;
        }
    }
}

