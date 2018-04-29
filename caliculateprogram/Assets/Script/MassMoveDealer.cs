using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MassMoveDealer : MonoBehaviour {
	MovingMass movemass;
	GameObject[, ] mathmasses;
	Vector2 rightVector = new Vector2 (1, 0);
	Vector2 leftVector = new Vector2 (-1, 0);
	Vector2 upVector = new Vector2 (0, 1);
	Vector2 downVector = new Vector2 (0, -1);
	[SerializeField]
	CurrentStageData currentdata;
	[SerializeField]
	FieldObjectMaker fieldobjectmaker;
	Subject<int> Clearedsubject = new Subject<int> ();
	public IObservable<int> OnCleared {
		get { return Clearedsubject; }
	}

	Subject<int> GameOveredsubject = new Subject<int> ();
	public IObservable<int> OnGameOvered {
		get { return GameOveredsubject; }
	}

	//4秒で1回転するロジック、サイン関数を用いた実装。

	public void LoadFieldObject () {
		GameObject obj = fieldobjectmaker.GetMovingMass ();
		movemass = obj.GetComponent<MovingMass> ();
		mathmasses = fieldobjectmaker.GetMathMasses ();
	}

	private void BaseMoveMethod (Vector2 directionpos) {
		Vector2 checkPos = movemass.GetMyPos () + directionpos;
		//まずやる事としては、次のマスがゴールマスか確認し、
		//ゴールマスであればクリア可能かを聞いてくる。クリア可能ならクリアメソッドを実行し、可能でないなら何もせずメソッド終了
		//ゴールマスでないなら、次のマスが既に通過済みかどうかを確認し、通過済みであればなにもせず終了、通過していないなら更新処理を実行
		if (!isInRange (checkPos)) {

			return;
		} //レンジ内でないなら終了
		if (currentdata.GetMoveCount () > currentdata.GetTargetMoveCount ()) { GameOveredsubject.OnNext (1); } //gameoverならゲームオーバー処理を行う。
		if (mathmasses[(int) checkPos.x, (int) checkPos.y].GetComponent<MathMass> ().isGoal ()) { //次のマスがゴールなら判定を行い、可ならクリア処理を走らせる
			if (currentdata.canClear ()) {
				updateClearedData ();
				Clearedsubject.OnNext (1);

			} else if (!currentdata.canClear ()) {
				Debug.Log ("can't goal yet!");
			}
		} else if (!(mathmasses[(int) checkPos.x, (int) checkPos.y].GetComponent<MathMass> ().isGoThrough ())) { //次のマスがゴールでない場合、通過済みでないなら処理を行う。
			RenewMoverNum (mathmasses[(int) checkPos.x, (int) checkPos.y].GetComponent<MathMass> ());
			movemass.SetMyPos ((int) checkPos.x, (int) checkPos.y);
			movemass.AddMyCount ();
			currentdata.SetCurrentSum (movemass.GetMyNumber ());
			currentdata.SetMoveCount (movemass.GetMyCount ());
		}
	}

	private void RenewMoverNum (MathMass mathmass) {
		int newNum = mathmass.caliculate (movemass.GetMyNumber ());
		movemass.ChangeMyNum (newNum);
		mathmass.ChangeThrough ();
	}

	public void pushRightButton () {
		if (hasNextMass ()) {
			BaseMoveMethod (rightVector);
		}
	}
	public void pushLeftButton () {
		if (hasNextMass ()) {
			BaseMoveMethod (leftVector);
		}
	}
	public void pushUpButton () {
		if (hasNextMass ()) {
			BaseMoveMethod (upVector);
		}
	}
	public void pushDownButton () {
		if (hasNextMass ()) {
			BaseMoveMethod (downVector);
		}
	}

	public void ReachGoalMethodTest () { //debug
		Debug.Log ("Goaled!!!");
	}

	private bool isInRange (Vector2 checknextmass) {
		if (0 < checknextmass.x & 0 < checknextmass.y & checknextmass.x < Config.maxGridNum & checknextmass.y < Config.maxGridNum) {
			return true;
		} else {
			Debug.LogWarning ("NextMass is Out of Range");
			return false;
		}
	}

	private bool hasNextMass () {
		if (movemass != null & mathmasses != null) {
			return true;
		} else {
			Debug.LogWarning ("No Applicable Mass");
			return false;
		}
	}

	private void updateClearedData () { //ベストスコアと、クリア済みフラグを立てる処理
		currentdata.saveClearedDatatoDataStrage ();
	}

	public GameObject[, ] getMathmasses () {
		return mathmasses;
	}

	public void testChange () {
		ReplaceMathKind (MathMass.massstate.divide, MathMass.massstate.add);
	}
	public void ReplaceMathKind (MathMass.massstate beforestate, MathMass.massstate afterstate) {
		MassKindChanger kindchanger = new MassKindChanger (mathmasses);
		kindchanger.ChangeMassKind ((int) beforestate, (int) afterstate);
	}

	public class MassKindChanger {
		GameObject[, ] m_mathmasses;

		public void ChangeMassKind (int　 beforeKind, int afterKind) {
			MathMass mathMass;
			for (int j = 0; j < m_mathmasses.GetLength (1); ++j) {
				for (int i = 0; i < m_mathmasses.GetLength (0); ++i) {
					if (mathMass = m_mathmasses[i, j].GetComponent<MathMass> ()) {
						if (mathMass.GetMyKind () == beforeKind) {
							mathMass.ChangeMyKind (afterKind);
							mathMass.ChangeMyMaterial ();
						}
					}
				}
			}
		}
		public MassKindChanger (GameObject[, ] mathmasses) {
			Debug.Log ("calledCOnstracta");
			m_mathmasses = mathmasses;
		}
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