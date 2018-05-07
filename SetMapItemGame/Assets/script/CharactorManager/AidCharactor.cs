using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AidCharactor : CharactorMove { //ゴールオブジェクトと接したら逆に回復させる
	[SerializeField]
	GameObject effectprefab;
	ClearConditionManager clearconditioner;
	Action decreaseEatCountmethod;

	//問題点としては、最初にアニメーションが切り替わらな事。

	public void decleaseEatCount () { //クリアコンディションの情報を更新する処理
		decreaseEatCountmethod ();
	}
	public void setdecleaseEatCount (Action act) { //クリアコンディションの情報を更新する処理
		decreaseEatCountmethod = act;
	}

	private void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Goal") {
			StartCoroutine (delaydestroy ());
			Instantiate (effectprefab, this.gameObject.transform.position, Quaternion.identity);
			decleaseEatCount ();
		}
	}

	private IEnumerator delaydestroy () {
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
		yield break;
	}

}