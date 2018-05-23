using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SelectsceneButton : MonoBehaviour { //レベル選択画面のボタンクラス
	Text mytext;
	[SerializeField]
	GameObject effectprefab;
	[SerializeField]
	int myStageCount;
	[SerializeField]
	Button btn;
	[SerializeField]
	GameObject m_clearedIconprefab;
	[SerializeField]
	GameObject m_Unplayblelaconprefab;
	private Subject<int> subject = new Subject<int> ();

	public IObservable<int> OnClickedStageButton {
		get { return subject; }
	}

	private void Start () {
		btn.onClick.AddListener (() => { changeScale (subject.OnNext); });
		//btn.onClick.AddListener(() => { subject.OnNext(myStageCount); });

	}

	public void RemoveButtonEvent () {
		Destroy (this);
	}

	public void changeThisText (int stage) {
		mytext = this.gameObject.GetComponentInChildren<Text> ();
		mytext.text = (stage).ToString ();
	}

	public void changeMystageCount (int stagecount) {
		myStageCount = stagecount;
	}
	public void ActiveClearedIcon () {
		m_clearedIconprefab.SetActive (true);
	}
	public void ActiveUnplaybleIcon () {
		m_Unplayblelaconprefab.SetActive (true);
	}
	private void changeScale (Action<int> act) {
		RectTransform recttrans = GetComponent<RectTransform> ();
		Sequence sequence = DOTween.Sequence ().OnStart (() => {
			recttrans.DOScale (new Vector3 (0.9f, 0.9f, 0.9f), 0.1f);
		}).Append (recttrans.DOScale (new Vector3 (1f, 1f, 1f), 0.2f)).OnComplete (() => act (myStageCount));
	}

}