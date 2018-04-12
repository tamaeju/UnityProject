using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FieldObjectEditUI : MonoBehaviour {//レベルデザインデータ作成用のボタンのスクリプト
	public Vector3 pos;
	public int stateNum;
	[SerializeField]
	private Text mytext;
	[SerializeField]
	Button btn;

	void Start() {
		changeButtonColor();
		changemychar();
		SetButtonMethod();
	}

	void SetButtonMethod() {
		btn.onClick.AddListener(() => addState());
		btn.onClick.AddListener(() => changeButtonColor());
		btn.onClick.AddListener(() => changemychar());
	}

	public void addState(){
		if (stateNum+1 < Enum.GetNames(typeof(MathMass.massstate)).Length+ Enum.GetNames(typeof(DebugUIkind)).Length) {
			stateNum++;
		}
		else { stateNum = 0; }
		changemychar();
		changeButtonColor();
	}



	public void changeState(int astateNum) {
		if (stateNum < Enum.GetNames(typeof(MathMass.massstate)).Length + Enum.GetNames(typeof(DebugUIkind)).Length) {
			stateNum = astateNum++;
		}
		else { stateNum = 0; }
		changeButtonColor();
		changemychar();
	}

	public void changemychar() {//自身のステイトのenumをとってきて、その最初の文字を入れ替える。
		mytext = GetComponentInChildren<Text>();
		int mathMassKindLength = Enum.GetNames(typeof(MathMass.massstate)).Length;

		if (stateNum < mathMassKindLength) {//ムーブオブジェクトのマスはMathMass.massstateの範囲内にないので、stringnameがnullになる、その対応のnullチェック
			var state = (MathMass.massstate)Enum.ToObject(typeof(MathMass.massstate), stateNum);
			var stringname = Enum.GetName(typeof(MathMass.massstate), state);
			mytext.text = stringname[0].ToString() + stringname[1].ToString() + stringname[2].ToString();
		}

		if (stateNum == mathMassKindLength + (int)DebugUIkind.movingobject) {
			var state = (DebugUIkind)Enum.ToObject(typeof(DebugUIkind), stateNum- mathMassKindLength);
			var stringname = Enum.GetName(typeof(DebugUIkind), state);
			mytext.text = stringname[0].ToString() + stringname[1].ToString() + stringname[2].ToString();
		}

		if (stateNum == mathMassKindLength + (int)DebugUIkind.goal) {
			var state = (DebugUIkind)Enum.ToObject(typeof(DebugUIkind), stateNum- mathMassKindLength);
			var stringname = Enum.GetName(typeof(DebugUIkind), state);
			mytext.text = stringname[0].ToString() + stringname[1].ToString() + stringname[2].ToString();
		}

	}

	public Vector2 returnThisPos() {
		return this.pos;
	}

	public int returnThisState() {
		return this.stateNum;
	}

	public void changeButtonColor() {
		if(stateNum == (int)MathMass.massstate.add )
			gameObject.GetComponent<Image>().color = Color.cyan;
		else if (stateNum == (int)MathMass.massstate.substract)
			gameObject.GetComponent<Image>().color = Color.magenta;
		else if (stateNum == (int)MathMass.massstate.multiplicate)
			gameObject.GetComponent<Image>().color = Color.green;
		else if (stateNum == (int)MathMass.massstate.divide)
			gameObject.GetComponent<Image>().color = Color.yellow;
		else if (stateNum == (int)MathMass.massstate.square)
			gameObject.GetComponent<Image>().color = Color.blue;
		else if (stateNum == (int)MathMass.massstate.root)
			gameObject.GetComponent<Image>().color = Color.red;
		else if (stateNum == Enum.GetNames(typeof(MathMass.massstate)).Length+(int)DebugUIkind.movingobject)
			gameObject.GetComponent<Image>().color = Color.white;
		else if (stateNum == Enum.GetNames(typeof(MathMass.massstate)).Length + (int)DebugUIkind.goal)
			gameObject.GetComponent<Image>().color = Color.black;
	}

	//ブロックなら青系、プレイヤーなら緑系、ゴールは黄色系,アイテム系は黒系で透明度をあげていく感じか。
	public enum DebugUIkind {
		movingobject,
		goal
	}



}
