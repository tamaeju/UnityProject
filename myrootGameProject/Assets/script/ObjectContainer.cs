using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectContainer : MonoBehaviour {

	[SerializeField]
	GameObject mapmassuipositionObject;//setbuttonsを入れる。UIを表示ONOFFするため
	[SerializeField]
	GameObject canvasposition;//leftcountをキャンバスにおくためだけのもの
	[SerializeField]
	GameObject ground;//groundの座標から設置位置を調整するため
	[SerializeField]
	GameObject[] instanceObjects;
	[SerializeField]
	GameObject[] itemObjects;
	[SerializeField]
	GameObject dragobjectmaker;
	[SerializeField] GameObject dragobjectleftcount;

	public GameObject getobjectleftCount() {
		return dragobjectleftcount;
	}
	public GameObject getdragobjectmaker() {
		return dragobjectmaker;
	}
	public GameObject[] getitemObjects() {
		return itemObjects;
	}
	public GameObject[] getinstanceObjects() {
		return instanceObjects;
	}
	public GameObject getground() {
		return ground;
	}
	public GameObject getmapmassuipositionObject() {
		return mapmassuipositionObject;
	}
	public GameObject getcanvasposition() {
		return canvasposition;
	}
	
}
