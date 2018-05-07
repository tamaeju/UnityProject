using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class TargetMove : CharactorMove {//ゴール以外のマップのターゲットのクラス

	[SerializeField]GameObject effectprefab;
	ClearConditionManager clearconditioner;
	Action increaseEatCountmethod;
	private void Start() {
		changeMoveAnimation();
	}
	//問題点としては、最初にアニメーションが切り替わらな事。


	public void incleaseEatCount() {//クリアコンディションの情報を更新する処理
		increaseEatCountmethod();
	}
	public void setincleaseEatCount(Action act) {//クリアコンディションの情報を更新する処理
		increaseEatCountmethod = act;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Goal") {
			changeAttackAnimation();
		StartCoroutine(delaydestroy());
			incleaseEatCount();
		}
	}

	private IEnumerator delaydestroy() {
		yield return new WaitForSeconds(1f);
		Destroy(this.gameObject);
		yield break;
	}

	private void changeAttackAnimation() {
		Animator animatorcomponent = GetComponent<Animator>();
		agent.speed = 0;
		animatorcomponent.SetFloat("Speed", agent.speed);
		animatorcomponent.SetBool("Attack",true);
		Action act = () => animatorcomponent.SetBool("Attack", false);//セットブールをfalseにしてアニメーションを切り替える。
		varguColutinMethod(act, 3);//3秒アニメーション
	}

	private void changeMoveAnimation() {
		Animator animatorcomponent = GetComponent<Animator>();
		animatorcomponent.SetFloat("Speed", agent.speed);
	}

	public override void changeSpeed(float newspeed, float effecttime) {//スピードを変えるメソッド。スーパークラスのメソッドに対して、アニメーションを変更する処理を追加
		wordmaker = gameObject.AddComponent<instance3Dword>();
		wordmaker.makeCountDownText(countdowntextprafab, (int)effecttime, curedeffectprefab);

		changeMoveAnimation();
		StartCoroutine(changeSpeedColutin(newspeed, effecttime));
		changeMoveAnimation();
		Action act = () => { changeMoveAnimation(); };
		waitAndDo(act, effecttime);
	}


}
