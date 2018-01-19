/*--------------------------------------------------------------------------------------------------------------------
 * Author:		王立奇
 * QQ:		8384748
 * Date:		2017-2-13
 * Function:		五子棋的棋盘点类（我这里是将五子棋棋盘由N*N个棋盘点构成）
 -------------------------------------------------------------------------------------------------------------------*/
using System;

namespace Silentdoer.FiveChess
{
	/// <summary>
	/// 可继承至MapPoint（但其实没太大变化，故就不弄了），MapPoint中有Color、Picture、Coord三个属性（还可加一个ChessType，和一个ChessPiece(基类)）。
	/// </summary>
	internal class FiveChessMapPoint:IShowPoint
	{
		public const ConsoleColor FIVE_CHESS_MAP_POINT_COLOR = ConsoleColor.Gray;  // 也可以用ResetColor()

		public const char FIVE_CHESS_MAP_POINT_PICTURE = '┿';

		/// <summary>
		/// 棋盘点的坐标初始化后不可更改。
		/// </summary>
		public PositiveCoordinate Coord { get; }

		/// <summary>
		/// 光标
		/// </summary>
		public Piece CursorPiece { get; set; }

		/// <summary>
		/// 在棋盘点上面的棋子，棋盘点和棋子是1对0或1对1的关系
		/// </summary>
		public FiveChessPiece ChessPiece { get; set; }

		public char MapPointPicture { get; }  // 由于五子棋棋盘的每个棋盘点的图片/形状都一样，故其实可以由FIVE_CHESS_MAP_POINT_PICTURE代替

		public FiveChessMapPoint(PositiveCoordinate coord)
		{
			Coord = coord;
			ChessPiece = null;
			MapPointPicture = FIVE_CHESS_MAP_POINT_PICTURE;
		}

		/// <summary>
		/// 画出此个棋盘点
		/// </summary>
		public void Show()
		{
			if(CursorPiece != null)
				CursorPiece.Show();
			else if(ChessPiece != null)
				ChessPiece.Show();
			else
			{
				Console.ForegroundColor = FIVE_CHESS_MAP_POINT_COLOR;
				Console.Write(MapPointPicture);
			}
		}

		/// <summary>
		/// 移除棋盘点上的棋子对象，若没有棋子返回null
		/// </summary>
		/// <returns>这里若写的更细的话，这里该返回基类ChessPiece引用</returns>
		public FiveChessPiece RemoveChessPiece()
		{
			var tmp = ChessPiece;
			ChessPiece = null;
			return tmp;
		}

		/// <summary>
		/// 判断此棋盘点有没有棋子，有返回true。
		/// </summary>
		/// <returns></returns>
		public bool HasChessPiece()
		{
			if (ChessPiece != null)
				return true;
			return false;
		}

		public Piece RemoveCursorPiece()
		{
			var tmp = CursorPiece;
			CursorPiece = null;
			return tmp;
		}
	}
}
