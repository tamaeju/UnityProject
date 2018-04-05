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
	IObservable<float> change;


	void Start() {
		change = this.gameObject.ObserveEveryValueChanged(_ => this.gameObject.transform.position.y);//トランスフォームyの値が変わった時にイベント発火するよう設定。


		countdowntextmaker = gameObject.AddComponent<instance3Dword>();
		countdowntextmaker.makeCountDownText(countdowntextprafab, (int)explosionduration);
		change.Subscribe(_ => setThisObjectActive());//changeへイベントの登録
		//iobservableインターフェースは、イベントの登録と、発火タイミングを設定するインターフェース
	}

	void setThisObjectActive() {//場所に設置された瞬間から始まるカウントダウンをどうするかだが、
		if (this.gameObject.transform.position.y > 2) { return; }
		else {
			StartCoroutine(waitandInstance());
			Observable.FromCoroutine(waitandInstance) //waitandinstanceが終わった瞬間へイベントの登録
				.Subscribe(_ => Destroy(this.gameObject)).AddTo(this);
		}
	}


	private IEnumerator waitandInstance() {

		for (int i = (int)explosionduration; i > 0; i--) {
			yield return new WaitForSeconds(1f);
		}
		GameObject explosion = Instantiate(bombeffectprefab,this.transform.position,Quaternion.identity) as GameObject;
		yield return new WaitForSeconds(1f);
		Destroy(explosion);
		yield break;
	}
}
