using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : CharactorMove {

	[SerializeField]GameObject effectprefab;
	ClearConditionManager clearconditioner;
	//接触された時に、キャンバスを出し、クリア表示を行う
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Instantiate(effectprefab, this.transform.position, Quaternion.identity);
			Destroy(this);
		}
	}
	public void getclearconditioner(ClearConditionManager aclearconditioner) {
		clearconditioner = aclearconditioner;
	}
	void updateclearcondition() {
		if (clearconditioner != null)
			clearconditioner.addRecentEatcount();
	}
	//キャラクターと接触したら、キャラクターの動きを数秒間止めて、クリアコンディションの情報を更新する。これはオブザーバーパターンで実装したいが、
	//実際はターゲットキャラを作成したらその
	//
}