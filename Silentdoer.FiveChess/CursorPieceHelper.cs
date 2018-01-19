/*--------------------------------------------------------------------------------------------------------------------
 * Author:		王立奇
 * QQ:		8384748
 * Date:		2017-2-13
 * Function:		光标Piece的辅助类和实体类，通过辅助类只能参数一个光标对象（因为一个游戏里只能有一个光标）
 -------------------------------------------------------------------------------------------------------------------*/
using System;

namespace Silentdoer.FiveChess
{
	public static class CursorPieceHelper
	{
		private static readonly CursorPiece CurPiece = null;

		public static CursorPiece GetCursorPiece()
		{
			return CurPiece ?? new CursorPiece();
		}

		/// <summary>
		/// 不知道C#能不能不允许上层代码主动new CursorPiece，但我内部可以new 它
		/// </summary>
		public class CursorPiece:Piece
		{
			public CursorPiece()
			{
				PieceColor = ConsoleColor.Blue;
				PiecePicture = '★';
			}

			public override void Show()
			{
				Console.ForegroundColor = PieceColor;
				Console.Write(PiecePicture);
			}
		}
	}
}
