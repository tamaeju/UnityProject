using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	[SerializeField]
	private GameObject effect;
	//接触された時に、キャンバスを出し、クリア表示を行う
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			
		}
	}
	void Update() {
		transform.Rotate(new Vector3(0, 1.5f, 0));
	}
}