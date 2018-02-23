using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

public class RootSearchAster : MonoBehaviour {



	class Program {
		static void Main(string[] args) {
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			Asterprogram AP = new Asterprogram();
			AP.createDefaltPointList();
			AP.makeStartPoint(0, 0);
			AP.makeGoalPoint(2, 0);
			AP.makeDefaultRoot();
			AP.changeRootCost(1, 0, 16);
			AP.changeRootCost(1, 1, 16);
			AP.CalculateGoalDistance();
			AP.startRouteSearch();
			sw.Stop();
			Console.WriteLine(sw.Elapsed);
			Console.ReadLine();
		}
	}
	class Asterprogram {
		private int listlength = 10;
		private PointObject[,] pointlist;
		private List<PointObject> openlist;
		private PointObject startPoint;
		private PointObject goalPoint;

		public Asterprogram() {
			pointlist = new PointObject[listlength, listlength];
			openlist = new List<PointObject>();
		}

		public void changeRootCost(int aX, int aY, int acost) {
			pointlist[aX, aY].deleteAllroot();
			makeroot(aX, aY, acost);
		}

		private void addOpenList(PointObject addpoint) {//オープンリストへの追加メソッド
			openlist.Add(addpoint);
		}

		private void deliteOpenList(PointObject deletepoint) {//オープンリストからの削除メソッド
			openlist.Remove(deletepoint);
		}

		public void createDefaltPointList() {//ポイントオブジェクトの配列のデフォルトデータを作成するメソッド
			for (int i = 0; i < listlength; i++) {
				for (int j = 0; j < listlength; j++) {
					pointlist[i, j] = new PointObject(i, j);
				}
			}
		}

		public void makeStartPoint(int x, int y) {

			pointlist[x, y].setStartPoint();
			pointlist[x, y].changeCostFromstart(0);
		}

		public void makeGoalPoint(int x, int y) {

			pointlist[x, y].setGoalPoint();
		}

		public void CalculateGoalDistance() {//ゴールポイントとの距離を測るメソッド
			for (int i = 0; i < listlength; i++) {
				for (int j = 0; j < listlength; j++) {
					pointlist[i, j].setDistancetoGoal(goalPoint);
				}
			}
		}

		public void searchMinimamPointandDetermine() {//openlistの中の推定コストが最小のポイントを取得し、ポイントのオープンフラグをオンにするメソッド

			openlist.Sort();
			openlist[0].checkDetermined();

			if (openlist[0].checkGoalPoint()) {
				Console.WriteLine(openlist[0].checkEstimaterCost());
				deliteOpenList(openlist[0]);
			}
			else if (openlist[0].checkEstimaterCost() != 9999) {
				checkRootCostandRenewCost(openlist[0]);
			}
			else {
				return;
			}

		}

		public void Showroot(PointObject aGoalPoint) {//ゴールオブジェクトから再帰的に最短経路を割り出すメソッド,aGoalPointにゴールオブジェクトの参照を入れて使うが、ぶっちゃけ見直し必要
			Console.WriteLine("{0} {1}", goalPoint.pointX, goalPoint.pointY);
			Showroot(aGoalPoint.parentPoint);
		}

		public void startRouteSearch() {//ポイントからつながる先のポイントを割り出し、コストを更新、およびオープンリストに追加するメソッド
			checkRootCostandRenewCost(startPoint);
		}

		public void checkRootCostandRenewCost(PointObject checkingpoint) {
			int beforeX, beforeY, afterX, afterY;
			beforeX = checkingpoint.pointX;
			beforeY = checkingpoint.pointY;

			foreach (var lists in pointlist[beforeX, beforeY].returnRootList()) {

				if (!lists.afterPoint.isDitermind())//決定済みでなければコスト更新判定を続ける
				{
					if (lists.afterPoint.checkCostFromstart() > checkingpoint.checkCostFromstart() + lists.cost)//costfromstartが低くなるのであれば、低くする
					{
						lists.afterPoint.changeCostFromstart(lists.afterPoint.checkCostFromstart() + lists.cost);
						lists.afterPoint.setEstimatedCost();

						if (!lists.afterPoint.checkAddOpenFlag()) {//オープンリストに追加されていなければ追加する。
							openlist.Add(lists.afterPoint);
							lists.afterPoint.AddOpenFlag();
						}
					}
				}
			}
			this.searchMinimamPointandDetermine();

		}
		//全ポイントリストにそれぞれのルートを持たせている（チェック用。y座標が偶数の場合は上と下につながるよう設定。y座標が奇数の場合は右と下につながるように設定。）
		public void makeDefaultRoot() {
			for (int j = 0; j < listlength; j++) {
				for (int i = 0; i < listlength; i++) {
					makeroot(i, j, 2);
				}
			}
		}
		public void makeroot(int i, int j, int aCost) {
			if (j + 1 < listlength) {
				pointlist[i, j].addRoot(pointlist[i, j + 1], aCost);
			}

			if (j - 1 >= 0) {
				pointlist[i, j].addRoot(pointlist[i, j - 1], aCost);
			}

			if (i + 1 < listlength) {
				pointlist[i, j].addRoot(pointlist[i + 1, j], aCost);
			}

			if (i - 1 >= 0) {
				pointlist[i, j].addRoot(pointlist[i - 1, j], aCost);
			}
		}

	}

	class root {//2次元配列にて使用。たどりつく先のポイントと、かかるコストを所持
		public PointObject afterPoint;
		public int cost;
		public root(PointObject aPoint, int acost) {
			afterPoint = aPoint;
			cost = acost;
		}
	}

	class PointObject : IComparable {//自身の座標、コスト、ゴールからの距離、推定コスト（コスト+ゴールからの距離）、ゴールフラグ、チェック中フラグ、繋がるルートのリスト、を所持している。
		public int pointX, pointY;
		private int distanceX, distanceY;
		private int costFromStart;
		public int estimatedcost;
		private bool determined;
		private bool checking;
		private bool startFlag;
		private bool goalFlag;
		private bool addedOpenlistFlag;
		private List<root> rootList;
		public PointObject parentPoint;
		public PointObject(int x, int y) {
			pointX = x;
			pointY = y;
			costFromStart = 9999;
			determined = false;
			checking = false;
			startFlag = false;
			goalFlag = false;
			addedOpenlistFlag = false;
			rootList = new List<root>();
			parentPoint = null;
		}

		public void addRoot(PointObject afterpoint, int rootcost) {
			rootList.Add(new root(afterpoint, rootcost));
		}
		public bool checkDeterminded() {
			return this.determined;
		}
		public bool checkChecking() {
			return this.checking;
		}
		public void setDistancetoGoal(PointObject goalpoint) {
			distanceX = Math.Abs(goalpoint.pointX - this.pointX);
			distanceY = Math.Abs(goalpoint.pointY - this.pointY);
		}
		public void setEstimatedCost() {
			estimatedcost = costFromStart + distanceX + distanceY;
		}
		public bool isDitermind() {
			return this.determined;
		}
		public void setStartPoint() {
			this.startFlag = true;
		}
		public void setGoalPoint() {
			this.goalFlag = true;
		}
		public void deleteAllroot() {
			this.rootList.Clear();
		}
		public void changeCostFromstart(int newCost) {
			this.costFromStart = newCost;
		}
		public int checkCostFromstart() {
			return this.costFromStart;
		}
		public bool checkAddOpenFlag() {
			return this.addedOpenlistFlag;
		}
		public void AddOpenFlag() {
			this.addedOpenlistFlag = true;
		}
		public int checkEstimaterCost() {
			return this.estimatedcost;
		}
		public bool checkStartPoint() {
			return this.startFlag;
		}
		public bool checkGoalPoint() {
			return this.goalFlag;
		}
		public bool checkDetermined() {
			return this.determined;
		}
		public PointObject checkParentsPoint() {
			return this.parentPoint;
		}
		public List<root> returnRootList() {
			return this.rootList;
		}
		
		public int CompareTo(object obj) {
			//nullより大きい
			if (obj == null) {
				return 1;
			}

			//違う型とは比較できない
			if (this.GetType() != obj.GetType()) {
				throw new ArgumentException("別の型とは比較できません。", "obj");
			}
			//このクラスが継承されることが無い（構造体など）ならば、次のようにできる
			//if (!(other is TestClass)) { }

			//Priceを比較する
			return estimatedcost.CompareTo(((PointObject)obj).estimatedcost);
			//または、次のようにもできる
			//return this.Price - ((Product)other).Price;
		}
	}
}
