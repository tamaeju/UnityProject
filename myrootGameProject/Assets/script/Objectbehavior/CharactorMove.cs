using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class CharactorMove : MonoBehaviour {
	[SerializeField]
	protected GameObject countdowntextprafab;
	[SerializeField]
	protected GameObject curedeffectprefab;
	private GameObject targetobject;

	protected NavMeshAgent agent;
	protected instance3Dword wordmaker;

	float normalSpeed = 1.5f;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		normalSpeed = agent.speed;
	}

	
	public virtual void changeSpeed(float newspeed, float effecttime) {//スピードを変えるメソッド。変える時間と変わった速度を引数として保持する。
		wordmaker = gameObject.AddComponent<instance3Dword>();
		wordmaker.makeCountDownText (countdowntextprafab, (int)effecttime,curedeffectprefab);
		StartCoroutine(changeSpeedColutin(newspeed, effecttime));
	}
	public void makewordmaker() {

	}

	protected IEnumerator changeSpeedColutin(float newSpeed, float effecttime) {
		agent.speed = newSpeed;
		yield return new WaitForSeconds(effecttime);
		agent.speed = normalSpeed;
		yield break;
	}

	public void waitAndDo(Action act, float waittime)
	{
		varguColutinMethod(act, waittime);
	}

	protected IEnumerator varguColutinMethod(Action act, float waittime) {
		yield return new WaitForSeconds(waittime);
		act();
		yield break;
	}
	public void setDestination(GameObject target) {
		agent = GetComponent<NavMeshAgent>();
		targetobject = target;
		agent.destination = targetobject.transform.position;
	}
	//itemと触れあってからの挙動がおかしいのでチェックが必要と思われる。
	//
}
