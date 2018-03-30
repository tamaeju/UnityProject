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

	private void Start() {
		targetobjects = new List<GameObject>();
	}

	public void addtargetobject(GameObject obj) {
		targetobjects.Add(obj);
	}

	public void setgoalobject(GameObject obj) {
		goalobject = obj;
	}

	public void setClearconditioner(ClearConditionManager claerconditioner) {
		foreach (var item in targetobjects) {
			item.GetComponent<TargetMove>().setincleaseEatCount(claerconditioner.addRecentEatcount);
		}
	}

	public void setditination() {//クリアコンディションマネージャーへの参照を持たせる
		foreach (var item in targetobjects) {
			item.GetComponent<TargetMove>().setDestination(goalobject);
		}
	}
}
