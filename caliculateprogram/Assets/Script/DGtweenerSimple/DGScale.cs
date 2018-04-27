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


public class DGScale : MonoBehaviour {
	RectTransform recttrans;
[SerializeField]
float delaytime;
[SerializeField]
float increaserate;
[SerializeField]
float effecttime;


	private void Start() {
		recttrans = this.gameObject.GetComponent<RectTransform>();
		changeScale();
	}
	private void changeScale() {
		transform.DOScale(new Vector3(increaserate, increaserate, 1f), effecttime).SetDelay(delaytime);
	}
}
//LevelSelectScene