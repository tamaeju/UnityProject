using UnityEngine;
using System.Collections;
using System;

public class TouchManager : MonoBehaviour {
	GameObject refObject;
	Quaternion charactorq;
	Vector3 instancePosition;
	Vector3 setPosition;
	Vector3 screenPotsition;
	RayEmit rayemitter;
	public MakeManager makemanager;
	MakeDraggedObject draggeeditem;
	private GameObject refrayObject;
	public DataManager datamanager;

	void Start() {
		rayemitter = new RayEmit();
	}

	public void makeDraggedObject(int objectKind) {

	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			try { draggeeditem = rayemitter.getObject().GetComponent<MakeDraggedObject>(); }
			catch { Debug.Log(string.Format("draggeeditem が null)", draggeeditem)); }
			if (draggeeditem !=null&& draggeeditem.GetType() == typeof(MakeDraggedObject) && draggeeditem.getObjectLeftCount() > 0) 
				{//ドラッグしたアイテムがmakedraggedobjectであり、かつレフトカウントが0より大きいなら
				int prefabkind = draggeeditem.getMyObjectKind();
				refObject = makemanager.InstanciateandGetRef(prefabkind, getInstanceposFromMouse(2));
			}
			else {
				Debug.Log("touched irregular obeject or noLeftItem");
			}
		}

		if (Input.GetMouseButton(0)) {//タッチされ続けている間、オブジェクトの位置を動かし続ける。
			getInstanceposFromMouse(2);
			if (refObject != null) {
				refObject.transform.position = instancePosition;
			}
			else {
			}
		}

		if (Input.GetMouseButtonUp(0)) {//タッチが離されたタイミングで、オブジェクトをつかんでいればnullを入れる。
			getInstanceposFromMouse(0);
			Vector3 indexVector3 = getIndexpos(instancePosition);//x,y,zが何番目の配列か調べる。
			if (datamanager.checkCanSet(indexVector3)&& refObject!=null) {//今のポジションのインデックスが配列内であり、セットできるのであれば
				draggeeditem.decreaseLeftCount();//レフトカウントを1減らす。
				refObject.transform.position = getRoundedgPos(instancePosition);
				datamanager.changeMapData(indexVector3, draggeeditem.getMyObjectKind());
				datamanager.updateCansetDatas(indexVector3);
			}
			else {
				Destroy(refObject);
			}
			refObject = null;
			draggeeditem = null;
		}
	}
	public Vector3 getIndexpos(Vector3 aPos) {//設置されているポジションのindexを返すメソッド
		Vector3 indexpos = new Vector3();
		indexpos.x = (float)Math.Round(aPos.x / makemanager.getBlockLength());
		indexpos.y = (float)Math.Round(aPos.y / makemanager.getBlockLength());
		indexpos.z = (float)Math.Round(aPos.z / makemanager.getBlockLength());
		return indexpos;
	}
	public Vector3 getRoundedgPos(Vector3 aPos) {//設置されるポジションを返すメソッド.getIndexPosにブロックのlengthをかけて、インデックスを実際に使えるvectorに変換している。
		Vector3 roundedpos = new Vector3();
		roundedpos.x = getIndexpos(aPos).x * makemanager.getBlockLength();
		roundedpos.y = getIndexpos(aPos).y * makemanager.getBlockLength();
		roundedpos.z = getIndexpos(aPos).z * makemanager.getBlockLength();
		return roundedpos;
	}
	public Vector3 getInstanceposFromMouse(int slideypos) {
		Vector3 screenPotsition;
		screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
		instancePosition.y = makemanager.getObjecthight() + slideypos;
		return instancePosition;
	}
}