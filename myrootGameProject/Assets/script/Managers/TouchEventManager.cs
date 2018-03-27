using UnityEngine;
using System.Collections;
using System;

public class TouchEventManager : MonoBehaviour {//画面をtouchした際の挙動を扱うクラス
	GameObject catchObject;
	RayEmit rayemitter;
	ItemMaker draggeeditem;
	DisplayMoveAgent DisplayMoveagent;
	Vector3 initialtappoint;
	MakeManager makemanager;
	MapDataManager mapdatamanager;
	DataCheck datachecker;
	MassDealer massdealer;

	[SerializeField]
	Meditator meditator;



	void Start() {//参照オブジェクトの初期化
		makemanager = meditator.getmakemanager();
		mapdatamanager = meditator.getmapdatamanager();
		datachecker = meditator.getdatachecker();
		massdealer = meditator.getmassdealer();
		rayemitter = new RayEmit();
	}



	void Update() {//マウス入力による処理分岐
		if (Input.GetMouseButtonDown(0)) {
			checkandSetTouchObjectKind();
			if (draggeeditem != null) { DragDownItemMaker(); }
			if (DisplayMoveagent != null) { DragDownScrollagent(); }
		}
		if (Input.GetMouseButton(0)) {
			if (draggeeditem != null) { DragOnItemMaker(); }
			if (DisplayMoveagent != null) { DragOnScrollagent(); }
		}
		if (Input.GetMouseButtonUp(0)) {
			DragUpItemMaker();
			catchObject = null;
			draggeeditem = null;
			DisplayMoveagent = null;
		}
	}


	void checkandSetTouchObjectKind() {//レイキャストを飛ばしたオブジェクトで参照を設定
		if (rayemitter.getObject().GetComponent<ItemMaker>() != null) {
			draggeeditem = rayemitter.getObject().GetComponent<ItemMaker>();
			DisplayMoveagent = null;
		}
		if (rayemitter.getObject().GetComponent<DisplayMoveAgent>() != null) {
			DisplayMoveagent = rayemitter.getObject().GetComponent<DisplayMoveAgent>();
			draggeeditem = null;
		}
	}


	void DragDownItemMaker() {
		if (draggeeditem.getObjectLeftCount() > 0) {
			int prefabkind = draggeeditem.getMyObjectKind();
			catchObject = makemanager.InstanciateandGetRef(prefabkind, massdealer.getInstanceposFromMouse(2));
		}
		else {
			Debug.Log(String.Format("draggeeditem.GetType() is {0} draggeeditem.getObjectLeftCount() is {1}", draggeeditem.GetType(), draggeeditem.getObjectLeftCount()));
		}
	}
	void DragOnItemMaker() {
			catchObject.transform.position = massdealer.getInstanceposFromMouse(2);
	}

	void DragUpItemMaker() {//Itemを対応した座標に設置するための判定および処理
		Vector3 instancePosition;
		instancePosition = massdealer.getInstanceposFromMouse(0);
		Vector3 indexVector3 = massdealer.getIndexpos(instancePosition);//x,y,zが何番目の配列か調べる。
		Debug.Log(String.Format("indexVector3.x, indexVector3.y, indexVector3.zはそれぞれ{0}{1}{2}", indexVector3.x, indexVector3.y, indexVector3.z));

		if (datachecker.checkCanSet(indexVector3) && catchObject != null) {//今のポジションのインデックスが配列内であり、セットできるのであれば処理実行
			draggeeditem.decreaseLeftCount();
			catchObject.transform.position = massdealer.getRoundedgPos(instancePosition);
			mapdatamanager.changeMapData(indexVector3, draggeeditem.getMyObjectKind());
			mapdatamanager.updateCansetDatas(indexVector3);
		}
		else {
			Destroy(catchObject);
		}
	}

	void DragDownScrollagent() {//タップ位置を取得
		initialtappoint = massdealer.getposFromMouse();
	}
	void DragOnScrollagent() {//初期のタップ位置との差分を取得する処理
		DisplayMoveagent.movescrollobject((initialtappoint - massdealer.getposFromMouse())/5);
	}
	void DragUpScrollagent() {

	}

}

