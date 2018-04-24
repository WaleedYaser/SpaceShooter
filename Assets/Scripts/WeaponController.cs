using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject boltPrefab;
	public Transform shotSpawn;

	public float dalayTime;
	public float fireRate;

	private AudioSource myAudioSource;

	void Start () {
		myAudioSource = GetComponent<AudioSource>();
		InvokeRepeating("Fire", dalayTime, fireRate);
	}

	private void Fire()
	{
		myAudioSource.Play();
		Instantiate(boltPrefab, shotSpawn.position, shotSpawn.rotation);
	}

}
