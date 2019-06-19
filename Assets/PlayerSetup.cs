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

    // Use this for initialization
    void Start () {
        netAnim = GetComponent<NetworkAnimator>();
        owner = ownerObj.GetComponent<NetworkPlayer>();
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
            master = GameObject.Find("MasterController").GetComponent<MasterController>();
            master.CurrentPlayer = gameObject;
        }
    }

    public void AttackTrigger()
    {
        if (!owner.isLocalPlayer) return;
        netAnim.SetTrigger("StrongAttack");
    }

}
