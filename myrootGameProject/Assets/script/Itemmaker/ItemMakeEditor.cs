using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ItemMakeEditor : MonoBehaviour {//itemmakerを作るためのUI

	readonly int buttonkindLength =Config.itemkindlength;//生成オブジェクトの種類
	[SerializeField]
	int objectkind;
	[SerializeField]
	int leftcount;
	[SerializeField]
	int UIbuttonnum;


	ItemmakeEditorCreater uidraggbuttonmanager;
	[SerializeField]
	Text counttext;


	public void ChangeObjectKind() {
		if (objectkind >= buttonkindLength - 1) {//要素番号なので-1
			objectkind = 0;
		}
		else {
			objectkind++;
		}
		var enmName = (ItemspeedChange.itemstate)Enum.ToObject(typeof(ItemspeedChange.itemstate), objectkind);
		this.gameObject.GetComponentInChildren<Text>().text = enmName.ToString();//番号からアイテムの名前をとってきて、それを反映する。

	}

	public void ChangeDisplayLeftCount() {
		counttext.text = leftcount.ToString();//番号からアイテムの名前をとってきて、それを反映する。

	}
	public void increaseObjectLeftCount() {
		leftcount++;
		ChangeDisplayLeftCount();
	}

	public void decreaseObjectLeftCount() {
		if (leftcount > 0) {
			leftcount--;
			ChangeDisplayLeftCount();
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
	public void setmotherobject(ItemmakeEditorCreater amotherobject) {
		uidraggbuttonmanager= amotherobject;
	}
	public void callUISave() {
		uidraggbuttonmanager.onclickSaveButton(this);
	}

}

