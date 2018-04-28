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

	public SimpleTouchPad touchPad;
	public SimpleAreaButton areaButton;

	private Rigidbody myRigidBody;
	private float horizontalVelocity, verticalVelocity;
	private Vector3 shipVelocity;

	private AudioSource myAudioSource;

	private Quaternion calibrationQuaternion;

	private void Start()
	{
		myRigidBody = GetComponent<Rigidbody> ();
		myAudioSource = GetComponent<AudioSource> ();
		CalibrateAccelerometer();
	}

	private void Update()
	{
// Standalone Input
		// if (Input.GetButton ("Fire1") && Time.time > nextTime) {
		// 	nextTime = Time.time + fireRate;
		// 	GameObject copy = Instantiate (boltPrefab, shotSpawn.position, shotSpawn.rotation);
		// 	myAudioSource.Play ();
		// }
// Mobile Input
		if (areaButton.CanFire() && Time.time > nextTime) {
			nextTime = Time.time + fireRate;
			GameObject copy = Instantiate (boltPrefab, shotSpawn.position, shotSpawn.rotation);
			myAudioSource.Play ();
		}
	}

	private void FixedUpdate()
	{
// Standalone Input
		// // Get input from the user
		// horizontalVelocity = Input.GetAxis ("Horizontal");
		// verticalVelocity = Input.GetAxis ("Vertical");

		// // Update velocity
		// shipVelocity = new Vector3 (horizontalVelocity, 0.0f, verticalVelocity) * speed;

// Mobile Input accelerometer
		// // Get input from the user
		// Vector3 rowAcceleration = Input.acceleration;
		// Vector3 acceleration = FixAcceleration(rowAcceleration);

		// // Update velocity
		// shipVelocity = new Vector3 (acceleration.x, 0.0f, acceleration.y) * speed;

// Mobile Input Touch
		Vector2 direction = touchPad.GetDirection();
		shipVelocity = new Vector3 (direction.x, 0.0f, direction.y) * speed;
// Rest of the code
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

	private void CalibrateAccelerometer()
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0, 0, -1), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
	}

	private Vector3 FixAcceleration(Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}
}
