using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowScroller : MonoBehaviour, IPointerDownHandler {

	public void OnPointerDown (PointerEventData _data) {
		MoveThisObject ();
	}

	private void MoveThisObject () {
		RectTransform rectform = GetComponent<RectTransform> ();
		rectform.DOMove (new Vector3 (rectform.position.x, 2000f, rectform.position.z),
			1.0f
		).OnComplete (() => Destroy (this.gameObject));
	}

}