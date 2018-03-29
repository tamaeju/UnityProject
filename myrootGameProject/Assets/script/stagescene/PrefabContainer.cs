using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabContainer : MonoBehaviour {

	[SerializeField]
	GameObject mapmassuipositionObject;//setbuttonsを入れる。UIを表示ONOFFするため
	[SerializeField]
	GameObject canvasposition;//leftcountをキャンバスにおくためだけのもの
	[SerializeField]
	GameObject ground;//groundの座標から設置位置を調整するため
	[SerializeField]
	GameObject[] instanceObjects = new GameObject[Config.blockkindlength];
	[SerializeField]
	GameObject[] itemObjects = new GameObject[Config.itemkindlength];
	[SerializeField]
	GameObject dragobjectmaker;
	[SerializeField] GameObject dragobjectleftcount;
	[SerializeField]
	GameObject word3Dprefab;

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
