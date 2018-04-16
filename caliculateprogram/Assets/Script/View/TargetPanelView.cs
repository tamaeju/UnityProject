using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class TargetPanelView : MonoBehaviour {
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
		var changeMC = stagedataVO.gameObject.ObserveEveryValueChanged(_ => stagedataVO.targetMoveCount );//currentMoveCountはパブリックじゃないといけない？
		changeMC.Subscribe(_ => this.RenewMovecountText(stagedataVO.GetTargetMoveCount().ToString()));

		var changeCMC = stagedataVO.gameObject.ObserveEveryValueChanged(_ => stagedataVO.targetSum );//currentMoveCountはパブリックじゃないといけない？
		changeCMC.Subscribe(_ => this.RenewCountText(stagedataVO.GettargetSum().ToString()));
	}
}
