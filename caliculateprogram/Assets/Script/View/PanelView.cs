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

public class PanelView : MonoBehaviour {
	[SerializeField]
	CurrentStageData stagedataVO;
	Vector2 normalposMoveEffectpos;
	Vector2 normalposSumEffectpos;
	[SerializeField]
	Text m_recentcountText;
	[SerializeField]
	Text m_recentmovecountText;
	[SerializeField]
	Text m_EffectOfcountText;
	[SerializeField]
	Text m_EffectOfmovecountText;
	[SerializeField]
	Text canGoalText;
	[SerializeField]
	Text notMatchGoalText;
	IObservable<long> changeMC;
	IObservable<long> changeCMC;
	long beforeCount;
	long countDifference;

	public void RenewCountText (long renewcount) {
		//出したいものとしては前回の数値との差分。
		countDifference = renewcount - beforeCount; //前回の入力値と今回の値の差分を取る（前回が5で今回が3なら差分は3）
		beforeCount = renewcount;
		m_recentcountText.text = renewcount.ToString ();
	}

	public void Start () {
		canGoalText.gameObject.SetActive (false);
		notMatchGoalText.gameObject.SetActive (false);
	}
	public void RenewMovecountText (long renewcount) {

		m_recentmovecountText.text = renewcount.ToString ();
	}

	private void createEffectOfCountText () {
		normalposSumEffectpos = m_EffectOfcountText.GetComponent<RectTransform> ().anchoredPosition;
		if (countDifference > 0) {
			m_EffectOfcountText.text = "+" + countDifference.ToString ();
		} else {
			m_EffectOfcountText.text = countDifference.ToString ();
		}
		RectTransform rect = m_EffectOfcountText.GetComponent<RectTransform> (); //rect.position = normalpos;//trial
		rect.DOJumpAnchorPos (normalposSumEffectpos, 1.5f, 2, 3f).OnComplete (() => m_EffectOfcountText.text = "");
	}

	private void createEffectOfmovecountText () {
		normalposMoveEffectpos = m_EffectOfmovecountText.GetComponent<RectTransform> ().anchoredPosition;
		m_EffectOfmovecountText.text = "+1";
		RectTransform rect = m_EffectOfmovecountText.GetComponent<RectTransform> ();
		rect.DOJumpAnchorPos (normalposMoveEffectpos, 1.5f, 2, 3f).OnComplete (() => m_EffectOfmovecountText.text = "");
	}

	public void createEffectOfCanGoal () {
		canGoalText.gameObject.SetActive (true);
		DOVirtual.DelayedCall (1f, () => canGoalText.gameObject.SetActive (false));
	}

	public void createEffectOfnotMatchGoalValue () {
		notMatchGoalText.gameObject.SetActive (true);
		DOVirtual.DelayedCall (1f, () => notMatchGoalText.gameObject.SetActive (false));
	}

	public void registRenewCountEvent () { //currentデータの値を見て、イベント実行をするよう指定するメソッド
		changeMC = stagedataVO.gameObject.ObserveEveryValueChanged (_ => stagedataVO.currentMoveCount);
		changeMC.Subscribe (_ => this.RenewMovecountText (stagedataVO.GetMoveCount ()));
		changeMC.Subscribe (_ => createEffectOfmovecountText ());

		changeCMC = stagedataVO.gameObject.ObserveEveryValueChanged (_ => stagedataVO.currentSum);
		changeCMC.Subscribe (_ => this.RenewCountText (stagedataVO.GetCurrentSum ()));
		changeCMC.Subscribe (_ => createEffectOfCountText ());
	}

}

//private Subject<string> actionsubject = new Subject<string>();

//public IObservable<string> CanvasScrolled {
//	get { return actionsubject; }
//)
//パットでかくなって消える感じ。

//エフェクトを表示開始し始めるというイベント登録をどのタイミングで行うかを決定する必要がありそう。
//作成時にゲームシーンがパネルビューに、お前エフェクトを出し始めろよという命令を行う必要がある？