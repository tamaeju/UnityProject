using UnityEngine;
using System.Collections;

public class RayEmit{

	float objectHight = 4.5f;

	void Update () {
	}
	public Vector3 checkPos() {
		Vector3 pos = new Vector3();
		RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.down,out hit)) {
			if (hit.collider != null) {
				GameObject hitObj = hit.collider.gameObject;
				pos = hitObj.transform.position;
				pos.y = objectHight;
				return pos;
			}
			else {
				pos.x = -1;pos.z = -1;pos.y = -1; return pos;
			}
		}
		else {
			pos.x = -1; pos.z = -1; pos.y = -1; return pos;
		}
	}
	public GameObject getObject() {
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.down, out hit)) {
			if (hit.collider != null) {
				GameObject hitObj = hit.collider.gameObject;
				Debug.Log("getobj");
				Debug.Log(hitObj);
				return hitObj;

			}
			else { Debug.Log("null"); return null;  }
			
		}
		else { Debug.Log("null"); return null; }
	}
}
