using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPointRayEmiter : MonoBehaviour {
	public LayerMask mask;
	private void Update() {

		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit hit;
		
		float maxDistance = 100;

		if (Physics.Raycast(ray, out hit, maxDistance, mask))
		{
			//Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
			Debug.Log(hit.collider.gameObject.name);
		}

		//Rayを画面に表示
		//Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 5, false);

	}
}
