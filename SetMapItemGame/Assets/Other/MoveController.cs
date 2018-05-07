using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class MoveController : MonoBehaviour {
	//ボタンを作成したい。上を押したら上に移動する形にする。
	//こいつ自身は誕生したら自分自身に
	[SerializeField]
	Button m_btn;
	private Subject<int> subject = new Subject<int>();

	public IObservable<int> OnClicked {
		get { return subject; }
	}

	private void Start() {
		m_btn.onClick.AddListener(delegate { subject.OnNext(1); });
	}
}
