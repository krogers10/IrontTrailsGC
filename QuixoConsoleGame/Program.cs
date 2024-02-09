using System;

public class Program
{
    static void Main()
    {
        Game game = new Game();
        AI ai = new AI();
        while (!game.isWin(game.gameBoard, 'X') && !game.isWin(game.gameBoard, 'O'))
        {
            game.printBoard();
            while (!game.updateBoard('X', game.getMove('X'))) ;
            if (game.isWin(game.gameBoard, 'X') || game.isWin(game.gameBoard, 'O')) { break; }
            Console.WriteLine();
            game.printBoard();
            while (!game.updateBoard('O', ai.findBestMove(game.gameBoard, 'O', 'X')));
        }

        game.printBoard();
        //Print result;
        if (game.isWin(game.gameBoard, 'X'))
        {
            Console.WriteLine("Player one won!");
        }
        else if (game.isWin(game.gameBoard, 'O'))
        {
            Console.WriteLine("AI won!");
        }
        else
        {
            Console.WriteLine("It was a tie.");
        }

        Console.ReadLine();
        
    }
}
