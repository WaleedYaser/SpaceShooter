using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreAmount;

	private GameController myGameController;

	private void Start()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");

		if (gameControllerObject != null)
			myGameController = gameControllerObject.GetComponent<GameController> ();

		if (myGameController == null)
			Debug.LogError ("'GameController' Not found");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Boundary"))
			return;
		Instantiate (explosion, transform.position, transform.rotation);

		if (other.CompareTag ("Player")) {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			myGameController.GameOver ();
		}

		myGameController.AddScore (scoreAmount);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
