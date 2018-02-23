using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication15
{
	class Program
	{
		static void Main(string[] args)
		{

			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			Dycstraprogram DP = new Dycstraprogram();
			DP.setDefaltRootCost();
			DP.setgoalpoint(9, 9);
			DP.setRootCost(0, 0, 0, 1, 12);
			DP.setRootCost(0, 0, 1, 0, 12);
			DP.setstartpoint(0, 0);
			sw.Stop();

			Console.WriteLine(sw.Elapsed);
			Console.ReadLine();
		}
	}
	class Dycstraprogram
	{
		System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
		int[,] costlist;
		bool[,] determinedlist;
		bool[,] checkinglist;
		bool[,] goallist;
		int[,] preCostlist;
		int[,] parentspointX;//例[0,1]の親ポイントが[0,0]なら、parentspointX[0,1]=0;parentspointY[0,1]=0
		int[,] parentspointY;
		int[,,,] rootCost;//例[0,1]から[0,2]に行くときのコストが7の場合は[0,1,0,2]＝7,いけないときはnull
		int Maxlistlength = 10;

		//初回にコンストラクタでコストと、falseを設定
		public Dycstraprogram()
		{
			sw.Start();
			costlist = new int[Maxlistlength, Maxlistlength];
			parentspointX = new int[Maxlistlength, Maxlistlength];
			parentspointY = new int[Maxlistlength, Maxlistlength];

			preCostlist = new int[Maxlistlength, Maxlistlength];
			goallist = new bool[Maxlistlength, Maxlistlength];
			determinedlist = new bool[Maxlistlength, Maxlistlength];
			checkinglist = new bool[Maxlistlength, Maxlistlength];

			//2次元リストの初期化



			for (int i = 0; i < Maxlistlength; i++)
			{
				for (int j = 0; j < Maxlistlength; j++)
				{
					costlist[i, j] = 9999;
					preCostlist[i, j] = 9999;
					determinedlist[i, j] = false;
				}
			}
		}

		public void setgoalpoint(int x, int y)
			
		{
			if (0<=x&&x < 10 &&0<=y&& y < 10)
				goallist[x, y] = true;
			else {
				Console.WriteLine("不正な値が入力されています。");
			}
		}

		public void setDefaltRootCost()
		{
			rootCost = new int[Maxlistlength, Maxlistlength, Maxlistlength, Maxlistlength];
			for (int i = 0; i < Maxlistlength; i++)
			{
				for (int j = 0; j < Maxlistlength; j++)
				{
					for (int k = 0; k < Maxlistlength; k++)
					{
						for (int L = 0; L < Maxlistlength; L++)
						{
							rootCost[i, j, k, L] = 1;
						}
					}
				}
			}
		}
		public void setRootCost(int i, int j, int k, int L, int cost)
		{
			rootCost[i, j, k, L] = cost;
		}
		//初期地点を設定するメソッド
		public void setstartpoint(int x, int y)
		{
			if (0 <= x && x < 10 && 0 <= y && y < 10)
			{
				costlist[x, y] = 0;
				determineCost(x, y);
			}
			else
			{
				Console.WriteLine("不正な値が入力されています。");
			}

		}
		//調べられたかどうかを設定するメソッド
		public void setDetermihed(int x, int y)
		{
			determinedlist[x, y] = true;
		}
		//自分の周り4マスを調べてコストを上書きするメソッド(oneOverRideCostメソッドとセット)
		public void cornerOverRideCost(int x, int y)
		{
			int left, right, up, below;
			below = y - 1;
			up = y + 1;
			left = x - 1;
			right = x + 1;
			if (below >= 0)
			{ oneOverRideCost(x, y, x, below); }
			if (up < costlist.GetLength(0))
			{
				oneOverRideCost(x, y, x, up);
			}
			if (left >= 0)
			{
				oneOverRideCost(x, y, left, y);
			}
			if (right < costlist.GetLength(1))
			{
				oneOverRideCost(x, y, right, y);
			}
			compare_determine();
		}

		//ポイント1点が親ポイント＋ルートのコストより高かったら上書き
		public void oneOverRideCost(int pX, int pY, int checkX, int checkY)
		{
			if (determinedlist[checkX, checkY] == false)
			{
				if (costlist[checkX, checkY] > costlist[pX, pY] + rootCost[pX, pY, checkX, checkY])
				{
					costlist[checkX, checkY] = costlist[pX, pY] + rootCost[pX, pY, checkX, checkY];
					parentspointX[checkX, checkY] = pX;
					parentspointY[checkX, checkY] = pY;
					preCostlist[checkX, checkY] = costlist[checkX, checkY];
					checkinglist[checkX, checkY] = true;

				}
			}
		}
		//推測中のポイントの中で最小のポイントを返し値を決定するメソッド
		public void compare_determine()
		{
			int MinCost = 9999;
			int MinXpoint = 0;
			int MinYpoint = 0;
			for (int i = 0; i < Maxlistlength; i++)
			{
				for (int j = 0; j < Maxlistlength; j++)
				{
					if (checkinglist[i, j] == true & MinCost > preCostlist[i, j])
					{
						MinCost = costlist[i, j];
						MinXpoint = i;
						MinYpoint = j;
					}

				}
			}
			if (MinXpoint == 9999) { return; }
				determineCost(MinXpoint, MinYpoint);

		}
		//推定ポイントでコストを比較して確定ポイントを探すためのメソッド
		public void determineCost(int checkX, int checkY)
		{
			determinedlist[checkX, checkY] = true;
			checkinglist[checkX, checkY] = false;
			preCostlist[checkX, checkY] = 9999;
			if (goallist[checkX, checkY] == true)
			{
				Console.WriteLine(costlist[checkX, checkY]);
				sw.Stop();
				Console.WriteLine("■処理Aにかかった時間");
				TimeSpan ts = sw.Elapsed;
				Console.WriteLine($"　{ts}");
				Console.WriteLine($"　{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒");
				Console.WriteLine($"　{sw.ElapsedMilliseconds}ミリ秒");
				Console.ReadLine();
			}
			cornerOverRideCost(checkX, checkY);
		}



	}
}