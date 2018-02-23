using UnityEngine;
using System.Collections;

public class MoveCharactor : MonoBehaviour {

		public Position GetMyPosition() {//charactorが動く時に自分がどこの座標にいるかを取得するメソッド。
			Position myposition = new Position();
			myposition.x = 1;//test
			myposition.y = 1;//test
			return myposition;
		}
		
		public Position GetNextPosition() {//charactorが動く時に自分の動くべき次の座標を取得するメソッド。
		Position myposition = new Position();
			myposition.x = 1;//test
			myposition.y = 1;//test
			return myposition;
		}

		public int[,] GetBestRoot() {//charactorがダイクストラマネージャークラスから最短経路を取得するクラス。

			//ダイクストラ法で、ゴールまでの最短経路を求める。その後てき

		}
		//charactorがダイクストラマネージャークラスに自分の座標位置を渡すメソッド
	}
