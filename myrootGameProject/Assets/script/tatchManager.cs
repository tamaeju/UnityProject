using UnityEngine;
using System.Collections;
using System;

public class tatchManager : MonoBehaviour {
	private static Vector3 TouchPosition = Vector3.zero;
	public GameObject[] instanceObject;
	GameObject refObject;
	public int[] LeftCount;
	Quaternion charactorq;
	Vector3 instancePosition;
	Vector3 setPosition;
	Vector3 screenPotsition;
	RayEmit rayemitter;
	MakeDraggedObject dragger;
	public GameObject ground;
	float groundhight;
	float instancehight;
	public GameObject groungrayemitter;
	private GameObject refrayObject;
	float blocklength = 0.9f;
	public GameObject Mapmanager;

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
		instancehight = groundhight+0.5f;
	}

	public void makeDraggedObject(int objectKind) {

	}

	void Update() {
		//タップしたらオブジェクトを置けちゃう問題はif文で、メイクドラッグドオブジェクトじゃなかったら以下の処理をしないでいいのでは。
		if (Input.GetMouseButtonDown(0)) {
			screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			instancePosition.y = instancehight + 2;
			if (rayemitter.getObject() != null) {
				if (rayemitter.getObject().GetComponent<MakeDraggedObject>().GetType() == typeof(MakeDraggedObject) && rayemitter.getObject().GetComponent<MakeDraggedObject>().getObjectLeftCount() > 0) {
					dragger = rayemitter.getObject().GetComponent<MakeDraggedObject>();
					int prefabkind = dragger.getMyObjectKind();
					refObject = Instantiate(instanceObject[prefabkind], instancePosition, charactorq) as GameObject;
					refrayObject = Instantiate(groungrayemitter, instancePosition, charactorq) as GameObject;
					dragger.decreaseLeftCount();
				}
			else { Debug.Log("noLeftItem"); }
			}
			else { rayemitter = null; }
		}
		//タッチされ続けている間、オブジェクトの位置を動かし続ける。
		if (Input.GetMouseButton(0)) {
			screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			instancePosition.y = instancehight + 2;
			if (refObject != null) {
				refObject.transform.position = instancePosition;
				refrayObject.transform.position = instancePosition;
			}
		}

		//タッチがはなされたタイミングで、オブジェクトの位置と対応する座標を割り出し、保持オブジェクトを設置可能かを受け取る。
		//もし設置可能であれば、置き、設置カウントを減らす
		if (Input.GetMouseButtonUp(0)) {
			screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			instancePosition.y = instancehight;
			if (refObject != null) {
				refObject.transform.position = getRoundedgpos(instancePosition);
				refrayObject.transform.position = getRoundedgpos(instancePosition);
				Mapmanager.GetComponent<LevelDesignCreate>().changeMapData(getindexpos(refObject.transform.position), dragger.getMyObjectKind());
				refObject = null;
				dragger = null;
			}
		}
		
	}
	public Vector3 getindexpos(Vector3 aPos) {//設置されているポジションのindexを返すメソッド
		Vector3 indexpos = new Vector3();
		indexpos.x = (float)Math.Round(aPos.x / blocklength);
		indexpos.y = (float)Math.Round(aPos.y / blocklength);
		indexpos.z = (float)Math.Round(aPos.z / blocklength);
		return indexpos;
	}
	public Vector3 getRoundedgpos(Vector3 aPos) {//設置されるポジションを返すメソッド
		Vector3 roundedpos = getindexpos(aPos);
		roundedpos.x = roundedpos.x * blocklength;
		roundedpos.y = roundedpos.y * blocklength;
		roundedpos.z = roundedpos.z * blocklength;
		return roundedpos;
	}

}