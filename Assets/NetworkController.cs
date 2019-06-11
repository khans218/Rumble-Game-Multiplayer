using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkController : NetworkBehaviour {

    public GameObject hostPannel;
    public GameObject waitingPannel;
    public GameObject playerListPannel;

    [ClientRpc]
    public void RpcStartGame()
    {
        hostPannel.SetActive(false);
        waitingPannel.SetActive(false);
        playerListPannel.SetActive(false);
    }
}
