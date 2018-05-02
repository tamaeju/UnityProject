using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScene : BaseGameScene { //ゲームシーンクラスとの違いは


	[SerializeField]
	LevelSelectCanvasManager canvasmanager;
	int usecolomn_of_mapdata = 3;

	void Start () { //メディエイターからの参照の取得

		csvmanager = meditator.getcsvmanager ();
		mapdatamanager = meditator.getmapdatamanager ();

	}

	public void stageCall (int myStageCount) { //ステージを呼び出す処理
		MapDataManager mapdatamanager = meditator.getmapdatamanager ();
		mapdatamanager.changeStageNum (myStageCount);
		makeObjectFromMapCsvButton ();
		canvasmanager.canvasdisplayOff ();

	}
}