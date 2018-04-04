using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BombItem : MonoBehaviour {
	[SerializeField]
	GameObject countdowntextprafab;
	[SerializeField]
	GameObject bombeffectprefab;
	[SerializeField]
	float explosionduration = 5;
	instance3Dword countdowntextmaker;



	void Start() {
		Debug.LogFormat(this.transform.position.y.ToString());
		var change = gameObject.ObserveEveryValueChanged(_ => this.transform.position.y);
		change.Subscribe(_ => setThisObjectActive());//hogeの値が変わると、呼ばれる
		Debug.LogFormat(this.transform.position.y.ToString());

		countdowntextmaker = gameObject.AddComponent<instance3Dword>();
		countdowntextmaker.makeCountDownText(countdowntextprafab, (int)explosionduration);
	}

	void setThisObjectActive() {//場所に設置された瞬間から始まるカウントダウンをどうするかだが、
		Debug.Log("clickedsetThisObjectActive");
		StartCoroutine(waitandInstance());
	}

	private IEnumerator waitandInstance() {

		for (int i = (int)explosionduration; i > 0; i--) {
			if (this.transform.position.y > 2) { i = (int)explosionduration; }
			yield return new WaitForSeconds(1f);
		}
		GameObject explosion = Instantiate(bombeffectprefab,this.transform.position,Quaternion.identity) as GameObject;
		yield return new WaitForSeconds(1f);
		Destroy(explosion);
		Destroy(this.gameObject);
		yield break;
	}
	//オブジェクトのy座標の位置が動いたら爆弾を動かし始める。
	//テキストプレハブを生成。
	//テキストプレハブの
	//秒毎にテキストプレハブの


}
