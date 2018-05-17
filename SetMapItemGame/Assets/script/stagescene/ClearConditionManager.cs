using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClearConditionManager : MonoBehaviour { //クリア条件を管理するクラス

	MapDataManager datamanager;
	ClearDataManager cleardatamanager;
	[SerializeField]
	Meditator meditator;

	[SerializeField]
	GameObject gameoverprefab;

	[SerializeField]
	GameObject instancecanvas;
	canvasmaker canvasMaker;

	//キャンバスメイカーを初期化時に宣言し取得しておく。

	[Watch] int recenteatcount; //現在食事数
	[Watch] int recenttime; //現在時間

	Vector2 eatconditionaltextpos = new Vector2 (-300, 160); //表示位置
	Text eatconditiontext;
	[SerializeField]
	GameObject eatconditiontexttprefab;

	Vector2 timelimittextpos = new Vector2 (-300, 120); //表示位置
	Text timelimitconditiontext;
	[SerializeField]
	GameObject timelimittextprefab;

	clearconditiondata[] conditionaldatas;

	void Start () { //cleardatamanagerとconditionaldatasをとってくるための初期化
		datamanager = meditator.getmapdatamanager ();
		cleardatamanager = meditator.getcleardatamanager ();
	}

	public void clearConditionSet () { //クリア条件の更新、クリア条件を表示するテキスト表示、ステージタイムの更新開始、今のところステージ開始時のみ呼び出し
		conditionaldatas = cleardatamanager.getclearconditondata ();
		getTextinstance ();
		recenttime = conditionaldatas[datamanager.getStageNum ()].timelimit;
		reflectTexttoDisplay ();
	}

	public void reflectTexttoDisplay () { //コンディションデータを画面内のテキストに反映する,表示を変えたいオブジェクトの生成と参照もしておく
		eatconditiontext.text = recenteatcount.ToString ();
		timelimitconditiontext.text = recenttime.ToString ();
	}
	public bool isClear () { //クリアしているかをbooleanで返すメソッド
		int stagenum = datamanager.getStageNum ();
		return conditionaldatas[stagenum].RequiredDeffenceCount >= recenteatcount; //ステージが0から始まっている点に要注意
	}

	public void getTextinstance () { //UImakerにテキストを作成してもらい参照を受け取るメソッド
 if (eatconditiontext == null && timelimitconditiontext == null) {
 MapEditorUIManager UImaker = meditator.getUImanager ();
 eatconditiontext = UImaker.MakeGetUIobject (eatconditiontexttprefab, eatconditionaltextpos).GetComponent<Text> ();
 timelimitconditiontext = UImaker.MakeGetUIobject (timelimittextprefab, timelimittextpos).GetComponent<Text> ();
		}
	}

	public void decreaseEatCount () {
		if (recenteatcount > 0) {
			recenteatcount--;
		}
	}
	public void decreaseTime () {
		if (recenttime > 0) {
			recenttime--;
		}
	}

	//1秒に1回タイムリミットをディクリーズする
	public IEnumerator timedecreasePerSecond () {
		int timelimit = conditionaldatas[datamanager.getStageNum ()].timelimit;
		for (int i = 0; i < timelimit; i++) {
			decreaseTime ();
			reflectTexttoDisplay ();
			yield return new WaitForSeconds (1.0f);
			if (i == timelimit - 1) {
				reflectTexttoDisplay ();
				gameOverEvent ();
				yield break; //ゲームオーバー処理
			}
		}
	}

	private void gameOverEvent () {
		if (isClear ()) { canvasMaker.showclearcanvas (recenteatcount); } else {
			canvasMaker.showGameovercanvas (recenteatcount);
		}
	}

	public void addRecentEatcount () {
		recenteatcount++;
		reflectTexttoDisplay (); //コンディションデータを画面内のテキストに反映する,表示を変えたいオブジェクトの生成と参照もしておく
	}
	public void decleaseRecentEatcount () {
		recenteatcount--;
		reflectTexttoDisplay (); //コンディションデータを画面内のテキストに反映する,表示を変えたいオブジェクトの生成と参照もしておく
	}

	public void setcanvasMaker (canvasmaker maker) {
		canvasMaker = maker;
	}

}