using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DGRotateSmall : MonoBehaviour { //星が永続的にくるくるする

	void Start () {
		Sequence seq = DOTween.Sequence ();
		seq.Append (
			transform.DORotate (new Vector3 (0f, 0f, 15f), // 終了時点のRotation
				1f // アニメーション時間
			)
		);
		seq.Append (
			transform.DORotate (new Vector3 (0f, 0f, 0f), // 終了時点のRotation
				1f // アニメーション時間
			)
		);
		seq.Append (
			transform.DORotate (new Vector3 (0f, 0f, -15f), // 終了時点のRotation
				1f // アニメーション時間
			)
		);

		seq.Append (
			transform.DORotate (new Vector3 (0f, 0f, 0f), // 終了時点のRotation
				1f // アニメーション時間
			)
		);
		seq.Play ().SetLoops (100);
	}

	void RoatateRightSlitely () {
		var tween = transform.DORotate (new Vector3 (0f, 0f, 30f), // 終了時点のRotation
			3f // アニメーション時間
		);
	}
	void RoatateLeftSlitely () {
		var tween = transform.DORotate (new Vector3 (0f, 0f, -30f), // 終了時点のRotation
			3f // アニメーション時間
		);
	}
	void RoatateDefalutRotate () {
		var tween = transform.DORotate (new Vector3 (0f, 0f, 0f), // 終了時点のRotation
			3f // アニメーション時間
		);
	}

}