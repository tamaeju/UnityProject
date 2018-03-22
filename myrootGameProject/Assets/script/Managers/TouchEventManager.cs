using UnityEngine;
using System.Collections;
using System;

public class TouchEventManager : MonoBehaviour {
	GameObject catchObject;
	Vector3 instancePosition;
	RayEmit rayemitter;
	ItemMaker draggeeditem;
	DisplayMoveAgent DisplayMoveagent;
	Vector3 initialtappoint;
	Vector3 tappointdifference;

	[SerializeField]
	Meditator meditator;
	MakeManager makemanager;
	DataManager datamanager;
	DataCheck datachecker;
	MassDealer massdealer;



	void Start() {
		makemanager = meditator.getmakemanager();
		datamanager = meditator.getdatamanager();
		datachecker = meditator.getdatachecker();
		massdealer = meditator.getmassdealer();
		rayemitter = new RayEmit();
	}



	void Update() {
		if (Input.GetMouseButtonDown(0)) {//ドラッグしたアイテムのアイテムメイカーコンポーネントを取ってこれたら、それに応じた引数でメソッドを実行する
			checkandSetTouchObjectKind();
			if (draggeeditem != null) { DragDownItemMaker(); }//draggeditemのオブジェクトをつかめたときはドラッグオンアイテムメイカーのメソッドを実行
			if (DisplayMoveagent != null) { DragDownScrollagent(); }//draggeditemのオブジェクトをつかめたときはドラッグオンスクローラーのメソッドを実行
		}
		if (Input.GetMouseButton(0)) {//タッチされ続けている間、オブジェクトの位置を動かし続ける。
			DragOnItemMaker();
			DragOnScrollagent();
		}
		if (Input.GetMouseButtonUp(0)) {//タッチが離されたタイミングで、オブジェクトをつかんでいればnullを入れる。
			DragUpItemMaker();
			catchObject = null;
			draggeeditem = null;
			DisplayMoveagent = null;
		}
	}


	void checkandSetTouchObjectKind() {
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
		if (catchObject != null) {
			catchObject.transform.position = massdealer.getInstanceposFromMouse(2);
		}
	}
	void DragUpItemMaker() {
		instancePosition = massdealer.getInstanceposFromMouse(0);
		Vector3 indexVector3 = massdealer.getIndexpos(instancePosition);//x,y,zが何番目の配列か調べる。
		if (datachecker.checkCanSet(indexVector3) && catchObject != null) {//今のポジションのインデックスが配列内であり、セットできるのであれば
			draggeeditem.decreaseLeftCount();//レフトカウントを1減らす。
			catchObject.transform.position = massdealer.getRoundedgPos(instancePosition);
			datamanager.changeMapData(indexVector3, draggeeditem.getMyObjectKind());
			datamanager.updateCansetDatas(indexVector3);
		}
		else {
			Destroy(catchObject);
		}

	}
	void DragDownScrollagent() {//タップ位置を取得
		initialtappoint = massdealer.getposFromMouse();
	}
	void DragOnScrollagent() {//初期のタップ位置との差分を取得し
		DisplayMoveagent.movescrollobject((initialtappoint - massdealer.getposFromMouse())/5);
		//ディスプレイ全体のサイズより大きく移動する場合は、修正を行う。
	}
	void DragUpScrollagent() {

	}

}

