using UnityEngine;
using System.Collections;

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
		instancehight = groundhight + 1;
	}

	public void makeDraggedObject(int objectKind) {

	}

	void Update() {

		if (Input.GetMouseButtonDown(0)) {
			screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			instancePosition.y = instancehight;
			if (rayemitter.getObject() != null) {
				dragger = rayemitter.getObject().GetComponent<MakeDraggedObject>();
				if (dragger.getObjectLeftCount() > 0) {
					int prefabkind = dragger.getMyObjectKind();
					refObject = Instantiate(instanceObject[prefabkind], instancePosition, charactorq) as GameObject;
					dragger.decreaseLeftCount();
				}
				else { Debug.Log("noLeftItem"); }
			}
			else { }
		}
		//タッチされ続けている間、オブジェクトの位置を動かし続ける。
		if (Input.GetMouseButton(0)) {
			screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			instancePosition.y = instancehight;
			if (refObject != null) {
				refObject.transform.position = instancePosition;
			}
		}

		//タッチがはなされたタイミングで、オブジェクトの位置と対応する座標を割り出し、保持オブジェクトを設置可能かを受け取る。
		//もし設置可能であれば、置き、設置カウントを減らす
		if (Input.GetMouseButtonUp(0)) {
			setPosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			setPosition.y = instancehight;
		}
	}
}
