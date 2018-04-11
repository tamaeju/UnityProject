using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MoveController : MonoBehaviour {
	
	Button m_btn;
	private Subject<int> subject = new Subject<int>();

	public IObservable<int> OnClicked {
		get { return subject; }
	}

	private void Start() {
		m_btn = this.gameObject.GetComponent<Button>();
		Action act = () => clickmethod();
		m_btn.onClick.AddListener(() => act());
	}
	public void clickmethod()
	{
		Debug.Log("clicked");
		subject.OnNext(1);
	}
}


//値が変更された時に、自身の値を変更する