using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SelectsceneButton : MonoBehaviour {//レベル選択画面のボタンクラス
	Text mytext;
	[SerializeField]
	GameObject effectprefab;
	[SerializeField]
	int myStageCount;
	[SerializeField]
	Button btn;




	private Subject<int> subject = new Subject<int>();

	private void Start() {

		btn.onClick.AddListener(delegate { subject.OnNext(myStageCount); });
	}

	public IObservable<int> OnClickedStageButton {
		get { return subject; }
	}



	public  void changeThisText(string textname) {
		mytext = this.gameObject.GetComponentInChildren<Text>();
		mytext.text = textname;
	}
	//ボタンをクリックした時に自身のmyStageCountを引数としてイベントを発行する。
	//イベント登録については生成時にゲームクリエイトシーンに登録してもらう。

	public void changeMystageCount(int stagecount) {//ボタンクリックで動作する処理。自分の値をステージレベルの引数として渡す。
		myStageCount = stagecount;
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
	//最高スコアを所持しているクラスを作りたい。
	//ステージ開始時とステージ終了時にスコアが表示されるよう修正を行いたい
}
