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
	GameObject countdowntextprafab;
	[SerializeField]
	GameObject curedeffectprefab;
	private GameObject targetobject;
	NavMeshAgent agent;
	instance3Dword wordmaker;
	float normalSpeed;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		normalSpeed = agent.speed;
	}

	
	public void changeSpeed(float newspeed, float effecttime) {//スピードを変えるメソッド。変える時間と変わった速度を引数として保持する。
		wordmaker = gameObject.AddComponent<instance3Dword>();
		wordmaker.getEffectTimePrefab(countdowntextprafab, curedeffectprefab);
		wordmaker.makeCountDownText();

		wordmaker.mekeEffectTimeWordcolutin((int)effecttime);
		StartCoroutine(changeSpeedColutin(newspeed, effecttime));
	}

	private IEnumerator changeSpeedColutin(float newSpeed, float effecttime) {
		agent.speed = newSpeed;
		yield return new WaitForSeconds(effecttime);
		agent.speed = normalSpeed;
		yield break;
	}

	public void waitAndDo(Action act, float waittime)
	{
		varguColutinMethod(act, waittime);
	}

	private IEnumerator varguColutinMethod(Action act, float waittime) {
		yield return new WaitForSeconds(waittime);
		act();
		yield break;
	}
	public void setDestination(GameObject target) {
		agent = GetComponent<NavMeshAgent>();
		targetobject = target;
		agent.destination = targetobject.transform.position;
	}
	public Vector3 getMyPosition() {
		return transform.position;
	}

}
