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

public class SelectsceneButton : MonoBehaviour {//レベル選択画面のボタンクラス
	Text mytext;
	[SerializeField]
	GameObject effectprefab;
	[SerializeField]
	int myStageCount;
	[SerializeField]
	Button btn;
	[SerializeField]
	GameObject m_clearedIconprefab;
	[SerializeField]
	GameObject m_Unplayblelaconprefab;



	private Subject<int> subject = new Subject<int>();

	public IObservable<int> OnClickedStageButton {
		get { return subject; }
	}

	private void Start() {
		btn.onClick.AddListener(() => { subject.OnNext(myStageCount); });//自身のステージを
		
	}

	public  void changeThisText(int stage) {
		mytext = this.gameObject.GetComponentInChildren<Text>();
		mytext.text = (stage+1).ToString();
	}

	public void changeMystageCount(int stagecount) {
		myStageCount = stagecount+1;
	}
	public void ActiveClearedIcon() {
		m_clearedIconprefab.SetActive(true);
	}
	public void ActiveUplaybleIcon() {
		m_Unplayblelaconprefab.SetActive(true);
	}
	//private void MoveParentsRect() {
	//	RectTransform  rectform = this.gameObject.transform.parent.parent.GetComponent<RectTransform>();
	//	rectform.DOMove(new Vector3(rectform.position.x, 2000f, rectform.position.z),
	//2.0f
	//).OnComplete(() => Destroy(this.gameObject));
	//}

	//public void makeEffectPrefab() {
	//	Vector3 instancepos;
	//	instancepos = this.transform.position;
	//	instancepos.z = -10;
	//	Instantiate(effectprefab, this.transform.position, Quaternion.identity);
	//}

	//	btn.onClick.AddListener(() => { MoveParentsRect();
	//});//自身のオブジェクトの移動メソッド
}
