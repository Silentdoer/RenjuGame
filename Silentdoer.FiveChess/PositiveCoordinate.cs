/*--------------------------------------------------------------------------------------------------------------------
 * Author:		王立奇
 * QQ:		8384748
 * Date:		2017-2-13
 * Function:		坐标类，X，Y值均为>=0的值。
 -------------------------------------------------------------------------------------------------------------------*/
using System;

namespace Silentdoer.FiveChess
{
	/// <summary>
	/// “正数”坐标，即X，Y均是>=0，若传的是负数，如-2，则会自动将其变为2
	/// </summary>
	public struct PositiveCoordinate
	{
		private int _rowY;
		/// <summary>
		/// row 是Y轴
		/// </summary>
		public int RowY { get { return _rowY; } set { _rowY = Math.Abs(value); } }

		private int _clmX;
		/// <summary>
		/// clm 是X轴
		/// </summary>
		public int ClmX { get { return _clmX; } set { _clmX = Math.Abs(value); } }

		public PositiveCoordinate(int rowY, int clmX)
		{
			_rowY = Math.Abs(rowY);
			_clmX = Math.Abs(clmX);
		}
	}
}
