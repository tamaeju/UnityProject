using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class StageUIMaker : MonoBehaviour {
	[SerializeField]
	GameObject[] UIpos;
	[SerializeField]
	GameObject UIprefab;
	public void makestageUI (string labelName, ReactiveProperty<int> RXcount, int pos) {
		GameObject instanceObject = Instantiate (UIprefab, UIpos[pos].transform.position, Quaternion.identity, UIpos[pos].transform) as GameObject; //ゲームオブジェクトの作成を行う。
		instanceObject.GetComponent<UIobject> ().instanceUIobject (labelName, RXcount);
	}
	public enum displayposition {
		leftupper,
		leftmiddle,
		leftbellow,
		centerupper,
		centermiddle,
		centerbellow,
		rightupper,
		rightmiddle,
		rightbellow,

	}

}