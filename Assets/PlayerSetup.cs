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
    PlayersManager playersManager;

    public class PlayerInfo
    {
        string name;
        GameObject prefab;

        public PlayerInfo(string _name, GameObject _prefab)
        {
            name = _name;
            prefab = _prefab;
        }

        public string getName() { return name; }
        public GameObject getPrefab() { return prefab; }
    }
    PlayerInfo myPlayer;
    public Text name;

    // Use this for initialization
    void Start () {
        //playersManager = GameObject.Find("PlayerList").GetComponent<PlayersManager>();
        //transform.parent = playersManager.transform;

        //myPlayer = new PlayerInfo(name.text, this.gameObject);
        //playersManager.AddPlayer(myPlayer);

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

    private void OnDestroy()
    {
        //playersManager.RemovePlayer(transform.GetSiblingIndex(), myPlayer);
    }

}
