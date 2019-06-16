using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	public GameObject[] PlayerPrefabs;
	public GameObject LoadingPanel;
    public GameObject matchMenu;
    public GameObject selectionMenu;
    public MyNetManager netManager;
    public Text Name;
    public Text IP;
    public Text RoomName;
	int Counter = 0;
	GameObject CurrentPrefab;

	// Use this for initialization
	void Start ()
	{
        netManager = GameObject.Find("NetworkManager").GetComponent<MyNetManager>();
        RoomName.text = PlayerPrefs.GetString("RoomName");
        Counter = PlayerPrefs.GetInt ("selectedplayer");
        Name.text = PlayerPrefs.GetString("PlayerName");
		FigureSelect ();
	}

    public void SetRoomName()
    {
        PlayerPrefs.SetString("RoomName", RoomName.text);
    }

    public void StartHost()
    {
        netManager.StartHost();
    }

    public void Search()
    {
        netManager.SearchMatch();
    }

    public void Join()
    {
        netManager.JoinMatch(IP.text);
    }

    public void SetName()
    {
        PlayerPrefs.SetString("PlayerName", Name.text);
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
        Destroy (CurrentPrefab);
        CurrentPrefab = Instantiate (PlayerPrefabs [Counter], Vector3.zero, Quaternion.identity);
	}

    public void EnableMatchMenu()
    {
        selectionMenu.SetActive(false);
        matchMenu.SetActive(true);
    }

    public void EnablePlayerSelectionMenu()
    {
        matchMenu.SetActive(false);
        selectionMenu.SetActive(true);
    }

	public void Play ()
	{
		LoadingPanel.SetActive (true);
		SceneManager.LoadSceneAsync (1);
	}


}
