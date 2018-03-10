using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtons_selectscene : MonoBehaviour {
	Text mytext;
	[SerializeField]
	GameObject effectprefab;

	public  void changeThisText(string textname) {
		mytext = this.gameObject.GetComponentInChildren<Text>();
		mytext.text = textname;
	}
	public void onClick() {
		Vector3 instancepos;
		instancepos = this.transform.position;
		instancepos.z = -10;
		Instantiate(effectprefab,this.transform.position,Quaternion.identity);
	}
}
