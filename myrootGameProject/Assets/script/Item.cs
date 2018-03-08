using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	GameObject playerobject;
	GameObject enemyobject;
	[SerializeField] private itemstate mystate;


	void OnTriggerEnter(Collider other)
	{
		if (mystate == itemstate.faster)
		{
			if (other.gameObject.tag == "Player")
			{
				playerobject = other.gameObject;
				playerobject.GetComponent<CharactorMove>().changeSpeed(5, 5);
			}
		}
		if (mystate == itemstate.slowdown)
		{
			if (other.gameObject.tag == "Enemy")
			{
				playerobject = other.gameObject;
				playerobject.GetComponent<CharactorMove>().changeSpeed(0.5f, 3f);
			}
		}
		if (mystate == itemstate.stop)
		{
			if (other.gameObject.tag == "Enemy")
			{
				playerobject = other.gameObject;
				playerobject.GetComponent<CharactorMove>().changeSpeed(0f, 3f);
			}
		}
	}
	public enum itemstate {
		faster,
		slowdown,
		stop,
	}
}
