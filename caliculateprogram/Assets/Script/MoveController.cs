using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class MoveController : MonoBehaviour {
	
	Button m_btn;
	private Subject<int> subject = new Subject<int>();

	public IObservable<int> OnClicked {
		get { return subject; }
	}

	private void Start() {
		m_btn = this.gameObject.GetComponent<Button>();
		m_btn.onClick.AddListener(() => clickmethod());
	}
	public void clickmethod()
	{
		//changeScale();
		Debug.Log("clicked");
		subject.OnNext(1);
	}
	private void changeScale() {
		RectTransform recttrans = GetComponent<RectTransform>();
		Sequence sequence = DOTween.Sequence().OnStart(() => {
			recttrans.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
		}).Append(recttrans.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f));
	}
}


//値が変更された時に、自身の値を変更する