using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public struct PlayerInfo
{
    public string PlayerName;
    public int PrefabIndex;
    public GameObject owner;
}

public class NetworkPlayer : NetworkBehaviour {

    MasterController master;
    [SyncVar]
    PlayerInfo myInfo;
    [SyncVar]
    bool isSetup;
    public GameObject CurrentPlayer;
    bool textSet = false;
    // Use this for initialization
    void Start() {
        master = GameObject.Find("MasterController").GetComponent<MasterController>();
        if (master.net.isGameStarted() && master.net.StartPannel.activeSelf)
        {
            master.net.StartPannel.SetActive(false);
        }
        transform.SetParent(master.playerListPannel);
        transform.localScale = new Vector3(1, 1, 1);
        if (!isSetup && isLocalPlayer)
        {
            master.owner = gameObject;
            if (isServer)
            {
                master.isHost = true;
                master.discovery = GameObject.Find("Broadcaster").GetComponent<DiscoveryController>();
                master.net.waitingPannel.SetActive(false);
            }
            myInfo.PlayerName = PlayerPrefs.GetString("PlayerName");
            myInfo.PrefabIndex = PlayerPrefs.GetInt("selectedplayer");
            CmdSetupPlayer(myInfo);
        }
    }

    private void Update()
    {
        if (!textSet)
        {
            if (isSetup && myInfo.PlayerName != "")
            {
                GetComponent<Text>().text = myInfo.PlayerName + "(" + myInfo.PrefabIndex.ToString() + ")";
                textSet = true;
            }
        }
    }

    public PlayerInfo getInfo()
    {
        return myInfo;
    }

    [Command]
    void CmdSetupPlayer(PlayerInfo player)
    {
        myInfo = player;
        myInfo.owner = gameObject;
        if (!master.net.isGameStarted())
        {
            master.net.SpawnPlayer(transform.GetSiblingIndex());
        }
        isSetup = true;
    }

    [Command]
    public void CmdDestroyPlayer()
    {
        NetworkServer.Destroy(CurrentPlayer);
    }

    [Command]
    public void CmdRankMe(int score)
    {
        RpcRankThisPlayer(score);
    }

    [ClientRpc]
    void RpcRankThisPlayer(int score)
    {
        master.net.AddPlayerToRank(score, this);
    }

}
