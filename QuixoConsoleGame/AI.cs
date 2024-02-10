using System;
using System.Collections.Generic;



class AI
{
    bool isWin(char[,] board, char player)
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


    


    char[,] updateBoard(char player, Move move, char[,] gameBoard)
    {

        // Shifts Up
        if (move.to.col == move.from.col && move.to.row == 0)
        {
            for (int i = move.from.row; i > 0; i--)
            {
                gameBoard[i, move.to.col] = gameBoard[i - 1, move.to.col];
            }
            gameBoard[0, move.to.col] = player;
            return gameBoard;
        }

        // Shifts Down
        if (move.to.col == move.from.col && move.to.row == 4)
        {
            for (int i = move.from.row; i < 4; i++)
            {
                gameBoard[i, move.to.col] = gameBoard[i + 1, move.to.col];
            }
            gameBoard[4, move.to.col] = player;
            return gameBoard;
        }

        // Shifts Right
        if (move.to.row == move.from.row && move.to.col == 0)
        {
            for (int i = move.from.col; i > 0; i--)
            {
                gameBoard[move.to.row, i] = gameBoard[move.to.row, i - 1];
            }
            gameBoard[move.to.row, 0] = player;
            return gameBoard;
        }

        // Shifts Left
        if (move.to.row == move.from.row && move.to.col == 4)
        {
            for (int i = move.from.col; i < 4; i++)
            {
                gameBoard[move.to.row, i] = gameBoard[move.to.row, i + 1];
            }
            gameBoard[move.to.row, 4] = player;
            return gameBoard;
        }

        Console.WriteLine("Invalid Move");
        return gameBoard;
    }

    // This suffle function taken from https://stackoverflow.com/questions/273313/randomize-a-listt
    private static Random rng = new Random();

    public static void Shuffle(List<Move> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Move value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    Move[] getPossibleMoves(char[,] board, char opponent)
    {
        List<Move> possibleMoves = new List<Move>();
        if (board[0, 0] != opponent)
        {
            possibleMoves.Add(new Move( new Pos( 0, 0), new Pos( 0, 4) ));
            possibleMoves.Add(new Move( new Pos( 0, 0), new Pos( 4, 0) ));
        }
        if (board[0, 4] != opponent)
        {
            possibleMoves.Add(new Move( new Pos( 0, 4), new Pos( 0, 0) ));
            possibleMoves.Add(new Move( new Pos( 0, 4), new Pos( 4, 4) ));
        }
        if (board[4, 0] != opponent)
        {
            possibleMoves.Add(new Move( new Pos( 4, 0), new Pos( 4, 4) ));
            possibleMoves.Add(new Move( new Pos( 4, 0), new Pos( 0, 0) ));
        }
        if (board[4, 4] != opponent)
        {
            possibleMoves.Add(new Move( new Pos( 4, 4), new Pos( 4, 0) ));
            possibleMoves.Add(new Move( new Pos( 4, 4), new Pos( 0, 4) ));
        }
        for (int i = 1; i < 4; i++)
        {
            if (board[0, i] != opponent)
            {
                possibleMoves.Add(new Move( new Pos(0, i), new Pos(0, 4)));
                possibleMoves.Add(new Move( new Pos(0, i), new Pos(0, 0)));
                possibleMoves.Add(new Move( new Pos(0, i), new Pos(4, i)));
            }
            if (board[4, i] != opponent)
            {
                possibleMoves.Add(new Move( new Pos(4, i), new Pos(4, 4)));
                possibleMoves.Add(new Move( new Pos(4, i), new Pos(4, 0)));
                possibleMoves.Add(new Move( new Pos(4, i), new Pos(0, i)));
            }
            if (board[i, 0] != opponent)
            {
                possibleMoves.Add(new Move(new Pos(i, 0), new Pos(4, 0)));
                possibleMoves.Add(new Move(new Pos(i, 0), new Pos(0, 0)));
                possibleMoves.Add(new Move(new Pos(i, 0), new Pos(i, 4)));
            }
            if (board[i, 4] != opponent)
            {
                possibleMoves.Add(new Move(new Pos(i, 4), new Pos(4, 4)));
                possibleMoves.Add(new Move(new Pos(i, 4), new Pos(0, 4)));
                possibleMoves.Add(new Move(new Pos(i, 4), new Pos(i, 0)));
            }
        }
        Shuffle(possibleMoves);
        return possibleMoves.ToArray();
    }

    int boardScore(char[,] board, char ai, char player)
    {

        //Maximize AI, Minimize player
        int score = 0;
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                if (board[i, j] == ai)
                {
                    score += 2;
                }
                else if (board[i, j] == player)
                {
                    score -= 2;
                }
            }
            if (board[i, i] == ai)
            {
                score++;
            }
            else if (board[i, i] == player)
            {
                score--;
            }
            if (board[i, 4 - i] == ai)
            {
                score++;
            }
            else if (board[i, 4 - i] == player)
            {
                score--;
            }
        }
        return score;
    }

    int minimax(char[,] board, int depth, char curPlayer, char ai, char player)
    {
        
        if (isWin(board, player)) return -1000 / depth;
        else if (isWin(board, ai)) return 1000 / depth;

        if (depth == 4)
        {
            return boardScore(board, ai, player);
        }
        
        Move[] possibleMoves = new Move[0];
        if (curPlayer == ai)
        {
            possibleMoves = getPossibleMoves(board, player);
        }
        else
        {
            possibleMoves = getPossibleMoves(board, ai);
        }
        
        if (possibleMoves.Length == 0) return 0;

        if (curPlayer == ai)
        {
            int best = -100000;

            foreach (Move move in possibleMoves)
            {
                char[,] boardCopy = new char[5, 5];
                Array.Copy(board, boardCopy, 25);

                boardCopy = updateBoard(ai, move, boardCopy);

                best = Math.Max(best, minimax(boardCopy, depth + 1, player, ai, player));
            }
            return best;
        }

        else
        {
            int best = 100000;

            foreach (Move move in possibleMoves)
            {
                char[,] boardCopy = new char[5, 5];
                Array.Copy(board, boardCopy, 25);

                boardCopy = updateBoard(player, move, boardCopy);

                best = Math.Min(best, minimax(boardCopy, depth + 1, ai, ai, player));
            }
            return best;
        }

    }

    public Move findBestMove(in char[,] board, char ai, char player)
    {
        

        int bestVal = -100000;
        Move bestMove = new Move();
        Move[] possibleMoves = getPossibleMoves(board, player);

        
        foreach (Move move in possibleMoves)
        {  
            char[,] boardCopy = new char[5,5];
            Array.Copy(board, boardCopy, 25);
            boardCopy = updateBoard(ai, move, boardCopy);

            int moveVal = minimax(boardCopy, 1, player, ai, player);
        
            if (moveVal > bestVal)
            {
                bestMove = move;
                bestVal = moveVal;
            }
            
        }
        return bestMove;
    }
}
