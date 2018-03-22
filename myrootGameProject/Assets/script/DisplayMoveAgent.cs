using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMoveAgent : MonoBehaviour {
	[SerializeField]
	Scroller scrollobject;
	public void movescrollobject(Vector3 movedistance) {
		scrollobject.move(movedistance);
	}

	//タッチされている位置を取得し、キャンバスマスクの位置を、その位置分加算して返す。

}
