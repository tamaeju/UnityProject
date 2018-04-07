using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DataView : MonoBehaviour {

	ReactiveProperty<int> m_recentcount = new ReactiveProperty<int>(0);
	ReactiveProperty<int> m_recentmovecount = new ReactiveProperty<int>(0);

	[SerializeField]
	Text m_recentcountText;
	[SerializeField]
	Text m_recentmovecountText;

	private void RenewText() {
		m_recentcountText.text = m_recentcount.ToString();
		m_recentmovecountText.text = m_recentmovecount.ToString();
	}
	private void Start() {
		m_recentcount.Subscribe(_ => RenewText());
		m_recentmovecount.Subscribe(_ => RenewText());
	}
	public void ChangeRecentCount(int count) {
		m_recentcount.Value = count;
	}
	public void ChangeRecentMoveCount(int count) {
		m_recentmovecount.Value = count;
	}

}

//private Subject<string> actionsubject = new Subject<string>();

//public IObservable<string> CanvasScrolled {
//	get { return actionsubject; }
//}