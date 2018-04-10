using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {//ゲームのシステムや不変の設定を取り扱うクラス。

	public readonly static float blocklength  = 1f;
	public readonly static float blockshiftlength = 0.1f;
	public readonly static int maxGridNum  = 10;
	public Transform groundtransform;
	public Vector3 firstblocklocalposition;
	public readonly static int stageCount = 100;
	public readonly static int dragbuttonNum = 3;
	public readonly static int itemkindlength = 8;
	public readonly static int blockkindlength = 12;
	public readonly static 	int usecolomn_of_mapdata = 3;
	public readonly static int filekindlength = 3;

	public Vector3 getFirstblocklocalposition() {//groundが0,0,0の位置にあるならローカル座標が出る。
		firstblocklocalposition = new Vector3(0,0,0);
		firstblocklocalposition.x = groundtransform.position.x - (maxGridNum * blocklength / 2);
		firstblocklocalposition.y = groundtransform.position.y - (maxGridNum * blocklength / 2);
		firstblocklocalposition.z = groundtransform.position.z - (maxGridNum * blocklength / 2);
		return firstblocklocalposition;
	}

	public static Vector2 settingObjectPos(int x, int y) {
		Vector2 returnPos = new Vector2((x * blocklength) + x * blockshiftlength, (y * blocklength) + (y * blockshiftlength));
		return returnPos;
	}


}
