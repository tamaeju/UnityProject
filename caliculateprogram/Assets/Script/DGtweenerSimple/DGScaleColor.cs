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
	[SerializeField]
	TextMesh goalMassEffect;
	private void Start() {
		transform = this.gameObject.GetComponent<Transform>();
		changeScale();
		changTextRamdomColor();
		goalMassEffectRamdomColor();
	}
	private void changeScale() {
		transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f).SetLoops(-1, LoopType.Yoyo).Play();
	}
	private void changTextRamdomColor() {
		Color rmdColor = new Color(UnityEngine.Random.Range(0f,1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
		// Imageの色変更
		DOTween.To(
			() => textmesh.color,                // 何を対象にするのか
			color => textmesh.color = color,    // 値の更新
			rmdColor,                    // 最終的な値
			1f
		).OnComplete(() => changTextRamdomColor());
	}
	private void goalMassEffectRamdomColor() {
		Color rmdColor = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
		// Imageの色変更
		DOTween.To(
			() => goalMassEffect.color,                // 何を対象にするのか
			color => goalMassEffect.color = color,    // 値の更新
			rmdColor,                    // 最終的な値
			1f
		).OnComplete(() => goalMassEffectRamdomColor());
	}

}
