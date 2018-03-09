using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private itemstate mystate;
	[SerializeField]private GameObject effect;
	public void Start(int kindindex) {
		mystate = (itemstate)kindindex;
		changeMyColour();
	}

	void OnTriggerEnter(Collider other)
	{
		if (mystate == itemstate.faster)
		{
			if (other.gameObject.tag == "Player")
			{
				other.GetComponent<CharactorMove>().changeSpeed(5, 5);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}
		}
		if (mystate == itemstate.slowdown)
		{
			if (other.gameObject.tag == "Player")
			{
				other.GetComponent<CharactorMove>().changeSpeed(0.5f, 3f);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}
		}
		if (mystate == itemstate.stop)
		{
			if (other.gameObject.tag == "Player")
			{
				other.GetComponent<CharactorMove>().changeSpeed(0f, 3f);
				Instantiate(effect, this.transform.position, effect.transform.rotation);
				Destroy(this.gameObject);
			}
		}
	}
	public enum itemstate {
		faster,
		slowdown,
		stop,
	}
	public void changeMyColour()
	{

		if ((int)mystate == 0)
			GetComponent<Renderer>().material.color = Color.red;
		if ((int)mystate == 1)
			GetComponent<Renderer>().material.color = Color.blue;
		if ((int)mystate == 2)
			GetComponent<Renderer>().material.color = Color.green;
		if ((int)mystate == 3)
			GetComponent<Renderer>().material.color = Color.yellow;
	}
}
