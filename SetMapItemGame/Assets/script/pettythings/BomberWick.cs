using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UniRx;

public class BomberWick : MonoBehaviour {
	[SerializeField]
	private float angularFrequency = 4f;
	// 30FPS
	static readonly float DeltaTime = 0.0333f;


	void Start() {
		this.gameObject.transform.parent.transform.Rotate(new Vector3(50, 0, 0));
		float angular = 50;
		float time = 0.0f;
		Observable.Interval(TimeSpan.FromSeconds(DeltaTime)).Subscribe(_ =>
		{
			time += angularFrequency * DeltaTime;
			angular = Mathf.Sin(time) * 0.5f;
			this.gameObject.transform.parent.transform.Rotate(new Vector3(angular, 0, 0));
		}).AddTo(this);
	}
}


