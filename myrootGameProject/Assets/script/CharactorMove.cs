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

	[SerializeField] private GameObject targetobject;
	NavMeshAgent agent;
	[SerializeField]
	GameObject effectwordprefab;
	GameObject effectwordobject;
	float normalSpeed;

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		normalSpeed = agent.speed;
		changeSpeed(0.1f,4);
	}


	//ゴールオブジェクト
	public void changeSpeed(float newspeed, float effecttime) {//スピードを変えるメソッド。変える時間と変わった速度を引数として保持する。
		//effectプレハブを作成し、そのテキストを1秒立つごとに変化させる。

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
	}//たどり着かせなければ勝ちのルール追加。
	 //イライラ棒システム
}
