using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombfire : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Targetchara") {
			Debug.Log("calleddesttroy");
			
			Destroy(other.gameObject);
		}
	}

}
