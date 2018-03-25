using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemmakeEditorManager : MonoBehaviour {//ItemmakerをエディットするUIを管理するオブジェクト

	[SerializeField]
	private GameObject[] Itemmaker;

	public GameObject canvasposition;
	[SerializeField]
	Meditator meditator;

	[SerializeField]
	GameObject UIButtonPrefab;
	int buttonNum = 3;
	int xposition = 301;
	int yposition = 131;


	void Start() {//ItemmakerEditorの生成。
		var parent = canvasposition.transform;
		Vector3 instancepos = new Vector3();
		instancepos = canvasposition.transform.position;
		instancepos.x = instancepos.x + xposition;
		instancepos.y = instancepos.y + yposition;
		Itemmaker = new GameObject[buttonNum];

		Itemmaker[0] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity,parent) as GameObject;
		instancepos.y = instancepos.y - 120;
		Itemmaker[1] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity, parent) as GameObject;
		instancepos.y = instancepos.y - 120;
		Itemmaker[2] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity, parent) as GameObject;


		setUIdragbuttonNum();
		setmyreference();

	}
	

	public　void onclickSaveButton(ItemMakeEditor dragbutton) {//saveボタンクリックで、引数に応じたeditorの値をセーブ
		CSVManager csvmanager = meditator.getcsvmanager();
		DataManager datamanager = meditator.getdatamanager();
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		datamanager.LoadALLdragitemdata();
		datamanager.UpdateDragitemData(dragbutton.getUIbuttonNum(), dragbutton.getObjectKind(), dragbutton.getLeftCount());
		csvmanager.itemCsvSave(datamanager.getItemData());
	}
	public void setUIdragbuttonNum() {
		for (int i = 0; i < Itemmaker.Length; i++) {
			Itemmaker[i].GetComponent<ItemMakeEditor>().changeobjectNum(i);
		}
	}
	public void deletebutton() {
		foreach (var item in Itemmaker) {
			Destroy(item);
		}
	}
	public void setmyreference() {
		foreach (var item in Itemmaker) {
			item.GetComponent<ItemMakeEditor>().setmotherobject(this.GetComponent<ItemmakeEditorManager>());
		}
	}
}
