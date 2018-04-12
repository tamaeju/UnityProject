using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {//ゲームのシステムや不変の設定を取り扱うクラス。

	public readonly static float blocklength  = 1f;
	public readonly static float blockshiftlength = 0.1f;
	public readonly static int maxGridNum  = 10;
	public Vector3 firstblocklocalposition;
	public readonly static int stageCount = 100;

	public readonly static int massKindColoumnNum = 2;
	public readonly static int massCountColoumnNum = 3;

	public readonly static int clearMovecountColoumnNum = 1;
	public readonly static int clearnumberColoumnNum = 2;


	public static Vector2 settingObjectPos(int x, int y) {
		Vector2 returnPos = new Vector2((x * blocklength) + x * blockshiftlength, (y * blocklength) + (y * blockshiftlength));
		return returnPos;
	}


}
