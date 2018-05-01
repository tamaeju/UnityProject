using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class distinationSetter : MonoBehaviour {
	List<GameObject> targetobjects;
	GameObject goalobject;
	List<GameObject> aiderobjects;

	private void Start () {
		targetobjects = new List<GameObject> ();
		aiderobjects = new List<GameObject> ();
	}

	public void addtargetobject (GameObject obj) {
		targetobjects.Add (obj);
	}

	public void setgoalobject (GameObject obj) {
		goalobject = obj;
	}

	public void setAIDobject (GameObject obj) {
		aiderobjects.Add (obj);
	}

	public void setReachGoalMethod (Action act) {
		foreach (var item in targetobjects) {
			item.GetComponent<TargetMove> ().setincleaseEatCount (act);
		}
	}

	public void setAidReachGoalMethod (Action act) {
		foreach (var item in aiderobjects) {
			if (item != null) {
				item.GetComponent<AidCharactor> ().setdecleaseEatCount (act);
			}
		}
	}

	public void setditination () { //クリアコンディションマネージャーへの参照を持たせる
		foreach (var item in targetobjects) {
			item.GetComponent<TargetMove> ().setDestination (goalobject);
		}
	}
	public void setAidditination () { //クリアコンディションマネージャーへの参照を持たせる
		foreach (var item in aiderobjects) {
			if (item != null) {
				item.GetComponent<AidCharactor> ().setDestination (goalobject);
			}
		}
	}
}