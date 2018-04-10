﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class MovingMass : MonoBehaviour {
	[SerializeField]//確認用にserializefieldにしている
	int m_number;
	[SerializeField]//確認用にserializefieldにしている
	int m_movecount;
	ReactiveProperty<Vector2> m_pos = new ReactiveProperty<Vector2>();

	void Start() {
		m_pos.AsObservable().Subscribe(_=> MovePosition(m_pos.Value));
	}

	public void SetMyPos(int posX, int posY){
		Vector2 newPos = new Vector2(posX, posY);
		m_pos.Value = newPos;

	}


	public Vector2 GetMyPos() {
		return m_pos.Value;
	}

	void MovePosition(Vector2 afterpos) {
		Vector2 newsetPos = settingObjectPos(afterpos);
		Vector3 afterpos3 = new Vector3(newsetPos.x, newsetPos.y,0);
		this.transform.position = afterpos3;
	}  
	public void ChangeMyNum(int num) {
		m_number = num;
	}

	public int GetMyNumber() {
		return m_number;
	}
	public void ChangeMyCount(int num) {
		m_movecount = num;
	}

	public void AddMyCount() {
		m_movecount++;
	}

	public int GetMyCount() {
		return m_movecount;
	}

	public Vector2 settingObjectPos(Vector2 afterpos) {
	
		return Config.settingObjectPos((int)afterpos.x, (int)afterpos.y);
	}
}