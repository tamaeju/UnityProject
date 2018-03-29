using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : CharactorMove {//ゴール以外のマップのターゲットのクラス



	[SerializeField]GameObject effectprefab;
	ClearConditionManager clearconditioner;

	public void getclearconditioner(ClearConditionManager aclearconditioner) {
		clearconditioner = aclearconditioner;
	}
	public void incleaseEatCount() {//クリアコンディションの情報を更新する処理
		if (clearconditioner != null)
			clearconditioner.addRecentEatcount();
	}
	//ゴールと接触したら、自身のインクリーズイートカウントメソッドを実行する。
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Goal") {

			StartCoroutine(delaydestroy());
			incleaseEatCount();
		}
	}

	private IEnumerator delaydestroy() {
		yield return new WaitForSeconds(1f);
		Destroy(this.gameObject);
		yield break;
	}
}
