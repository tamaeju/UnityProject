using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	[SerializeField]
	private GameObject effect;
	//接触された時に、キャンバスを出し、クリア表示を行う
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			
		}
		if (other.gameObject.tag == "Targetchara") {
			Destroy(other);
		}
	}
}