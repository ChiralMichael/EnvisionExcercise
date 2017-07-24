using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace EnvisionExercise.UnitTests
{
	[TestClass]
	public class GameTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Guess_ThrowsExceptionOnNullGuess()
		{
			var game = new Game();

			game.Guess(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Guess_ThrowsExceptionOnTooFewColors()
		{
			var game = new Game();

			var guess = new BallColor[] { BallColor.Blue, BallColor.Red, BallColor.Green };
			game.Guess(guess.ToList());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Guess_ThrowsExceptionOnTooManyColors()
		{
			var game = new Game();

			var guess = new BallColor[] { BallColor.Blue, BallColor.Red, BallColor.Green, BallColor.Yellow, BallColor.Yellow };

			game.Guess(guess.ToList());
		}

		[TestMethod]
		public void Guess_GivesSuccessForCorrectGuess()
		{
			var game = new Game();
			var selection = new BallColor[] { BallColor.Red, BallColor.Blue, BallColor.Green, BallColor.Yellow };
			SetSelection(game, selection);

			game.Guess(selection);

			Assert.AreEqual(4, game.Location);
			Assert.AreEqual(4, game.Color);
		}

		[TestMethod]
		public void Guess_GivesExpectedResultForExample1()
		{
			var game = new Game();
			var selection = new BallColor[] { BallColor.Blue, BallColor.Blue, BallColor.Red, BallColor.Red };
			var guess = new BallColor[] { BallColor.Red, BallColor.Blue, BallColor.Green, BallColor.Yellow };
			SetSelection(game, selection);

			game.Guess(guess);

			Assert.AreEqual(1, game.Location);
			Assert.AreEqual(2, game.Color);
		}

		[TestMethod]
		public void Guess_GivesExpectedResultForExample2()
		{
			var game = new Game();
			var selection = new BallColor[] { BallColor.Red, BallColor.Red, BallColor.Green, BallColor.Yellow };
			var guess = new BallColor[] { BallColor.Red, BallColor.Green, BallColor.Blue, BallColor.Red };
			SetSelection(game, selection);

			game.Guess(guess);

			Assert.AreEqual(1, game.Location);
			Assert.AreEqual(3, game.Color);
		}

		private void SetSelection(Game game, IList<BallColor> colors)
		{
			game._selectedBalls = colors;
			game._selectedColors = new int[Game.NumColors];
			foreach(var color in colors)
			{
				game._selectedColors[(int)color]++;
			}
		}
	}
}
