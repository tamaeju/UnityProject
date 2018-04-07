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
	int m_number;
	Vector2 m_pos;

	public void SetMyPos(Vector2 pos) {
		m_pos = pos;
	}


	public Vector2 GetMyPos() {
		return  m_pos;
	}

	void MovePosition(Vector2 afterpos) {
		Vector3 afterpos3 = new Vector3(afterpos.x, afterpos.y,0);
		this.transform.position = afterpos3;
	}
	public void ChangeMyNum(int num) {
		m_number = num;
	}
	public int GetMyNumber() {
		return m_number;
	}
}
