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

public class DGScaleOnlyY : MonoBehaviour {

	[SerializeField]
	float increaserate;
	[SerializeField]
	float effecttime;

	private void Start () {
		changeScale ();
	}
	private void changeScale () {
		transform.DOScale (new Vector3 (1f, increaserate, 1f), effecttime);
	}
}
//LevelSelectScene