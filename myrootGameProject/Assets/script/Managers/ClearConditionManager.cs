using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearConditionManager : MonoBehaviour {//クリア条件を管理するクラス
	CSVManager csvmanager;
	MapDataManager datamanager;
	[SerializeField]
	Meditator meditator;
	[SerializeField]
	GameObject scenecanvasprefab;
	[SerializeField]
	GameObject gameoverprefab;


	ClearDataManager cleardatamanager;
	
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
		csvmanager = meditator.getcsvmanager();
		datamanager = meditator.getmapdatamanager();
		conditionaldatas = new clearconditiondata[Config.stageCount];
		cleardatamanager = meditator.getcleardatamanager();
	}

	public void stageStart() {//ステージタイムの更新開始、今のところステージ開始時のみ呼び出し
		conditionaldatas = cleardatamanager.getclearconditondata();
		getTextinstance();//食事条件と、残りタイムの関連テキストを生成し、参照の取得を行う。
		reflectTexttoDisplay();
		recenttime = conditionaldatas[datamanager.getStageNum()].timelimit;
		StartCoroutine(timedecreasePerSecond());
		showstartcanvas();

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
		return 0 < recenttime && conditionaldatas[stagenum].RequiredKillCount <= recenteatcount; //ステージが0から始まっている点に要注意
	}

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
		if (isClear()) { showclearcanvas(); }
		else {
			Instantiate(gameoverprefab, this.transform.position, Quaternion.identity); }
	}
	public void addRecentEatcount() {
		recenteatcount++;
	}
	private void showclearcanvas() {
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position,Quaternion.identity) as GameObject;
		CanvasManager canvas = clearcanvasobject.GetComponent<CanvasManager>();
		canvas.changeTitleText("clear!");
		canvas.changeMessagetext("conglatulation!");
		canvas.changeScorelabel("防衛数");
		canvas.changeScoreText(recenteatcount);
		canvas.setButtonscroll();
		canvas.changebackcolor(Color.yellow);
	}
	private void showstartcanvas() {

		int timelimit = conditionaldatas[datamanager.getStageNum()].timelimit;
		int needeatcount = conditionaldatas[datamanager.getStageNum()].RequiredKillCount;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position, Quaternion.identity) as GameObject;
		CanvasManager canvas = clearcanvasobject.GetComponent<CanvasManager>();
		canvas.changeTitleText("stagestart!");
		canvas.changeMessagetext("");
		canvas.changeScorelabel("目標防衛数");
		canvas.changeScoreText(needeatcount);
		canvas.changeTimelabel("残時間");
		canvas.changeTimeText(timelimit);
		canvas.setButtonscroll();
		canvas.changebackcolor(Color.green);
	}

	//private void debugshowdata() {
	//	for (int j = 0; j < conditionaldatas.Length; ++j) {
	//			Debug.Log(String.Format("ステージは{0}, 必要イート数は{1}, 有時刻は{2}", j, conditionaldatas[j].RequiredKillCount, conditionaldatas[j].timelimit));
	//		}
	//}

}
