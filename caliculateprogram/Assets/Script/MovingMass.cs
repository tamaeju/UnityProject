using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class MovingMass : MonoBehaviour {
	ReactiveProperty<int> m_number = new ReactiveProperty<int>();
	ReactiveProperty<Vector2> m_pos = new ReactiveProperty<Vector2>();
	Action m_act;

	void Start() {
		m_pos.Subscribe(_=> MovePosition(m_pos.Value));
		m_number.Subscribe(_ => m_act());
	}

	public void SetMyPos(int posX, int posY){
		Vector2 newPos = new Vector2(posX, posY);
		m_pos.Value = newPos;
	}


	public Vector2 GetMyPos() {
		return m_pos.Value;
	}

	void MovePosition(Vector2 afterpos) {
		Vector3 afterpos3 = new Vector3(afterpos.x, afterpos.y,0);
		this.transform.position = afterpos3;
	}
	public void ChangeMyNum(int num) {
		m_number.Value = num;
	}

	public int GetMyNumber() {
		return m_number.Value;
	}
}
