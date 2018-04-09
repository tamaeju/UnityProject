using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PanelView : MonoBehaviour {
	[SerializeField]
	CurrentStageData stagedataVO;

	[SerializeField]
	Text m_recentcountText;
	[SerializeField]
	Text m_recentmovecountText;

	private void RenewCountText(String renewtext) {
		m_recentcountText.text = renewtext.ToString();
		
	}
	private void RenewMovecountText(String renewtext) {
		m_recentmovecountText.text = renewtext.ToString();
	}

	private void Start() {
		var changeMC = stagedataVO.gameObject.ObserveEveryValueChanged(_ => stagedataVO.currentMoveCount);//currentMoveCountはパブリックじゃないといけない？
		changeMC.Subscribe(_ => this.RenewMovecountText(stagedataVO.GetMoveCount().ToString()));

		var changeCMC = stagedataVO.gameObject.ObserveEveryValueChanged(_ => stagedataVO.currentCaliculationQuantity);//currentMoveCountはパブリックじゃないといけない？
		changeCMC.Subscribe(_ => this.RenewMovecountText(stagedataVO.GettargetCaliculationQuantity().ToString()));
	}
}

//private Subject<string> actionsubject = new Subject<string>();

//public IObservable<string> CanvasScrolled {
//	get { return actionsubject; }
//}