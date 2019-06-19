using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInfo
{
    public string PlayerName;
    public int PrefabIndex;
    public GameObject owner;
}

public class PlayersManager : MonoBehaviour {

    public Transform[] spawnPoints;
    public MasterController master;

}
