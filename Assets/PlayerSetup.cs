using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] ComponentsToDisable;
    NetworkAnimator netAnim;

    MasterController master;
	// Use this for initialization
	void Start () {
        netAnim = GetComponent<NetworkAnimator>();
        if (!isLocalPlayer)
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
            master.CurrentPlayer = this.gameObject;
        }
	}

    public void AttackTrigger()
    {
        if (!isLocalPlayer) return;
        netAnim.SetTrigger("StrongAttack");
    }
}
