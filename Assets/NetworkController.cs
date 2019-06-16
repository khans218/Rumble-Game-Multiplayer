using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkController : NetworkBehaviour {

    public GameObject hostPannel;
    public GameObject waitingPannel;
    public GameObject StartPannel;

    [ClientRpc]
    public void RpcStartGame()
    {
        StartPannel.SetActive(false);
    }
}
