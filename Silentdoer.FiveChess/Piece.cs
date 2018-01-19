/*--------------------------------------------------------------------------------------------------------------------
 * Author:		王立奇
 * QQ:		8384748
 * Date:		2017-2-13
 * Function:		各种Piece的基类，是抽象类。
 -------------------------------------------------------------------------------------------------------------------*/
using System;

namespace Silentdoer.FiveChess
{
	public abstract class Piece:IShowPoint
	{
		public char PiecePicture { get; protected set; }

		public ConsoleColor PieceColor { get; set; }

		public abstract void Show();
	}
}
