using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Invector;

public class MasterController : MonoBehaviour
{

	public List<GameObject> PlayerPrefabs = new List<GameObject>();
	public GameObject[] EnemyPrefabs;
	public GameObject CurrentPlayer;
	Invector.vHealthController HealthTemp;
	public Transform SpawnPoint;
	int Score = 0;
	public Text ScoreDisplay;
	public GameObject GameOverPanel;
	public Text GameOverHighscoreText;
	public GameObject HighscoreMessage;
	public GameObject LoadingPanel;
    public NetworkController net;
    public DiscoveryController discovery;
    public Transform playerListPannel;
    public GameObject ConnectionLostScreen;
    public bool isHost = false;
    public GameObject owner;
    bool GameOverCalled = false;
    bool RankVisible = false;
    bool dead = false;

    void Awake ()
	{
        /*
		if (CurrentPlayer == null && EnemyPrefabs != null) {
			CurrentPlayer = Instantiate (PlayerPrefabs [PlayerPrefs.GetInt ("selectedplayer")], SpawnPoint.transform.position, SpawnPoint.transform.rotation) as GameObject;
		}
		HealthTemp = CurrentPlayer.GetComponent <vHealthController> ();
        */
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
        if (net.isGameStarted() && !net.GameOver)
        {
            if (PlayerPrefabs.Count == 1)
            {
                PlayerPrefabs[0].GetComponent<vHealthController>().CustomCurrentHealth(0f);
            } else if (PlayerPrefabs.Count == 0 && isHost && !GameOverCalled)
            {
                GameOverCalled = true;
                Invoke("CallGameOver", 2f);
            }
        } else if (net.GameOver && !RankVisible)
        {
            RankVisible = true;
            GameOver();
        }
        if (HealthTemp == null)
        {
            if (CurrentPlayer == null) return;
            HealthTemp = CurrentPlayer.GetComponent<vHealthController>();
        }
		if (HealthTemp.currentHealth <= 0 && !dead) {
            //CallGameOver (5);
            dead = true;
            Invoke("DestroyCurrentPlayer", 1f);
		}

	}

    void DestroyCurrentPlayer()
    {
        owner.GetComponent<NetworkPlayer>().CmdDestroyPlayer();
    }

    public int getScore()
    {
        return Score;
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

	public void CallGameOver ()
	{
        net.GameOver = true;
	}

	void GameOver ()
	{
        //Time.timeScale = 0f;

		if (Score > PlayerPrefs.GetInt ("highscore")) {
			PlayerPrefs.SetInt ("highscore", Score);
			HighscoreMessage.SetActive (true);
		}

		GameOverHighscoreText.text = PlayerPrefs.GetInt ("highscore").ToString ();
        net.UpdateRanking();
		GameOverPanel.SetActive (true);

        if (isHost)
        {
            net.GameOver = true;
            Invoke("RestartGame", 5f);
        }
	}

    void RestartGame()
    {
        net.RestartGame();
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

	}

}
