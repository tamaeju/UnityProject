using UnityEngine;
using System.Collections;
using System;

public class TouchManager : MonoBehaviour {
	private static Vector3 TouchPosition = Vector3.zero;
	public GameObject[] instanceObject;
	GameObject refObject;
	public int[] LeftCount;
	Quaternion charactorq;
	Vector3 instancePosition;
	Vector3 setPosition;
	Vector3 screenPotsition;
	RayEmit rayemitter;
	MakeDraggedObject draggeeditem;
	public GameObject ground;
	float groundhight;
	float instancehight;
	public GameObject groungrayemitter;
	private GameObject refrayObject;
	float blocklength = 0.9f;
	public GameObject Mapmanager;
	private LevelDesignCreate manager;
	bool Istatch;

	// Use this for initialization
	void Start() {
		LeftCount = new int[3];
		for (int i = 0; i < LeftCount.Length; i++) {
			LeftCount[i] = 2;
			charactorq = Quaternion.Euler(90f, 0f, 0f);
		}
		rayemitter = new RayEmit();
		groundhight = ground.transform.position.y;
		instancehight = groundhight + 0.5f;
		manager = Mapmanager.GetComponent<LevelDesignCreate>();
	}

	public void makeDraggedObject(int objectKind) {

	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			setInstanceposFromMouse(2);
			try { draggeeditem = rayemitter.getObject().GetComponent<MakeDraggedObject>(); }
			catch { Debug.Log(string.Format("draggeeditem" + "が{0}", draggeeditem)); }
			if (draggeeditem !=null&& draggeeditem.GetType() == typeof(MakeDraggedObject) && draggeeditem.getObjectLeftCount() > 0) 
				{//ドラッグしたアイテムがmakedraggedobjectであり、かつレフトカウントが0より大きいなら
				int prefabkind = draggeeditem.getMyObjectKind();
				refObject = Instantiate(instanceObject[prefabkind], instancePosition, charactorq) as GameObject;
			}
			else {
				Debug.Log("noLeftItem");
			}
		}

		if (Input.GetMouseButton(0)) {//タッチされ続けている間、オブジェクトの位置を動かし続ける。
			setInstanceposFromMouse(2);
			if (refObject != null) {
				refObject.transform.position = instancePosition;
			}
			else {
			}
		}

		if (Input.GetMouseButtonUp(0)) {//タッチが離されたタイミングで、オブジェクトをつかんでいればnullを入れる。
			setInstanceposFromMouse(0);
			Vector3 indexVector3 = getIndexpos(instancePosition);//x,y,zが何番目の配列か調べる。
			if (manager.checkCanSet(indexVector3)&& refObject!=null) {//今のポジションのインデックスが配列内であり、セットできるのであれば
				draggeeditem.decreaseLeftCount();//レフトカウントを1減らす。
				refObject.transform.position = getRoundedgPos(instancePosition);
				manager.changeMapData(indexVector3, draggeeditem.getMyObjectKind());
				manager.updateCansetDatas(indexVector3);
				manager.updateCansetDatas(indexVector3);
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
		indexpos.x = (float)Math.Round(aPos.x / blocklength);
		indexpos.y = (float)Math.Round(aPos.y / blocklength);
		indexpos.z = (float)Math.Round(aPos.z / blocklength);
		return indexpos;
	}
	public Vector3 getRoundedgPos(Vector3 aPos) {//設置されるポジションを返すメソッド
		Vector3 roundedpos = new Vector3();
		roundedpos.x = getIndexpos(aPos).x * blocklength;
		roundedpos.y = getIndexpos(aPos).y * blocklength;
		roundedpos.z = getIndexpos(aPos).z * blocklength;
		return roundedpos;
	}
	public void setInstanceposFromMouse(int slideypos) {
		screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
		instancePosition.y = instancehight + slideypos;
	}
}