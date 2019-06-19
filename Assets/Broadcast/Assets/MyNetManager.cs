using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetManager : NetworkManager
{

    public NetworkDiscovery discovery;
    public InputField IP;

    public void JoinGameBroadcastTest()
    {
        networkPort = 7777;
        networkAddress = IP.text;
        StartClient();
    }

    public void JoinMatch(string IP)
    {
        networkPort = 7777;
        networkAddress = IP;
        StartClient();
    }

    public void StartGame()
    {
        StartHost();
        discovery.Initialize();
        discovery.StartAsServer();
    }

    private void OnFailedToConnect(NetworkConnectionError error)
    {
    }

    public void SearchMatch()
    {
        if (discovery.running)
        {
            discovery.StopBroadcast();
            discovery.running = false;
        }
        Invoke("listenBroadcast", 1f);
    }

    void listenBroadcast()
    {
        discovery.Initialize();
        discovery.StartAsClient();
    }

	public override void OnStartClient(NetworkClient client)
	{
        if (discovery.running) { discovery.StopBroadcast(); }
        //discovery.showGUI = false;
	}

	public override void OnStopClient()
	{
        if (discovery.running) { discovery.StopBroadcast(); }
		//discovery.showGUI = true;
	}

}
