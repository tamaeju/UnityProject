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

public class PanelView : MonoBehaviour {
	[SerializeField]
	CurrentStageData stagedataVO;

	[SerializeField]
	Text m_recentcountText;
	[SerializeField]
	Text m_recentmovecountText;
	[SerializeField]
	Text m_EffectOfcountText;
	[SerializeField]
	Text m_EffectOfmovecountText;


	private void RenewCountText(String renewtext) {
		m_recentcountText.text = renewtext.ToString();
		
	}
	private void RenewMovecountText(String renewtext) {
		m_recentmovecountText.text = renewtext.ToString();
		addEffectOfmovecountText();
	}

	private void addEffectOfmovecountText() {
		m_EffectOfmovecountText.text = "+1";
		RectTransform rect = m_EffectOfmovecountText.GetComponent<RectTransform>();
		rect.DOScale(new Vector3(2f, 2f, 2f), 1f).OnComplete(() =>
		m_EffectOfmovecountText.text = "");
	}

	private void Start() {
		var changeMC = stagedataVO.gameObject.ObserveEveryValueChanged(_ => stagedataVO.currentMoveCount);//currentMoveCountはパブリックじゃないといけない？
		changeMC.Subscribe(_ => this.RenewMovecountText(stagedataVO.GetMoveCount().ToString()));

		var changeCMC = stagedataVO.gameObject.ObserveEveryValueChanged(_ => stagedataVO.currentSum);//currentMoveCountはパブリックじゃないといけない？
		changeCMC.Subscribe(_ => this.RenewCountText(stagedataVO.GetCurrentSum().ToString()));
	}
}

//private Subject<string> actionsubject = new Subject<string>();

//public IObservable<string> CanvasScrolled {
//	get { return actionsubject; }
//)
//パットでかくなって消える感じ。
