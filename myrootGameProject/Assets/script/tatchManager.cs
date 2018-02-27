using UnityEngine;
using System.Collections;

public class tatchManager : MonoBehaviour {
	private static Vector3 TouchPosition = Vector3.zero;
	public GameObject instanceObject;
	GameObject refObject;
	public int[] LeftCount;
	Quaternion charactorq;
	Vector3 instancePosition;
	Vector3 setPosition;
	Vector3 screenPotsition;

	bool Istatch;
	public float instanceVectorY = 4.5f;

	// Use this for initialization
	void Start () {
		LeftCount = new int[3];
		for (int i = 0; i < LeftCount.Length; i++) {
			LeftCount[i] = 2;
			charactorq = Quaternion.Euler(90f, 0f, 0f);

		}
	}
	
	// Update is called once per frame
	void Update () {
		//タッチされた時に、タッチされたオブジェクトを取得し、その場所に対応したプレハブを作成する。
		if (Input.GetMouseButtonDown(0)) {
			screenPotsition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z);
			instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			instancePosition.y = instanceVectorY;
			refObject = Instantiate(instanceObject, instancePosition, charactorq) as GameObject;

		}
		//タッチされ続けている間、オブジェクトの位置を動かし続ける。
		if (Input.GetMouseButton(0)) {
			screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			instancePosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			instancePosition.y = instanceVectorY;
			refObject.transform.position = instancePosition;


		}

		//タッチがはなされたタイミングで、オブジェクトの位置と対応する座標を割り出し、保持オブジェクトを設置可能かを受け取る。
		//もし設置可能であれば、置き、設置カウントを減らす
		if (Input.GetMouseButtonUp(0)) {
			setPosition = Camera.main.ScreenToWorldPoint(screenPotsition);
			setPosition.y = instanceVectorY;
			Destroy(refObject);
		}
	}






}
