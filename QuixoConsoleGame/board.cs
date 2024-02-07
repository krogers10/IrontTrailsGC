
using System;

public struct Pos
{
    public int row;
    public int col;

    public static bool operator ==(Pos A, Pos B)
    {
        if (A.col == B.col && A.row == B.row)
        {
            return true;
        }
        return false;
    }
    public static bool operator !=(Pos A, Pos B)
    {
        if (A.col == B.col && A.row == B.row)
        {
            return false;
        }
        return true;
    }

    public Pos(int row, int col)
    {
        this.row = row;
        this.col = col;
    }
}

public struct Move
{
    public Pos from;
    public Pos to;

    public Move(Pos from, Pos to)
    {
        this.from = from;
        this.to = to;
    }


}


public class Game
{
    public char[,] gameBoard = { {'-', '-', '-', '-', '-'},
                                 {'-', '-', '-', '-', '-'},
                                 {'-', '-', '-', '-', '-'},
                                 {'-', '-', '-', '-', '-'},
                                 {'-', '-', '-', '-', '-'} };
    public bool updateBoard(char player, Move move)
    {
        if (move.to == move.from)
        {
            Console.WriteLine("Invalid Move");
            return false;
        }

        // Shifts Up
        if (move.to.col == move.from.col && move.to.row == 0)
        {
            for (int i = move.from.row; i > 0; i--)
            {
                gameBoard[i, move.to.col] = gameBoard[i - 1, move.to.col];
            }
            gameBoard[0, move.to.col] = player;
            return true;
        }

        // Shifts Down
        if (move.to.col == move.from.col && move.to.row == 4)
        {
            for (int i = move.from.row; i < 4; i++)
            {
                gameBoard[i, move.to.col] = gameBoard[i + 1, move.to.col];
            }
            gameBoard[4, move.to.col] = player;
            return true;
        }

        // Shifts Right
        if (move.to.row == move.from.row && move.to.col == 0)
        {
            for (int i = move.from.col; i > 0; i--)
            {
                gameBoard[move.to.row, i] = gameBoard[move.to.row, i - 1];
            }
            gameBoard[move.to.row, 0] = player;
            return true;
        }

        // Shifts Left
        if (move.to.row == move.from.row && move.to.col == 4)
        {
            for (int i = move.from.col; i < 4; i++)
            {
                gameBoard[move.to.row, i] = gameBoard[move.to.row, i + 1];
            }
            gameBoard[move.to.row, 4] = player;
            return true;
        }

        Console.WriteLine("Invalid Move");
        return false;
    }

    public bool isWin(char[,] board, char player)
    {
        for (int i = 0; i < 5; i++)
        {
            if (board[i, 0] == player &&
                board[i, 1] == player &&
                board[i, 2] == player &&
                board[i, 3] == player &&
                board[i, 4] == player)
            {
                return true;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (board[0, i] == player &&
                board[1, i] == player &&
                board[2, i] == player &&
                board[3, i] == player &&
                board[4, i] == player)
            {
                return true;
            }
        }
        if (board[0, 0] == player &&
                board[1, 1] == player &&
                board[2, 2] == player &&
                board[3, 3] == player &&
                board[4, 4] == player)
        {
            return true;
        }
        if (board[0, 4] == player &&
                board[1, 3] == player &&
                board[2, 2] == player &&
                board[3, 1] == player &&
                board[4, 0] == player)
        {
            return true;
        }
        return false;
    }

    public void printBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(gameBoard[i, 0] + " " + gameBoard[i, 1] + " " + gameBoard[i, 2] + " " + gameBoard[i, 3] + " " + gameBoard[i, 4]);
        }

    }

    public Move getMove(char player)
    {
        Pos from = new Pos();
        Pos to = new Pos();
        bool incorrectMove;
        do
        {
            incorrectMove = false;
            Console.WriteLine("Make a move From:");
            Console.WriteLine("Row:");
            from.row = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Column:");
            from.col = Convert.ToInt32(Console.ReadLine());

            //Check for Perimeter Piece
            if (!((from.col == 0 || from.col == 4) || (from.row == 0 || from.row == 4)))
            {
                Console.WriteLine("That's not a perimeter piece!");
                incorrectMove = true;
            }
            if (!(gameBoard[from.row, from.col] == player || gameBoard[from.row, from.col] == '-'))
            {
                Console.WriteLine("That's not your piece!");
                incorrectMove = true;
            }
        } while (incorrectMove);


        Console.WriteLine("Make a move To:");
        Console.WriteLine("Row:");
        to.row = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Column:");
        to.col = Convert.ToInt32(Console.ReadLine());
        return new Move(from, to);
    }
}
