using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TestLoadSceneAsync : MonoBehaviour {

	private AsyncOperation testasync;

	public IEnumerator LoadScene (string sceneName) {
		Debug.Log ("LoadSceneis called");
		testasync = Application.LoadLevelAsync (sceneName);
		testasync.allowSceneActivation = false;

		while (!testasync.isDone) {
			Debug.Log (testasync.progress * 100 + "%");
			yield return new WaitForSeconds (0);
		}
		yield return testasync;
	}

	public void sceneTransitionTest () {
		Debug.Log ("sceneTransitionTest is called");
		testasync.allowSceneActivation = true;
	}
	// private AsyncOperation async;

	// IEnumerator Start () { //指定文字列のシーンを読み込み開始するようにする処理

	// 	// 非同期でロード開始
	// 	async = Application.LoadLevelAsync ("NextScene");
	// 	// デフォルトはtrue。ロード完了したら勝手にシーンきりかえ発生しないよう設定。
	// 	async.allowSceneActivation = false;

	// 	// 非同期読み込み中の処理
	// 	while (!async.isDone) {
	// 		Debug.Log (async.progress * 100 + "%");

	// 		yield return new WaitForSeconds (0);
	// 	}
	// 	yield return async;
	// }
	// void Update () {
	// 	if (Input.GetMouseButtonDown (0)) {
	// 		// タッチしたら遷移する（検証用）
	// 		async.allowSceneActivation = true;
	// 	}
	// }
}