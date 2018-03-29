using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearConditionManager : MonoBehaviour {//クリア条件を管理するクラス

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

	int recenteatcount;//現在食事数
	int recenttime;//現在時間

	Vector2 eatconditionaltextpos = new Vector2(-300, 160);//表示位置
	Text eatconditiontext;
	[SerializeField]
	GameObject eatconditiontexttprefab;

	Vector2 timelimittextpos = new Vector2(-300, 120);//表示位置
	Text timelimitconditiontext;
	[SerializeField]
	GameObject timelimittextprefab;

	clearconditiondata[] conditionaldatas;


	void Start() {//conditionaldatasをとってくるための初期化
		datamanager = meditator.getmapdatamanager();
		conditionaldatas = new clearconditiondata[Config.stageCount];
		cleardatamanager = meditator.getcleardatamanager();


	}

	public void stageStart() {//ステージタイムの更新開始、今のところステージ開始時のみ呼び出し
		conditionaldatas = cleardatamanager.getclearconditondata();
		getTextinstance();//食事条件と、残りタイムの関連テキストを生成し、参照の取得を行う。
		reflectTexttoDisplay();
		recenttime = conditionaldatas[datamanager.getStageNum()].timelimit;
		canvasMaker = gameObject.AddComponent<canvasmaker>();
		canvasMaker.getsececanvas(instancecanvas);
		canvasMaker.showstartcanvas(conditionaldatas[datamanager.getStageNum()]);
		StartCoroutine(timedecreasePerSecond());
		

	}

	public void UpdateALLcleardata() {
		conditionaldatas = cleardatamanager.getclearconditondata();
	}



	public void reflectTexttoDisplay() {//コンディションデータを画面内のテキストに反映する,表示を変えたいオブジェクトの生成と参照もしておく
		int stagenum = datamanager.getStageNum();
		eatconditiontext.text = recenteatcount.ToString();
		timelimitconditiontext.text = recenttime.ToString();
	}
	public bool isClear() {//クリアしているかをbooleanで返すメソッドを持つ、ゴール
		int stagenum = datamanager.getStageNum();
		return conditionaldatas[stagenum].RequiredDeffenceCount >= recenteatcount; //ステージが0から始まっている点に要注意
	}//タイムリミットを過ぎたらゲームオーバー

	

	public void getTextinstance(){
		if (eatconditiontext == null && timelimitconditiontext == null) {
			eatconditiontext = meditator.getUImanager().MakeGetUIobject(eatconditiontexttprefab, eatconditionaltextpos).GetComponent<Text>();
			timelimitconditiontext = meditator.getUImanager().MakeGetUIobject(timelimittextprefab, timelimittextpos).GetComponent<Text>();
		}
	}

	public void decreaseEatCount() {
		if (recenteatcount > 0) {
			recenteatcount--;
		}
	}
	public void decreaseTime() {
		if (recenttime > 0) {
			recenttime--;
		}
	}

	//1秒に1回タイムリミットをディクリーズする
	private IEnumerator timedecreasePerSecond() {
		int timelimit = conditionaldatas[datamanager.getStageNum()].timelimit;
		for (int i = 0; i < timelimit; i++) {
			decreaseTime();
			reflectTexttoDisplay();
			yield return new WaitForSeconds(1.0f);
			if (i == timelimit - 1) {
				reflectTexttoDisplay();
				gameOverEvent();
				yield break;//ゲームオーバー処理
			}
		}
	}

	private void gameOverEvent() {
		if (isClear()) { canvasMaker.showclearcanvas(recenteatcount); }
		else {
			Instantiate(gameoverprefab, this.transform.position, Quaternion.identity); }
	}
	public void addRecentEatcount() {
		recenteatcount++;
		reflectTexttoDisplay(); //コンディションデータを画面内のテキストに反映する,表示を変えたいオブジェクトの生成と参照もしておく

	}

}
