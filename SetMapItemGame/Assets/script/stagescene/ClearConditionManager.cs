using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
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
	[Watch] ReactiveProperty<int> recenteatcount; //現在食事数
	[Watch] ReactiveProperty<int> recenttime; //現在時間
	//UIメイカーに命令してUIを作成するよう指定する。
	Text eatconditiontext;
	[SerializeField]
	GameObject eatconditiontexttprefab;

	Text timelimitconditiontext;
	[SerializeField]
	GameObject timelimittextprefab;

	clearconditiondata[] conditionaldatas;
	[SerializeField]
	StageUIMaker stageUImaker;

	void Start () { //cleardatamanagerとconditionaldatasをとってくるための初期化
		datamanager = meditator.getmapdatamanager ();
		cleardatamanager = meditator.getcleardatamanager ();
	}
	public void makeClearConditionDisplay () {
		stageUImaker.makeStageConditionUI ("被ダメージ数", recenteatcount, 0);
		stageUImaker.makeStageConditionUI ("残り防衛時間", recenttime, 1);
	}
	public void clearConditionSet () { //クリア条件の更新、クリア条件を表示するテキスト表示、ステージタイムの更新開始、今のところステージ開始時のみ呼び出し
		conditionaldatas = cleardatamanager.getclearconditondata ();
		recenttime = new ReactiveProperty<int> (conditionaldatas[datamanager.getStageNum ()].timelimit);
		recenteatcount = new ReactiveProperty<int> (0);
	}
	public bool isClear () { //クリアしているかをbooleanで返すメソッド
		int stagenum = datamanager.getStageNum ();
		return conditionaldatas[stagenum].RequiredDeffenceCount >= recenteatcount.Value; //ステージが0から始まっている点に要注意
	}
	public void decreaseEatCount () {
		if (recenteatcount.Value > 0) {
		recenteatcount.Value--;
		}
	}
	public void decreaseTime () {
		if (recenttime.Value > 0) {
			recenttime.Value--;
		}
	}
	public IEnumerator timedecreasePerSecond () {
		int timelimit = conditionaldatas[datamanager.getStageNum ()].timelimit;
		for (int i = 0; i < timelimit; i++) {
			decreaseTime ();
			yield return new WaitForSeconds (1.0f);
			if (i == timelimit - 1) {
				gameOverEvent ();
				yield break; //ゲームオーバー処理
			}
		}
	}

	private void gameOverEvent () {
		if (isClear ()) { canvasMaker.showclearcanvas (recenteatcount.Value, conditionaldatas[datamanager.getStageNum ()].timelimit); } else {
			canvasMaker.showGameovercanvas (recenteatcount.Value);
		}
	}

	public void addRecentEatcount () {
		recenteatcount.Value++;

	}
	public void decleaseRecentEatcount () {
		recenteatcount.Value--;
	}

	public void setcanvasMaker (canvasmaker maker) {
		canvasMaker = maker;
	}

}