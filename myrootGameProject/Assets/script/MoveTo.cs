using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

	public Transform goal;

	void Start() {
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position;
	}
	public Vector3 returnMypos() {
		return this.transform.position;
	} 
}