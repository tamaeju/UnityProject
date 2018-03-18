using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectsceneButton : MonoBehaviour {
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
	public void makeEffectPrefab() {
		Vector3 instancepos;
		instancepos = this.transform.position;
		instancepos.z = -10;
		Instantiate(effectprefab,this.transform.position,Quaternion.identity);
	}
	//ボタンクリックで発動するメソッド。datapathmanagerのパスを変更するために、自分の値を引数として渡す。
	public void changeMystageCount(int stagecount) {
		myStageCount = stagecount;
	}
	public void stageCall() {
		getRefOfMeditator();
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		ButtonEventManager buttonmanager = meditator.getbuttonmanager();
		datapathmanager.ChangeMapCSVNum(myStageCount);
		buttonmanager.makeObjectfromSelectScene(myStageCount);
		parentActiveOff();
	}
	public void getRefOfMeditator() {
		GameObject parent = this.transform.parent.gameObject;
		meditator = parent.GetComponent<LevelSelectCanvasManager>().sendMeditator();

	}
	public void parentActiveOff() {
		GameObject parent = this.transform.parent.parent.parent.gameObject;
		parent.SetActive(false); 

	}
}
