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
		btn.onClick.AddListener(() => addState());
		btn.onClick.AddListener(()=> changeButtonColor());
		btn.onClick.AddListener(()=> changemychar());
	}

	public void addState(){
		if (stateNum+1 < Enum.GetNames(typeof(MathMass.massstate)).Length) {
			stateNum++;
		}
		else { stateNum = 0; }
		changemychar();
		changeButtonColor();
	}



	public void changeState(int astateNum) {
		if (stateNum+1 < Enum.GetNames(typeof(MathMass.massstate)).Length) {
			stateNum = astateNum++;
		}
		else { stateNum = 0; }
		changeButtonColor();
		changemychar();
	}

	public void changemychar() {//自身のステイトのenumをとってきて、その最初の文字を入れ替える。
		mytext = GetComponentInChildren<Text>();
		var state =  (MathMass.massstate)Enum.ToObject(typeof(MathMass.massstate), stateNum);
		var stringname = Enum.GetName(typeof(MathMass.massstate), state);
		if (stringname != null) {//ムーブオブジェクトのマスはMathMass.massstateの範囲内にないので、stringnameがnullになる、その対応のnullチェック
			mytext.text = stringname[0].ToString() + stringname[1].ToString() + stringname[2].ToString();
		}
		else {
			mytext.text = "！！";
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
			gameObject.GetComponent<Image>().color = Color.red;
		if (stateNum == (int)MathMass.massstate.divide)
			gameObject.GetComponent<Image>().color = Color.blue;
		if (stateNum == (int)MathMass.massstate.multiplicate)
			gameObject.GetComponent<Image>().color = Color.magenta;
		if (stateNum == (int)MathMass.massstate.root)
			gameObject.GetComponent<Image>().color = Color.white;
		if (stateNum == (int)MathMass.massstate.square)
			gameObject.GetComponent<Image>().color = Color.green;
		if (stateNum == (int)MathMass.massstate.substract)
			gameObject.GetComponent<Image>().color = Color.yellow;
	}

	//ブロックなら青系、プレイヤーなら緑系、ゴールは黄色系,アイテム系は黒系で透明度をあげていく感じか。

}
