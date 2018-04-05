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
using UniRx;

public class Goal : MonoBehaviour {

	[SerializeField]
	private GameObject effect;
	//接触された時に、キャンバスを出し、クリア表示を行う
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Targetchara") {
			Renderer rend = GetComponent<Renderer>();
			Color color = rend.material.color;
			color.a = 0.6f;
			rend.material.color = color;
			color.a = 1f;
			var timer = Observable.Timer(System.TimeSpan.FromSeconds(1));
			timer.Subscribe(_ => rend.material.color = color);
		}
	}
	void Update() {
		transform.Rotate(new Vector3(0, 1.5f, 0));
	}
}