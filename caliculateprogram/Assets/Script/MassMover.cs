using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class MassMover : MonoBehaviour {
	Vector2 recentpos;
	MovingMass movemass;
	MathMass mathmass;

	public bool canGo(Vector2 movemass,Vector2 checkmass) {
		return //checkmass;
	}

	

	//現在のプレイヤーの位置から、入力された先にあるマスオブジェクトが踏破済みかどうかを判断し、
	//もし踏破済みであれば、プレイヤー位置を変更し計算後のプレイヤーの値を更新する。
	//もしゴールオブジェクトだった場合踏破計算結果がターゲットの数値と一致していればプレイヤーオブジェクトを移動させてゲームクリアとする。


}
