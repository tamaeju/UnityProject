using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;
using DG.Tweening;



public class WindowScroller : MonoBehaviour,IPointerDownHandler {

	public void OnPointerDown(PointerEventData _data) {
		MoveThisObject();
	}

	private void MoveThisObject() {
		RectTransform rectform = GetComponent<RectTransform>();
		rectform.DOMove(new Vector3(rectform.position.x, 2000f, rectform.position.z),
	2.0f
	).OnComplete(() => Destroy(this.gameObject));
	}

}
