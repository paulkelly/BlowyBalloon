using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BalloonFaceAnim))]
public class BalloonMovement : MonoBehaviour, GameEvents.GameEventListener
{
	public bool popped = false;

	public float fanRadius = 0.4f;
	public float fanForce = 15f;
	
	public float windForce = 18f;

	Vector2 fanOffset = new Vector2(0, 0.4f);

	Vector2 fanDirection = Vector3.right;
	Vector3 fanRotation = Vector3.right;
	
	float fanPower = 0f;
	float getBlownStrength = 0.4f;

	GameObject fan;
	GameObject balloon;
	
	ParticleSystem fanParticles;
	ParticleSystem confetti;

	// Use this for initialization
	void Start ()
	{
		GameEvents.GameEventManager.registerListener(this);
		
		fan = transform.FindChild ("Fan").gameObject;
		balloon = transform.FindChild ("Balloon").FindChild("Face").gameObject;
		fanParticles = fan.transform.FindChild("Particle System").GetComponent<ParticleSystem>();
		fanParticles.enableEmission = false;
		confetti = transform.FindChild ("Balloon").FindChild("Confetti").GetComponent<ParticleSystem>();
		fanDirection = fanRotation * fanRadius;
	}
	
	void OnDisable()
	{
		GameEvents.GameEventManager.unregisterListener(this);
	}

	void FixedUpdate ()
	{
		
		if (fanPower > 0)
		{
			Vector2 force = fanDirection.normalized * -fanForce * fanPower;
			force.y = force.y * 4;
			transform.rigidbody2D.AddForce(force);
		}
		
		if(!popped)
		{
			transform.rigidbody2D.AddForce(new Vector2(windForce, 0));
		}

		fan.transform.position = new Vector3(transform.position.x + fanDirection.x + fanOffset.x, transform.position.y + fanDirection.y + fanOffset.y, fan.transform.position.z);

		float maxRotateAmount = 15f;
		//The speed at which maximum rotation is achieved
		float maxRotationSpeed = 6;
		float rotateAmount = ((rigidbody2D.velocity.magnitude / maxRotationSpeed) * maxRotateAmount);
		if(rotateAmount > maxRotateAmount)
		{
			rotateAmount = maxRotateAmount;
		}

		Quaternion rotateTo = Quaternion.Euler (new Vector3 (rigidbody2D.velocity.normalized.y * rotateAmount, rigidbody2D.velocity.normalized.x * -rotateAmount, 0));

		balloon.transform.rotation = Quaternion.Slerp (balloon.transform.rotation, rotateTo, 1f);
	}

	public void BlowOther(Transform other)
	{
		if(other.GetComponent<BalloonMovement>() != null && fan != null)
		{
			other.GetComponent<BalloonMovement>().GetBlown (fan.transform, fanPower);
		}
	}

	void GetBlown(Transform blower, float otherFanPower)
	{
		if(popped)
		{
			return;
		}

		if (otherFanPower > 0)
		{
			Vector2 pushDirection = new Vector2(transform.position.x - blower.position.x, transform.position.y - blower.position.y);
			float magnitude = 13 - pushDirection.magnitude;

			magnitude = magnitude * magnitude;

			transform.rigidbody2D.AddForce(pushDirection.normalized * magnitude * otherFanPower * getBlownStrength);
		}
	}

	public void SetFanPower(float power)
	{

		if(popped)
		{
			fanPower = 0;
			fanParticles.enableEmission = false;
			return;
		}

		fanPower = power;
		if(fanPower > 0)
		{
			fanParticles.enableEmission = true;
		}
		else
		{
			fanParticles.enableEmission = false;
		}
	}

	public void Move(float h, float v)
	{
		if(popped)
		{
			return;
		}

		Vector2 direction = new Vector2 (h, v);

		if (direction.magnitude > 0.6f)
		{
			fanDirection = direction.normalized * fanRadius;
			fanRotation = new Vector3( 0, 0, Mathf.Atan2(v, h) * 180 / Mathf.PI);
			fan.transform.rotation = Quaternion.Euler (fanRotation);
		}
	}

	public void Pop()
	{
		if(popped)
		{
			return;
		}
		
		confetti.Play();
		SoundManager.Instance.PlayPop();
		popped = true;
		rigidbody2D.velocity = Vector2.zero;
		GetComponent<BalloonFaceAnim> ().Pop ();
	}

	public void Reset(Vector3 pos)
	{
		rigidbody2D.velocity = Vector2.zero;
		transform.position = pos;
		popped = false;
		GetComponent<BalloonFaceAnim> ().Reset ();
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag.Equals("BarbedWire"))
		{
			GetComponent<BalloonFaceAnim>().Attack(true);
		}
	}
	
	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag.Equals("BarbedWire"))
		{
			GetComponent<BalloonFaceAnim>().Attack(false);
		}
	}
	
	public void receiveEvent(GameEvents.GameEvent e)
	{
		if(e.GetType().Name.Equals("CelebrateEvent"))
		{
			GetComponent<BalloonFaceAnim>().Celebrate();
		}
		
	}
}
