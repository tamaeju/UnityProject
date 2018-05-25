using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventManager : MonoBehaviour {//ボタンを押した時のクリック処理の大方を扱うクラス。
	[SerializeField]
	Meditator meditator;


	public void CanvasOFFButton() {//キャンバスの表示オフ
		Transform trasnform = meditator.getprefabcontainer().getmapmassuipositionObject().GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(false);
		}
	}
	public void CanvasONButton() {//キャンバスの表示オン
		Transform trasnform = meditator.getprefabcontainer().getmapmassuipositionObject().GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(true);
		}
	}
	public void callloadMapCSV() {//指定のcsvからデータを読み込み、UIでのボタンのstateを変える。
		MapEditorUIManager UImanager = meditator.getUImanager();
		UImanager.ChangeMapEditCSV();
	}
}

