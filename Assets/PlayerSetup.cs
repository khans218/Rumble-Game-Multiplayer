using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] ComponentsToDisable;
    NetworkAnimator netAnim;
    MasterController master;
    [SyncVar]
    public GameObject ownerObj;
    public NetworkPlayer owner;
    int index;

    // Use this for initialization
    void Start () {
        master = GameObject.Find("MasterController").GetComponent<MasterController>();
        master.PlayerPrefabs.Add(gameObject);
        netAnim = GetComponent<NetworkAnimator>();
        index = ownerObj.transform.GetSiblingIndex();
        owner = ownerObj.GetComponent<NetworkPlayer>();
        owner.CurrentPlayer = gameObject;
        GetComponentInChildren<Text>().text = owner.getInfo().PlayerName;
        if (!owner.isLocalPlayer)
        {
            this.tag = "Enemy";
            this.gameObject.layer = 9;
            foreach (Behaviour component in ComponentsToDisable)
            {
                component.enabled = false;
            }
        } else
        {
            master.CurrentPlayer = gameObject;
        }
    }

    private void Update()
    {
        if (master.net.isGameStarted()) return;
        if (index != ownerObj.transform.GetSiblingIndex())
        {
            index = ownerObj.transform.GetSiblingIndex();
            Vector3 pos = master.net.spawnPoints[index].position;
            transform.position = pos;
            transform.forward = -pos;
        }
        if (GetComponentInChildren<Text>().text != owner.getInfo().PlayerName)
        {
            GetComponentInChildren<Text>().text = owner.getInfo().PlayerName;
        }
    }

    public void DisableControl() { 
}

    private void OnDestroy()
    {
        if (owner != null && owner.isLocalPlayer)
        {
            owner.CmdRankMe(master.getScore());
        }
        master.PlayerPrefabs.Remove(gameObject);
    }

    public void AttackTrigger()
    {
        if (!owner.isLocalPlayer) return;
        netAnim.SetTrigger("StrongAttack");
    }

}
