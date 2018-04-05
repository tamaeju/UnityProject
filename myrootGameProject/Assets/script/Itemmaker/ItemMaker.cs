using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemMaker : MonoBehaviour {//ドラッグ＆ドロップで、アイテムを生成するオブジェクト
	[SerializeField]private int MyObjectKind;
	[SerializeField]private int ObjectLeftCount;
	[SerializeField]private Text scoretext;
	[SerializeField]private Text labeltext;

	public void Start() {
		scoretext.text = "Left " + ObjectLeftCount.ToString();
		changeLabelText();
	}

	public int getMyObjectKind() {
		return MyObjectKind;
	}
	public int getObjectLeftCount() {
		return ObjectLeftCount;
	}
	public void decreaseLeftCount() {
		if(ObjectLeftCount>0)
		ObjectLeftCount--;
		changeScoreText();
	}
	public void changeScoreText() {
		scoretext.text = "Left " + ObjectLeftCount.ToString();
	}
	public void changeLabelText() {
		var enmName = (ItemspeedChange.itemstate)Enum.ToObject(typeof(ItemspeedChange.itemstate), MyObjectKind);
		labeltext.text = enmName.ToString();
	}
	public void setREFofLeftCount(Text ascoretext) {
		scoretext = ascoretext;
	}
	public void setREFofItemlabel(Text ascoretext) {
		labeltext = ascoretext;
	}
	
	public void setMyObjectKind(int kind)
	{
		MyObjectKind =  kind;
		changeScoreText();
	}
	public void setObjectLeftCount(int count)
	{
		ObjectLeftCount = count;
	}
}
