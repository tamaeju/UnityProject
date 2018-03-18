using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakeEditor : MonoBehaviour {//itemmakerを作るためのUI

	readonly int buttonkindLength =Config.itemkindlength;//生成オブジェクトの種類
	[SerializeField]
	int objectkind;
	[SerializeField]
	int leftcount;
	[SerializeField]
	int UIbuttonnum;
	[SerializeField]
	ItemmakeEditorManager uidraggbuttonmanager;

	
	public void ChangeObjectKind() {
		if (objectkind >= buttonkindLength - 1) {//要素番号なので-1
			objectkind = 0;
		}
		else {
			objectkind++;
		}
		var enmName = (Item.itemstate)Enum.ToObject(typeof(Item.itemstate), objectkind);
		this.gameObject.GetComponentInChildren<Text>().text = enmName.ToString();//番号からアイテムの名前をとってきて、それを反映する。

	}
	public void increaseObjectLeftCount() {
		leftcount++;
	}

	public void decreaseObjectLeftCount() {
		if (leftcount > 0) {
			leftcount--;
		}
		else { }
	}
	public int getObjectKind() {
		return objectkind;
	}
	public int getLeftCount() {
		return leftcount;
	}
	public int getUIbuttonNum()
	{
		return UIbuttonnum;
	}
	public void changeobjectNum(int num) {
		UIbuttonnum = num;
	}
	public void setmotherobject(ItemmakeEditorManager amotherobject) {
		uidraggbuttonmanager= amotherobject;
	}
	public void callUISave() {
		uidraggbuttonmanager.onclickSaveButton(this);
	}

}

