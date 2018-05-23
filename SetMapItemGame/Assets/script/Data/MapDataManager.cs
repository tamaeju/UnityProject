using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MapDataManager : MonoBehaviour { //ゲームデータを保存、使用、返すクラス。データの保持はこのクラスに任せ、値の返却は別クラスに任せたほうがまとまる。（datagetterみたいなクラス）

	private static int maxGridNum = Config.maxGridNum; //最大要素数
	int[, ] _leveldesigndata;
	bool[, ] _canSetDatas;
	[SerializeField]
	Meditator meditator;
	MassDealer massdealer;

	void Start () { //各データの初期化
		_canSetDatas = new bool[maxGridNum, maxGridNum];
		_leveldesigndata = new int[maxGridNum, maxGridNum];
		massdealer = meditator.getmassdealer ();
	}

	public void changeMapData (Vector3 aSetpos, int objectkind) { //レベルデザインデータを更新するメソッド
		Vector2 setpos = new Vector2 ();
		setpos = massdealer.parseVector3XYZtoVector2XZ (aSetpos);
		_leveldesigndata[(int) setpos.x, (int) setpos.y] = objectkind;
	}

	public void makeLevelDesignData () { //objectseteditorから、レベルデザインの更新を行う処理
		GameObject[] UIobjects = meditator.getUImanager ().getUIobjects ();
		for (int j = 0; j < maxGridNum; ++j) {
			for (int i = 0; i < maxGridNum; ++i) {
				_leveldesigndata[i, j] = UIobjects[j * maxGridNum + i].GetComponent<MapEditorbutton> ().returnThisState ();
			}
		}
	}

	public int[, ] getLevelDesignData () { //レベルデザインデータ取得用処理
		return _leveldesigndata;
	}

	public bool[, ] getcanSetDatas () {
		return _canSetDatas;
	}

	public void updateCansetDatas (int[, ] existencepoints) { //ブロックが置いてあるポイントをfalseにして、それ以外をtrueにする
		for (int j = 0; j < existencepoints.GetLength (1); ++j) {
			for (int i = 0; i < existencepoints.GetLength (0); ++i) {
				if (existencepoints[i, j] == 1) {
					_canSetDatas[i, j] = false;
				} else { _canSetDatas[i, j] = true; }
			}
		}
	}

	public void updateCansetDatas (Vector3[] existencepoints) { //移動オブジェクトの存在するポイント4点をfalseにして、それ以外をtrueにする
		Vector2 cehckvector2;
		foreach (var item in existencepoints) {
			cehckvector2 = massdealer.parseVector3XYZtoVector2XZ (item);
			_canSetDatas[(int) cehckvector2.x, (int) cehckvector2.y] = false;
		}
	}

	public void updateCansetDatas (Vector3 existencepoints) { //cansetdataの更新処理
		Vector2 cehckvector2 = massdealer.parseVector3XYZtoVector2XZ (existencepoints);
		_canSetDatas[(int) cehckvector2.x, (int) cehckvector2.y] = false;
	}
}