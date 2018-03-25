using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

	public readonly static float blocklength  = 0.9f;
	public readonly static int maxGridNum  = 10;
	public Transform groundtransform;
	public Vector3 firstblocklocalposition;
	public readonly static int stageCount = 100;
	public readonly static int dragbuttonNum = 3;
	public readonly static int itemkindlength = 8;
	public readonly static int blockkindlength = 12;

	public Vector3 getFirstblocklocalposition() {//groundが0,0,0の位置にあるならローカル座標が出る。
		firstblocklocalposition = new Vector3(0,0,0);
		firstblocklocalposition.x = groundtransform.position.x - (maxGridNum * blocklength / 2);
		firstblocklocalposition.y = groundtransform.position.y - (maxGridNum * blocklength / 2);
		firstblocklocalposition.z = groundtransform.position.z - (maxGridNum * blocklength / 2);
		return firstblocklocalposition;
	}

	public enum blockkind {
		road,
		block,
		player,
		mob,
		goal,
		item1,
		item2,
		item3,
		item4,
		item5,
		item6,
		item7
	}

}
