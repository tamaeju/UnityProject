using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragButtonManager : MonoBehaviour {

	GameObject[] UIDragButton;//生成する
	DataManager datamanager;

	[SerializeField]
	GameObject UIButtonPrefab;


	// Use this for initialization
	void Start() {
		Vector3 instancepos = new Vector3();
		instancepos = UIButtonPrefab.transform.position;
		UIDragButton[0] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity) as GameObject;
		instancepos.y = instancepos.y - 200;
		UIDragButton[1] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity) as GameObject;
		instancepos.y = instancepos.y - 200;
		UIDragButton[2] = Instantiate(UIButtonPrefab, instancepos, Quaternion.identity) as GameObject;
	}


	public　void onclickSaveButton(int buttonkind) {//saveボタンクリックで、引数に応じたボタンオブジェクトの値をデータマネージャーに渡す。
		UIDragButton UIbutton = UIDragButton[buttonkind].GetComponent<UIDragButton>();
		datamanager.Updatdragitemdata(buttonkind, UIbutton.getObjectKind(), UIbutton.getLeftCount());
	}
}
