using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silentdoer.FiveChess;

namespace Silentdoer.Main
{
	class Program
	{
		static void Main(string[] args)
		{
			var game = new FiveChessContext(10);
			game.Start();
			Console.ReadKey();
		}
	}
}
