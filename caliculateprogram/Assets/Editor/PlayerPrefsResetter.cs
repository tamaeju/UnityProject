using UnityEditor;
using UnityEngine;

/// <summary>
/// PlayerPrefsを初期化するクラス
/// </summary>
public static class PlayerPrefsResetter {

	[MenuItem ("Tools/Reset PlayerPrefs")]
	public static void ResetPlayerPrefs () {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();

		Debug.Log ("Reset PlayerPrefs");
	}

}