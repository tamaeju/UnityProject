using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;


public class ScrollObject : MonoBehaviour {
	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	public float doubleratio = 1600f;
	public void changeposition(float scrollvalue) {
		rectform = GetComponent<RectTransform>();
		variableVector3 = rectform.position;
		variableVector3.y= scrollvalue * doubleratio;
		rectform.position = variableVector3;
	}
}
