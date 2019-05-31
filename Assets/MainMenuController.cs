using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
	public GameObject[] PlayerPrefabs;
	public GameObject LoadingPanel;
	int Counter = 0;
	GameObject CurrentPrefab;

	// Use this for initialization
	void Start ()
	{
		Counter = PlayerPrefs.GetInt ("selectedplayer");
		FigureSelect ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void RightKey ()
	{
		Counter += 1;
		if (Counter >= PlayerPrefabs.Length) {
			Counter = 0;
		}
		PlayerPrefs.SetInt ("selectedplayer", Counter);
		FigureSelect ();
	}

	public void LeftKey ()
	{
		Counter -= 1;
		if (Counter < 0) {
			Counter = PlayerPrefabs.Length - 1;
		}
		PlayerPrefs.SetInt ("selectedplayer", Counter);
		FigureSelect ();
	}

	void FigureSelect ()
	{
		for (int i = 0; i < PlayerPrefabs.Length; i++) {
			if (i == Counter) {
				Destroy (CurrentPrefab);
				CurrentPrefab = Instantiate (PlayerPrefabs [i], Vector3.zero, Quaternion.identity);
			}
		}
	}

	public void Play ()
	{
		LoadingPanel.SetActive (true);
		SceneManager.LoadSceneAsync (1);
	}


}
