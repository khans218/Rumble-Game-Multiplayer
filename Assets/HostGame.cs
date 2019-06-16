using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : NetworkDiscovery {

    NetworkManager network;


	// Use this for initialization
	void Start () {
        network = NetworkManager.singleton;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
