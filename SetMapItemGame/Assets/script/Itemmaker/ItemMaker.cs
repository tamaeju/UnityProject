using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ItemMaker : MonoBehaviour { //ドラッグ＆ドロップで、アイテムを生成するオブジェクト
	[SerializeField] private int MyObjectKind;
	[SerializeField] public ReactiveProperty<int> ObjectLeftCount;

	[SerializeField] Texture[] textures;

	public int getMyObjectKind () {
		return MyObjectKind;
	}
	public int getObjectLeftCount () {
		return ObjectLeftCount.Value;
	}
	public void decreaseLeftCount () {
		if (ObjectLeftCount.Value > 0)
			ObjectLeftCount.Value--;
	}

	public void changeLabelText () {
		var enmName = (ItemspeedChange.itemstate) Enum.ToObject (typeof (ItemspeedChange.itemstate), MyObjectKind);

	}
	public string getMyKind () {
		var enmName = (ItemspeedChange.itemstate) Enum.ToObject (typeof (ItemspeedChange.itemstate), MyObjectKind);
		return enmName.ToString ();
	}

	public void setMyObjectKind (int kind) {
		MyObjectKind = kind;
	}
	public void setObjectLeftCount (int count) {
		ObjectLeftCount = new ReactiveProperty<int> ();
		ObjectLeftCount.Value = count;
	}
	public void changeMyTexture (int textureNum) {
		GetComponent<Renderer> ().material.mainTexture = textures[textureNum];
	}

}