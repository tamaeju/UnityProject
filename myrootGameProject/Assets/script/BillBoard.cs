using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

	void Update() {
		Vector3 p = Camera.main.transform.position;
		//p.y = transform.position.y;//カメラの目線をプレイヤーの同水平位置に設定するための設定。
		transform.LookAt(p);
	}
}