using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragButtonManager : MonoBehaviour {

	[SerializeField]
	private GameObject[] UIDragButton;//生成する

	public GameObject canvasposition;
	[SerializeField]
	Meditator meditator;

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



	public　void onclickSaveButton(UIDragButton dragbutton) {//saveボタンクリックで、引数に応じたボタンオブジェクトの値をデータマネージャーに渡す。
		DataManager datamanager = meditator.getdatamanager();
		datamanager.UpdateDragitemData(dragbutton.getUIbuttonNum(), dragbutton.getObjectKind(), dragbutton.getLeftCount());

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
		foreach (var item in UIDragButton) {
			item.GetComponent<UIDragButton>().setmotherobject(this.GetComponent<UIDragButtonManager>());
		}
	}
	//セーブボタンを押したら、各ボタンオブジェクトに、UIマネージャーが値を聞いて
}
