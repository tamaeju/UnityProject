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
	TextMesh countdowntext;
	int bombCount = 5;

	void Start() {
		countdowntextobject = Instantiate(countdowntextprafab,this.transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
		countdowntext = countdowntextobject.GetComponent<TextMesh>();
		setThisObjectActive();//テスト用にいったんここに置く
	}

	void setThisObjectActive() {//場所に設置された瞬間から始まるカウントダウンをどうするかだが、
		StartCoroutine(waitANDinstance());
	}

	private IEnumerator waitANDinstance() {

		for (int i = bombCount; i > 0; i--) {
			countdowntextobject.transform.position = this.transform.position;
			countdowntext.text = i.ToString();
			yield return new WaitForSeconds(1f);
		}
		Destroy(countdowntext.gameObject); 
		Instantiate(bombeffectprefab);
		Destroy(this);
		yield break;
	}

}
