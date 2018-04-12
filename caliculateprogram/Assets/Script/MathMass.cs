using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MathMass : MonoBehaviour {
	int m_number;
	massstate m_state;
	Vector2 m_pos;
	bool isGoalMass;
	ReactiveProperty<bool> wasGothrough = new ReactiveProperty<bool>();

	[SerializeField]
	TextMesh m_masskindtext;
	//[SerializeField]
	//TextMesh m_masscounttext;

	public enum massstate {
		add,
		substract,
		multiplicate,
		divide,
		square,
		root
	}

	//値に自分の数字を投げた時に答えを返してほしい
	public int caliculate(int oldnum) {
		if(m_state == massstate.add){
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
			return oldnum = oldnum* oldnum;
		}
		if (m_state == massstate.root) {
			return oldnum = (int)Math.Sqrt(oldnum);
		}
		else return oldnum;
	}

	public bool isGoThrough() {
		return wasGothrough.Value;
	}

	public void ChangeThrough() {
		wasGothrough.Value = true;
		deliteTextObject();
		RoatateSlowly();
	}


	public void ChangeDarkColor() {
		if (wasGothrough.Value == true) {//ここでの条件判定をするのではなく、subscribeのところのwhereで判定すべき。注意
			Color newColor = this.GetComponent<Renderer>().material.color;
			newColor.r = 0.5f;
			newColor.g = 0.5f;
			newColor.b = 0.5f;
			newColor.a = 0.3f;
			this.GetComponent<Renderer>().material.color = newColor;
		}
	}

	public void deliteTextObject() {
		Destroy(m_masskindtext.gameObject);
		//Destroy(m_masscounttext);
}

	public void ChangeNormalColor() {
		Color newColor = this.GetComponent<Renderer>().material.color;
		newColor.r = 1f;
		newColor.g = 1f;
		newColor.b = 1f;
		newColor.a = 1f;
		this.GetComponent<Renderer>().material.color = newColor;
	}

	private void Start() {
		wasGothrough.AsObservable().Subscribe(_ => ChangeDarkColor());
	}

	private void RenewText() {
		m_masskindtext.text = GetMyString();
		//m_masscounttext.text = m_number.ToString();
	}

	public void ChangeMynumber(int num) {
		 m_number = num;
		RenewText();

	}
	public void ChangeMyKind(int kindnum) {
		 m_state = (massstate)Enum.ToObject(typeof(massstate), kindnum);
		RenewText();
	}

	private String GetMyString() {
		if (m_state == massstate.add) { return "+2"; }
		else if (m_state == massstate.substract) { return "-2"; }
		else if (m_state == massstate.multiplicate) { return "×2"; }
		else if (m_state == massstate.divide) { return "÷2"; }
		else if (m_state == massstate.square) { return "^2"; }
		else if (m_state == massstate.root) { return "√2"; }
		else if (isGoalMass == true) { return "Goal"; }
		else { return "ERR"; }
	}

	public void SetMyPos(int posX, int posY) {
		m_pos.x = posX;
		m_pos.y = posY;
	}

	public Vector2 GetMyPos() {
		return m_pos;
	}
	private void RoatateSlowly() {
		StartCoroutine(RoatateSlowlyColutin());
	}

	private IEnumerator RoatateSlowlyColutin() {//指定した距離を1秒かけて動くメソッド
		int totalRotateNumer = 120;
		for (int i = 0; i < totalRotateNumer; i++) {
			float totalRotateAmount = 90f;
			float eachRotateAmount = totalRotateAmount/ totalRotateNumer;
			this.transform.Rotate(0f, eachRotateAmount, 0f);
			yield return null;
		}
	}

	public bool isGoal() {
		return isGoalMass;
	}

	public void ChangeisGoal() {
		isGoalMass = true;
	}

}
