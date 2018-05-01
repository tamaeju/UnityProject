using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorbutton : MonoBehaviour { //レベルデザインデータ作成用のボタンのスクリプト
	public Vector3 pos;
	public int stateNum;
	[SerializeField]
	private Text mytext;
	Color blackcolor = new Color (0, 0, 0, 1);

	public void addState () {
		if (stateNum + 1 < Enum.GetNames (typeof (blockkind)).Length) {
			stateNum++;
			changemychar ();
		} else { stateNum = 0; }
		changemychar ();
		changeButtonColour ();
	}
	void Start () {
		changeButtonColour ();
		changemychar ();
	}
	public void changeState (int astateNum) {
		if (stateNum < Enum.GetNames (typeof (blockkind)).Length) {
			stateNum = astateNum++;
			changemychar ();
		} else { stateNum = 0; }
		changemychar ();
		changeButtonColour ();

	}
	public void changemychar () { //自身のステイトのenumをとってきて、その最初の文字を入れ替える。
		mytext = GetComponentInChildren<Text> ();
		var state = (blockkind) Enum.ToObject (typeof (blockkind), stateNum);
		var stringname = Enum.GetName (typeof (blockkind), state);
		mytext.text = stringname[0].ToString () + stringname[1].ToString () + stringname[2].ToString ();
	}

	public Vector2 returnThisPos () {
		return this.pos;
	}

	public int returnThisState () {
		return this.stateNum;
	}
	public void changeButtonColour () {
		if (stateNum == (int) blockkind.nothing)
			gameObject.GetComponent<Image> ().color = Color.red;
		if (stateNum == (int) blockkind.block)
			gameObject.GetComponent<Image> ().color = Color.blue;
		if (stateNum == (int) blockkind.player)
			gameObject.GetComponent<Image> ().color = Color.green;
		if (stateNum == (int) blockkind.curetarget)
			gameObject.GetComponent<Image> ().color = Color.white;
		if (stateNum == (int) blockkind.target2)
			gameObject.GetComponent<Image> ().color = Color.white;
		if (stateNum == (int) blockkind.goal)
			gameObject.GetComponent<Image> ().color = Color.yellow;
		if (stateNum == (int) blockkind.StopItem || stateNum == (int) blockkind.SlowItem || stateNum == (int) blockkind.Bomb || stateNum == (int) blockkind.FastItem || stateNum == (int) blockkind.FastItem) { //ブラックカラーのアルファの値をstatenumで割った値とする。
			Color newcolor = new Color (0, 0, 0, blackcolor.a / stateNum);
			gameObject.GetComponent<Image> ().color = newcolor;
		}
		// if (stateNum == (int) blockkind.disappearblock)
		// 	gameObject.GetComponent<Image> ().color = Color.black;
		// if (stateNum == (int) blockkind.switchdisappear)
		// 	gameObject.GetComponent<Image> ().color = Color.black;

	}

	public enum blockkind { //PrefabContainerのInstanceObjectsと一致しているので、名前を都度変える必要あり（問題）
 nothing,
 block,
 player,
 curetarget,
 target2,
 goal,
 StopItem,
 SlowItem,
 Bomb,
 FastItem
	}
}