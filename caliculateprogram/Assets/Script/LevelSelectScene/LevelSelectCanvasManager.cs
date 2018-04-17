using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSelectCanvasManager : MonoBehaviour {//レベルセレクト画面のセレクトボタンを生成するクラス

	int horizontaldivisionNum =5;
	float horizontalsize;
	float verticalsize;
	int totalButtonCount = Config.stageCount;
	[SerializeField]
	GameObject buttonprefabclone;
	GameObject[] buttonobjects;
	Vector2 originpos;
	float buffalength = 45;
	[SerializeField]
	GameObject canvasBackGround;
	[SerializeField]
	canvasmaker canvasmaker;


	[SerializeField]
	GameObject levelselectscenecanvaspos;

	void Start () {

		horizontalsize = buttonprefabclone.GetComponent<RectTransform>().sizeDelta.x;
		verticalsize = buttonprefabclone.GetComponent<RectTransform>().sizeDelta.y;

		var parent = this.transform;
		originpos = new Vector2((horizontalsize+50),   450f);
		buttonobjects = new GameObject[totalButtonCount];
		for (int i = 0; i < totalButtonCount; i++) {
			buttonobjects[i] = Instantiate(buttonprefabclone, getUIPos(i),Quaternion.identity, parent) as GameObject;
			SelectsceneButton selectscenebutton = buttonobjects[i].GetComponent<SelectsceneButton>();
			selectscenebutton.changeThisText("Level  "+(i+1).ToString());
			selectscenebutton.changeMystageCount(i);
			selectscenebutton.OnClickedStageButton.Subscribe(stage => testLoadScene(stage));
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

	Vector2 getUIPosfromArraycount(Vector2 arrayNum) {//getUIposで出た2次元座標位置から、実際の表示座標を出すメソッド
		Vector2 getpos = new Vector2();
		getpos.x = arrayNum.x * horizontalsize + buffalength* (arrayNum.x+1);
		getpos.y = -1*(arrayNum.y * verticalsize + buffalength * (arrayNum.y));
		return getpos;
	}


	public void canvasdisplayOff() {
		levelselectscenecanvaspos.SetActive(false);
	}

	public void goUPcanvasBackGround() {
		int movedistance = 500;
		int moveupperlimit = 2200;
		int canvasposY = 284;//UIのためキャンバスオブジェクトの左下から設定されているため変換のためキャンバスオブジェクトの座標分補正する必要あり
		RectTransform newtransform = canvasBackGround.GetComponent<RectTransform>();
		if (newtransform.position.y + movedistance < moveupperlimit) {//現在のy座標プラス移動後のy座標の値が最大移動値よりも小さいならば
			Vector3 newPos = new Vector3(newtransform.position.x, newtransform.position.y + movedistance, newtransform.position.z);
			newtransform.DOMove(newPos, 0.5f);
		}
		else {
			Vector3 newPos = new Vector3(newtransform.position.x, moveupperlimit+ canvasposY, newtransform.position.z);
			newtransform.DOMove(newPos, 0.5f);
		}

	}

	public void goDowncanvasBackGround() {
		int movedistance = -500;
		int moveunderlimit = 0;
		int canvasposY = 284;//UIのためキャンバスオブジェクトの左下から設定されているため変換のため
		RectTransform newtransform = canvasBackGround.GetComponent<RectTransform>();
		if (newtransform.position.y + movedistance > moveunderlimit) {//現在のy座標プラス移動後のy座標の値が最大移動値よりも小さいならば
			Vector3 newPos = new Vector3(newtransform.position.x, newtransform.position.y + movedistance, newtransform.position.z);
			newtransform.DOMove(newPos, 0.5f);
		}
		else {
			Vector3 newPos = new Vector3(newtransform.position.x, moveunderlimit + canvasposY, newtransform.position.z);
			newtransform.DOMove(newPos, 0.5f);
		}
	}

	void testLoadScene(int stage) {
		Debug.LogFormat("clicked stage{0}", stage);
	}

	//ボタンをタップすると、キャンバスメイカーに命令してウインドウを作成する。
	//ウインドウに乗っている情報は、

}
