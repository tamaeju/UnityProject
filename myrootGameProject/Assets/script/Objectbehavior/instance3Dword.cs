using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class instance3Dword : MonoBehaviour {//このコンポーネントを所持しているオブジェクトは、mekeEffectTimeWordcolutinを使用する事によって、ワードを自在に出す事ができる。

	GameObject countdowntextprafab;
	GameObject curedeffectprefab;
	GameObject countdowntextobject;
	TextMesh countdowntext;

	public void makeCountDownText() {
		countdowntextobject = Instantiate(countdowntextprafab, this.transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
		countdowntext = countdowntextobject.GetComponent<TextMesh>();
	}

	public void mekeEffectTimeWordcolutin(int effecttime) {//場所に設置された瞬間から始まるカウントダウンをどうするかだが、
		StartCoroutine(mekeEffectTimeWord(effecttime));
	}

	private IEnumerator mekeEffectTimeWord(int effecttime) {
		for (int i = effecttime; i > 0; i--) {

			countdowntextobject.transform.position = this.gameObject.transform.position;
			countdowntext.text = i.ToString();
			yield return new WaitForSeconds(1f);
		}
		Destroy(countdowntextobject);
		Destroy(this);
		Instantiate(curedeffectprefab);
		yield break;
	}
	public void getEffectTimePrefab(GameObject textprafab, GameObject effectprefab) {
		countdowntextprafab = textprafab;
		curedeffectprefab = effectprefab;
	}


}

