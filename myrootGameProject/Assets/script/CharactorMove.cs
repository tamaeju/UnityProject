using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharactorMove : MonoBehaviour {
	delegate void Action();
	public GameObject TargetObject;
	UnityEngine.AI.NavMeshAgent agent;

	void Start() {

		agent.SetDestination(TargetObject.transform.position);
	}

	public void changeSpeed(float newSpeed, float waittime) {//スピードを変えるメソッド。変える時間と変わった速度を引数として保持する。
		StartCoroutine(changespeedcolutin(5f, 5f));
		//checkClass cclas = new checkClass();
		//StartCoroutine(vargucolutinmethod(cclas.setvalue1, 5f));
		//StartCoroutine(vargucolutinmethod(cclas.setvalue2, 5f));
	}

	private IEnumerator changespeedcolutin(float newSpeed, float effecttime) {
		float oldSpeed = agent.speed;
		yield return new WaitForSeconds(effecttime);
		agent.speed = newSpeed;
		yield break;
	}
	//	private IEnumerator vargucolutinmethod(Action act, float effecttime) {//戻り値なしのメソッドを引数としていただき、実行するメソッド 引数にはメソッドが入る。
	//		Action acter = new Action(act);
	//		acter();
	//		yield return new WaitForSeconds(effecttime);
	//		yield break;
	//	}

	//}
	//class checkClass {
	//	public void setvalue1() { Debug.Log("check1"); }
	//	public void setvalue2() { Debug.Log("check2"); }
	//}
}
