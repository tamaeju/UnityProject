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
	GameObject m_EffectOfcountText;
	[SerializeField]
	GameObject m_EffectOfmovecountText;
	[SerializeField]
	GameObject canGoalText;
	[SerializeField]
	GameObject notMatchGoalText;
	[SerializeField]
	GameObject setSpecialMassText;
	IObservable<long> changeMC;
	IObservable<long> changeCMC;
	Vector3 oldSumCountPos;
	Vector3 oldMoveCountPos;
	long beforeCount;
	long countDifference;

	public void RenewCountText (long renewcount) {
		//出したいものとしては前回の数値との差分。
		countDifference = renewcount - beforeCount; //前回の入力値と今回の値の差分を取る（前回が5で今回が3なら差分は3）
		beforeCount = renewcount;
		m_recentcountText.text = renewcount.ToString ();
	}

	public void Start () {
		oldSumCountPos = m_EffectOfcountText.GetComponent<RectTransform> ().position; //現在のアンカーポジションを取得
		oldMoveCountPos = m_EffectOfmovecountText.GetComponent<RectTransform> ().position; //現在のアンカーポジションを取得
		m_EffectOfcountText.SetActive (false);
		m_EffectOfmovecountText.SetActive (false);
		canGoalText.gameObject.SetActive (false);
		notMatchGoalText.gameObject.SetActive (false);
		setSpecialMassText.gameObject.SetActive (false);
	}
	public void RenewMovecountText (long renewcount) {
		m_recentmovecountText.text = renewcount.ToString ();
	}

	private void createEffectOfCountText () {
		m_EffectOfcountText.GetComponent<RectTransform> ().position = oldSumCountPos;
		m_EffectOfcountText.SetActive (true);

		if (countDifference > 0) {
			m_EffectOfcountText.GetComponent<Text> ().text = "+" + countDifference.ToString ();
		} else {
			m_EffectOfcountText.GetComponent<Text> ().text = countDifference.ToString ();
		}
		//countdeiffernceを取得し、+なら+とつける
		Vector3 newPos = new Vector3 (oldSumCountPos.x, oldSumCountPos.y + 10f, oldSumCountPos.z);
		m_EffectOfcountText.GetComponent<RectTransform> ().DOMove (newPos, 1f).OnComplete (() => m_EffectOfcountText.SetActive (false));
	}

	private void createEffectOfmovecountText () {
		m_EffectOfmovecountText.GetComponent<RectTransform> ().position = oldMoveCountPos;
		m_EffectOfmovecountText.SetActive (true);
		m_EffectOfmovecountText.GetComponent<Text> ().text = "+1";
		Vector3 newPos = new Vector3 (oldMoveCountPos.x, oldMoveCountPos.y + 10f, oldMoveCountPos.z);
		m_EffectOfmovecountText.GetComponent<RectTransform> ().DOMove (newPos, 1f).OnComplete (() => m_EffectOfmovecountText.SetActive (false));
	}

	public void createEffectOfCanGoal () {
		canGoalText.gameObject.SetActive (true);
		DOVirtual.DelayedCall (1.5f, () => canGoalText.gameObject.SetActive (false));
	}

	public void createEffectOfnotMatchGoalValue () {
		notMatchGoalText.gameObject.SetActive (true);
		DOVirtual.DelayedCall (1.5f, () => notMatchGoalText.gameObject.SetActive (false));
	}
	public void createSpecialMassMessage () {
		setSpecialMassText.gameObject.SetActive (true);
		DOVirtual.DelayedCall (1.5f, () => setSpecialMassText.gameObject.SetActive (false));
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