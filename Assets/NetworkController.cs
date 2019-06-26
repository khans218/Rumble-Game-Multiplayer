using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkController : NetworkBehaviour {

    SortedDictionary<int, List<NetworkPlayer> > Ranking = new SortedDictionary<int, List<NetworkPlayer>>();

    public GameObject waitingPannel;
    public GameObject StartPannel;
    public MasterController master;
    NetworkManager manager;
    [SyncVar]
    bool GameStarted = false;
    [SyncVar]
    public bool GameOver = false;
    int playerCount = 0;
    public Transform[] spawnPoints;
    public Transform RankingPannel;
    public GameObject RankInfo;
    public GameObject HostEndGamePannel;

    private void Start()
    {
        manager = NetworkManager.singleton;
    }

    private void Update()
    {
        if (!isServer || HostEndGamePannel.activeSelf) return;
        if (master.playerListPannel.childCount != playerCount)
        {
            playerCount = master.playerListPannel.childCount;
            ResetBroadcast();
        }
    }

    public void RestartGame()
    {
        manager.ServerChangeScene("TestSceneNew");
    }

    public void SpawnPlayer(int index)
    {
        PlayerInfo player = master.playerListPannel.transform.GetChild(index).gameObject.GetComponent<NetworkPlayer>().getInfo();
        GameObject obj = Instantiate(manager.spawnPrefabs[player.PrefabIndex]);
        obj.GetComponent<PlayerSetup>().ownerObj = player.owner;
        Vector3 pos = spawnPoints[index].position;
        obj.transform.position = pos;
        obj.transform.forward = -pos;
        NetworkServer.SpawnWithClientAuthority(obj, player.owner);
    }

    public void AddPlayerToRank(int score, NetworkPlayer player)
    {
        if (Ranking.ContainsKey(score))
        {
            List<NetworkPlayer> players = Ranking[score];
            players.Add(player);
        } else
        {
            List<NetworkPlayer> players = new List<NetworkPlayer>();
            players.Add(player);
            Ranking.Add(score, players);
        }
    }

    public void UpdateRanking()
    {
        foreach(KeyValuePair<int, List<NetworkPlayer>> pair in Ranking)
        {
            foreach(NetworkPlayer player in pair.Value)
            {
                if (player != null)
                {
                    GameObject rank = Instantiate(RankInfo);
                    rank.GetComponent<PlayerScoreSetter>().SetScore(player.getInfo().PlayerName, pair.Key, player.isLocalPlayer);
                    rank.transform.SetParent(RankingPannel);
                    rank.transform.localScale = new Vector3(1f, 1f, 1f);
                    rank.transform.SetAsFirstSibling();
                }
            }
        }
    }

    public void StartGame()
    {
        if (playerCount > 1)
        {
            RpcStartGame();
        }
    }

    [ClientRpc]
    void RpcStartGame()
    {
        StartPannel.SetActive(false);
        if (master.isHost)
        {
            GameStarted = true;
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
        if (master.isHost)
        {
            if (HostEndGamePannel.activeSelf)
            {
                master.LoadingPanel.SetActive(true);
                manager.StopHost();
            } else
            {
                RpcEndGame();
            }
        } else
        {
            master.LoadingPanel.SetActive(true);
            manager.StopClient();
        }
    }

    [ClientRpc]
    public void RpcEndGame()
    {
        if (master.isHost && master.discovery.running)
        {
            master.discovery.StopBroadcast();
        }
        if (StartPannel.activeSelf)
        {
            StartPannel.SetActive(false);
        }
        HostEndGamePannel.SetActive(true);
    }

}
