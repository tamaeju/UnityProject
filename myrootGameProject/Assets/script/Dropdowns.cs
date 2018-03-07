using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdowns : MonoBehaviour {
	public Dropdown dropdown;
	void Start() {
		if (dropdown) {
			dropdown.ClearOptions();    //現在の要素をクリアする
			List<string> list = new List<string>();
			for (int i = 0; i < 20; i++) {
				list.Add("csv"+i.ToString());
			}
			dropdown.AddOptions(list);  //新しく要素のリストを設定する
			dropdown.value = 1;         //デフォルトを設定(0～n-1)
		}
	}
}
