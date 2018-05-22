using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIobject : MonoBehaviour {
	[SerializeField]
	Text m_label;
	[SerializeField]
	Text m_count;
	[SerializeField]
	Image m_image;
	private void changeLabel1 (string newtext) {
		m_label.text = newtext;
	}

	private void changecount1 (int newNum) {
		m_count.text = newNum.ToString ();
	}

	private void setUIReactiveProperties (ReactiveProperty<int> count) {
		count.Subscribe (x => changecount1 (x));
	}

	public void instanceUIobject (string textlabel, ReactiveProperty<int> count) { //初期化時実行メソッド
		changeLabel1 (textlabel);
		changecount1 (count.Value);
		setUIReactiveProperties (count);
	}
	public void changeBackgroundSprite (Sprite newSprite) { //初期化時実行メソッド
		m_image.sprite = newSprite;
	}
}