using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BombItem : MonoBehaviour {
	[SerializeField]
	GameObject countdowntextprafab;
	[SerializeField]
	GameObject bombeffectprefab;
	GameObject countdowntextobject;
	Text countdowntext;
	int bombCount = 3;

	void Start() {
		countdowntextobject = Instantiate(countdowntextprafab,this.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
		countdowntext = countdowntextobject.GetComponent<Text>();
	}

	void setThisObjectActive() {//場所に設置された瞬間から始まるカウントダウンをどうするかだが、
		StartCoroutine(waitANDinstance());
	}

	void OnTriggerEnter(Collider other) {//グラウンドに置かれた瞬間からカウントダウン開始
		if (other.gameObject.tag == "ground") {
			setThisObjectActive();
		}
	}

	private IEnumerator waitANDinstance() {
		countdowntextobject.transform.position = this.transform.position;
		for (int i = bombCount; i > 0; i--) {
			countdowntext.text = i.ToString();
			yield return new WaitForSeconds(1f);
		}
		Instantiate(bombeffectprefab);
		Destroy(this);
		yield break;
	}

}
