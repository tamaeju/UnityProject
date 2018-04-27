using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialCanvasBehavior : MonoBehaviour, IPointerDownHandler { //タイトルのキャンバスを動かすためのコンポーネント、eventsystemを使用
	[SerializeField]
	Text titletext;
	[SerializeField]
	Image clearedsign;
	[SerializeField]
	Sprite[] stockImage;
	[SerializeField]
	GameObject m_displayimage;
	int m_stockImageNum;

	int m_stageNum;
	//メンバー変数にステージ番号をもらい、onnextはそれで実行する。

	Vector3 variableVector3 = new Vector3 ();
	RectTransform rectform;
	RectTransform RTElementTitle;

	private void Start () {
		m_stockImageNum = 0;
	}
	public void setStageNum (int stageNUm) {
		m_stageNum = stageNUm;
	}

	public void changeTitleText (string title) {
		titletext.text = title;
	}

	public void OnPointerDown (PointerEventData _data) {
		TutorialPlay();
	}

	private void MoveThisObject () {
		rectform = GetComponent<RectTransform> ();
		rectform.DOMove (new Vector3 (rectform.position.x, 2000f, rectform.position.z),
			3.0f
		).OnComplete (() => Destroy (this.gameObject));
	}


	private void TutorialPlay(){
		if (m_stockImageNum < stockImage.Length-1) {
			m_stockImageNum++;
			m_displayimage.GetComponent<Image>().sprite = stockImage[m_stockImageNum];
			}
		else{
			MoveThisObject();
		}

	}




	public void ClearedIconGetActive () {
		clearedsign.gameObject.SetActive (true);
	}

}