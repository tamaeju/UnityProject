using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownUI : MonoBehaviour {
	public Dropdown dropdown;
	void Start() {
		dropdown = this.GetComponent<Dropdown>();
		if (dropdown) {
			dropdown.ClearOptions();
			//現在の要素をクリアする
			List<string> list = new List<string>();
			for (int i = 0; i < 20; i++) {
				list.Add("csv"+i.ToString());
			}
			dropdown.AddOptions(list);  //新しく要素のリストを設定する
		}
	}
}
