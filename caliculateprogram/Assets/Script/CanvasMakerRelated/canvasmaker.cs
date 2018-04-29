using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvasmaker : MonoBehaviour { //ゲームスタート時とクリア時のキャンバスを作成するクラス

	[SerializeField]
	GameObject scenecanvasprefab;
	[SerializeField]
	GameObject UIpos;
	[SerializeField]
	GameObject startcanvasprefab;
	[SerializeField]
	GameObject tutorialcanvasprefab;

	//クリアキャンバスをタップした時はレベル選択画面へ移動する処理を行う。
	//そのためキャンバスの作成、そのキャンバスにイベントの設定を行う必要がある。
	//
	public void showclearcanvas (long currentMoveCount, long TargetCount) { //クリア時には目標ターゲット数と現在移動数
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate (scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior> ();
		canvas.changeTitleText ("clear!");
		canvas.changeMessagetext ("ステージクリア!"); //canvas.changeMessagetext("CONGLATULATION!");
		canvas.changeElement1label ("移動回数"); //MOVECOUNT
		canvas.changeElement1Text (currentMoveCount);
		canvas.changeElement2label ("目標数"); //TargetCount
		canvas.changeElement2Text (TargetCount);
		canvas.ClearedDecolateImage();
		canvas.CanvasTouched.Subscribe (_ => SceneManager.LoadScene ("LevelSelect"));
	}
	public void showstartcanvas (long TargetCount, long TargetMoveCount, Action tutorialAction = null) {
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate (startcanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior> ();
		// canvas.changeTitleText ("STAGE START!"); //STAGE START
		// canvas.changeMessagetext ("ゴールと同じ数でゴールにぶつかろう！"); //ゴールと同じ数でゴールにぶつかろう
		// canvas.changeElement1label ("目標数"); //TARGET COUNT
		// canvas.changeElement1Text (TargetCount);
		// canvas.changeElement2label ("移動回数"); //TARGET MOVECOUNT
		// canvas.changeElement2Text (TargetMoveCount);
		if (tutorialAction != null) {
		canvas.CanvasTouched.Subscribe (_ => showTutorialcanvas ()); //チュートリアルの実行メソッドをもらっていれば実行。
		}

	}

	public void showGameovercanvas (long currentMoveCount, long TargetMoveCount) {
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate (scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior> ();
		canvas.changeTitleText ("GAMEOVER"); //GAMEOVER
		canvas.changeMessagetext (""); //MOVECOUNT OVER!
		canvas.changeElement1label ("移動回数"); //MOVECOUNT
		canvas.changeElement1Text (currentMoveCount);
		canvas.changeElement2label ("移動回数上限"); //TargetMoveCount
		canvas.changeElement2Text (TargetMoveCount);
		canvas.CanvasTouched.Subscribe (_ => SceneManager.LoadScene ("LevelSelect"));
	}

	public void showLevelDisplaycanvas (int stageCount, DataStorage dataStorage, CurrentStageData currentData, Action<int> gamestartEvent, Action deletewindowEvent) {
		//(int stageCount, long TargetCount, long TargetMoveCount ,Action<int> gamestartEvent, Action deletewindowEvent)
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate (scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior> ();
		canvas.changeTitleText ("STAGE" + stageCount.ToString ());
		canvas.changeMessagetext (""); //Play this stage?
		canvas.changeElement1label ("目標数"); //TARGET COUNT
		canvas.changeElement1Text (currentData.GettargetSum ());
		canvas.changeElement2label ("移動回数上限"); //TARGET MOVECOUNT
		canvas.changeElement2Text (currentData.GetTargetMoveCount ());
		canvas.backButtonActiveOn ();

		canvas.setStageNum (stageCount);
		canvas.CanvasTouched.Subscribe (stage => gamestartEvent (stage));
		canvas.CanvasTouched.Subscribe (_ => deletewindowEvent ());
		if (dataStorage.isStageClear (stageCount)) {
			canvas.ClearedIconGetActive ();
			canvas.changeHightScoreText (dataStorage.getMaxStageScore (stageCount));
		}

	}
	public void showTutorialcanvas () {
		//(int stageCount, long TargetCount, long TargetMoveCount ,Action<int> gamestartEvent, Action deletewindowEvent)
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate (tutorialcanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		TutorialCanvasBehavior canvas = clearcanvasobject.GetComponent<TutorialCanvasBehavior> ();

	}

	void testMethod () {
		Debug.Log ("clicked");
		//SceneManager.LoadScene(x);
	}

}

//if (dataStorage.isStageClear(stageCount)) {

//}