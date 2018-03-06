using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MakeDraggedObject : MonoBehaviour {
	[SerializeField]private int MyObjectKind;
	[SerializeField]private int ObjectLeftCount;
	[SerializeField]private Text scoretext;//TextというのはあくまでコンポーネントのTextであるので、文字を変えたいならText.textと書く必要がある。

	public void Start() {
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
		scoretext.text = "LeftCount" + ObjectLeftCount.ToString();
	}
}
