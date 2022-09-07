using System;
using Reversi;
using ApolloRoboto.Commander;
// using CommandLine;

namespace App
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			
			Board board = BoardPreset.Startup();
			int score = board.GetScore(Player.WHITE);
			Utils.PrintBoard(board);
		}


		// static void Main(string[] args)
		// {
		// 	Console.OutputEncoding = System.Text.Encoding.UTF8;

		// 	IPlayer p1 = null;
		// 	IPlayer p2 = null;
			
		// 	Parser.Default.ParseArguments<CMDOptions>(args)
		// 	.WithParsed(options => {
		// 		options.Validate();

		// 		switch(options.Bot)
		// 		{
		// 			case 0: // game with two players
		// 				p1 = new HumanPlayer();
		// 				p2 = new HumanPlayer();
		// 				break;
		// 			case 1: // game with one bot
		// 				p1 = new Recurse2Bot();
		// 				p2 = new HumanPlayer();
		// 				break;
		// 			case 2: // automated game
		// 				p1 = new Recurse2Bot();
		// 				p2 = new Recurse2Bot();
		// 				break;
		// 		}
		// 	});

		// 	Play(p1, p2);
		// }

		// static void Play(IPlayer p1, IPlayer p2)
		// {
		// 	// randomly set black or white
		// 	if(new Random().NextDouble() > 0.5)
		// 	{
		// 		IPlayer tmp = p1;
		// 		p1 = p2;
		// 		p2 = tmp;
		// 	}

		// 	Board board = BoardPreset.Startup();
		// 	while(!board.IsGameOver())
		// 	{
		// 		Console.Clear();
		// 		Utils.PrintBoard(board);

		// 		PositionInformation nextMove;

		// 		switch(board.Turn)
		// 		{
		// 			case Player.BLACK:
		// 				nextMove = p1.NextMove(board, Player.BLACK);
		// 				break;
		// 			case Player.WHITE:
		// 				nextMove = p2.NextMove(board, Player.WHITE);
		// 				break;
		// 			default:
		// 				throw new Exception("Invalid player");
		// 		}

		// 		board.Place(nextMove.Pos, board.Turn);
		// 		board.SwitchTurn();
		// 	}

		// 	Console.WriteLine("=== GAME OVER ===");
		// 	Utils.PrintBoard(board);
		// 	Console.WriteLine("Winner is " + board.GetWinner());
		// }
	}
}
