using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class LevelSelectCanvasManager : MonoBehaviour {//レベルセレクト画面のセレクトボタンを生成するクラス

	int horizontaldivisionNum =5;
	float horizontalsize;
	float verticalsize;
	int arraysizeofUIbutton = 80;
	[SerializeField]
	GameObject buttonprefabclone;
	GameObject[] buttonobjects;
	Vector2 originpos;
	float buffalength = 45;


	[SerializeField]
	GameObject levelselectscenecanvaspos;

	void Start () {

		horizontalsize = buttonprefabclone.GetComponent<RectTransform>().sizeDelta.x;
		verticalsize = buttonprefabclone.GetComponent<RectTransform>().sizeDelta.y;

		var parent = this.transform;
		originpos = new Vector2((horizontalsize+50),   400f);
		buttonobjects = new GameObject[arraysizeofUIbutton];
		for (int i = 0; i < arraysizeofUIbutton; i++) {
			buttonobjects[i] = Instantiate(buttonprefabclone, getUIPos(i),Quaternion.identity, parent) as GameObject;
			//SelectsceneButton selectscenebutton = buttonobjects[i].GetComponent<SelectsceneButton>();
			//selectscenebutton.changeThisText("Level  "+(i+1).ToString());
			//selectscenebutton.changeMystageCount(i);
			//selectscenebutton.OnClickedStageButton.Subscribe(stage => { levelselectscene.stageCall(stage); });
		}
	}

	Vector2 getUIPos(int elementnum) {//getUIArrayとgetUIPosfromArraycountの混合メソッド
		Vector2 getpos = new Vector2();
		getpos = getUIArray(elementnum);
		getpos = getUIPosfromArraycount(getpos);
		getpos = originpos + getpos;
		return getpos;
	}

	Vector2 getUIArray(int elementnum) {//要素番号を入れたら、縦何番目、横何番目に表示すべきかを割り算と余数で教えてくれる。
		Vector2 getpos = new Vector2();
		getpos.x = elementnum % horizontaldivisionNum;//例余りが0なら左端、1なら左から2番目、2なら左から3番目、3なら左から4番目に生成
		getpos.y = elementnum / horizontaldivisionNum;//例0～3個目は0列目、4～7は1列目
		return getpos;//[0,0]は←左上)
	}

	Vector2 getUIPosfromArraycount(Vector2 arrayNum) {//getUIposで出た2次元情報から、実際の表示座標を出すメソッド
		Vector2 getpos = new Vector2();
		getpos.x = arrayNum.x * horizontalsize + buffalength* (arrayNum.x+1);
		getpos.y = -1*(arrayNum.y * verticalsize + buffalength * (arrayNum.y));
		return getpos;
	}
	public void canvasdisplayOff() {
		levelselectscenecanvaspos.SetActive(false);
	}



}
