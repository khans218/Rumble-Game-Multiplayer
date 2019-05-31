using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCommandController : NetworkBehaviour {

    [Command]
    public void CmdHello(GameObject player)
    {
        player.transform.GetChild(0).transform.gameObject.SetActive(true);
    }

}
