using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemmakeEditorManager : MonoBehaviour {//ItemmakerをエディットするUIを作成、管理するクラス。

	[SerializeField]
	private GameObject[] Itemmaker;

	public GameObject ItemmakeEdiorcanvaspos;
	[SerializeField]
	Meditator meditator;

	[SerializeField]
	GameObject UIButtonPrefab;
	int buttonNum = 3;
	int xposition = 301;
	int yposition = 131;


	void Start() {//ItemmakerEditorの生成と、何番目のitemmakerEditorかの指定と、自身への参照を渡している。
		var parent = ItemmakeEdiorcanvaspos.transform;
		Vector3 instancepos = new Vector3();
		instancepos = ItemmakeEdiorcanvaspos.transform.position;
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
		ItemDataManager itemdatamanager = meditator.getitemdatamanager();
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		itemdatamanager.LoadALLdragitemdata();
		itemdatamanager.UpdateDragitemData(dragbutton.getUIbuttonNum(), dragbutton.getObjectKind(), dragbutton.getLeftCount());
		csvmanager.itemCsvSave(itemdatamanager.getItemData());
	}
	public void setUIdragbuttonNum() {//上から何番目かを指定する処理
		for (int i = 0; i < Itemmaker.Length; i++) {
			Itemmaker[i].GetComponent<ItemMakeEditor>().changeobjectNum(i);
		}
	}
	public void deletebutton() {//UIを消す際の処理
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
