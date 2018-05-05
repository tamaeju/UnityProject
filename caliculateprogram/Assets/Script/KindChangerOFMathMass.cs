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
	GameObject buttonObject; //所持しているボタンでオブジェクトの実行をつかさどる。

	[SerializeField]
	Text buttontext;
	public void Start () {
		buttonObject.SetActive (false); //不可視にするためだけの処理
	}
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
	public void setMathMasses (GameObject[, ] mathmasses) {
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
		Color oldcolor = buttonObject.GetComponent<Image> ().color;
		buttonObject.GetComponent<Image> ().color = new Color (oldcolor.r, oldcolor.g, oldcolor.b, 1f);
	}
	public void makeKindChangeButton (int buttonkind) { //ボタンオブジェクトを生成し、メソッド種類の名前を設定、メソッドの設定はsetChangeMassMethodで後で実行する。
		buttonObject.SetActive (true);
		Color oldcolor = buttonObject.GetComponent<Image> ().color;
		buttonObject.GetComponent<Image> ().color = new Color (oldcolor.r, oldcolor.g, oldcolor.b, 0.3f);
		MathMass.massstate enmVal = (MathMass.massstate) Enum.ToObject (typeof (MathMass.massstate), buttonkind); //ボタン種類に応じたmassstateを取得。

		string strVal = GetButtonName (enmVal); //Enum.GetName (typeof (MathMass.massstate), enmVal)//適応するオブジェクトを表示
		buttontext.text = strVal;
	}

	private string GetButtonName (MathMass.massstate kind) {
		if (kind == MathMass.massstate.SAddtoSub) {
			return "+を−にチェンジ";
		} else if (kind == MathMass.massstate.SdivetoMul) {
			return "÷を×にチェンジ";
		} else if (kind == MathMass.massstate.SMultodive) {
			return "×を÷にチェンジ";
		} else if (kind == MathMass.massstate.SSubtoDiv) {
			return "-を÷にチェンジ";
		} else {
			return "異常値がセットされています";
		}

	}
	//この後必要となるのは、表示されたオブジェクト
}