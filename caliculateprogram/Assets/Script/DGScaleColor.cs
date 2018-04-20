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



public class DGScaleColor : MonoBehaviour {
	Transform transform;
	[SerializeField]
	TextMesh textmesh;
	private void Start() {
		transform = this.gameObject.GetComponent<Transform>();
		changeScale();
		changRamdomColor();
	}
	private void changeScale() {
		transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f).SetLoops(-1, LoopType.Yoyo).Play();
	}
	private void changRamdomColor() {
		Color rmdColor = new Color(UnityEngine.Random.Range(0f,1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
		// Imageの色変更
		DOTween.To(
			() => textmesh.color,                // 何を対象にするのか
			color => textmesh.color = color,    // 値の更新
			rmdColor,                    // 最終的な値
			1f
		// アニメーション時間
		).OnComplete(() => changRamdomColor());
	}

}
//.onComplete(
//			() => image.color,                // 何を対象にするのか
//			color => image.color = color,    // 値の更新
//			Color.yellow,                    // 最終的な値
//			3f
//			);