/*--------------------------------------------------------------------------------------------------------------------
 * Author:		王立奇
 * QQ:		8384748
 * Date:		2017-2-13
 * Function:		五子棋的环境类，内部包括运行逻辑、移动方法、刷新、游戏开始等功能。
 -------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Linq;

namespace Silentdoer.FiveChess
{
	/// <summary>
	/// 五子棋环境类。
	/// </summary>
	public class FiveChessContext
	{
		private FiveChessMapPoint[][] _fiveChessMap;  // 由于MapPoint是internal的，故它只能被同项目的类所引用，且不能是public，因为public比internal的访问级要高。

		private readonly TwoPlayerChessColorEnum[] _playerColors = { TwoPlayerChessColorEnum.PlayerOneColor, TwoPlayerChessColorEnum.PlayerTwoColor };

		private bool _isOver = false;

		/// <summary>
		/// 光标所在坐标
		/// </summary>
		public PositiveCoordinate CursorCoord { get; protected set; }

		/// <summary>
		/// 赢的玩家名
		/// </summary>
		public string SuccessPlayer { get; protected set; } = "没有赢家";

		/// <summary>
		/// 五子棋玩家集对象
		/// </summary>
		internal FiveChessPlayer[] Players { get; set; }

		/// <summary>
		/// 当前的用户，（类似状态模式中的状态属性）
		/// </summary>
		public FiveChessPlayer CurPlayer { get; protected set; }

		/// <summary>
		/// 五子棋盘边长
		/// </summary>
		public int FiveChessMapSideLength { get; }  // 内部也不允许set

		/// <summary>
		/// 玩家数，常量
		/// </summary>
		public int PlayerCount { get; } = 2;

		/// <summary>
		/// 初始化五子棋盘点集，参数为row/clm上有多少个棋盘点，该值必须为奇数，否则自动加1。
		/// Player的初始化也可以放在外面然后传参传进来（但是这样又不好保证外面只new了两个Player）
		/// 或者里面改下Player的代码，使得可以new后配置参数，然后这里先new两个Player，外面来赋值（这种比较好）。
		/// </summary>
		public FiveChessContext(int mapSideLength)
		{
			FiveChessMapSideLength = (mapSideLength / 2) * 2 + 1;  // 确保是奇数
			Players = new[] { new FiveChessPlayer(_playerColors[0]), new FiveChessPlayer(_playerColors[1]) };
			CurPlayer = Players[0];
			CursorCoord = new PositiveCoordinate(FiveChessMapSideLength / 2, FiveChessMapSideLength / 2);
			InitMap();
			_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX].CursorPiece = CursorPieceHelper.GetCursorPiece();
		}

		/// <summary>
		/// 主动设置两个玩家的名字
		/// </summary>
		public void SetPlayerName(string playerNameOne, string playerNameTwo)
		{
			Players[0].PlayerName = playerNameOne;
			Players[1].PlayerName = playerNameTwo;
		}

		/// <summary>
		/// 转换状态（由于我的状态固定是两种，故事先生成好了这两种“状态”，用切换而非用SetState()，教课式的状态模式是这里有个State参数，由外部生成新状态对象）
		/// </summary>
		protected void SwitchState()  // 叫SwitchPlayer()比较好
		{
			// 教课式状态模式是这里用参数的state替换当前的state，然后用“当前”state执行某些操作。
			CurPlayer = Players.First(p => p.PlayerColor != CurPlayer.PlayerColor);
		}

		/// <summary>
		/// 初始化五子棋盘点集，参数为row/clm上有多少个棋盘点
		/// new Map的过程到底放在外面好还是这里就OK待讨论。
		/// </summary>
		protected void InitMap()
		{
			_fiveChessMap = new FiveChessMapPoint[FiveChessMapSideLength][];
			for(int i = 0; i < FiveChessMapSideLength; i++)
			{
				_fiveChessMap[i] = new FiveChessMapPoint[FiveChessMapSideLength];
			}
			for(int i = 0; i < _fiveChessMap.Length; i++)
			{
				for(int j = 0; j < _fiveChessMap[i].Length; j++)
				{
					_fiveChessMap[i][j] = new FiveChessMapPoint(new PositiveCoordinate(i, j));
				}
			}
		}

		/// <summary>
		/// 刷新棋盘
		/// </summary>
		protected void RefreshMap()
		{
			Console.SetCursorPosition(0, 0);  // 比Clear的方式要好，不过还可以再进一步用局部刷新/重输出的方式。
			foreach(var row in _fiveChessMap)
			{
				foreach(var cellClm in row)
				{
					cellClm.Show();
					Console.Write("  ");  // 格式需要
				}
				Console.WriteLine(Environment.NewLine);  // 格式需要
			}
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("方向键控制方向;Q和E退出游戏;Enter和空格是落子;当前用户{0}", CurPlayer.PlayerName.PadRight(15, '*'));
		}

		protected void MoveCursor(ConsoleKey key)
		{
			switch(key)
			{
				case ConsoleKey.UpArrow:  // 向上
					if(CursorCoord.RowY - 1 >= 0)
					{
						_fiveChessMap[CursorCoord.RowY - 1][CursorCoord.ClmX].CursorPiece =
							_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX].RemoveCursorPiece();
						CursorCoord = new PositiveCoordinate(CursorCoord.RowY - 1, CursorCoord.ClmX);
					}
					break;
				case ConsoleKey.RightArrow:  // 向右
					if(CursorCoord.ClmX + 1 <= FiveChessMapSideLength - 1)
					{
						_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX + 1].CursorPiece =
							_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX].RemoveCursorPiece();
						CursorCoord = new PositiveCoordinate(CursorCoord.RowY, CursorCoord.ClmX + 1);
					}
					break;
				case ConsoleKey.DownArrow:  // 向下
					if(CursorCoord.RowY + 1 <= FiveChessMapSideLength - 1)
					{
						_fiveChessMap[CursorCoord.RowY + 1][CursorCoord.ClmX].CursorPiece =
							_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX].RemoveCursorPiece();
						CursorCoord = new PositiveCoordinate(CursorCoord.RowY + 1, CursorCoord.ClmX);
					}
					break;
				case ConsoleKey.LeftArrow:  // 向左
					if(CursorCoord.ClmX - 1 >= 0)
					{
						_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX - 1].CursorPiece =
							_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX].RemoveCursorPiece();
						CursorCoord = new PositiveCoordinate(CursorCoord.RowY, CursorCoord.ClmX - 1);
					}
					break;
			}
			RefreshMap();
		}

		/// <summary>
		/// 落子，不需刷新
		/// </summary>
		protected void DropChessPiece()
		{
			if(!_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX].HasChessPiece())
			{
				_fiveChessMap[CursorCoord.RowY][CursorCoord.ClmX].ChessPiece = new FiveChessPiece(CurPlayer);
			} else
			{
				return;
			}
			Console.Beep();
			// 判断是否5连
			if(HasFiveJoin())
			{
				SuccessPlayer = CurPlayer.PlayerName;
				_isOver = true;
				return;
			}
			SwitchState();  // 切换玩家（落子权）
		}

		/// <summary>
		/// 判断是否5连（包括5个以上的连接）
		/// 这里判断的时候可以写成Task并发判断（各个方向），有兴趣的同学可以自己改一下。
		/// </summary>
		protected bool HasFiveJoin()
		{
			// 判断X轴方向
			var rowSuc = 1;
			for(int i = CursorCoord.ClmX - 1; i >= 0; i--)
			{
				var tmpMapPoint = _fiveChessMap[CursorCoord.RowY][i];
				if(tmpMapPoint.HasChessPiece() && tmpMapPoint.ChessPiece.FiveChessPlayer == CurPlayer)
				{
					rowSuc++;
					if(rowSuc >= 5)  // or ==
						return true;
				} else  // 被对手的子或空白“堵住”了。
				{
					break;
				}
			}
			for(int i = CursorCoord.ClmX + 1; i < FiveChessMapSideLength; i++)
			{
				var tmpMapPoint = _fiveChessMap[CursorCoord.RowY][i];
				if(tmpMapPoint.HasChessPiece() && tmpMapPoint.ChessPiece.FiveChessPlayer == CurPlayer)
				{
					rowSuc++;
					if(rowSuc >= 5)
						return true;
				} else  // 被对手的子或空白“堵住”了。
				{
					break;
				}
			}
			// Y轴方向
			var clmSuc = 1;
			for(int i = CursorCoord.RowY - 1; i >= 0; i--)
			{
				var tmpMapPoint = _fiveChessMap[i][CursorCoord.ClmX];
				if(tmpMapPoint.HasChessPiece() && tmpMapPoint.ChessPiece.FiveChessPlayer == CurPlayer)
				{
					clmSuc++;
					if(clmSuc >= 5)  // or ==
						return true;
				} else  // 被对手的子或空白“堵住”了。
				{
					break;
				}
			}
			for(int i = CursorCoord.RowY + 1; i < FiveChessMapSideLength; i++)
			{
				var tmpMapPoint = _fiveChessMap[i][CursorCoord.ClmX];
				if(tmpMapPoint.HasChessPiece() && tmpMapPoint.ChessPiece.FiveChessPlayer == CurPlayer)
				{
					clmSuc++;
					if(clmSuc >= 5)
						return true;
				} else  // 被对手的子或空白“堵住”了。
				{
					break;
				}
			}
			// 斜杠\方向
			var slashSuc = 1;
			for(int i = CursorCoord.RowY - 1; i >= 0; i--)
			{
				var tmp = CursorCoord.ClmX - (CursorCoord.RowY - i);
				if(tmp < 0)
					break;
				var tmpMapPoint = _fiveChessMap[i][tmp];
				if(tmpMapPoint.HasChessPiece() && tmpMapPoint.ChessPiece.FiveChessPlayer == CurPlayer)
				{
					slashSuc++;
					if(slashSuc >= 5)
						return true;
				} else  // 被对手的子或空白“堵住”了。
				{
					break;
				}
			}
			for(int i = CursorCoord.RowY + 1; i < FiveChessMapSideLength; i++)
			{
				var tmp = CursorCoord.ClmX + (i - CursorCoord.RowY);
				if(tmp >= FiveChessMapSideLength)
					break;
				var tmpMapPoint = _fiveChessMap[i][tmp];
				if(tmpMapPoint.HasChessPiece() && tmpMapPoint.ChessPiece.FiveChessPlayer == CurPlayer)
				{
					slashSuc++;
					if(slashSuc >= 5)
						return true;
				} else  // 被对手的子或空白“堵住”了。
				{
					break;
				}
			}
			// 反斜杠/方向
			var backslashSuc = 1;
			for(int i = CursorCoord.RowY - 1; i >= 0; i--)
			{
				var tmp = CursorCoord.ClmX + (CursorCoord.RowY - i);
				if(tmp >= FiveChessMapSideLength)
					break;
				var tmpMapPoint = _fiveChessMap[i][tmp];
				if(tmpMapPoint.HasChessPiece() && tmpMapPoint.ChessPiece.FiveChessPlayer == CurPlayer)
				{
					backslashSuc++;
					if(backslashSuc >= 5)
						return true;
				} else  // 被对手的子或空白“堵住”了。
				{
					break;
				}
			}
			for(int i = CursorCoord.RowY + 1; i < FiveChessMapSideLength; i++)
			{
				var tmp = CursorCoord.ClmX - (i - CursorCoord.RowY);
				if(tmp < 0)
					break;
				var tmpMapPoint = _fiveChessMap[i][tmp];
				if(tmpMapPoint.HasChessPiece() && tmpMapPoint.ChessPiece.FiveChessPlayer == CurPlayer)
				{
					backslashSuc++;
					if(backslashSuc >= 5)
						return true;
				} else  // 被对手的子或空白“堵住”了。
				{
					break;
				}
			}
			return false;
		}

		protected void ProcessKeyPress(ConsoleKeyInfo keyInfo)
		{
			switch(keyInfo.Key)
			{
				#region 退出游戏

				case ConsoleKey.E:
				case ConsoleKey.Q:
					_isOver = true;
					Console.WriteLine();
					Console.Write("强制结束游戏。");
					break;

				#endregion 退出游戏

				#region 方向控制

				case ConsoleKey.UpArrow:  // 向上
				case ConsoleKey.RightArrow:  // 向右
				case ConsoleKey.DownArrow:  // 向下
				case ConsoleKey.LeftArrow:  // 向左
					MoveCursor(keyInfo.Key);
					break;

				#endregion 方向控制

				#region 落子

				case ConsoleKey.Enter:
				case ConsoleKey.Spacebar:
					DropChessPiece();
					break;

					#endregion 落子
			}
		}

		/// <summary>
		/// 开始游戏
		/// </summary>
		public void Start()
		{
			RefreshMap();
			while(!_isOver)
			{
				var keyInfo = Console.ReadKey(true);  // true，不将按下的键显示出来。
				ProcessKeyPress(keyInfo);
			}
			Console.Beep(300, 1000);
			Console.WriteLine();
			Console.WriteLine("游戏赢家是：{0}，请按任意键结束程序。", SuccessPlayer);
		}
	}
}