using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemmakeEditorCreater : MonoBehaviour { //ItemmakerをエディットするUIを作成、管理するクラス。

	[SerializeField]
	private GameObject[] Itemmaker;

	[SerializeField]
	GameObject UIButtonPrefab;
	int buttonNum = 3;
	int xposition = 301;
	int yposition = 131;
	[SerializeField]
	DataStorage dataholder;
	[SerializeField]
	CSVManager csvmanager;

	[SerializeField]
	GameObject[] instancePOSes;

	void Start () { //ItemmakerEditorの生成と、何番目のitemmakerEditorかという指定と、自身への参照を渡している。

		Itemmaker = new GameObject[buttonNum];
		Itemmaker[0] = Instantiate (UIButtonPrefab, instancePOSes[0].transform.position, Quaternion.identity, instancePOSes[0].transform) as GameObject;
		Itemmaker[1] = Instantiate (UIButtonPrefab, instancePOSes[1].transform.position, Quaternion.identity, instancePOSes[1].transform) as GameObject;
		Itemmaker[2] = Instantiate (UIButtonPrefab, instancePOSes[2].transform.position, Quaternion.identity, instancePOSes[2].transform) as GameObject;

		setUIdragbuttonNum ();
		setmyreference ();

	}

	public　 void onclickSaveButton (ItemMakeEditor dragbutton) { //saveボタンクリックで、アイテムデータマネージャーの値の更新と、引数に応じたeditorの値をcsvにセーブ

		dataholder.GetDragItemElements ();
		dataholder.UpdateDragitemData (dragbutton.getUIbuttonNum (), dragbutton.getObjectKind (), dragbutton.getLeftCount ());
		csvmanager.itemCsvSave (dataholder.GetDragItemElements ());
	}

	public void setUIdragbuttonNum () { //上から何番目かを指定する処理
		for (int i = 0; i < Itemmaker.Length; i++) {
			Itemmaker[i].GetComponent<ItemMakeEditor> ().changeobjectNum (i);
		}
	}
	public void deletebutton () { //UIを消す際の処理
		foreach (var item in Itemmaker) {
			Destroy (item);
		}
	}
	public void setmyreference () {
		foreach (var item in Itemmaker) {
			item.GetComponent<ItemMakeEditor> ().setmotherobject (this.GetComponent<ItemmakeEditorCreater> ());
		}
	}
}