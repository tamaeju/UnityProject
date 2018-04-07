using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class TargetDataView : MonoBehaviour {

	ReactiveProperty<int> m_recenttargetcount = new ReactiveProperty<int>(0);
	ReactiveProperty<int> m_recenttargetmovecount = new ReactiveProperty<int>(0);

	[SerializeField]
	Text m_recentcountText;
	[SerializeField]
	Text m_recentmovecountText;

	private void RenewText() {
		m_recentcountText.text = m_recenttargetcount.ToString();
		m_recentmovecountText.text = m_recenttargetmovecount.ToString();
	}
	private void Start() {
		m_recenttargetcount.Subscribe(_ => RenewText());
		m_recenttargetmovecount.Subscribe(_ => RenewText());
	}
	public void ChangeRecentTargetCount(int count) {
		m_recenttargetcount.Value = count;
	}
	public void ChangeRecentTargetMoveCount(int count) {
		m_recenttargetmovecount.Value = count;
	}

}
