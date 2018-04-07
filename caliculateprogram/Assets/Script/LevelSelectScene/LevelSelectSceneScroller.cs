using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelectSceneScroller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {//タイトルのキャンバスを動かすためのコンポーネント、eventsystemを使用。
	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	Vector3 touchdownpos;
	Vector3 touchexitpos;
	Vector3 differencedistance;



	public void OnPointerEnter(PointerEventData eventData) {
		Vector3 screenPotsition, worldtapPosition;
		screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		worldtapPosition = Camera.main.ScreenToWorldPoint(screenPotsition);
		touchexitpos = worldtapPosition;
		Debug.LogFormat("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData ped) {
		Vector3 screenPotsition, worldtapPosition;
		screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		worldtapPosition = Camera.main.ScreenToWorldPoint(screenPotsition);
		touchexitpos = worldtapPosition;
		differencedistance = touchexitpos - touchdownpos;
		ChangeObjectPos();
		Debug.LogFormat("OnPointerExit");
	}

	public void OnPointerDown(PointerEventData ped) {
		Vector3 screenPotsition, worldtapPosition;
		screenPotsition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		worldtapPosition = Camera.main.ScreenToWorldPoint(screenPotsition);
		touchdownpos = worldtapPosition;
		Debug.LogFormat("OnPointerDown");
	}
	private void ChangeObjectPos() {
		rectform = this.GetComponent<RectTransform>();
		Vector3 newPos = new Vector3(rectform.position.x + differencedistance.x, rectform.position.y + differencedistance.y, rectform.position.z);
		this.GetComponent<RectTransform>().position = newPos;
	}
}
