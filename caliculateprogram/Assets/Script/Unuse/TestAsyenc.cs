using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class TestAsyenc : MonoBehaviour {//別スレッドでscene読み込みを行い、scene読み込みが終わったら、ロード遷移するメソッド
	//void Start() {
	//	StartCoroutine(GenerateLoadProcess("hogehoge"));
	//}

	private IEnumerator GenerateLoadProcess(string sceneName) {
		AsyncOperation asyncOperation = Application.LoadLevelAdditiveAsync(sceneName);
		asyncOperation.allowSceneActivation = false;
		while (asyncOperation.progress < 0.9f && asyncOperation.isDone == false)
			yield return null;
		asyncOperation.allowSceneActivation = true;
	}
}
//タイトル画面表示と同時にこのメソッドを用いて、読み込みを開始させる。
//↓
//