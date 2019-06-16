using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetManager : NetworkManager
{

    public NetworkDiscovery discovery;
    NetworkManager manager;

    private void Start()
    {
        manager = NetworkManager.singleton;
    }

    public void JoinMatch(string IP)
    {
        manager.networkPort = 7777;
        manager.networkAddress = IP;
        manager.StartClient();
    }

    public void SearchMatch()
    {
        discovery.Initialize();
        discovery.StartAsClient();
    }

    public override void OnStartHost()
	{
		discovery.Initialize();
        discovery.StartAsServer();
    }

	public override void OnStartClient(NetworkClient client)
	{
        //discovery.showGUI = false;
	}

	public override void OnStopClient()
	{
		discovery.StopBroadcast();
		//discovery.showGUI = true;
	}

}
