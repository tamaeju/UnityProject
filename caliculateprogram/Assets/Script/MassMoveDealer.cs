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
	[SerializeField]
	KindChangerOFMathMass kindChanger;
	[SerializeField]
	SoundPlayer semaker;
	[SerializeField]
	PanelView effectcreator; //パネルビューが機能を内包しているため参照しているが、後ほどパネルビュー内部の実装を単一に分割する必要がある。
	Subject<int> Clearedsubject = new Subject<int> ();
	public IObservable<int> OnCleared {
		get { return Clearedsubject; }
	}

	Subject<int> GameOveredsubject = new Subject<int> ();
	public IObservable<int> OnGameOvered {
		get { return GameOveredsubject; }
	}

	public void LoadFieldObject () {
		GameObject obj = fieldobjectmaker.GetMovingMass ();
		movemass = obj.GetComponent<MovingMass> ();
		mathmasses = fieldobjectmaker.GetMathMasses ();
	}
	private void BaseMoveMethod (Vector2 directionpos) {
		Vector2 checkPos = movemass.GetMyPos () + directionpos;
		if (!isInRange (checkPos)) { //レンジ内でないなら終了
			return;
		}
		if (currentdata.GetMoveCount () > currentdata.GetTargetMoveCount ()) { GameOveredsubject.OnNext (1); } //gameoverならゲームオーバー処理を行う。
		MathMass checkMathMass = mathmasses[(int) checkPos.x, (int) checkPos.y].GetComponent<MathMass> ();
		if (checkMathMass.isGoal ()) { //次のマスがゴールなら判定を行い、判定が可ならクリア処理を走らせる
			if (currentdata.canClear ()) {
				updateClearedData ();
				Clearedsubject.OnNext (1);
			} else if (!currentdata.canClear ()) {
				Debug.Log ("can't goal yet!");
				effectcreator.createEffectOfnotMatchGoalValue ();
			}
		} else if (!(checkMathMass.isGoThrough ())) { //次のマスが通過済みでないなら下記処理を行う。
			semaker.playSEs (checkMathMass.GetMyKind ()); //種類に対応した効果音を鳴らす
			RenewMoverNum (mathmasses[(int) checkPos.x, (int) checkPos.y].GetComponent<MathMass> ());
			movemass.SetMyPos ((int) checkPos.x, (int) checkPos.y);
			movemass.AddMyCount ();
			currentdata.SetCurrentSum (movemass.GetMyNumber ());
			currentdata.SetMoveCount (movemass.GetMyCount ());
			//もしmathmasses[(int) checkPos.x, (int) checkPos.y]のマス種類がスペシャルマスの領域であれば、kindchangerに特殊メソッドの準備をONさせる。
			if (checkMathMass.GetMyKind () > (int) MathMass.massstate.goal) {
				semaker.playSEs (4);
				kindChanger.setChangeMassMethod (checkMathMass.GetMyKind ());
				effectcreator.createSpecialMassMessage ();
			}
			if (currentdata.GettargetSum () == currentdata.GetCurrentSum ()) {
				effectcreator.createEffectOfCanGoal ();
			}
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
		if (0 <= checknextmass.x & 0 <= checknextmass.y & checknextmass.x < Config.maxGridNum & checknextmass.y < Config.maxGridNum) {
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
}