using System;
using System.Collections.Generic;

namespace EnvisionExercise
{
	public class Program
	{
		static void Main(string[] args)
		{
			var game = new Game();
			while (game.Location != Game.SelectionSize)
			{
				Console.Write("Please Enter Your Guess:");
				var input = Console.ReadLine();
				try
				{
					var guess = ParseGuess(input);
					game.Guess(guess);
				} catch (ArgumentException ex)
				{
					Console.WriteLine();
					Console.Error.WriteLine($"Invalid input: {ex.Message}");
					continue;
				}

				Console.WriteLine();
				Console.WriteLine($"Location={game.Location}, Color={game.Color}");
			}

			Console.WriteLine("You win!");
		}

		static IList<BallColor> ParseGuess(string guess)
		{
			var colors = new List<BallColor>();
			
			foreach(var color in guess.ToUpper())
			{
				switch(color)
				{
					case 'R':
						colors.Add(BallColor.Red);
						break;
					case 'G':
						colors.Add(BallColor.Green);
						break;
					case 'B':
						colors.Add(BallColor.Blue);
						break;
					case 'Y':
						colors.Add(BallColor.Yellow);
						break;
					default:
						throw new ArgumentException($"Could not recognize color '{color}'");
				}
			}


			return colors;
		}

    }
}