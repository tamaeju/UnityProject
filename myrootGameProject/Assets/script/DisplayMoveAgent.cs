using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMoveAgent : MonoBehaviour {
	[SerializeField]
	Scroller scrollobject;
	public void movescrollobject(Vector3 movedistance) {
		scrollobject.move(movedistance);
	}
	public void destroythisobject() {
		Destroy(this.gameObject);
	}
	

}
