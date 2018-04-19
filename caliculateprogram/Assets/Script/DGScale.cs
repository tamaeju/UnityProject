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
	Transform transform;
	private void Start() {
		transform = this.gameObject.GetComponent<Transform>();
	}
	private void changeScale() {
		transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f).SetLoops(-1, LoopType.Yoyo).Play();
	}
}
