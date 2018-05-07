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
using UnityEngine.SceneManagement;
using UniRx;

public class canvasmaker : MonoBehaviour {//ゲームスタート時とクリア時のキャンバスを作成するクラス

	GameObject scenecanvasprefab;
	

	public void showclearcanvas(int recenteatcount, Action act　=null) {
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position, Quaternion.identity) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("clear!");
		canvas.changeMessagetext("conglatulation!");
		canvas.changeScorelabel("防衛数");
		canvas.changeScoreText(recenteatcount);
		canvas.changebackcolor(Color.yellow);
		canvas.setMethod(act);
	}
	public void showstartcanvas(clearconditiondata conditionaldata, Action canvasmethod) {

		int timelimit = conditionaldata.timelimit;
		int deffencecount = conditionaldata.RequiredDeffenceCount;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position, Quaternion.identity) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("stagestart!");
		canvas.changeMessagetext("");
		canvas.changeScorelabel("目標防衛数");
		canvas.changeScoreText(deffencecount);
		canvas.changeTimelabel("残時間");
		canvas.changeTimeText(timelimit);
		canvas.changebackcolor(Color.green);
		canvas.setMethod(canvasmethod);
	}

	public void showGameovercanvas(int recenteatcount) {
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position, Quaternion.identity) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("gameover");
		canvas.changeMessagetext("failed");
		canvas.changeScorelabel("到達数");
		canvas.changeScoreText(recenteatcount);
		canvas.changebackcolor(Color.magenta);
		canvas.CanvasScrolled.Subscribe(x => testloadScene(x)); 
	}

	public void testloadScene(string x) {
		Debug.LogFormat("testloadScene");
		SceneManager.LoadScene(x);
	}

	public void getscenecanvas(GameObject canvasprefab) {
		scenecanvasprefab = canvasprefab;
	}
}
