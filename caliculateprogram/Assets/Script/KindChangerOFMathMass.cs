using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class KindChangerOFMathMass : MonoBehaviour { //スペシャルマスを作成するクラス
	GameObject[, ] m_mathmasses; //massmovedealerから受け取るパラメータ
	[SerializeField]
	Button methodDealButton; //所持しているボタンでオブジェクトの実行をつかさどる。
	[SerializeField]
	GameObject buttonPrefab; //所持しているボタンでオブジェクトの実行をつかさどる。
	private void ChangeMassKind (MathMass.massstate beforeKind, MathMass.massstate afterKind) {
		MathMass mathMass;
		for (int j = 0; j < m_mathmasses.GetLength (1); ++j) {
			for (int i = 0; i < m_mathmasses.GetLength (0); ++i) {
				if (mathMass = m_mathmasses[i, j].GetComponent<MathMass> ()) { //movemassがあったところは空白なのでこの条件式が必要
					if (mathMass.GetMyKind () == (int) beforeKind) {
						mathMass.ChangeMyKind ((int) afterKind);
						mathMass.ChangeMyMaterial ();
					}
				}
			}
		}
	}
	public KindChangerOFMathMass (GameObject[, ] mathmasses) {
		Debug.Log ("calledCOnstracta");
		m_mathmasses = mathmasses;
	}
	public void setChangeMassMethod (int massstate) { //自身の所持するボタンオブジェクトにメソッドの登録を行う処理
		methodDealButton.onClick.RemoveAllListeners ();
		if (massstate == (int) MathMass.massstate.SAddtoSub) {
			methodDealButton.onClick.AddListener (() => ChangeMassKind (MathMass.massstate.add, MathMass.massstate.substract));
		} else if (massstate == (int) MathMass.massstate.SdivetoMul) {
			methodDealButton.onClick.AddListener (() => ChangeMassKind (MathMass.massstate.divide, MathMass.massstate.multiplicate));
		} else if (massstate == (int) MathMass.massstate.SIncreasetoDecrease) {
			methodDealButton.onClick.AddListener (() => ChangeMassKind (MathMass.massstate.add, MathMass.massstate.substract));
		} else if (massstate == (int) MathMass.massstate.SMultodive) {
			methodDealButton.onClick.AddListener (() => ChangeMassKind (MathMass.massstate.multiplicate, MathMass.massstate.divide));
		} else if (massstate == (int) MathMass.massstate.SSubtoDiv) {
			methodDealButton.onClick.AddListener (() => ChangeMassKind (MathMass.massstate.substract, MathMass.massstate.divide));
		}
	}
	public void makeKindChangeButton (int buttonkind) { //ボタンオブジェクトを生成し、メソッド種類の名前を設定、メソッドの設定はsetChangeMassMethodで後で実行する。
		Text buttontext;

		MathMass.massstate enmVal = (MathMass.massstate) Enum.ToObject (typeof (MathMass.massstate), buttonkind);
		string strVal = Enum.GetName (typeof (MathMass.massstate), enmVal);
		buttontext = instancedObj.GetComponentInChildren<Text> ();
		buttontext.text = strVal;
	}
}