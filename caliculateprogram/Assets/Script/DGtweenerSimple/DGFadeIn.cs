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



public class DGFadeIn : MonoBehaviour {
[SerializeField]
float delaytime;
	private void Start() {
		//changColor();
	}
	// private void changColor() {
	// 	Color thisColor = GetComponent<Image>().color;
	// 	Color afterColor = new Color(thisColor.r,thisColor.g,thisColor.b,1f);
	// 	GetComponent<Image>().color = new Color(thisColor.r,thisColor.g,thisColor.b,0f);
	// 	// Imageの色変更
	// 	DOTween.To(
	// 		() => GetComponent<Image>().color,                // 何を対象にするのか
	// 		color => GetComponent<Image>().color = color,    // 値の更新
	// 		afterColor,                    // 最終的な値
	// 		delaytime
	// 	);
	// }
}
