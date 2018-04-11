using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class ShaderTest : MonoBehaviour {
	void Start() {
		StartCoroutine(changeShader());
	}

	IEnumerator changeShader() {
		int totalRotateNumer = 30;
		float step = 1f / totalRotateNumer;
		for (int i = totalRotateNumer; i > -totalRotateNumer; i--) {
			GetComponent<Renderer>().material.SetFloat("_Flip", step* i);
			yield return null;
		}
	}
}
