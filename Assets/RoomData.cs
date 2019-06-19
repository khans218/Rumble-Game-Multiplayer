using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour {
    string RoomName;
    string Address;
    int Players;
    MainMenuController main;

    public void SetupRoom(string _name, string _address, int _players, MainMenuController _main)
    {
        RoomName = _name;
        Players = _players;
        main = _main;
        Address = _address;
        UpdateText();
    }
    public void UpdatePlayers(int _players)
    {
        Players = _players;
        UpdateText();
    }
    public void JoinGame()
    {
        if (Players == 8) return;
        main.Join(Address);
    }
    void UpdateText()
    {
        Text text = GetComponentInChildren<Text>();
        text.text = RoomName + " " + Players.ToString() + "/8 (Press To Join)";
    }
    public string getName() { return RoomName; }
    public int getPlayersCount() { return Players; }
    public GameObject getButton() { return gameObject; }
}
