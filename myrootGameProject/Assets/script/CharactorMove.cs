using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;


public class CharactorMove : MonoBehaviour {

	public GameObject TargetObject;
	Action act;
	NavMeshAgent agent;


	void Awake() {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = TargetObject.transform.position;
		StartCoroutine(delaySetDestination(2f));
	}

	void Update()
	{
		//Debug.Log(TargetObject.transform.position);
		act = () =>{ Debug.Log(TargetObject.transform.position); };
		StartCoroutine(vargucolutinmethod(act, 3));
	}

	//ゴールオブジェクト
	public void changeSpeed(float newSpeed, float waittime) {//スピードを変えるメソッド。変える時間と変わった速度を引数として保持する。
		StartCoroutine(changespeedcolutin(5f, 5f));

	}

	private IEnumerator changespeedcolutin(float newSpeed, float effecttime) {
		float oldSpeed = agent.speed;
		yield return new WaitForSeconds(effecttime);
		agent.speed = newSpeed;
		yield break;
	}
	//2フレーム後にオブジェクトの位置を把握し、そこを目的値にするようなスクリプトを書きたい。
	private IEnumerator delaySetDestination(float waittime)
	{
		yield return new WaitForSeconds(waittime);
		agent.destination = TargetObject.transform.position;
		yield break;
	}

	private IEnumerator vargucolutinmethod(Action act, float waittime)
	{//戻り値なしのメソッドを引数としていただき、実行するメソッド 引数にはメソッドが入る。
		yield return new WaitForSeconds(waittime);
		Debug.Log("Called,vargucolutinmethod");
		act();
		yield break;
	}
}
