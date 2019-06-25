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
    public InputField Name;
    public InputField RoomName;
    public GameObject MatchButton;
    public GameObject RoomList;
    SortedDictionary<string, RoomData> Rooms = new SortedDictionary<string, RoomData>();
	int Counter = 0;
	GameObject CurrentPrefab;
    public GameObject TimeoutScreen;
    public GameObject ConnectionLostScreen;
    public GameObject Canvas;

	// Use this for initialization
	void Start ()
	{
        if (!GameObject.FindObjectOfType<ErrorScreenDestroyer>())
        {
            Canvas.SetActive(true);
        }
        netManager = GameObject.Find("NetworkManager").GetComponent<MyNetManager>();
        RoomName.text = PlayerPrefs.GetString("RoomName");
        Counter = PlayerPrefs.GetInt ("selectedplayer");
        Name.text = PlayerPrefs.GetString("PlayerName");
		FigureSelect ();
	}

    public void AddRoom(string address, string data)
    {
        int index = data.IndexOf(":");
        string roomName = data.Substring(0, index);
        int players = int.Parse(data.Substring(index + 1, data.Length - index - 1));
        if (Rooms.ContainsKey(address))
        {
            RoomData room = Rooms[address];
            if (room.getPlayersCount() != players)
            {
                room.UpdatePlayers(players);
            }
        }
        else
        {
            GameObject button = Instantiate(MatchButton);
            button.transform.SetParent(RoomList.transform);
            button.transform.localScale = new Vector3(1, 1, 1);
            RoomData room = button.GetComponent<RoomData>();
            room.SetupRoom(roomName, address, players, this);
            Rooms.Add(address, room);
        }
    }

    public void Refresh()
    {
        ClearRooms();
        netManager.SearchMatch();
    }

    void ClearRooms()
    {
        Rooms.Clear();
        for (int i = 0; i < RoomList.transform.childCount; i++)
        {
            Destroy(RoomList.transform.GetChild(i).gameObject);
        }
    }

    void SetRoomName()
    {
        PlayerPrefs.SetString("RoomName", RoomName.text);
    }

    public void StartHost()
    {
        SetRoomName();
        if (PlayerPrefs.GetString("RoomName") == null || PlayerPrefs.GetString("RoomName") == "") return;
        LoadingPanel.SetActive(true);
        netManager.StartHost();
    }

    public void Join(string address)
    {
        LoadingPanel.SetActive(true);
        netManager.JoinMatch(address);
    }

    void SetName()
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
        SetName();
        if (PlayerPrefs.GetString("PlayerName") == null || PlayerPrefs.GetString("PlayerName") == "") return;
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
