using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UniRx;

public class TiltObject : MonoBehaviour {

	[SerializeField]
	private float angularFrequency = 4f;
	// 30FPS
	static readonly float DeltaTime = 0.0333f;
	[SerializeField]
	private GameObject effect;

	private void Start() {
		this.gameObject.transform.transform.Rotate(new Vector3(-50, 180, 0));
		float angular = 10;
		float time = 0.0f;
		Observable.Interval(TimeSpan.FromSeconds(DeltaTime)).Subscribe(_ => {
			time += angularFrequency * DeltaTime;
			angular = Mathf.Sin(time) * 0.5f;
			this.gameObject.transform.transform.Rotate(new Vector3(angular, 0, 0));
		}).AddTo(this);
	}

}
