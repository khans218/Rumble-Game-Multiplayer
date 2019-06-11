using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersManager : MonoBehaviour {
    List<PlayerSetup.PlayerInfo> players = new List<PlayerSetup.PlayerInfo>();
    public Text playerInfoUI;
    public GameObject playerList;
    public Transform[] spawnPoints;

    public void AddPlayer(PlayerSetup.PlayerInfo player)
    {
        players.Add(player);
        Text playerName = Instantiate(playerInfoUI);
        playerName.transform.SetParent(playerList.transform);
        playerName.gameObject.transform.localScale = new Vector3(1, 1, 1);
        UpdatePlayerList();
    }

    public void RemovePlayer(int id, PlayerSetup.PlayerInfo _player)
    {
        players.Remove(_player);
        Destroy(playerList.transform.GetChild(id).gameObject);
        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        Debug.Log(players.Count);
        for (int i = 0; i < players.Count; i++)
        {
            Text text = playerList.transform.GetChild(i).GetComponent<Text>();
            text.text = i.ToString() + ". " + players[i].getName();
            players[i].getPrefab().transform.position = spawnPoints[i].position;
            players[i].getPrefab().transform.forward = -spawnPoints[i].position;
        }
    }

}
