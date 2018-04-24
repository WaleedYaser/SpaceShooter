using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	public GameObject[] enemies;
	public Vector3 spawnValues;

	public int enemyCount = 10;
	public float startWait = 5f;
	public float spawnWait = 0.5f;
	public float waveWait = 4f;

	public Text scoreText;
	public Text gameOverText;
	public Text restartText;

	private int score;
	private bool gameOver;
	private bool restart;

	private Vector3 spawnPosition;

	private void Start()
	{
		gameOver = false;
		restart = false;
		gameOverText.text = "";
		restartText.text = "";

		score = 0;
		UpdateScore ();

		StartCoroutine (SpawnWaves ());
	}

	private void Update()
	{
		if (restart && Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	private IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i = 0; i < enemyCount; i++) {
				GameObject enemy = enemies[Random.Range(0, enemies.Length)];
				spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Instantiate (enemy, spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restart = true;
				restartText.text = "Press 'R' to restart";
				break;
			}
		}
	}

	public void AddScore(int amount)
	{
		score += amount;
		UpdateScore ();
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverText.text = "GAME OVER!";
	}

	private void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
