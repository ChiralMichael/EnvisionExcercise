using System;
using System.Collections.Generic;

namespace EnvisionExercise
{
	public class Game
    {
		public const int SelectionSize = 4;
		public const int NumColors = 4;
		public const int PenSize = 16;


		private IList<BallColor> _ballPen;

		//internal for unit testing
		//any real-world code would have something like a "game initializer" class that we would inject via IoC
		internal IList<BallColor> _selectedBalls; 
		internal int[] _selectedColors;

		private Random _rnd = new Random();

		public int Location { get; private set; } = 0;
		public int Color { get; private set; } = 0;

		public Game()
		{
			InitializePen();
			ChooseBalls();
		}

		public void Guess( IList<BallColor> colors )
		{
			if (colors?.Count != NumColors)
			{
				throw new ArgumentException($"Must supply {SelectionSize} colors");
			}

			Location = 0;
			Color = 0;

			var guessedColors = new int[NumColors];

			var selectionEnumerator = _selectedBalls.GetEnumerator();
			selectionEnumerator.MoveNext();

			foreach(var color in colors)
			{
				guessedColors[(int)color]++;
				if (selectionEnumerator.Current == color)
				{
					Location++;
				}
				selectionEnumerator.MoveNext();
			}

			for(int i = 0; i < NumColors; i++)
			{
				var result = Math.Min(guessedColors[i], _selectedColors[i]);
				Color += result;
			}
		}

		private void InitializePen()
		{
			_ballPen = new List<BallColor>();
			for (int i = 0; i < PenSize; i++) 
			{
				//The problem space is contrived, so it's difficult to know whether
				//a dynamic initialization of the pen is "useful" as far as code maintenance would go
				//I chose to do so because there was no reason not to and it's slightly less verbose
				var ball = (BallColor)(i % NumColors);
				_ballPen.Add(ball);
			}
		}

		private void ChooseBalls()
		{
			_selectedBalls = new List<BallColor>();
			_selectedColors = new int[NumColors];

			for (int i = 0; i < SelectionSize; i++)
			{
				var ball = ChooseBallFromPen();
				_selectedBalls.Add(ball);
				_selectedColors[(int)ball]++;
			}
		}

		private BallColor ChooseBallFromPen()
		{
			var rndBallIndex = _rnd.Next(0, _ballPen.Count);
			var ball = _ballPen[rndBallIndex];
			_ballPen.RemoveAt(rndBallIndex);
			return ball;
		}


    }
}
