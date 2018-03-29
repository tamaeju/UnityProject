using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectsceneButton : MonoBehaviour {//レベル選択画面のボタンクラス
	Text mytext;
	[SerializeField]
	GameObject effectprefab;
	[SerializeField]
	int myStageCount;
	[SerializeField]
	Meditator meditator;
	
	public  void changeThisText(string textname) {
		mytext = this.gameObject.GetComponentInChildren<Text>();
		mytext.text = textname;
	}

	
	public void changeMystageCount(int stagecount) {//ボタンクリックで動作する処理。自分の値をステージレベルの引数として渡す。
		myStageCount = stagecount;
	}
	public void stageCall() {//ステージを呼び出す処理
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		ButtonEventManager buttonmanager = meditator.getbuttonmanager();
		datapathmanager.ChangeMapCSVNum(myStageCount);
		buttonmanager.makeObjectButton();
		parentActiveOff();
	}
	public void getMeditatorRef(Meditator　ameditator) {
		meditator = ameditator;
	}
	public void parentActiveOff() {//キャンバスを不可視にするための処理
		GameObject parent = this.transform.parent.parent.parent.gameObject;
		parent.SetActive(false); 

	}
	public void makeEffectPrefab() {
		Vector3 instancepos;
		instancepos = this.transform.position;
		instancepos.z = -10;
		Instantiate(effectprefab, this.transform.position, Quaternion.identity);
	}
}
