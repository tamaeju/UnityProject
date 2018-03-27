using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MapEditorbutton : MonoBehaviour {//レベルデザインデータ作成用のボタンのスクリプト
	public Vector3 pos;
	public int stateNum;//+1した数字を返す。
	[SerializeField]
	private Text mytext;
	Color blackcolor = new Color(0,0,0,1);

	public void addState(){
		if (stateNum <  Config.blockkindlength-1) {
			stateNum++;
			changemychar();
		}
		else { stateNum = 0; }
		changemychar();
		changeButtonColour();
	}
	void Start() {
		changeButtonColour();
		changemychar();
	}
	public void changeState(int astateNum) {
		if (stateNum < Config.blockkindlength) {
			stateNum = astateNum++;
			changemychar();
		}
		else { stateNum = 0; }
		changeButtonColour();
		changemychar();
	}
	public void changemychar() {//自身のステイトのenumをとってきて、その最初の文字を入れ替える。
		mytext = GetComponentInChildren<Text>();
		var state =  (blockkind)Enum.ToObject(typeof(blockkind), stateNum);
		var stringname = Enum.GetName(typeof(blockkind), state);
		mytext.text = stringname[1].ToString(); 
	}

	public Vector2 returnThisPos() {
		return this.pos;
	}

	public int returnThisState() {
		return this.stateNum+1;//0が存在しないため基本的に+1を返す。
	}
	public void changeButtonColour() {
		if(stateNum == (int)blockkind._1block)
			gameObject.GetComponent<Image>().color = Color.red;
		if (stateNum == (int)blockkind._2block)
			gameObject.GetComponent<Image>().color = Color.blue;
		if (stateNum == (int)blockkind._3block)
			gameObject.GetComponent<Image>().color = Color.green;
		if (stateNum == (int)blockkind._4block)
			gameObject.GetComponent<Image>().color = Color.white;
		if (stateNum == (int)blockkind._5block)
			gameObject.GetComponent<Image>().color = Color.white;
		if (stateNum == (int)blockkind._6block)
			gameObject.GetComponent<Image>().color = Color.yellow;
		if (stateNum == (int)blockkind._7block || stateNum == (int)blockkind._8block || stateNum == (int)blockkind._9block)
		{//ブラックカラーのアルファの値をstatenumで割った値とする。
			Color newcolor = new Color(0,0,0,blackcolor.a/ stateNum);
			gameObject.GetComponent<Image>().color = newcolor;
		}

	}
	//ブロックなら青系、プレイヤーなら緑系、ゴールは黄色系,アイテム系は黒系で透明度をあげていく感じか。
	public enum blockkind {
		_1block,
		_2block,
		_3block,
		_4block,
		_5block,
		_6block,
		_7block,
		_8block,
		_9block,
	}
}
