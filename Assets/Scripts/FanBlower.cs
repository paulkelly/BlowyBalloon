using UnityEngine;
using System.Collections;

public class FanBlower : MonoBehaviour
{

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.Equals (transform.parent.gameObject)) {
						return;
				}
		if (collider.gameObject.tag == "Player")
		{
			transform.parent.GetComponent<BalloonMovement> ().BlowOther (collider.transform);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.Equals (transform.parent.gameObject)) {
			return;
		}
		if (collider.gameObject.tag == "Player")
		{
			transform.parent.GetComponent<BalloonMovement> ().BlowOther (collider.transform);
		}
	}
}
