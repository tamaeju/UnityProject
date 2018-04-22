using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UniRx;

public class canvasmaker : MonoBehaviour {//ゲームスタート時とクリア時のキャンバスを作成するクラス

	[SerializeField]
	GameObject scenecanvasprefab;
	[SerializeField]
	GameObject UIpos;


	//クリアキャンバスをタップした時はレベル選択画面へ移動する処理を行う。
	//そのためキャンバスの作成、そのキャンバスにイベントの設定を行う必要がある。
	//
	public void showclearcanvas(long currentMoveCount, long TargetCount) {//クリア時には目標ターゲット数と現在移動数
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("clear!");
		canvas.changeMessagetext("CONGLATULATION!");
		canvas.changeElement1label("MOVECOUNT");
		canvas.changeElement1Text(currentMoveCount);
		canvas.changeElement2label("TargetCount");
		canvas.changeElement2Text(TargetCount);
		canvas.CanvasTouched.Subscribe(_ => SceneManager.LoadScene("LevelSelectScene"));
	}
	public void showstartcanvas(long TargetCount, long TargetMoveCount) {
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("STAGE START!");
		canvas.changeMessagetext("ゴールと同じ数でゴールにぶつかろう！");
		canvas.changeElement1label("TARGET COUNT");
		canvas.changeElement1Text(TargetCount);
		canvas.changeElement2label("TARGET MOVECOUNT");
		canvas.changeElement2Text(TargetMoveCount);
	}

	public void showGameovercanvas(long currentMoveCount, long TargetMoveCount) {
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("GAMEOVER");
		canvas.changeMessagetext("MOVECOUNT OVER!");
		canvas.changeElement1label("MOVECOUNT");
		canvas.changeElement1Text(currentMoveCount);
		canvas.changeElement2label("TargetMoveCount");
		canvas.changeElement2Text(TargetMoveCount);
		canvas.CanvasTouched.Subscribe(_ => SceneManager.LoadScene("LevelSelectScene"));
	}

	public void showLevelDisplaycanvas(int stageCount,DataStorage dataStorage, CurrentStageData currentData ,Action<int> gamestartEvent, Action deletewindowEvent) {
		//(int stageCount, long TargetCount, long TargetMoveCount ,Action<int> gamestartEvent, Action deletewindowEvent)
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("STAGE"+ stageCount.ToString());
		canvas.changeMessagetext("Play this stage?");
		canvas.changeElement1label("TARGET COUNT");
		canvas.changeElement1Text(currentData.GettargetSum());
		canvas.changeElement2label("TARGET MOVECOUNT");
		canvas.changeElement2Text(currentData.GetTargetMoveCount());
		canvas.backButtonActiveOn();

		canvas.setStageNum(stageCount);
		canvas.CanvasTouched.Subscribe(stage => gamestartEvent(stage));
		canvas.CanvasTouched.Subscribe(_ => deletewindowEvent());
		if (dataStorage.isStageClear(stageCount)) { canvas.ClearedIconGetActive(); }
		//dataStorage.getMaxStageScore(stageCount);

	}

	void testMethod() {
		Debug.Log("clicked");
		//SceneManager.LoadScene(x);
	}



}
