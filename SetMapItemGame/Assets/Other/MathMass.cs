using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathMass : MonoBehaviour {
	int m_number;
	massstate m_state;
	bool m_isgthrough;

	enum massstate {
		add,
		substract,
		multiplicate,
		divide
	}
	//値に自分の数字を投げた時に答えを返してほしい
	public int caliculate(int oldnum) {
		if(m_state == massstate.add){
			return oldnum + m_number;
		}
		if (m_state == massstate.substract) {
			return oldnum - m_number;
		}
		if (m_state == massstate.multiplicate) {
			return oldnum * m_number;
		}
		if (m_state == massstate.divide) {
			return oldnum / m_number;
		}
		else return 999999;
	}
	public bool isGoThrough() {
		return m_isgthrough;
	}
	public void changeThrough() {
		m_isgthrough = true;
	}
}
