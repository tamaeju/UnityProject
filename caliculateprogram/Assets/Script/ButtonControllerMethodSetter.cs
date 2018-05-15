using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	[SerializeField]
	TestLoadSceneAsync testscenemanager;

	void Start () {
		setControllerMethod ();
	}

	public void setControllerMethod () { //ゲームオブジェクトをシリアライズフィールドで入れて、処理を実装
		m_upbutton.OnClicked.Subscribe (x => { massmovedealer.pushUpButton (); });
		m_downbutton.OnClicked.Subscribe (x => { massmovedealer.pushDownButton (); });
		m_rightbutton.OnClicked.Subscribe (x => { massmovedealer.pushRightButton (); });
		m_leftbutton.OnClicked.Subscribe (x => { massmovedealer.pushLeftButton (); });
		m_backbutton.OnClicked.Subscribe (_ => testscenemanager.sceneTransitionTest ());
	}

}