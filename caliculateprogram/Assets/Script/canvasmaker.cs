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
	public void showclearcanvas(int currentMoveCount,int TargetCount) {//クリア時には目標ターゲット数と現在移動数
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("clear!");
		canvas.changeMessagetext("CONGLATULATION!");
		canvas.changeElement1label("MOVECOUNT");
		canvas.changeElement1Text(currentMoveCount);
		canvas.changeElement2label("TargetCount");
		canvas.changeElement2Text(TargetCount);
		//canvas.changebackcolor(Color.yellow);
		canvas.CanvasTouched.Subscribe(x=> testMethod(x));
	}
	public void showstartcanvas(int TargetMoveCount, int TargetCount) {
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("STAGE START!");
		canvas.changeMessagetext("ゴールと同じ数でゴールにぶつかろう！");
		canvas.changeElement1label("TARGET COUNT");
		canvas.changeElement1Text(TargetCount);
		canvas.changeElement2label("TARGET MOVECOUNT");
		canvas.changeElement2Text(TargetMoveCount);
		//canvas.changebackcolor(Color.green);
		//canvas.CanvasTouched.Subscribe(x => SceneManager.LoadScene(x));
	}

	public void showGameovercanvas(int currentMoveCount, int TargetMoveCount) {
		var parent = UIpos.transform;
		GameObject clearcanvasobject = Instantiate(scenecanvasprefab, this.transform.position, Quaternion.identity, parent) as GameObject;
		Canvasbehavior canvas = clearcanvasobject.GetComponent<Canvasbehavior>();
		canvas.changeTitleText("GAMEOVER");
		canvas.changeMessagetext("MOVECOUNT OVER!");
		canvas.changeElement1label("MOVECOUNT");
		canvas.changeElement1Text(currentMoveCount);
		canvas.changeElement2label("TargetMoveCount");
		canvas.changeElement2Text(TargetMoveCount);
		//canvas.changebackcolor(Color.magenta);
		canvas.CanvasTouched.Subscribe(x => testMethod(x));
	}

	void testMethod(string x) {
		Debug.Log("clicked");
		//SceneManager.LoadScene(x);
	}

	private void Start() {
		//this.transform.position = Vector3.zero;
		showclearcanvas(1, 1);
		showstartcanvas(1, 1);
		showGameovercanvas(1, 1);
	}

}
