using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class MoveController : MonoBehaviour {

	
	Button m_btn;
	private Subject<int> subject = new Subject<int>();

	public IObservable<int> OnClicked {
		get { return subject; }
	}

	private void Start() {
		m_btn = GetComponent<Button>();
		m_btn.onClick.AddListener(delegate { subject.OnNext(1); });
	}
}


//値が変更された時に、自身の値を変更する