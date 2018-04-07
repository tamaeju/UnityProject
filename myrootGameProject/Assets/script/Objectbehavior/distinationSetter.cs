using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class distinationSetter : MonoBehaviour {
	List<GameObject> targetobjects;
	GameObject goalobject;
	GameObject aiderobjects;

	private void Start() {
		targetobjects = new List<GameObject>();
	}

	public void addtargetobject(GameObject obj) {
		targetobjects.Add(obj);
	}

	public void setgoalobject(GameObject obj) {
		goalobject = obj;
	}

	public void setAIDobject(GameObject obj) {
		aiderobjects = obj;
	}

	public void setReachGoalMethod(ClearConditionManager claerconditioner) {
		foreach (var item in targetobjects) {
			item.GetComponent<TargetMove>().setincleaseEatCount(claerconditioner.addRecentEatcount);
		}
	}

	public void setAidReachGoalMethod(ClearConditionManager claerconditioner) {
		aiderobjects.GetComponent<AidCharactor>().setdecleaseEatCount(claerconditioner.decleaseRecentEatcount);
	}

	public void setditination() {//クリアコンディションマネージャーへの参照を持たせる
		foreach (var item in targetobjects) {
			item.GetComponent<TargetMove>().setDestination(goalobject);
		}
	}
	public void setAidditination() {//クリアコンディションマネージャーへの参照を持たせる
		aiderobjects.GetComponent<AidCharactor>().setDestination(goalobject);
	}
}
