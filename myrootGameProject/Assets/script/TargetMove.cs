using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : CharactorMove {//ゴール以外のマップのターゲットのクラス

	[SerializeField]GameObject effectprefab;
	ClearConditionManager clearconditioner;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Instantiate(effectprefab, this.transform.position, Quaternion.identity);
			Destroy(this);
		}
	}
	public void getclearconditioner(ClearConditionManager aclearconditioner) {
		clearconditioner = aclearconditioner;
	}
	void updateclearcondition() {//クリアコンディションの情報を更新する処理
		if (clearconditioner != null)
			clearconditioner.addRecentEatcount();
	}

}