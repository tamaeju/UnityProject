using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragButtonManager : MonoBehaviour {

	[SerializeField]
	GameObject[] UIDragButton;//生成する
	DataManager datamanager;
	public GameObject canvasposition;
	CSVManager csvmanager;

	[SerializeField]
	GameObject UIButtonPrefab;
	int buttonNum = 3;
	int xposition = 301;
	int yposition = 131;

	// Use this for initialization
	void Start() {
		var parent = canvasposition.transform;
		Vector3 instancepos = new Vector3();
		instancepos = canvasposition.transform.position;
		instancepos.x = instancepos.x + xposition;
		instancepos.y = instancepos.y + yposition;
		UIDragButton = new GameObject[buttonNum];

		UIDragButton[0] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity,parent) as GameObject;
		instancepos.y = instancepos.y - 120;
		UIDragButton[1] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity, parent) as GameObject;
		instancepos.y = instancepos.y - 120;
		UIDragButton[2] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity, parent) as GameObject;

		setUIdragbuttonNum();
		setmyreference();
	}



	public　void onclickSaveButton(int buttonkind) {//saveボタンクリックで、引数に応じたボタンオブジェクトの値をデータマネージャーに渡す。
		Debug.Log("called  "+"onclickSaveButton");
		Debug.Log(buttonkind);
		if (UIDragButton[buttonkind] = null) { Debug.Log(null); }
		else { UIDragButton[buttonkind].GetComponent<UIDragButton>().getObjectKind(); }
		//UIbuttonが定義すらされていない。この理由は、プレハブのUIボタンを見に行ってしまっている事。つまりマップ上にあるマネージャーの参照をちゃんととらないといけない。

		UIDragButton UIbutton = UIDragButton[buttonkind].GetComponent<UIDragButton>();
		datamanager.Updatdragitemdata(buttonkind, UIbutton.getObjectKind(), UIbutton.getLeftCount());

	}
	public void setUIdragbuttonNum() {
		for (int i = 0; i < UIDragButton.Length; i++) {
			UIDragButton[i].GetComponent<UIDragButton>().changeobjectNum(i);
		}
	}
	public void deletebutton() {
		foreach (var item in UIDragButton) {
			Destroy(item);
		}
	}
	public void setmyreference() {
		UIDragButtonManager motherobject = this.GetComponent<UIDragButtonManager>();
		foreach (var item in UIDragButton) {
			item.GetComponent<UIDragButton>().setmotherobject(motherobject);
		}
	}

}
