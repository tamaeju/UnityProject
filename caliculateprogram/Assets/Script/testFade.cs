using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class testFade : MonoBehaviour {

	private bool isMainColor = false;
	[SerializeField]
	Color color1 = Color.white, color2 = Color.white;
	[SerializeField]
	UnityEngine.UI.Image image = null;

	[SerializeField]
	CanvasGroup group = null;

	[SerializeField]
	Fade fade = null;

	public void Fadeout() {
		group.blocksRaycasts = false;
		fade.FadeIn(1, () => {
			image.color = (isMainColor) ? color1 : color2;
			isMainColor = !isMainColor;
		});
	}
}
