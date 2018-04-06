using System.Collections;using UniRx;using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ControllerMethodSetter : MonoBehaviour {
	[SerializeField]
	MoveController m_upbutton;
	[SerializeField]
	MoveController m_downbutton;
	[SerializeField]
	MoveController m_rightbutton;
	[SerializeField]
	MoveController m_leftbutton;

	public void setControllerMethod() {//ゲームオブジェクトをシリアライズフィールドで入れて、処理を実装
		m_upbutton.OnClicked.Subscribe(x => { Debug.LogFormat("入れたい処理"); });
		m_downbutton.OnClicked.Subscribe(x => { Debug.LogFormat("入れたい処理"); });
		m_rightbutton.OnClicked.Subscribe(x => { Debug.LogFormat("入れたい処理"); });
		m_leftbutton.OnClicked.Subscribe(x => { Debug.LogFormat("入れたい処理"); });
	}



}
