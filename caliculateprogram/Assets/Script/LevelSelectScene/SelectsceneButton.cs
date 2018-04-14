using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SelectsceneButton : MonoBehaviour {//レベル選択画面のボタンクラス
	Text mytext;
	[SerializeField]
	GameObject effectprefab;
	[SerializeField]
	int myStageCount;
	[SerializeField]
	Button btn;

	private Subject<int> subject = new Subject<int>();

	public IObservable<int> OnClickedStageButton {
		get { return subject; }
	}

	private void Start() {
		btn.onClick.AddListener(() => { subject.OnNext(myStageCount); });
	}

	public  void changeThisText(string textname) {
		mytext = this.gameObject.GetComponentInChildren<Text>();
		mytext.text = textname;
	}

	public void changeMystageCount(int stagecount) {
		myStageCount = stagecount;
	}

	//public void makeEffectPrefab() {
	//	Vector3 instancepos;
	//	instancepos = this.transform.position;
	//	instancepos.z = -10;
	//	Instantiate(effectprefab, this.transform.position, Quaternion.identity);
	//}

}
