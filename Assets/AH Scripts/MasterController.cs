using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Invector;

public class MasterController : MonoBehaviour
{

	public GameObject[] PlayerPrefabs;
	public GameObject[] EnemyPrefabs;
	[HideInInspector]public GameObject CurrentPlayer;
	Invector.vHealthController HealthTemp;
	public Transform SpawnPoint;
	int Score = 0;
	public Text ScoreDisplay;
	public GameObject GameOverPanel;
	public Text GameOverScoreText;
	public Text GameOverHighscoreText;
	public GameObject HighscoreMessage;
	public GameObject LoadingPanel;


	void Awake ()
	{
		if (CurrentPlayer == null && EnemyPrefabs != null) {
			CurrentPlayer = Instantiate (PlayerPrefabs [PlayerPrefs.GetInt ("selectedplayer")], SpawnPoint.transform.position, SpawnPoint.transform.rotation) as GameObject;
		}
		HealthTemp = CurrentPlayer.GetComponent <vHealthController> ();
	}

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1f;
	}

	// Update is called once per frame
	void Update ()
	{
		ScoreDisplay.text = "SCORE: " + Score.ToString ();
		if (Input.GetKeyDown (KeyCode.O)) {
			Time.timeScale = 0.2f;
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			Time.timeScale = 1f;
		}


		if (HealthTemp.currentHealth <= 0) {
			CallGameOver (5);
		}

	}

	public void CallNewEnemyCreation ()
	{
		Invoke ("CreateNewEnemy", 1f);
	}

	void CreateNewEnemy ()
	{
		Instantiate (EnemyPrefabs [Random.Range (0, (EnemyPrefabs.Length - 1))], new Vector3 (Random.Range (-3.8f, 3.8f), -0.03f, Random.Range (-3.8f, 3.8f)), Quaternion.identity);
	}

	public void ScorePlusPlus ()
	{
		Score += 1;
	}

	public void CallGameOver (float x)
	{
		Invoke ("GameOver", x);
	}

	void GameOver ()
	{

		Time.timeScale = 0f;

		if (Score > PlayerPrefs.GetInt ("highscore")) {
			PlayerPrefs.SetInt ("highscore", Score);
			HighscoreMessage.SetActive (true);
		}

		GameOverScoreText.text = Score.ToString ();
		GameOverHighscoreText.text = PlayerPrefs.GetInt ("highscore").ToString ();

		GameOverPanel.SetActive (true);

	}

	public void GameOverMenu (int x)
	{
		switch (x) {
		case 0: //Restart
			LoadingPanel.SetActive (true);
			SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
			break;
		case 1: //Exit
			LoadingPanel.SetActive (true);
			SceneManager.LoadSceneAsync (0);
			break;
		}
	}

	public void PlayerAttack ()
	{
		CurrentPlayer.transform.GetChild (0).transform.gameObject.SetActive (true);
	}

}
