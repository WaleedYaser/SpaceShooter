using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundaries
{
	public float minX, maxX, minZ, maxZ;
}

public class PlayerController : MonoBehaviour {

	public float speed = 10;
	public float tiltFactor = 4;
	public Boundaries boundaries;

	public GameObject boltPrefab;
	public Transform shotSpawn;

	public float fireRate;
	private float nextTime;

	private Rigidbody myRigidBody;
	private float horizontalVelocity, verticalVelocity;
	private Vector3 shipVelocity;

	private AudioSource myAudioSource;

	private void Start()
	{
		myRigidBody = GetComponent<Rigidbody> ();
		myAudioSource = GetComponent<AudioSource> ();
	}

	private void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextTime) {
			nextTime = Time.time + fireRate;
			GameObject copy = Instantiate (boltPrefab, shotSpawn.position, shotSpawn.rotation);
			myAudioSource.Play ();
		}
	}

	private void FixedUpdate()
	{
		// Get input from the user
		horizontalVelocity = Input.GetAxis ("Horizontal");
		verticalVelocity = Input.GetAxis ("Vertical");

		// Update velocity
		shipVelocity = new Vector3 (horizontalVelocity, 0.0f, verticalVelocity) * speed;
		myRigidBody.velocity = shipVelocity;

		// Add some tilt
		myRigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, myRigidBody.velocity.x * -tiltFactor);

		// Clamp position to the play area
		myRigidBody.position = new Vector3 (
			Mathf.Clamp(myRigidBody.position.x, boundaries.minX, boundaries.maxX),
			0.0f,
			Mathf.Clamp(myRigidBody.position.z, boundaries.minZ, boundaries.maxZ)
		);
	}
}
