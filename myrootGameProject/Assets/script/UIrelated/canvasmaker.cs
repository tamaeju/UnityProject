using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasmaker : MonoBehaviour {//ゲームスタート時とクリア時のキャンバスを作成するクラス

	GameObject scenecanvasprefab;

	public void showclearcanvas(int recenteatcount) {
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position, Quaternion.identity) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("clear!");
		canvas.changeMessagetext("conglatulation!");
		canvas.changeScorelabel("防衛数");
		canvas.changeScoreText(recenteatcount);
		canvas.changebackcolor(Color.yellow);
	}
	public void showstartcanvas(clearconditiondata conditionaldata) {

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
	}

	public void showGameovercanvas(int recenteatcount) {
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position, Quaternion.identity) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("gameover");
		canvas.changeMessagetext("failed");
		canvas.changeScorelabel("到達数");
		canvas.changeScoreText(recenteatcount);
		canvas.changebackcolor(Color.magenta);
	}

	public void getsececanvas(GameObject canvasprefab) {
		scenecanvasprefab = canvasprefab;
	}
}
