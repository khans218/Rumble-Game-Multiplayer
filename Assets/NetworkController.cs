using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkController : NetworkBehaviour {

    public GameObject waitingPannel;
    public GameObject StartPannel;
    public MasterController master;
    NetworkManager manager;
    [SyncVar]
    bool GameStarted = false;
    int playerCount = 0;

    private void Start()
    {
        manager = NetworkManager.singleton;
    }

    private void Update()
    {
        if (!isServer) return;
        if (master.playerListPannel.childCount != playerCount)
        {
            playerCount = master.playerListPannel.childCount;
            ResetBroadcast();
        }
    }

    public void SpawnPlayer(int index)
    {
        PlayerInfo player = master.playerListPannel.transform.GetChild(index).gameObject.GetComponent<NetworkPlayer>().getInfo();
        GameObject obj = Instantiate(manager.spawnPrefabs[player.PrefabIndex]);
        obj.GetComponent<PlayerSetup>().ownerObj = player.owner;
        Vector3 pos = master.playersManager.spawnPoints[index].position;
        obj.transform.position = pos;
        obj.transform.forward = -pos;
        NetworkServer.SpawnWithClientAuthority(obj, player.owner);
    }

    [ClientRpc]
    public void RpcStartGame()
    {
        StartPannel.SetActive(false);
        if (master.isHost)
        {
            //if (master.discovery.running) { master.discovery.StopBroadcast(); }
            GameStarted = true;
            /*
            for (int i = 0; i < master.playerListPannel.transform.childCount; i++)
            {
                PlayerInfo player = master.playerListPannel.transform.GetChild(i).gameObject.GetComponent<NetworkPlayer>().getInfo();
                GameObject obj = Instantiate(manager.spawnPrefabs[player.PrefabIndex]);
                obj.GetComponent<PlayerSetup>().ownerObj = player.owner;
                Vector3 pos = master.playersManager.spawnPoints[i].position;
                obj.transform.position = pos;
                obj.transform.forward = -pos;
                NetworkServer.SpawnWithClientAuthority(obj, player.owner);
            }
            */
        }
    }

    public bool isGameStarted() { return GameStarted; }


    void ResetBroadcast()
    {
        if (master.discovery.running) { master.discovery.StopBroadcast(); }
        master.discovery.broadcastData =  PlayerPrefs.GetString("RoomName") + ":" + playerCount.ToString();
        Invoke("Broadcast", 0.5f);
    }

    void Broadcast()
    {
        if (master.discovery.running || !master.isHost) return;
        master.discovery.Initialize();
        master.discovery.StartAsServer();
    }

    public void LeaveGame()
    {
        master.LoadingPanel.SetActive(true);
        if (master.isHost)
        {
            RpcEndGame();
        } else
        {
            manager.StopClient();
        }
    }

    [ClientRpc]
    public void RpcEndGame()
    {
        //put some extra code to let players know host has ended game
        if (master.isHost && master.discovery.running)
        {
            master.discovery.StopBroadcast();
        }
        manager.StopHost();
    }

}
