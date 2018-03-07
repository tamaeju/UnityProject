using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;


public class CharactorMove : MonoBehaviour {

	[SerializeField] private GameObject targetobject;
	Action act;
	NavMeshAgent agent;


	void Start() {
		agent = GetComponent<NavMeshAgent>();
	}



	//ゴールオブジェクト
	public void changeSpeed(float newspeed, float waittime) {//スピードを変えるメソッド。変える時間と変わった速度を引数として保持する。
		StartCoroutine(changeSpeedColutin(5f, 5f));

	}

	private IEnumerator changeSpeedColutin(float newSpeed, float effecttime) {
		float oldSpeed = agent.speed;
		yield return new WaitForSeconds(effecttime);
		agent.speed = newSpeed;
		yield break;
	}
	//2フレーム後にオブジェクトの位置を把握し、そこを目的値にするようなスクリプトを書きたい。
	private IEnumerator delaySetDestination(float waittime) {
		yield return new WaitForSeconds(waittime);
		agent.destination = targetobject.transform.position;
		yield break;
	}

	private IEnumerator varguColutinMethod(Action act, float waittime) {//戻り値なしのメソッドを引数としていただき、実行するメソッド 引数にはメソッドが入る。
		yield return new WaitForSeconds(waittime);
		Debug.Log("Called,vargucolutinmethod");
		act();
		yield break;
	}
	public void setDestination(GameObject target) {//プレイヤーオブジェクトのエージェントの目的オブジェクトをゴールオブジェクトに変更。
		agent = GetComponent<NavMeshAgent>();
		targetobject = target;
		agent.destination = targetobject.transform.position;
		Debug.Log("calledsetdestination");
	}
	public Vector3 getMyPosition() {
		return transform.position;
	}

}
