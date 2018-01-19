/*--------------------------------------------------------------------------------------------------------------------
 * Author:		王立奇
 * QQ:		8384748
 * Date:		2017-2-13
 * Function:		五子棋的玩家类，五子棋玩家的阵营颜色只能是两种之一
 -------------------------------------------------------------------------------------------------------------------*/
using System;

namespace Silentdoer.FiveChess
{
	/// <summary>
	/// 此类可以继承一个Player的基类，不过我省麻烦就懒得继承了。
	/// </summary>
	public class FiveChessPlayer
	{
		public FiveChessPlayer(TwoPlayerChessColorEnum playerColor)
		{
			PlayerColor = playerColor;
			PlayerName = $"Player_{(ConsoleColor)PlayerColor}";
		}

		public FiveChessPlayer(string playerName, TwoPlayerChessColorEnum playerColor)
		{
			PlayerName = playerName;
			PlayerColor = playerColor;
		}

		public string PlayerName { get; set; }

		/// <summary>
		/// 这个类型是ConsoleColor或TwoPlayerChessColorEnum都可以。
		/// </summary>
		public TwoPlayerChessColorEnum PlayerColor { get; }
	}
}
