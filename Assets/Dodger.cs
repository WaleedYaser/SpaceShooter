using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodger : MonoBehaviour {

	public float dodge;
	public float dodgeSpeed;
	public float tilt;

	public Vector2 dodgeDelay;
	public Vector2 dodgeTime;
	public Vector2 dodgeWait;
	
	public Boundaries boundaries;

	private float targetDodge;
	private Rigidbody myRigidBody;

	void Start () {
		myRigidBody = GetComponent<Rigidbody>();
		StartCoroutine(Dodge());
	}
	
	IEnumerator Dodge()
	{
		yield return new WaitForSeconds(Random.Range(dodgeDelay.x, dodgeDelay.y));

		while(true)
		{
			targetDodge = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(dodgeTime.x, dodgeTime.y));
			targetDodge = 0;
			yield return new WaitForSeconds(Random.Range(dodgeWait.x, dodgeWait.y));
		}
	}

	void FixedUpdate () {
		float newDodge = Mathf.MoveTowards(myRigidBody.velocity.x, targetDodge, Time.deltaTime * dodgeSpeed);
		myRigidBody.velocity = new Vector3(newDodge, 0, myRigidBody.velocity.z);

		myRigidBody.position = new Vector3(
			Mathf.Clamp(myRigidBody.position.x, boundaries.minX, boundaries.maxX),
			0,
			Mathf.Clamp(myRigidBody.position.z, boundaries.minZ, boundaries.maxZ)
		);

		myRigidBody.rotation = Quaternion.Euler(0, 0, -myRigidBody.velocity.x * tilt);
	}
}
