using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour {
	int horizontaldivisionNum =4;
	float UIbuttonhorizontalsize;
	float UIbuttonverticalsize;
	int arraysizeofUIbutton = 128;
	public GameObject buttonprefabclone;
	GameObject[] buttonobjects;
	Vector2 originpos;
	float buffalength = 20;


	void Start () {
		UIbuttonhorizontalsize = buttonprefabclone.GetComponent<RectTransform>().sizeDelta.x;
		UIbuttonverticalsize = buttonprefabclone.GetComponent<RectTransform>().sizeDelta.y;
		var parent = this.transform;
		originpos = new Vector2((UIbuttonhorizontalsize / 2),   400f);
		buttonobjects = new GameObject[arraysizeofUIbutton];
		for (int i = 0; i < arraysizeofUIbutton; i++) {
			buttonobjects[i] = Instantiate(buttonprefabclone, getUIPos(i),Quaternion.identity, parent) as GameObject;
			buttonobjects[i].GetComponent<LevelSelectButtons_selectscene>().changeThisText("Level  "+(i+1).ToString());
		}
	}

	Vector2 getUIPos(int elementnum) {//下２つの混合メソッド
		Vector2 getpos = new Vector2();
		getpos = getUIArray(elementnum);
		getpos = getUIPosfromArraycount(getpos);
		getpos = originpos + getpos;
		return getpos;
	}

	Vector2 getUIArray(int elementnum) {//要素番号を入れたら、縦何番目、横何番目かを教えてくれる。（不使用メソッド）
		Vector2 getpos = new Vector2();
		getpos.x = elementnum % horizontaldivisionNum;//例余りが0なら左端、1なら左から2番目、2なら左から3番目、3なら左から4番目に生成
		getpos.y = elementnum / horizontaldivisionNum;//例0～3個目は0列目、4～7は1列目
		return getpos;//[0,0]は←左上)
	}

	Vector2 getUIPosfromArraycount(Vector2 arrayNum) {//配列から表示位置を設定する。（不使用メソッド）
		Vector2 getpos = new Vector2();
		getpos.x = arrayNum.x * UIbuttonhorizontalsize + buffalength* (arrayNum.x+1);
		getpos.y = -1*(arrayNum.y * UIbuttonverticalsize + buffalength * (arrayNum.y));
		return getpos;
	}


}
