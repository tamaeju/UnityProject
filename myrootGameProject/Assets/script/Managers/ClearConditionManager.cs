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

	int m_stageneedeatcount;//目標食事数
	int recenteatcount;//現在食事数
	int m_stagetimelimit;//目標制限時間
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
	}

	public void stageStart() {//ステージタイムの更新開始、今のところステージ開始時のみ呼び出し
		getClearcondition();//クリアコンディション
		getTextinstance();//食事条件と、残りタイムの関連テキストを生成し、参照の取得を行う。
		reflectTexttoDisplay();
		recenttime = m_stagetimelimit;
		StartCoroutine(timedecreasePerSecond());
	}

	public void getClearcondition() {//csvmanagerを介してクリア条件をとってくる。
		m_stageneedeatcount = conditionaldatas[datamanager.getStageNum()].RequiredKillCount;
		m_stagetimelimit = conditionaldatas[datamanager.getStageNum()].timelimit;
	}



	public void UpdateALLcleardata(clearconditiondata[] clearconditions) {
		conditionaldatas = clearconditions;
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
		if (m_stageneedeatcount > 0) {
			m_stageneedeatcount--;
		}
	}
	public void decreaseTime() {
		if (recenttime > 0) {
			recenttime--;
		}
	}

	//1秒に1回タイムリミットをディクリーズする
	private IEnumerator timedecreasePerSecond() {
		for (int i = 0; i < m_stagetimelimit; i++) {
			decreaseTime();
			reflectTexttoDisplay();
			yield return new WaitForSeconds(1.0f);
			if (i == m_stagetimelimit-1) {
				reflectTexttoDisplay();
				gameOverEvent();
				yield break;//ゲームオーバー処理
			}
		}
	}

	private void gameOverEvent() {
		Instantiate(gameoverprefab, this.transform.position, Quaternion.identity);
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
		canvas.changeScoreText(0);
		canvas.setButtonscroll();
		canvas.changebackcolor(Color.yellow);
	}
	private void showstartcanvas() {
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position, Quaternion.identity) as GameObject;
		CanvasManager canvas = clearcanvasobject.GetComponent<CanvasManager>();
		canvas.changeTitleText("stagestart!");
		canvas.changeMessagetext("");
		canvas.changeScorelabel("目標防衛数");
		canvas.changeScoreText(0);
		canvas.changeTimelabel("残時間");
		canvas.changeTimeText(0);
		canvas.setButtonscroll();
		canvas.changebackcolor(Color.green);
	}

}
