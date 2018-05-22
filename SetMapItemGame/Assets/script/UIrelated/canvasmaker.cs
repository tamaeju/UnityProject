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

	public void showclearcanvas (int recenteatcount, int clearTime, Action act　 = null) {
		GameObject clearcanvasobject = Instantiate (scenecanvasprefab, this.transform.position, Quaternion.identity, this.transform) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior> ();
		canvas.changeTitleText ("clear!");
		canvas.changeMessagetext ("conglatulation!");
		canvas.changeScorelabel ("エネミー侵略数");
		canvas.changeScoreText (recenteatcount);
		canvas.changeTimelabel ("防衛した時間");
		canvas.changeTimeText (clearTime);
		canvas.changebackcolor (Color.yellow);
		canvas.setMethod (act);
	}
	public void showstartcanvas (clearconditiondata conditionaldata, Action canvasmethod) {

		int timelimit = conditionaldata.timelimit;
		int deffencecount = conditionaldata.RequiredDeffenceCount;
		GameObject clearcanvasobject = Instantiate (scenecanvasprefab, this.transform.position, Quaternion.identity, this.transform) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior> ();
		canvas.changeTitleText ("stagestart!");
		canvas.changeMessagetext ("アイテムを使ってエネミーの侵略を防ぎきろう！");
		canvas.changeScorelabel ("目標防衛数");
		canvas.changeScoreText (deffencecount);
		canvas.changeTimelabel ("エネミー侵略時間");
		canvas.changeTimeText (timelimit);
		canvas.changebackcolor (Color.green);
		canvas.setMethod (canvasmethod);
	}

	public void showGameovercanvas (int recenteatcount) {
		GameObject clearcanvasobject = Instantiate (scenecanvasprefab, this.transform.position, Quaternion.identity, this.transform) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior> ();
		canvas.changeTitleText ("gameover");
		canvas.changeMessagetext ("failed");
		canvas.changeScorelabel ("エネミー侵略数");
		canvas.changeScoreText (recenteatcount);
		canvas.changebackcolor (Color.magenta);
		canvas.CanvasScrolled.Subscribe (x => testloadScene (x));
	}

	public void testloadScene (string x) {
		Debug.LogFormat ("testloadScene");
		SceneManager.LoadScene (x);
	}
}