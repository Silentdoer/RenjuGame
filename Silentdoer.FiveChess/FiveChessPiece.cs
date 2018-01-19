/*--------------------------------------------------------------------------------------------------------------------
 * Author:		王立奇
 * QQ:		8384748
 * Date:		2017-2-13
 * Function:		五子棋的棋子类，棋子附着于棋盘点
 -------------------------------------------------------------------------------------------------------------------*/
using System;

namespace Silentdoer.FiveChess
{
	/// <summary>
	/// FiveChessPiece可继承ChessPiece（乃至Piece），但是懒得弄了，注意，我这里的Chess是所有棋类的基类的意思，而非国际象棋。
	/// </summary>
	internal class FiveChessPiece:Piece
	{
		private const char FIVE_CHESS_PIECE_PICTURE = '●';  // 五子棋棋子均是这个形状。

		public FiveChessPlayer FiveChessPlayer { get; }  // 对于围棋而言这个是可以改变的，因为围棋某方将另一方的棋子集S围住后，S将变为它的棋子

		public new TwoPlayerChessColorEnum PieceColor { get; }

		public FiveChessPiece(FiveChessPlayer fiveChessPlayer)
		{
			FiveChessPlayer = fiveChessPlayer;
			PieceColor = fiveChessPlayer.PlayerColor;
			PiecePicture = FIVE_CHESS_PIECE_PICTURE;
		}

		/// <summary>
		/// “画出自己”，也就是说棋子画出自己这颗棋子，这其实是不符合我们人类的观念的，但是要用到设计模式是会有很多这种不合人类思维的设计。
		/// </summary>
		public override void Show()
		{
			Console.ForegroundColor = (ConsoleColor)PieceColor;
			Console.Write(PiecePicture);
		}
	}
}
