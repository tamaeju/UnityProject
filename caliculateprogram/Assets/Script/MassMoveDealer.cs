using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class MassMoveDealer : MonoBehaviour {
	MovingMass movemass;
	GameObject[,] mathmasses;
	Vector2 rightVector = new Vector2(1, 0);
	Vector2 leftVector = new Vector2(-1, 0);
	Vector2 upVector = new Vector2(0, 1);
	Vector2 downVector = new Vector2(0, -1);
	[SerializeField]
	CurrentStageData currentdata;
	[SerializeField]
	FieldObjectMaker fieldobjectmaker;
	Subject<int> Clearedsubject = new Subject<int> ();
	public IObservable<int> OnCleared {
		get { return Clearedsubject; }
	}

	Subject<int> GameOveredsubject = new Subject<int>();
	public IObservable<int> OnGameOvered {
		get { return GameOveredsubject; }
	}



	//4秒で1回転するロジック、サイン関数を用いた実装。

	public void LoadFieldObject() {
		GameObject obj = fieldobjectmaker.GetMovingMass();
		movemass = obj.GetComponent<MovingMass>();
		mathmasses = fieldobjectmaker.GetMathMasses();
	}

	private void BaseMoveMethod(Vector2 directionpos) {
		Vector2 checkPos = movemass.GetMyPos() + directionpos;
		//まずやる事としては、次のマスがゴールマスか確認し、
		//ゴールマスであればクリア可能かを聞いてくる。クリア可能ならクリアメソッドを実行し、可能でないなら何もせずメソッド終了
		//ゴールマスでないなら、次のマスが既に通過済みかどうかを確認し、通過済みであればなにもせず終了、通過していないなら更新処理を実行
		if (currentdata.GetMoveCount()> currentdata.GetTargetMoveCount()) { GameOveredsubject.OnNext(1); }
		if (mathmasses[(int)checkPos.x, (int)checkPos.y].GetComponent<MathMass>().isGoal()) {
			if (currentdata.canClear()) {
				Clearedsubject.OnNext(1);
			}
			else if (!currentdata.canClear()) {
			}
		}
		else if (!(mathmasses[(int)checkPos.x, (int)checkPos.y].GetComponent<MathMass>().isGoThrough())) {
			RenewMoverNum(mathmasses[(int)checkPos.x, (int)checkPos.y].GetComponent<MathMass>());
			movemass.SetMyPos((int)checkPos.x, (int)checkPos.y);
			movemass.AddMyCount();
			currentdata.SetCurrentSum(movemass.GetMyNumber());
			currentdata.SetMoveCount(movemass.GetMyCount());
		}
		
	}

	private void RenewMoverNum(MathMass mathmass) {
		int newNum = mathmass.caliculate(movemass.GetMyNumber());
		movemass.ChangeMyNum(newNum);
		mathmass.ChangeThrough();
	}

	public void pushRightButton() {
		BaseMoveMethod(rightVector);
	}
	public void pushLeftButton() {
		BaseMoveMethod(leftVector);
	}
	public void pushUpButton() {
		BaseMoveMethod(upVector);
	}
	public void pushDownButton() {
		BaseMoveMethod(downVector);
	}
	public void pushBackButton() {
	}
	public void ReachGoalMethodTest() {//debug
		Debug.Log("Goaled!!!");
	}


	
	//もし現在のmovecountがターゲットmovecountより少ない場合、ゲームオーバーメソッドを呼び出す。
	//

	//OnNext自体はこのクラスが実行する。
	//ゲームシーンがこのクラスのsubjectをとってきて、ゲームシーンのメソッドをsubscribeする。

}

//private MathMass[,] ComvertMathsmassObjectType(GameObject[,] gotmathmass) {
//	MathMass[,] newMathmass = new MathMass[gotmathmass.GetLength(0), gotmathmass.GetLength(1)];
//	foreach (var item in gotmathmass) {
//		newMathmass
//		}
//	return
//	}


//ゲームクリア時の処理として適切なものとしては、
//ムーブマスディーラーにゲームシーンが終了時の処理を記載、
//クリア→ムーブマスディーラーがカレントデータクラスから現状のデータをとってくる→ゲームシーンクラスに

//最初にmoveCountにエフェクトが乗るのが許せない。→