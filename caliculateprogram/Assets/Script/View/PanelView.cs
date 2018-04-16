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
	IObservable<int> changeMC;
	IObservable<int> changeCMC;
	int Countdefference;

	private void Start() {
		normalposSumEffectpos = m_EffectOfcountText.GetComponent<RectTransform>().anchoredPosition;
		normalposMoveEffectpos = m_EffectOfmovecountText.GetComponent<RectTransform>().anchoredPosition;
		
		Debug.LogFormat("{0}{1}", normalposMoveEffectpos.x, normalposMoveEffectpos.y);
	}

	private void RenewCountText(int renewcount) {
		Countdefference = Countdefference + renewcount;
		m_recentcountText.text = renewcount.ToString();
	}

	private void RenewMovecountText(int renewcount) {

		m_recentmovecountText.text = renewcount.ToString();
	}

	private void createEffectOfCountText() {
		if (Countdefference > 0) {
			m_EffectOfcountText.text = "+"+ Countdefference.ToString();
				}
		else  {
			m_EffectOfcountText.text = "-" + Countdefference.ToString();
			RectTransform rect = m_EffectOfcountText.GetComponent<RectTransform>();
			//rect.position = normalpos;//trial
			rect.DOJumpAnchorPos(
		normalposSumEffectpos,      // 移動終了地点
		1,               // ジャンプする力
		2,               // ジャンプする回数
		1.0f              // アニメーション時間
	).OnComplete(() =>
			m_EffectOfcountText.text = "");
		}
	}

	private void createEffectOfmovecountText() {
		//m_EffectOfmovecountText.GetComponent<RectTransform>().position = normalpos;//連続動作による位置ずれを避けるため。
		m_EffectOfmovecountText.text = "+1";
		RectTransform rect = m_EffectOfmovecountText.GetComponent<RectTransform>();
		//rect.position = normalpos;//trial
		rect.DOJumpAnchorPos(
	normalposMoveEffectpos,      // 移動終了地点
	1,               // ジャンプする力
	2,               // ジャンプする回数
	1.0f              // アニメーション時間
).OnComplete(() =>
		m_EffectOfmovecountText.text = "");
	}

	public void registRenewCountEvent() {//currentデータの値を見て、イベント実行をするよう指定するメソッド
		changeMC = stagedataVO.gameObject.ObserveEveryValueChanged(_ => stagedataVO.currentMoveCount);
		changeMC.Subscribe(_ => this.RenewMovecountText(stagedataVO.GetMoveCount()));
		changeMC.Subscribe(_ => createEffectOfmovecountText());

		changeCMC = stagedataVO.gameObject.ObserveEveryValueChanged(_ => stagedataVO.currentSum);
		changeCMC.Subscribe(_ => this.RenewCountText(stagedataVO.GetCurrentSum()));
		changeCMC.Subscribe(_ => createEffectOfCountText());
	}


}

//private Subject<string> actionsubject = new Subject<string>();

//public IObservable<string> CanvasScrolled {
//	get { return actionsubject; }
//)
//パットでかくなって消える感じ。

//エフェクトを表示開始し始めるというイベント登録をどのタイミングで行うかを決定する必要がありそう。
//作成時にゲームシーンがパネルビューに、お前エフェクトを出し始めろよという命令を行う必要がある？
