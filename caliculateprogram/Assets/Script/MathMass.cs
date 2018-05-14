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

public class MathMass : MonoBehaviour {
	int m_number;
	massstate m_state;
	Vector2 m_pos;
	bool isGoalMass;
	ReactiveProperty<bool> wasGothrough = new ReactiveProperty<bool> ();

	[SerializeField]
	TextMesh m_masskindtext;

	[SerializeField]
	Material[] massMaterials;

	private void Start () {
		wasGothrough.AsObservable ().Subscribe (_ => ChangeDarkColor ());

	}
	public enum massstate {
		add,
		substract,
		multiplicate,
		divide,
		square,
		movingobject,
		goal,
		SAddtoSub,
		SSubtoAdd,
		SSubtoDiv,
		SMultodive,
		SdivetoMul,
		SIncreasetoDecrease
	}

	//値に自分の数字を投げた時に答えを返してほしい
	public int caliculate (int oldnum) {
		if (m_state == massstate.add) {
			return oldnum + 2;
		}
		if (m_state == massstate.substract) {
			return oldnum - 2;
		}
		if (m_state == massstate.multiplicate) {
			return oldnum * 2;
		}
		if (m_state == massstate.divide) {
			return oldnum / 2;
		}
		if (m_state == massstate.square) {
			return oldnum = oldnum * oldnum;
		} else return oldnum;
	}

	public bool isGoThrough () {
		return wasGothrough.Value;
	}

	public void ChangeThrough () {
		wasGothrough.Value = true;
		this.gameObject.transform.DORotate (new Vector3 (0f, 0f, 360f), 1.5f, RotateMode.FastBeyond360);
	}

	public void ChangeDarkColor () {
		if (wasGothrough.Value == true) { //ここでの条件判定をするのではなく、subscribeのところのwhereで判定すべき。注意
			Color newColor = this.GetComponent<Renderer> ().material.color;
			newColor.r = 0.6f;
			newColor.g = 0.6f;
			newColor.b = 0.6f;
			newColor.a = 0.5f;
			this.GetComponent<Renderer> ().material.color = newColor;
		}
	}

	public void deliteTextObject () {
		Destroy (m_masskindtext.gameObject);
	}

	private void RenewText () {
		//m_masskindtext.text = GetMyString();
		//m_masscounttext.text = m_number.ToString();
	}

	public void ChangeMynumber (int num) {
		m_number = num;
		RenewText ();

	}
	public void ChangeMyKind (int kindnum) {
		m_state = (massstate) Enum.ToObject (typeof (massstate), kindnum);
		RenewText ();
	}

	private String GetMyString () {
		if (m_state == massstate.add) { return "+2"; } else if (m_state == massstate.substract) { return "-2"; } else if (m_state == massstate.multiplicate) { return "×2"; } else if (m_state == massstate.divide) { return "÷2"; } else if (m_state == massstate.square) { return "^2"; } else if (isGoalMass == true) { return "Goal"; } else { return "ERR"; }
	}

	public void SetMyPos (int posX, int posY) {
		m_pos.x = posX;
		m_pos.y = posY;
	}

	public Vector2 GetMyPos () {
		return m_pos;
	}

	public void ChangeMyMaterial () {
		GetComponent<Renderer> ().material　 = massMaterials[(int) m_state];
	}

	public bool isGoal () {
		return isGoalMass;
	}

	public void ChangeisGoal () {
		isGoalMass = true;
		RenewText ();
	}
	public int GetMyKind () {
		return (int) m_state;
	}

}