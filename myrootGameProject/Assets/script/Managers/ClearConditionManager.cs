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
	DataManager datamanager;
	[SerializeField]
	Meditator meditator;

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
		datamanager = meditator.getdatamanager();
		conditionaldatas = new clearconditiondata[Config.stageCount];
	}

	public void stageStart() {//ステージタイムの更新開始
		getClearcondition();
		getTextinstance();
		reflectDisplay();
		recenttime = m_stagetimelimit;
		StartCoroutine(timedecreasePerSecond());
	}

	public void getClearcondition() {//csvmanagerを介してクリア条件をとってくる。
		int stagenum = datamanager.getStageNum();
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		DataChangerFromJaG jagchanger = meditator.getjagchanger();
		int[][] jagcleardata = csvmanager.getJagDataElement(datapathmanager.getconditiondatapath());
		UpdateALLcleardata(jagchanger.parsejagtodobleClearconditiondatas(jagcleardata));
		m_stageneedeatcount = conditionaldatas[stagenum].RequiredKillCount;
		m_stagetimelimit = conditionaldatas[stagenum].timelimit;
	}

	public void UpdateALLcleardata(clearconditiondata[] clearconditions) {
		conditionaldatas = clearconditions;
	}



	public void reflectDisplay() {//コンディションデータを画面内のテキストに反映する,表示を変えたいオブジェクトの生成と参照もしておく
		int stagenum = datamanager.getStageNum();
		eatconditiontext.text = recenteatcount.ToString();
		timelimitconditiontext.text = recenttime.ToString();
	}
	public bool isClear() {//クリアしているかをbooleanで返すメソッドを持つ、ゴール
		int stagenum = datamanager.getStageNum();
		return 0 < recenttime && conditionaldatas[stagenum].RequiredKillCount <= recenteatcount; //ステージが0から始まっている点に要注意
	}

	public void getTextinstance(){
		if (eatconditiontext == null && timelimitconditiontext == null) {//多分オッケー
			eatconditiontext = meditator.getmakemanager().MakeGetUIobject(eatconditiontexttprefab, eatconditionaltextpos).GetComponent<Text>();
			timelimitconditiontext = meditator.getmakemanager().MakeGetUIobject(timelimittextprefab, timelimittextpos).GetComponent<Text>();
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
			reflectDisplay();
			yield return new WaitForSeconds(1.0f);
			if (i == m_stagetimelimit-1) {
				reflectDisplay();
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

}
