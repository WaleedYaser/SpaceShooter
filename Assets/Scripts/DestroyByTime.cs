using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float lifeTime = 2f;

	private void Start()
	{
		Destroy (gameObject, lifeTime);
	}
}
