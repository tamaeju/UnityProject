using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIDragButton : MonoBehaviour {

	readonly int buttonkindLength = 8;//生成オブジェクトの種類
	[SerializeField]
	int objectkind;
	[SerializeField]
	int leftcount;

	public void ChangeObjectKind() {
		if (objectkind >= buttonkindLength - 1) {//要素番号なので-1
			objectkind = 0;
		}
		else {
			objectkind++;
		}
		//ボタンの子オブジェクトにアクセスして、テキストを変更する。
		this.gameObject.GetComponentInChildren<Text>().text = objectkind.ToString();
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
}

