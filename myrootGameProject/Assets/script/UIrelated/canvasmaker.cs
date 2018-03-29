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
		canvas.setButtonscroll();
		canvas.changebackcolor(Color.yellow);
	}
	public void showstartcanvas(clearconditiondata conditionaldata) {

		int timelimit = conditionaldata.timelimit;
		int needeatcount = conditionaldata.RequiredDeffenceCount;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, transform.position, Quaternion.identity) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("stagestart!");
		canvas.changeMessagetext("");
		canvas.changeScorelabel("目標防衛数");
		canvas.changeScoreText(needeatcount);
		canvas.changeTimelabel("残時間");
		canvas.changeTimeText(timelimit);
		canvas.setButtonscroll();
		canvas.changebackcolor(Color.green);
	}

	public void getsececanvas(GameObject canvasprefab) {
		scenecanvasprefab = canvasprefab;
	}
}
