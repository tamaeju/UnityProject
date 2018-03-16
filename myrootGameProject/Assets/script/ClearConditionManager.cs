using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClearConditionManager : MonoBehaviour {
	CSVManager csvmanager;
	DataManager datamanager;
	[SerializeField]
	Meditator meditator;


	int m_stageneedeatcount;
	int m_stagetimelimit;//制限時間自体
	int recenttime;//現在時間

	Vector2 eatconditionaltextpos = new Vector2(200, 200);
	Text eatconditiontext;
	[SerializeField]
	GameObject eatconditiontexttprefab;

	Vector2 timelimittextpos = new Vector2(300, 300);
	Text timelimitconditiontext;
	[SerializeField]
	GameObject timelimittextprefab;

	clearconditiondata[] conditionaldatas;


	void Start() {
		csvmanager = meditator.getcsvmanager();
		datamanager = meditator.getdatamanager();
	   conditionaldatas = new clearconditiondata[Config.stageCount];

		//conditionaldatasをcsvから読みこむ必要あり、
		getClearcondition();
		getTextinstance();
		reflectDisplay();
		recenttime = m_stagetimelimit;
		StartCoroutine(timedecreasePerSecond());
	}
	
	
	public void getClearcondition() {//csvmanagerを介して、クリア条件をとってくる。
		int stagenum = datamanager.getStageNum();
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		DataChangerFromJaG jagchanger = meditator.getjagchanger();
		int[][] jagcleardata = csvmanager.getJagDataElement(datapathmanager.getcsvdatapath(2));//クリアコンディショナルデータをcsvからロード
		UpdateALLcleardata(jagchanger.parsejagtodobleClearconditiondatas(jagcleardata));
		m_stageneedeatcount = conditionaldatas[stagenum].RequiredKillCount;
		m_stagetimelimit = conditionaldatas[stagenum].timelimit;
	}

	public void UpdateALLcleardata(clearconditiondata[] clearconditions) {
		conditionaldatas = clearconditions;
	}



	public void reflectDisplay() {//とってきたクリア条件を画面内のテキストに反映する,表示を変えたいオブジェクトの生成と参照もしておく
		int stagenum = datamanager.getStageNum();
		eatconditiontext.text = conditionaldatas[stagenum].RequiredKillCount.ToString();
		timelimitconditiontext.text = conditionaldatas[stagenum].timelimit.ToString();

	}
	public bool isClear() {//クリアしているかをbooleanで返すメソッドを持つ、ゴール
		int stagenum = datamanager.getStageNum();
		return 0 < recenttime && conditionaldatas[stagenum].RequiredKillCount <= m_stageneedeatcount; //ステージが0から始まっている点に要注意
	}

	public void getTextinstance(){
		eatconditiontext = meditator.getmakemanager().MakeGetUIobject(eatconditiontexttprefab, eatconditionaltextpos).GetComponent<Text>();
		timelimitconditiontext = meditator.getmakemanager().MakeGetUIobject(timelimittextprefab, timelimittextpos).GetComponent<Text>();
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
			yield return new WaitForSeconds(1.0f);
			if (i == m_stagetimelimit) {
				yield break;//ゲームオーバー処理
			}
		}
	}
}
