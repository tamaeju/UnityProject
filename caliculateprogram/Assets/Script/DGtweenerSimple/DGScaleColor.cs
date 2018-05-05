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

public class DGScaleColor : MonoBehaviour { //文字の色を変えたり、オブジェクトのサイズを変えたり、オブジェクト自体の色を汎用的に変更するコンポーネント
	Transform transform;
	[SerializeField]
	TextMesh textmesh;
	[SerializeField]
	TextMesh goalMassEffect;
	[SerializeField]
	Renderer changebleRender;

	private void Start () {
		transform = this.gameObject.GetComponent<Transform> ();
		changeScale ();
		changTextRamdomColor ();
		goalMassEffectRamdomColor ();
	}
	private void changeScale () {
		transform.DOScale (new Vector3 (1.1f, 1.1f, 1.1f), 2f).SetLoops (-1, LoopType.Yoyo).Play ();
	}
	private void changTextRamdomColor () {
		if (textmesh != null) {
			Color rmdColor = new Color (UnityEngine.Random.Range (0f, 1f), UnityEngine.Random.Range (0f, 1f), UnityEngine.Random.Range (0f, 1f), 1f);
			// Imageの色変更
			DOTween.To (
				() => textmesh.color, // 何を対象にするのか
				color => textmesh.color = color, // 値の更新
				rmdColor, // 最終的な値
				1f
			).OnComplete (() => changTextRamdomColor ());
		}
	}
	private void goalMassEffectRamdomColor () {
		if (goalMassEffect != null) {
			Color rmdColor = new Color (UnityEngine.Random.Range (0f, 1f), UnityEngine.Random.Range (0f, 1f), UnityEngine.Random.Range (0f, 1f), 1f);
			// Imageの色変更
			DOTween.To (
				() => goalMassEffect.color, // 何を対象にするのか
				color => goalMassEffect.color = color, // 値の更新
				rmdColor, // 最終的な値
				1f
			).OnComplete (() => goalMassEffectRamdomColor ());
		}
	}
	private void changMaterialRamdomColor () {
		if (changebleRender != null) {
			Color rmdColor = new Color (UnityEngine.Random.Range (0f, 1f), UnityEngine.Random.Range (0f, 1f), UnityEngine.Random.Range (0f, 1f), 1f);
			// Imageの色変更
			DOTween.To (
				() => changebleRender.material.color, // 何を対象にするのか
				color => changebleRender.material.color = color, // 値の更新
				rmdColor, // 最終的な値
				1f
			).OnComplete (() => changMaterialRamdomColor ());
		}

	}

}