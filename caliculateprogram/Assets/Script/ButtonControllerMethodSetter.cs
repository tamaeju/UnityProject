using System.Collections;
using UniRx;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControllerMethodSetter : MonoBehaviour {
	[SerializeField]
	MoveController m_upbutton;
	[SerializeField]
	MoveController m_downbutton;
	[SerializeField]
	MoveController m_rightbutton;
	[SerializeField]
	MoveController m_leftbutton;
	[SerializeField]
	MoveController m_backbutton;
	[SerializeField]
	MassMoveDealer massmovedealer;

	public void setControllerMethod() {//ゲームオブジェクトをシリアライズフィールドで入れて、処理を実装
		m_upbutton.OnClicked.Subscribe(x => { massmovedealer.pushUpButton() ; });
		m_downbutton.OnClicked.Subscribe(x => { massmovedealer.pushDownButton(); });
		m_rightbutton.OnClicked.Subscribe(x => { massmovedealer.pushRightButton(); });
		m_leftbutton.OnClicked.Subscribe(x => { massmovedealer.pushLeftButton(); });
		m_backbutton.OnClicked.Subscribe(x => { massmovedealer.pushBackButton(); });
	}



}
