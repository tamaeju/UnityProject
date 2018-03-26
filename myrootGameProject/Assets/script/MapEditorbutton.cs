using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MapEditorbutton : MonoBehaviour {//レベルデザインデータ作成用のボタンのスクリプト
	public Vector3 pos;
	public int stateNum;
	[SerializeField]
	private Text mytext;
	Color blackcolor = new Color(0,0,0,1);

	public void addState(){
		if (stateNum <  Config.blockkindlength) {
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
	public void changemychar() {
		//自身のステイトのenumをとってきて、その最初の文字を入れ替える。
		mytext = GetComponentInChildren<Text>();
		var state =  (blockkind)Enum.ToObject(typeof(blockkind), stateNum);
		var stringname = Enum.GetName(typeof(blockkind), state);
		mytext.text = stringname[0].ToString(); 
	}

	public Vector2 returnThisPos() {
		return this.pos;
	}

	public int returnThisState() {
		return this.stateNum;
	}
	public void changeButtonColour() {
		if(stateNum == (int)blockkind.nothing)
			gameObject.GetComponent<Image>().color = Color.red;
		if (stateNum == (int)blockkind.block)
			gameObject.GetComponent<Image>().color = Color.blue;
		if (stateNum == (int)blockkind.player)
			gameObject.GetComponent<Image>().color = Color.green;
		if (stateNum == (int)blockkind.target)
			gameObject.GetComponent<Image>().color = Color.white;
		if (stateNum == (int)blockkind.target2)
			gameObject.GetComponent<Image>().color = Color.white;
		if (stateNum == (int)blockkind.goal)
			gameObject.GetComponent<Image>().color = Color.yellow;
		if (stateNum == (int)blockkind.item1|| stateNum == (int)blockkind.item2 || stateNum == (int)blockkind.item3 || stateNum == (int)blockkind.item4 || stateNum == (int)blockkind.item4 || stateNum == (int)blockkind.item5)
		{//ブラックカラーのアルファの値をstatenumで割った値とする。
			Color newcolor = new Color(0,0,0,blackcolor.a/ stateNum);
			gameObject.GetComponent<Image>().color = newcolor;
		}
		if (stateNum == (int)blockkind.disappearblock)
			gameObject.GetComponent<Image>().color = Color.black;
		if (stateNum == (int)blockkind.switchdisappear)
			gameObject.GetComponent<Image>().color = Color.black;

	}
	//ブロックなら青系、プレイヤーなら緑系、ゴールは黄色系,アイテム系は黒系で透明度をあげていく感じか。
	public enum blockkind {
		nothing,
		block,
		player,
		target,
		target2,
		goal,
		item1,
		item2,
		item3,
		item4,
		item5,
		disappearblock,
		switchdisappear

	}
}
