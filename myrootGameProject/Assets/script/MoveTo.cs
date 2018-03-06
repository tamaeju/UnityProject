using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

	public Transform goal;
	UnityEngine.AI.NavMeshAgent agent;
	void Start() {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position;
	}
	public Vector3 returnMypos() {
		return this.transform.position;
	}
	public void changeSpeed(float anAfterspeed) {
		agent.speed = anAfterspeed;
	} 
}